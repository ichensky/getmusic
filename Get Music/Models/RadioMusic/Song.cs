using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Get_Music.Models.RadioMusic
{
    [Serializable]
    public class Song
    {
        #region Properties        
        public string SongName { get; set; }

        public string ArtistName { get; set; }
        #endregion Properties

        #region Constructors
        public Song()
        {
        }

        public Song(string songName, string artistName)
        {
            this.SongName = songName;
            this.ArtistName = artistName;
        }
        #endregion // Constructors
    }
}
