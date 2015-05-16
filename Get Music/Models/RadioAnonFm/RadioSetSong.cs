using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Get_Music.Models.RadioAnonFm
{
    public class RadioSetSong
    {
        public string SongName { get; set; }

        /// <summary>
        /// Gets or sets the time24.
        /// </summary>
        /// <value>
        /// The time in 24 hours.
        /// </value>
        public string Time24 { get; set; }

        public string ArtistName { get; set; }
    }
}
