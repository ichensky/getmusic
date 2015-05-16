using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Get_Music.Models.RadioMusic
{
    [Serializable]
    public class PlaySong
    {
        #region Properties
        public string SongName { get; set; }

        /// <summary>
        /// Gets or sets the id dj.
        /// </summary>
        /// <value>
        /// The id of Dj witch set this song.
        /// </value>
        public string DjName { get; set; }

        /// <summary>
        /// Gets or sets the time when this song was played.
        /// </summary>
        /// <example>
        /// 01.10.2012 18:08
        /// </example>
        /// <value>
        /// The time when this song was played
        /// </value>
        public DateTime PlaySongDateTime { get; set; }

        /// <summary>
        /// Gets or sets the style set.
        /// </summary>
        /// <value>
        /// The style set. { 0 - live; 1 - robot; }
        /// </value>
        public int StyleSet { get; set; }
        #endregion // Properties        

        #region Constructors
        public PlaySong()
        {
        }

        public PlaySong(string songName, string djName, DateTime playSongDateTime, int styleSet)
        {
            this.SongName = songName;
            this.DjName = djName;
            this.PlaySongDateTime = playSongDateTime;
            this.StyleSet = styleSet;
        }
        #endregion // Constructors
    }
}
