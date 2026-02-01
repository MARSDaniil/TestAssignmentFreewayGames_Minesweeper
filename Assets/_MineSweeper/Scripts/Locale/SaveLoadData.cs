using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;


    public class SaveLoadData {

        #region Public

        public static string SaveConfigPath = Application.persistentDataPath + "/v" + Application.version + "/";

        public static void SaveObjectToResourceFolder<T>(T Object, string FileName) {
            byte[] AsBytes = ToByteArray(Object);
            SaveBytesToFile(FileName, "Assets/BB/Resources/", AsBytes);
        }

        public static void SaveBytesToFile(string Name, string Path, byte[] Data, string PostFix = ".bytes") {
            if (!Directory.Exists(Path)) {
                Directory.CreateDirectory(Path);
            }
            string PathWithFile = Path + Name + PostFix;
            File.WriteAllBytes(PathWithFile, Data);
        }

        public static byte[] LoadBytesFromFile(string Name, string Path, string PostFix = ".bytes") {
            string path = Path + Name + PostFix;
            byte[] bytes = null;
            if (File.Exists(path)) {
                bytes = File.ReadAllBytes(path);
            }
            return bytes;
        }

        public static T LoadObjectFromResourcesData<T>(string FileName) where T : class {
            return (FromByteArray<T>(SaveLoadData.LoadBytesFromResourcesData(FileName)));
        }

        public static byte[] LoadBytesFromResourcesData(string fileName) {
            TextAsset resAsset = (TextAsset) Resources.Load(fileName, typeof(TextAsset));
            if (resAsset != null) {
                return resAsset.bytes;
            }
            return null;
        }

        public static bool SaveAsJSON<T>(T SaveData, string Path, string FileName) {
            string DataAsJSONString = JsonUtility.ToJson(SaveData);
            File.WriteAllText(Path + FileName + ".json", DataAsJSONString);
            return (false);
        }

        public static string LoadTextFileFromRes(string FileName) {
            TextAsset TA = Resources.Load<TextAsset>(FileName);
            if (TA) {
                return (TA.text);
            }
            return (null);
        }

        public static T LoadObjectFromJSONRes<T>(string FileName) {
            string JSONContent = LoadTextFileFromRes(FileName);
            if (!string.IsNullOrEmpty(JSONContent)) {
                return (JsonConvert.DeserializeObject<T>(JSONContent));
            }
            return (default);
        }

        public static string LoadTextFile(string FilePath) {
            string ReturnValue = string.Empty;
            using (StreamReader SR = new StreamReader(FilePath)) {
                while (SR.EndOfStream) {
                    ReturnValue += SR.ReadLine();
                }
                SR.Close();
            }
            return (ReturnValue);
        }

        public static T[] LoadObjects<T>(string path, string pathPattern) where T : class {
            string[] filesPathes = Directory.GetFiles(path, pathPattern);

            if (filesPathes.Length == 0)
                return null;

            T[] objArray = new T[filesPathes.Length];

            int i = 0;
            foreach (string filePath in filesPathes) {
                byte[] bytes = File.ReadAllBytes(filePath);
                T obj = FromByteArray<T>(bytes);
                objArray[i] = obj;
                i++;
            }

            return objArray;
        }

        public static T LoadObjectFromFile<T>(string path) where T : class {
            byte[] bytes = File.ReadAllBytes(path);
            return (FromByteArray<T>(bytes));
        }

        public static T FromByteArray<T>(byte[] byteArray) where T : class {
            if (byteArray != null) {
                using (MemoryStream stream = new MemoryStream(byteArray)) {
                    BinaryFormatter bf = new BinaryFormatter();
                    T data = bf.Deserialize(stream) as T;
                    return data;
                }
            } else {
                return default(T);
            }
        }

        public static byte[] ToByteArray(object data) {
            using (MemoryStream stream = new MemoryStream()) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, data);
                return stream.ToArray();
            }
        }
        #endregion

    }



