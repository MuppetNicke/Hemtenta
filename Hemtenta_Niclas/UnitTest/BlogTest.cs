using System;
using NUnit.Framework;
using HemtentaTdd2017;
using HemtentaTdd2017.blog;

namespace UnitTest
{
    [TestFixture]
    public class BlogTest
    {
        [TestCase(null, "hejsan")]
        [TestCase("niclas", null)]
        [TestCase(null, null)]
        public void LoginUser_Fail_NullProperties_NullReferenceException(string name, string password)
        {
            Blog b = new Blog();
            User u = new User(name);
            u.Password = password;

            Assert.Throws<NullReferenceException>(() => b.LoginUser(u));
        }

        [Test]
        public void LoginUser_Fail_NullReferenceException()
        {
            Blog b = new Blog();

            Assert.Throws<NullReferenceException>(() => b.LoginUser(null));
        }

        [Test]
        public void LogoutUser_Fail_NullProperties_NullReferenceException()
        {

        }

    }
}
