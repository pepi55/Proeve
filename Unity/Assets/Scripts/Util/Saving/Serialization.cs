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
        /// <summary>
        /// File types that are defined.
        /// </summary>
        public enum fileTypes
        {
            binary,
            text
        }

        /// <summary>
        /// Location of the save data
        /// </summary>
        public static string saveFolderName = "GameData";
        /// <summary>
        /// A dictonary contain information related to a filetype
        /// </summary>
        readonly public static Dictionary<fileTypes, string> fileExstentions = new Dictionary<fileTypes, string>
        {
            { fileTypes.binary,     ".bin"      },
            { fileTypes.text,       ".txt"     }
        },

        FileLocations = new Dictionary<fileTypes, string>
        {
            { fileTypes.binary,     "Data"      },
            { fileTypes.text,       "Data"      }
        };
        #endregion

        /// <summary>
        /// Generates a string for where the file is located
        /// </summary>
        /// <param name="fileType">The type of file can matter for directory</param>
        /// <returns></returns>
        private static string SaveLocation(fileTypes fileType)
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

        /// <summary>
        /// Returns file type with name attached
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="fileType">The type of file</param>
        /// <returns>Name + Type </returns>
        private static string GetFileType(string fileName, fileTypes fileType)
        {
            return fileName + fileExstentions[fileType];
        }
        
        /// <summary>
        /// Save file to disk
        /// </summary>
        /// <typeparam name="T">Type of the file</typeparam>
        /// <param name="fileName">File name with out exstentions</param>
        /// <param name="fileType">The type of file</param>
        /// <param name="data">The actual data fo the file</param>
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

        /// <summary>
        /// Loads a file from disk
        /// </summary>
        /// <typeparam name="T">Type of the file</typeparam>
        /// <param name="fileName"> Name of the file</param>
        /// <param name="fileType">The file exstention Type</param>
        /// <param name="outputData">A ref for the file that will be loaded</param>
        /// <returns>if the loading was succesfull. Needed because a save file can be non existant</returns>
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

        /// <summary>
        /// Used to generate an error when there is one while saving or loading
        /// </summary>
        /// <param name="message">The message that will be shown</param>
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