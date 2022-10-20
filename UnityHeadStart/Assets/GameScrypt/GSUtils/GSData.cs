using System;
using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace GameScrypt.GSUtils
{
    public static class GSData
    {
#pragma warning disable 0414
        public static readonly string _version = "3.0.0";
#pragma warning restore 0414
        public static string GetDataValue(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index, StringComparison.Ordinal) + index.Length);
            if (value.Contains("|"))
                value = value.Remove(value.IndexOf('|'));
            return value;
        }
        public static int GetIntDataValue(string data, string index)
        {
            int numb;
            var success = int.TryParse(GetDataValue(data, index), out numb);

            return success ? numb : 0;
        }
        public static bool GetBoolDataValue(string data, string index)
        {
            var value = GetDataValue(data, index);
            if (string.IsNullOrEmpty(value) || value.Equals("0"))
                return false;
            return true;
        }
        public static long GetLongDataValue(string data, string index)
        {
            long numb;
            var success = long.TryParse(GetDataValue(data, index), out numb);

            return success ? numb : 0;
        }
        public static long GetLongDataValue(string data)
        {
            long numb;
            var success = long.TryParse(data, out numb);

            return success ? numb : 0;
        }

        public static string GetProfilePictureName(string username, int id)
        {
            return string.Format("{0}_{1}", username.Replace(" ", "_"), id);
        }

        public static string GetProfilePictureName(string username, long id)
        {
            return string.Format("{0}_{1}", username.Replace(" ", "_"), id);
        }

        public static string SavePic(Texture2D pic, int width, int height, string picName)
        {
            string path = Application.persistentDataPath + string.Format("/{0}.png", picName);
            try
            {
                byte[] bytes = pic.EncodeToPNG();

                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }

            return path;
        }

        public static Texture2D ReadPic(string picName)
        {
            string path = Application.persistentDataPath + string.Format("/{0}.png", picName);

            try
            {
                var bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(128, 128);
                tex.LoadImage(bytes);
                return tex;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
            return null;
        }

        // Take a shot immediately
        static IEnumerator Start()
        {
            yield return UploadPNG();
        }

        static IEnumerator UploadPNG(Texture2D pic = null, int width = 0, int height = 0)
        {
            // We should only read the screen buffer after rendering is complete
            yield return new WaitForEndOfFrame();

            if (pic == null)
            {
                // Create a texture the size of the screen, RGB24 format
                if (width < 1)
                    width = Screen.width;
                if (height < 1)
                    height = Screen.height;
                pic = new Texture2D(width, height, TextureFormat.RGB24, false);
            }

            // Read screen contents into the texture
            pic.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            pic.Apply();

            // Encode texture into PNG
            byte[] bytes = pic.EncodeToPNG();
            UnityEngine.Object.Destroy(pic);

            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        }

        public static string EncodeTextInBytes(string text)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(text);
            var textBytes = string.Empty;
            for (var i = 0; i < bytes.Length; i++)
            {
                textBytes += bytes[i].ToString() + ',';
            }
            return textBytes;
        }

        public static string AddPathsToText(string[] paths)
        {
            var fullText = string.Empty;
            int count = 0;
            foreach (string path in paths)
            {
                fullText += path;
                count++;
                if (count != paths.Length)
                {
                    fullText += ";";
                }
            }
            return fullText;
        }

        public static string[] GetPathsFromText(string fullText)
        {
            var paths = new string[2];
            var splitString = fullText.Split(';');
            paths[0] = splitString[0];
            paths[1] = splitString[1];
            return paths;
        }

        public static string DecodeTextFromBytes(string text)
        {
            int count = text.Split(',').Length - 1;
            byte[] bytes = new byte[count];

            for (var i = 0; i < count; i++)
            {
                var index = text.IndexOf(',');
                var numberString = text.Substring(0, index);

                var number = Convert.ToInt32(numberString);
                byte bit = Convert.ToByte(number);

                bytes[i] = bit;

                int toRemove = (index + 1);
                text = text.Substring(toRemove, text.Length - toRemove);
            }

            return System.Text.Encoding.Unicode.GetString(bytes);
        }

        public static int CountLinesInFile(string filePath)
        {
            int count = 0;
            using (System.IO.StreamReader r = new System.IO.StreamReader(filePath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                    count++;
            }
            return count;
        }

        public static byte[] StringToBytes(string str)
        {
            byte[] numArray = new byte[str.Length * 2];
            System.Buffer.BlockCopy((System.Array)str.ToCharArray(), 0, (System.Array)numArray, 0, numArray.Length);
            return numArray;
        }

        public static string BytesToString(byte[] bytes)
        {
            char[] chArray = new char[bytes.Length / 2];
            System.Buffer.BlockCopy((System.Array)bytes, 0, (System.Array)chArray, 0, bytes.Length);
            return new string(chArray);
        }

        public static int GetWeekNumberOfTheYear(DateTime now, int year)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static Color GetColor(string hex)
        {
            Color newCol;
            if (ColorUtility.TryParseHtmlString(hex, out newCol))
            {
                return newCol;
            }
            return new Color();
        }
    }
}