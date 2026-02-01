using System;
using System.Collections.Generic;
[Serializable]
    public class LocaleData {
        public string LanguageCode;
        public List<LocaleEntry> Data;

        public LocaleData() {
            this.Data = new List<LocaleEntry>();
        }

        public LocaleData(string languageCode)
            : this() {
            this.LanguageCode = languageCode;
        }

        public string GetFileName(bool json) {
            return "Locale_" + this.LanguageCode + (json ? ".json" : ".bytes");
        }
    }

    [Serializable]
    public class LocaleEntry {
        public string SID;
        public string LString;

        public LocaleEntry() {
        }

        public LocaleEntry(string sid, string lString) {
            this.SID = sid;
            this.LString = lString;
        }
    }
