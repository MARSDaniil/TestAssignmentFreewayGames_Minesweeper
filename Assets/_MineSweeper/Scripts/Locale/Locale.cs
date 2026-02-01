using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Constants;
public class Locale : MonoBehaviour {
        #region Fields
        private Dictionary<string, string> CurrentLocaleDictionary;

        public ELang CurrentLanguage {
            get; private set;
        }

        public event UnityAction<bool> OnInitDone;
        public event UnityAction OnLanguageChangedEvent;

        public static Locale I;
        #endregion


        public static Locale Get() {
            return I;
        }
        public static string GetText(string SID) {
            if (I != null) {
                return I.Get(SID);
            }

            return SID;
        }

        public void Init() {
            StartCoroutine(InitializeCoroutine());
        }

        private IEnumerator InitializeCoroutine() {
            CurrentLanguage = SystemLanguageToStr(Application.systemLanguage);

            LocaleData LocaleD = new LocaleData();
            yield return StartCoroutine(LoadLanguageData(CurrentLanguage.ToString(), LocaleD));

            if (string.IsNullOrEmpty(LocaleD.LanguageCode) && CurrentLanguage != Constants.ELang.EN) {
                Debug.LogError("Can't load locale " + CurrentLanguage + ". Try to load English");
                yield return StartCoroutine(LoadLanguageData(Constants.ELang.EN.ToString(), LocaleD));
                if (!string.IsNullOrEmpty(LocaleD.LanguageCode)) {
                    CurrentLanguage = Constants.ELang.EN;
                }
            }

            bool IsSuccess = false;

            if (!string.IsNullOrEmpty(LocaleD.LanguageCode)) {
                IsSuccess = true;
                InitLocaleDictionary(LocaleD);
                CurrentLanguage = StrToLang(LocaleD.LanguageCode);
            }

            I = this;
            SetTimePostfixes();
            OnInitDone?.Invoke(IsSuccess);

        }

        public void SetTimePostfixes() {
            string Times = Get("SID_TIME_FORMAT");
            string[] TimeIds = Times.Split(',');
            TimerFormat.SetTimePostfixes(TimeIds);
        }

        public string Get(string SID) {
            if (!string.IsNullOrEmpty(SID)) {
                SID = SID.Trim();
                if (CurrentLocaleDictionary != null && CurrentLocaleDictionary.ContainsKey(SID)) {
                    return (CurrentLocaleDictionary[SID]);
                }
            }

            return (SID);
        }

        #region Helpers
        private Constants.ELang SystemLanguageToStr(SystemLanguage Lang) {
            switch (Lang) {
                case SystemLanguage.English:
                    return Constants.ELang.EN;
                case SystemLanguage.Russian:
                    return Constants.ELang.RU;
                default:
                    return Constants.ELang.EN;
            }
        }

        private IEnumerator LoadLanguageData(string Lang, LocaleData LocaleDReturn) {
            yield return null;
            LocaleData LocaleD = SaveLoadData.LoadObjectFromJSONRes<LocaleData>("Locale_" + Lang);

            if (LocaleD != null) {
                LocaleDReturn.Data = LocaleD.Data;
                LocaleDReturn.LanguageCode = LocaleD.LanguageCode;
            } else {
                Debug.LogError($"[ERROR] Can't load Language Data with Lang = " + Lang);
            }
        }

        private void InitLocaleDictionary(LocaleData LData) {
            if (LData != null && LData.Data != null && LData.Data.Count > 0) {
                CurrentLocaleDictionary = new Dictionary<string, string>();

                foreach (LocaleEntry LEntry in LData.Data) {
                    if (LEntry != null && !string.IsNullOrEmpty(LEntry.SID)) {
                        if (!CurrentLocaleDictionary.ContainsKey(LEntry.SID)) {
                            CurrentLocaleDictionary.Add(LEntry.SID, LEntry.LString);
                        }
                    }
                }
            }
        }
        private Constants.ELang StrToLang(string LangStr) {
            return (Constants.ELang) System.Enum.Parse(typeof(Constants.ELang), LangStr);
        }

        private void UpdateTexts(LocaleText[] TextArray) {
            if (TextArray == null) {
                return;
            }

            foreach (LocaleText LText in TextArray) {
                LText.UpdateText(this);
            }
        }
        #endregion

    }
