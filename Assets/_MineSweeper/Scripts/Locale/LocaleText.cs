using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Constants;

    public class LocaleText : MonoBehaviour {

        #region Fields
        protected string Sid;
        public Graphic Label;
        protected bool IsInitialised;
        [SerializeField] protected bool IsCheckForNewline;
        #endregion

        #region Unity Event Functions
        private void Start() {
            if (IsInitialised) {
                return;
            }

            if (Locale.Get() != null) {
                Label = GetComponent<Text>();
                if (Label) {
                    Sid = (Label as Text).text;
                    if (IsCheckForNewline)
                        (Label as Text).text = Locale.GetText(Sid).Replace(@"\N", "\n");
                    else
                        (Label as Text).text = Locale.GetText(Sid);
                } else {
                    Label = GetComponent<TMP_Text>();
                    if (Label) {
                        Sid = (Label as TMP_Text).text;
                        if (IsCheckForNewline)
                            (Label as TMP_Text).text = Locale.GetText(Sid).Replace(@"\N", "\n");
                        else
                            (Label as TMP_Text).text = Locale.GetText(Sid);
                    }
                }
            } else {
                //Debug.LogError($"[ERROR] {gameObject} Locale Text -> Locale Manager is not initialized!");
            }
        }
        #endregion

        #region Public
        public virtual void UpdateText(Locale Loc) {
            if (Label != null) {
                if (IsCheckForNewline) {
                    SetTextInternal(Loc.Get(Sid).Replace(@"\N", "\n"));
                } else {
                    SetTextInternal(Loc.Get(Sid));
                }
            }
        }

        private void SetTextInternal(string S) {
            if (Label is Text) {
                (Label as Text).text = S;
            } else if (Label is TMP_Text) {
                (Label as TMP_Text).text = S;
            }
        }

        public virtual void SetText(string NewText, string Sid) {
            IsInitialised = true;
            this.Sid = Sid;

            if (Label != null) {
                if (IsCheckForNewline)
                    NewText = NewText.Replace(@"\N", "\n");
                SetTextInternal(NewText);
            }
        }
        #endregion

    }
