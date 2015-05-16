using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Get_Music.Models.RadioMusic;
using System.Xml.Serialization;
using HtmlAgilityPack;
using Get_Music.Models.RadioAnonFm;
using Get_Music.Handlers.Parsers.RadioAnonFm;
using Get_Music.Handlers.DataProviders;

namespace Get_Music
{
    class Program
    {
        public static void downloadFile(string remoteFilename, string localFilename)
        {
            WebClient client = new WebClient();
            client.DownloadFile(remoteFilename, localFilename);
        }

        public static void logPr(string str)
        {
            Console.Clear();
            Console.WriteLine(str);
        }

        private static void downloadAnonFmLogs(string urlLogs, string folder)
        {
            Directory.CreateDirectory(folder);

            var nameFileWithLogs = folder + "/logs.html";
            // Скачиваем файл с логами anon.fm
            downloadFile(urlLogs, nameFileWithLogs);

            var fileWithLogs = FileHandler.ReadFileAsString(nameFileWithLogs);
            var matches = Regex.Matches(fileWithLogs, @"a href='[0-9]+.html'");
            
            // Скачиваем логи anon.fm (на каждый день по файлу)
            int i = 0;
            foreach (var match in matches)
            {
                logPr("Download " + i + "/" + matches.Count + " of pages.");
                var nameFileWithLog = match.ToString().Replace("a href='", "").Replace("'", "");
                var urlLog = urlLogs + nameFileWithLog;

                downloadFile(urlLog, folder + "/" + nameFileWithLog);                
                i++;
            }
            logPr("Downloading is completed.");
        }

        static void Main(string[] args)
        {
            var urlLogs = "http://anon.fm/logs/";
            var folderRoot = "anon.fm";
            var folderLogs = folderRoot + "/logs";
            var fileDbFullName = folderRoot + "/AnonFm";
            
            // Скачиваем логи anon.fm в folder (если там такие есть - перезапись) 
            downloadAnonFmLogs(urlLogs, folderLogs);

            var filesWithLogs = Directory.GetFiles(folderLogs).Where(f => new Regex(@"[0-9]+[.]html").IsMatch(f));
            var anonFmDb = new AnonFmDb();
            
            int i = 0;
            foreach (var file in filesWithLogs)
            {
                anonFmDb.DbConcat(new AnonFmDb(LogFileParser.parseAnonFmLogFile(file), file));                
                logPr("Handle " + i + "/" + filesWithLogs.Count() + " of files.");
                i++;
            }
            logPr("Handling is completed.");

            logPr("Write data to Data Base. Please wait.");
            RadioMusicDataProvider.WriteToDb(anonFmDb.DbClearDuplicates(), fileDbFullName);
            logPr("Writing is completed");           

            Console.ReadLine();
        }
    }
}
