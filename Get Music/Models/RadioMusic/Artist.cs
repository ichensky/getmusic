using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Get_Music.Models.RadioMusic
{
    [Serializable]
    public class Artist
    {
        #region Properties
        public string ArtistName { get; set; }
        #endregion // Properties

        #region Constructors
        public Artist()
        {
        }

        public Artist(string artistName)
        {
            this.ArtistName = artistName;
        }
        #endregion // Constructors
    }
}
