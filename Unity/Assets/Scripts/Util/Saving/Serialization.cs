using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Util
{
    public static class Serialization
    {
        [DllImport("__Internal")]
        private static extern void SyncFiles();

        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);

        #region fileSaveSettings
        public enum fileTypes
        {
            binary,
            text
        }

        public static string saveFolderName = "GameData";
        readonly public static Dictionary<fileTypes, string> fileExstentions = new Dictionary<fileTypes, string>
        {
            { fileTypes.binary,     ".bin"      },
            { fileTypes.text,       ".text"     }
        },

        FileLocations = new Dictionary<fileTypes, string>
        {
            { fileTypes.binary,     "Data"      },
            { fileTypes.text,       "Data"      }
        };
        #endregion

        public static string SaveLocation(fileTypes fileType)
        {
            
            string saveLocation = Application.dataPath;
            if (!Application.isEditor)
                saveLocation += "/..";
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                saveLocation = Application.persistentDataPath;

            saveLocation += "/" + saveFolderName + "/" + FileLocations[fileType] + "/";
            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }
            return saveLocation;
        }

        public static string GetFileType(string fileName, fileTypes fileType)
        {
            return fileName + fileExstentions[fileType];
        }

        public static void Save<T>(string fileName, fileTypes fileType, T data)
        {
            string saveFile = SaveLocation(fileType);
            saveFile += GetFileType(fileName, fileType);

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(stream, data);
                stream.Close();
            }
            catch (Exception e)
            {

                PlatformSafeMessage("Failed to Save: " + e.Message);
            }


            if (Application.platform == RuntimePlatform.WebGLPlayer)
                SyncFiles();

            Debug.Log("Saved file: " + saveFile);

        }

        public static bool Load<T>(string fileName, fileTypes fileType, ref T outputData)
        {
            string saveFile = SaveLocation(fileType);
            saveFile += GetFileType(fileName, fileType);
            bool returnval = false;


            if (!File.Exists(saveFile))
            {
                outputData = default(T);
                returnval = false;
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(saveFile, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                outputData = data;
                returnval = true;
                stream.Close();
            }
            return returnval;
        }

        private static void PlatformSafeMessage(string message)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                WindowAlert(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

    }
}