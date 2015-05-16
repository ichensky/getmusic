using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Get_Music.Models.RadioAnonFm;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Get_Music.Handlers.Parsers.RadioAnonFm
{
    class LogFileParser
    {
        public static List<RadioSet> parseAnonFmLogFile(string file)
        {
            var radioSetList = new List<RadioSet>();

            HtmlDocument document = new HtmlDocument();
            document.Load(file);

            // ul
            var ulTags = document.DocumentNode.SelectNodes(".//ul[@class='list_station_search']");
            if (ulTags != null)
            {
                foreach (var ulTag in ulTags)
                {
                    // li
                    var liTags = ulTag.SelectNodes(".//li");
                    if (liTags != null)
                    {
                        foreach (var liTag in liTags)
                        {
                            var radioSet = new RadioSet();
                            // p 
                            var pTags = liTag.SelectNodes("p");
                            if (pTags != null)
                            {
                                foreach (var pTag in pTags)
                                {
                                    if (pTag.Attributes["class"] != null && pTag.Attributes["class"].Value == "pict_station")
                                    {
                                        // a
                                        var aimgTag = pTag.SelectSingleNode(".//a[@class='logo']");
                                        if (aimgTag != null)
                                        {
                                            // img
                                            var imgTag = aimgTag.SelectSingleNode(".//img");
                                            if (imgTag != null)
                                            {
                                                if (imgTag.Attributes["src"] != null)
                                                {
                                                    if (imgTag.Attributes["src"].Value.Contains("live.png"))
                                                    {
                                                        // Style set
                                                        radioSet.StyleSet = 0;
                                                    }
                                                    else if (imgTag.Attributes["src"].Value.Contains("robot.png"))
                                                    {
                                                        // Style set
                                                        radioSet.StyleSet = 1;
                                                    }
                                                }
                                                if (imgTag.Attributes["title"] != null)
                                                {
                                                    // Dj
                                                    radioSet.RadioSetDjName = imgTag.Attributes["title"].Value;
                                                }
                                            }
                                        }

                                        // span
                                        var spanTag = pTag.SelectSingleNode(".//span");
                                        if (spanTag != null)
                                        {
                                            // a
                                            var aTag = spanTag.SelectSingleNode(".//a[@class='h4']");
                                            if (aTag != null)
                                            {
                                                // Broadcast name
                                                radioSet.BroadcastName = aTag.InnerText;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var radioSetSong = new RadioSetSong();

                                        var pChildTags = pTag.ChildNodes;
                                        if (pChildTags != null)
                                        {
                                            var playThisFlag = pChildTags.Any(node => (node.Attributes["class"] != null) && (node.Attributes["class"].Value == "play-this"));
                                            foreach (var pChildTag in pChildTags)
                                            {
                                                // a
                                                if (pChildTag.Name == "a")
                                                {
                                                    if (pChildTag.Attributes["class"] != null && pChildTag.Attributes["class"].Value == "play-this")
                                                    {
                                                        // Play this
                                                        playThisFlag = true;
                                                    }
                                                    if (pChildTag.Attributes["class"] != null && pChildTag.Attributes["class"].Value == "song")
                                                    {
                                                        // Song name
                                                        radioSetSong.SongName = pChildTag.InnerText;
                                                    }
                                                    else if (pChildTag.Attributes["class"] != null && pChildTag.Attributes["class"].Value == "artist")
                                                    {
                                                        // Artist name
                                                        radioSetSong.ArtistName = pChildTag.InnerText;
                                                    }
                                                }
                                                else if (playThisFlag && (pChildTag.Name == "#text"))
                                                {
                                                    var match = Regex.Match(pChildTag.InnerText, @"[0-9]+[:][0-9]+&nbsp;&nbsp;");
                                                    if (match.Success)
                                                    {
                                                        radioSetSong.Time24 = match.Value.Replace("&nbsp;&nbsp;", "");
                                                    }
                                                }
                                            }
                                            radioSet.RadioSetSongList.Add(radioSetSong);
                                        }
                                    }
                                }
                            }
                            radioSetList.Add(radioSet);
                        }                        
                    }
                }
            }

            return radioSetList;
        }

    }
}
