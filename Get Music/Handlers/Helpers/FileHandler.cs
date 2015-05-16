using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Get_Music
{
  /// <summary>
    /// Read or write into the file
    /// </summary>
    class FileHandler
    {
        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileFullName">Full name of the file.</param>
        /// <example>C:\my.file</example>
        /// <returns></returns>
        public static List<string> ReadFileAsList(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                throw new Exception("There is no file in path: " + fileFullName);
            }

            var list = new List<string>();
            var file = new StreamReader(fileFullName);
            while (!file.EndOfStream)
            {
                list.Add(file.ReadLine());
            }

            file.Close();

            return list;
        }

        public static string ReadFileAsString(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                throw new Exception("There is no file in path: " + fileFullName);
            }

            var str = "";
            var file = new StreamReader(fileFullName);

            str = file.ReadToEnd();

            file.Close();

            return str;
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="fileFullName">Full name of the file.</param>
        /// <example>C:\my.file</example>
        /// <param name="lines">The lines.</param>
        /// <param name="addToFile">if set to <c>true</c> [add to file].</param>
        /// <param name="ifExistSkip">if set to <c>true</c> [this file skip].</param>
        public static void WriteToFile(string fileFullName, List<string> lines, bool addToFile = false, bool ifExistSkip = false)
        {
            if (!File.Exists(fileFullName))
            {
                throw new Exception("There is no file in path: " + fileFullName);
            }

            if (ifExistSkip)
            {
                return;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileFullName, addToFile))
            {
                foreach (var line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }

        public static void WriteToFile(string fileFullName, string str, bool addToFile = false, bool ifExistSkip = false)
        {
            if (!File.Exists(fileFullName))
            {
                throw new Exception("There is no file in path: " + fileFullName);
            }

            if (ifExistSkip)
            {
                return;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileFullName, addToFile))
            {
                file.WriteLine(str);
            }
        }

        public static void CreateFile(string fileFullName)
        {
            try
            {
                using (FileStream fs = File.Create(fileFullName))
                {
                }
            }
            catch (Exception)
            {

                throw new Exception("Couldn't create file in path: " + fileFullName);
            }
        }
    }
}
