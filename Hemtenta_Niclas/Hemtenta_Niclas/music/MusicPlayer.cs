using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {

        private List<ISong> SongsInQueue = new List<ISong>();

        private FakeMediaDatabase _MediaDatabase = new FakeMediaDatabase();

        public NickPod SoundPlayer = new NickPod();

        public int NumSongsInQueue{ get { return SongsInQueue.Count; } }

        public void LoadSongs(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                throw new NullReferenceException();

            _MediaDatabase.OpenConnection();

            List<ISong> fetchedSongs = _MediaDatabase.FetchSongs(search);
            SongsInQueue.AddRange(fetchedSongs);
            _MediaDatabase.CloseConnection();
        }

        public void NextSong()
        {
            if (SongsInQueue.Count > 0)
                SongsInQueue.RemoveAt(0);

            if (SongsInQueue.Count > 1)
            {
                SoundPlayer.Stop();
                SoundPlayer.Play(SongsInQueue.ElementAt(0));
            }
            else
                SoundPlayer.Stop();
        }

        public string NowPlaying()
        {
            if (string.IsNullOrWhiteSpace(SoundPlayer.NowPlaying))
                return "Tystnad råder";
            else
                return "Spelar " + SoundPlayer.NowPlaying;
        }

        public void Play()
        {
            //if (SoundPlayer.NowPlaying == "")
            //    NextSong();
            if (string.IsNullOrWhiteSpace(SoundPlayer.NowPlaying) && SongsInQueue.Count > 0)
                SoundPlayer.Play(SongsInQueue.ElementAt(0));
        }

        public void Stop()
        {
            SoundPlayer.Stop();
        }
    }

    public class FakeMediaDatabase : IMediaDatabase
    {
        public bool IsConnected { get; private set; }

        public List<Song> Songs = new List<Song>()
        {
            new Song("song1"),
            new Song("song2"),
            new Song("song3"),
            new Song("song4"),
            new Song("song5"),
            new Song("Nicke's kärleksballader"),
            new Song("Best of Nicke '65"),
            new Song("Nicke's best Disco hits!")
        };

        public void CloseConnection()
        {
            if (IsConnected)
                IsConnected = false;
            else
                throw new DatabaseClosedException();
        }

        public List<ISong> FetchSongs(string search)
        {
            List<ISong> fetchedSongs = new List<ISong>();

            if (string.IsNullOrWhiteSpace(search))
                throw new NullReferenceException();

            if (IsConnected)
            {
                foreach (Song song in Songs)
                {
                    if (song.Title.ToLower().Contains(search.ToLower()))
                        fetchedSongs.Add(song);
                }
            }
            else
                throw new DatabaseClosedException();

            return fetchedSongs;
        }

        public void OpenConnection()
        {
            if (!IsConnected)
                IsConnected = true;
            else
                throw new DatabaseAlreadyOpenException();
        }
    }

    public class Song : ISong
    {
        public string Title { get; private set; }

        public Song(string title)
        {
            Title = title;
        }

        public Song ToSong(ISong song)
        {
            Song s = new Song("");
            s.Title = song.Title;
            return s;
        }
    }

    public class NickPod : ISoundMaker
    {
        public string NowPlaying { get; private set; }

        public void Play(ISong song)
        {
            if (song == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(NowPlaying))
                NowPlaying = song.Title;
        }

        public void Stop()
        {
            NowPlaying = "";
        }
    }
}
