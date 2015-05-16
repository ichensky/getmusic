using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Get_Music.Models.RadioMusic
{
    public abstract class Db
    {
        public IEnumerable<Dj> ListDjs { get; set; }
        public IEnumerable<Song> ListSongs { get; set; }
        public IEnumerable<Artist> ListArtists { get; set; }
        public IEnumerable<PlaySong> ListPlaySongs { get; set; }

        public Db()
        {
        }

        public Db DbDistinct(bool listDjs = true, bool listSongs = true, bool listArtists = true, bool listPlaySongs = false)
        {
            if (listDjs)
            {
                this.ListDjs = this.ListDjs.GroupBy(dj => dj.Nic).Select(dj => dj.First());
            }
            if (listSongs)
            {
                this.ListArtists = this.ListArtists.GroupBy(artist => artist.ArtistName).Select(artist => artist.First());
            }

            if (listArtists)
            {
                this.ListSongs = this.ListSongs.GroupBy(song => song.SongName).Select(song => song.First())
                   .GroupBy(song => song.ArtistName).Select(song => song.First());
            }

            if (listPlaySongs)
            {
                this.ListPlaySongs = this.ListPlaySongs.GroupBy(playSong => playSong.PlaySongDateTime).Select(playSong => playSong.First());
            }

            return this;
        }

        public Db DbClearDuplicates()
        {
            this.ListDjs = this.ListDjs.GroupBy(dj => dj.Nic).Select(dj => dj.First());

            this.ListArtists = this.ListArtists.GroupBy(artist => artist.ArtistName).Select(artist => artist.First());

            this.ListSongs = this.ListSongs.GroupBy(song => song.SongName).Select(song => song.First())
                .GroupBy(song => song.ArtistName).Select(song => song.First());

            this.ListPlaySongs = this.ListPlaySongs.GroupBy(playSong => playSong.PlaySongDateTime).Select(playSong => playSong.First());

            return this;
        }

        public void DbConcat(Db db)
        {
            this.ListDjs = this.ListDjs.Concat(db.ListDjs);
            this.ListArtists = this.ListArtists.Concat(db.ListArtists);
            this.ListSongs = this.ListSongs.Concat(db.ListSongs);
            this.ListPlaySongs = this.ListPlaySongs.Concat(db.ListPlaySongs);
        }
    }
}
