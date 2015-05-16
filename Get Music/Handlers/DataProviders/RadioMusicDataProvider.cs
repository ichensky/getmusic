using Get_Music.Handlers.Helpers;
using Get_Music.Models.RadioMusic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Get_Music.Handlers.DataProviders
{
    public class RadioMusicDataProvider
    {
        public static IEnumerable<object> ClearListDjs(IEnumerable<Dj> listDjs, XmlDocument xmlDocument, string nodePath = "/NewDataSet/Dj/Nic")
        {
            return listDjs.GroupBy(dj => dj.Nic).Select(dj => dj.First())
                .Where(dj => !XmlHelper.IsNodeExist(xmlDocument, nodePath, dj.Nic));
        }

        public static IEnumerable<object> ClearListArtists(IEnumerable<Artist> listArtists, XmlDocument xmlDocument, string nodePath = "/NewDataSet/Artist/ArtistName")
        {
            return listArtists.GroupBy(artist => artist.ArtistName).Select(artist => artist.First())
                .Where(artist => !XmlHelper.IsNodeExist(xmlDocument, nodePath, artist.ArtistName));
            
        }

        public static IEnumerable<object> ClearListSongs(IEnumerable<Song> listSongs, XmlDocument xmlDocument, string nodePath = "/NewDataSet/Song")
        {
            return listSongs.GroupBy(song => song.SongName).Select(song => song.First())
                .GroupBy(song => song.ArtistName).Select(song => song.First())
                .Where(song => !XmlHelper.IsNodeExist(xmlDocument, nodePath,
                    XmlHelper.GetXmlNode(new XElement("Song",
                        new XElement("SongName", song.SongName),
                        new XElement("ArtistName", song.ArtistName)))));
        }

        public static IEnumerable<object> ClearPlaySongs(IEnumerable<PlaySong> listPlaySongs, XmlDocument xmlDocument, string nodePath = "/NewDataSet/PlaySong")
        {
            return listPlaySongs.GroupBy(playSong => playSong.PlaySongDateTime).Select(playSong => playSong.First())
                .Where(song => !XmlHelper.IsNodeExist(xmlDocument, nodePath,
                    XmlHelper.GetXmlNode(new XElement("PlaySong",
                        new XElement("SongName", song.SongName),
                        new XElement("DjName", song.DjName),
                        new XElement("PlaySongDateTime", song.PlaySongDateTime),
                        new XElement("StyleSet", song.StyleSet)))));
        }

        public static void WriteToDb(Db db, string fileDbFullName, bool ifNoExistCreate = true)
        {
            var dbFullName = fileDbFullName + typeof(Dj).Name + ".xml";
            var xmlDocument = XmlHandler.LoadXmlDocument(dbFullName);
            XmlHandler.WriteObjectToFile(dbFullName, ClearListDjs(db.ListDjs, xmlDocument));

            dbFullName = fileDbFullName + typeof(Artist).Name + ".xml";
            xmlDocument = XmlHandler.LoadXmlDocument(dbFullName);
            XmlHandler.WriteObjectToFile(dbFullName, ClearListArtists(db.ListArtists, xmlDocument));

            dbFullName = fileDbFullName + typeof(Song).Name + ".xml";
            xmlDocument = XmlHandler.LoadXmlDocument(dbFullName);
            XmlHandler.WriteObjectToFile(dbFullName, ClearListSongs(db.ListSongs, xmlDocument));

            dbFullName = fileDbFullName + typeof(PlaySong).Name + ".xml";
            xmlDocument = XmlHandler.LoadXmlDocument(dbFullName);            
            XmlHandler.WriteObjectToFile(dbFullName, ClearPlaySongs(db.ListPlaySongs, xmlDocument));
        }
    }
}
