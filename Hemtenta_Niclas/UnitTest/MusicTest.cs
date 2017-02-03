using System;
using NUnit.Framework;
using HemtentaTdd2017;
using HemtentaTdd2017.music;

namespace UnitTest
{
    [TestFixture]
    public class MusicTest
    {
        [Test]
        public void Play()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song");

            mp.Play();

            string result = mp.NowPlaying();

            Assert.That(result, Is.EqualTo("Spelar song1"));
        }

        [Test]
        public void Play_Fail_Nothing()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.Play();

            string result = mp.NowPlaying();

            Assert.That(result, Is.EqualTo("Tystnad råder"));
        }

        [Test]
        public void LoadSongs_Succeed()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song");

            int result = mp.NumSongsInQueue;

            Assert.That(result, Is.EqualTo(5));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void LoadSongs_Fail_NullReferenceException(string songName)
        {
            MusicPlayer mp = new MusicPlayer();

            Assert.Throws<NullReferenceException>(() => mp.LoadSongs(songName));
        }

        [Test]
        public void Stop_Succeed()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("sONg");

            mp.Play();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song1"));

            mp.Stop();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));
        }

        [Test]
        public void Stop_Fail_Nothing()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.Stop();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));
        }

        [Test]
        public void NowPlaying_NothingPlaying()
        {
            MusicPlayer mp = new MusicPlayer();

            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));
        }

        [Test]
        public void NowPlaying_Succeed()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song");
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));

            mp.Play();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song1"));
        }

        [Test]
        public void NowPlaying_Succeed_WithNextSong()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song");
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));

            mp.Play();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song1"));

            mp.NextSong();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song2"));
        }

        [Test]
        public void NextSong_TakesAwaySong()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song");
            Assert.That(mp.NumSongsInQueue, Is.EqualTo(5));

            mp.Play();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song1"));

            mp.NextSong();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song2"));
            Assert.That(mp.NumSongsInQueue, Is.EqualTo(4));
        }

        [Test]
        public void NextSong_OneSongInList()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.LoadSongs("song1");
            Assert.That(mp.NumSongsInQueue, Is.EqualTo(1));

            mp.Play();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Spelar song1"));

            mp.NextSong();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));
            Assert.That(mp.NumSongsInQueue, Is.EqualTo(0));
        }

        [Test]
        public void NextSong_EmptyList()
        {
            MusicPlayer mp = new MusicPlayer();

            mp.NextSong();
            Assert.That(mp.NowPlaying(), Is.EqualTo("Tystnad råder"));
        }


    }
}
