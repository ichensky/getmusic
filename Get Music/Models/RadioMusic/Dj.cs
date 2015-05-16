using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Get_Music.Models.RadioMusic
{
    /// <summary>
    /// Dj of radio
    /// </summary>
    [Serializable]
    public class Dj
    {
        #region Properties
        public string Nic { get; set; }
        #endregion // Properties

        #region Constructors
        public Dj()
        {
        }

        public Dj(string nic)
        {
            this.Nic = nic;
        }
        #endregion // Constructors
    }
}
