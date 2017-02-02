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


    }
}
