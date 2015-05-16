using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Get_Music.Models.RadioAnonFm
{
    /// <summary>
    /// Time period of life of radio
    /// </summary>
    public class RadioSet
    {
        #region Properties
        public string BroadcastName { get; set; }

        public string RadioSetDjName { get; set; }

        public List<RadioSetSong> RadioSetSongList { get; set; }
        
        /// <summary>
        /// Gets or sets the style set.
        /// </summary>
        /// <value>
        /// The style set. { 0 - live; 1 - robot; }
        /// </value>
        public int StyleSet { get; set; }
        #endregion // Properties

        #region Constructors
        public RadioSet()
        {
            RadioSetSongList = new List<RadioSetSong>();
        }
        #endregion // Constructors

        
    }
}
