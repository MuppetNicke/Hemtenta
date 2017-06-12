using System;
using NUnit.Framework;
using HemtentaTdd2017;
using HemtentaTdd2017.blog;
using Moq;

namespace UnitTest
{

    [TestFixture]
    public class BlogTest
    {

        private Mock<IAuthenticator> auth;


        [SetUp]
        public void SetUp()
        {
            auth = new Mock<IAuthenticator>();
        }

        [TestCase(null, "guest")]
        [TestCase("niclas", null)]
        [TestCase(null, null)]
        public void LoginUser_Fail_NullProperties_NullReferenceException(string name, string password)
        {
            Blog b = new Blog(auth.Object);
            User u = new User(name);
            u.Password = password;

            Assert.Throws<NullReferenceException>(() => b.LoginUser(u));
        }

        [TestCase("niclas", "guest1")]
        [TestCase("nicke", "guest2")]
        [TestCase("nick", "guest2")]
        public void LoginUser_Fail_WrongPasswordException(string name, string password)
        {
            Blog b = new Blog(auth.Object);
            User u = new User(name);
            u.Password = password;
            auth.Setup(x => x.GetUserFromDatabase(name)).Returns(new User(name));


            Assert.Throws<WrongPasswordException>(() => b.LoginUser(u));
        }

        [Test]
        public void LoginUser_Fail_NullReferenceException()
        {
            Blog b = new Blog(auth.Object);

            Assert.Throws<NullReferenceException>(() => b.LoginUser(null));
        }

        [Test]
        public void LoginUser_Fail_UserNotFoundException()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("BLABLA");
            u.Password = "guest";

            Assert.Throws<UserNotFoundException>(() => b.LoginUser(u));
        }

        [TestCase("niclas", "guest")]
        [TestCase("nicke", "guest")]
        [TestCase("nick", "guest")]
        public void LoginUser_Succeed_True(string name, string password)
        {
            Blog b = new Blog(auth.Object);
            User u = new User(name);
            auth.Setup(x => x.GetUserFromDatabase(name)).Returns(new User(name));
            u.Password = password;

            b.LoginUser(u);

            Assert.That(b.UserIsLoggedIn, Is.EqualTo(true));
        }

        [TestCase(null, "guest")]
        [TestCase("niclas", null)]
        [TestCase(null, null)]
        public void LogoutUser_Fail_NullProperties_NullReferenceException(string name, string password)
        {
            Blog b = new Blog(auth.Object);
            User u = new User(name);
            u.Password = password;

            Assert.Throws<NullReferenceException>(() => b.LogoutUser(u));
        }

        [Test]
        public void LogoutUser_Fail_NullReferenceException()
        {
            Blog b = new Blog(auth.Object);

            Assert.Throws<NullReferenceException>(() => b.LogoutUser(null));
        }

        [Test]
        public void LogoutUser_Fail_UserNotFoundException()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("BLABLA");
            u.Password = "hejsan";

            Assert.Throws<UserNotFoundException>(() => b.LogoutUser(u));
        }

        [TestCase("niclas", "guest")]
        [TestCase("nicke", "guest")]
        [TestCase("nick", "guest")]
        public void LogoutUser_Succeed_True(string name, string password)
        {
            Blog b = new Blog(auth.Object);
            User u = new User(name);
            u.Password = password;
            auth.Setup(x => x.GetUserFromDatabase(name)).Returns(new User(name));

            b.LoginUser(u);
            b.LogoutUser(u);

            Assert.That(b.UserIsLoggedIn, Is.EqualTo(false));
        }

        [Test]
        public void PublishPage_Fail_NullReferenceException()
        {
            Blog b = new Blog(auth.Object);

            Assert.Throws<NullReferenceException>(() => b.PublishPage(null));
        }

        [TestCase(null, "He's really cool")]
        [TestCase("The Witcher", null)]
        [TestCase(null, null)]
        public void PublishPage_Fail_NullProperties_NullReferenceException(string title, string content)
        {
            Blog b = new Blog(auth.Object);
            Page p = new Page()
            {
                Title = title,
                Content = content
            };

            Assert.Throws<NullReferenceException>(() => b.PublishPage(p));
        }

        [Test]
        public void PublishPage_Fail_NotLoggedIn_False()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("niclas");
            u.Password = "guest";
            auth.Setup(x => x.GetUserFromDatabase(u.Name)).Returns(new User(u.Name));
            Page p = new Page()
            {
                Title = "The Witcher",
                Content = "He's really cool"
            };

            bool result = b.PublishPage(p);

            //Behövs egentligen inte, men lite extra säkerhet.
            b.LogoutUser(u);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void PublishPage_Succeed_True()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("niclas");
            u.Password = "guest";
            auth.Setup(x => x.GetUserFromDatabase(u.Name)).Returns(new User(u.Name));
            Page p = new Page()
            {
                Title = "The Witcher",
                Content = "He's really cool"
            };
            b.LoginUser(u);

            bool result = b.PublishPage(p);

            Assert.That(result, Is.EqualTo(true));
        }

        [TestCase(null, "XXX-PILLS", "4 CHEAP")]
        [TestCase("you@gmail.com", null, "4 CHEAP")]
        [TestCase("you@gmail.com", "XXX-PILLS", null)]
        [TestCase(null, null, null)]
        [TestCase("", "XXX-PILLS", "4 CHEAP")]
        [TestCase("you@gmail.com", "", "4 CHEAP")]
        [TestCase("you@gmail.com", "XXX-PILLS", "")]
        [TestCase("", "", "")]
        [TestCase(" ", "XXX-PILLS", "4 CHEAP")]
        [TestCase("you@gmail.com", " ", "4 CHEAP")]
        [TestCase("you@gmail.com", "XXX-PILLS", " ")]
        [TestCase(" ", " ", " ")]
        public void SendEmail_Fail_NullOrEmptyParameter_0(string address, string caption, string body)
        {
            Blog b = new Blog(auth.Object);
            int result = b.SendEmail(address, caption, body);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SendEmail_Fail_NotLoggedIn_0()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("niclas");
            u.Password = "hejsan";
            auth.Setup(x => x.GetUserFromDatabase(u.Name)).Returns(new User(u.Name));

            //Just in case...
            b.LogoutUser(u);

            int result = b.SendEmail("you@gmail.com", "XXX-PILLS", "4 CHEAP");

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SendEmail_Succeed_1()
        {
            Blog b = new Blog(auth.Object);
            User u = new User("niclas");
            u.Password = "guest";
            auth.Setup(x => x.GetUserFromDatabase(u.Name)).Returns(new User(u.Name));

            b.LoginUser(u);

            int result = b.SendEmail("you@gmail.com", "XXX-PILLS", "4 CHEAP");

            Assert.That(result, Is.EqualTo(1));
        }

        

    }
}
