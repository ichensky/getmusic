using Get_Music.Models.RadioMusic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Get_Music.Models.RadioAnonFm
{
    public class AnonFmDb : Db
    {
        private Tuple<int, int, int> getDateYMD(string date)
        {
            var dateYMD = Regex.Match(date, "[0-9]{8}");
            int year = 0;
            int mounth = 1;
            int day = 1;


            try
            {
                year = Convert.ToInt32(dateYMD.Value.Remove(4));
            }
            catch (Exception)
            {
            }

            try
            {
                mounth = Convert.ToInt32(dateYMD.Value.Remove(0, 4).Remove(2));
            }
            catch (Exception)
            {
            }

            try
            {
                day = Convert.ToInt32(dateYMD.Value.Remove(0, 6));
            }
            catch (Exception)
            {
            }

            return Tuple.Create(year, mounth, day);
        }

        private Tuple<int, int> getTimeHM(string time24)
        {
            var time24Split = time24.Split(':');
            int hours = 0;
            int minutes = 0;

            try
            {
                hours = Convert.ToInt32(time24Split[0]);
            }
            catch (Exception)
            {
            }

            try
            {
                minutes = Convert.ToInt32(time24Split[1]);
            }
            catch (Exception)
            {
            }

            return Tuple.Create(hours, minutes);
        }

        public AnonFmDb()
        {
            this.ListDjs = new List<Dj>();
            this.ListArtists = new List<Artist>();
            this.ListSongs = new List<Song>();
            this.ListPlaySongs = new List<PlaySong>();
        }

        public AnonFmDb(List<RadioSet> radioSetList, string dateYMD)
        {
            var date = getDateYMD(dateYMD);

            var listDjs = new List<Dj>();
            var listSongs = new List<Song>();
            var listArtists = new List<Artist>();
            var listPlaySongs = new List<PlaySong>();

            foreach (var radioSet in radioSetList)
            {
                listDjs.Add(new Dj(radioSet.RadioSetDjName));

                foreach (var songList in radioSet.RadioSetSongList)
                {
                    var time = getTimeHM(songList.Time24);

                    listArtists.Add(new Artist(songList.ArtistName));
                    listSongs.Add(new Song(songList.SongName, songList.ArtistName));
                    listPlaySongs.Add(new PlaySong(songList.SongName, radioSet.RadioSetDjName,
                        new DateTime(date.Item1, date.Item2, date.Item3, time.Item1, time.Item2, 0),
                        radioSet.StyleSet));
                }
            }

            this.ListDjs = listDjs;
            this.ListSongs = listSongs;
            this.ListArtists = listArtists;
            this.ListPlaySongs = listPlaySongs;
        }     
    }
}
