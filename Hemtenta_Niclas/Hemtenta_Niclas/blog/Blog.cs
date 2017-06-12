using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {

        private IAuthenticator auth;

        public Blog(IAuthenticator auth)
        {
            this.auth = auth;
        }

        public IAuthenticator Auth
        {
            get { return auth; }
            set { auth = value; }
        }

        public bool UserIsLoggedIn { get; private set; }

        public void LoginUser(User u)
        {
            if (u == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(u.Name) || string.IsNullOrWhiteSpace(u.Password))
                throw new NullReferenceException();

            var getUser = auth.GetUserFromDatabase(u.Name);

            if (getUser == null)
                throw new UserNotFoundException();

            if (u.Password != getUser.Password)
                throw new WrongPasswordException();

            UserIsLoggedIn = true;
        }

        public void LogoutUser(User u)
        {
            if (u == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(u.Name) || string.IsNullOrWhiteSpace(u.Password))
                throw new NullReferenceException();

            if (u == null)
                throw new NullReferenceException();

            if (auth.GetUserFromDatabase(u.Name) == null)
                throw new UserNotFoundException();
            else
                UserIsLoggedIn = false;
        }

        public bool PublishPage(Page p)
        {
            if (string.IsNullOrWhiteSpace(p.Content) || string.IsNullOrWhiteSpace(p.Title))
                throw new NullReferenceException();
            else
            {
                if (UserIsLoggedIn == true)
                    return true;
            }

            return false;
        }

        public int SendEmail(string address, string caption, string body)
        {
            if (!string.IsNullOrWhiteSpace(address) || !string.IsNullOrWhiteSpace(caption) || !string.IsNullOrWhiteSpace(body))
            {
                if (UserIsLoggedIn == true)
                    return 1;
            }

            return 0;
        }


    }

    public class UserNotFoundException : Exception { }

    public class WrongPasswordException : Exception { }

    //internal class FakeAuthenticator : IAuthenticator
    //{

    //    private List<User> Users = new List<User>
    //    {
    //        new User("") { Name = "niclas", Password = "hejsan" },
    //        new User("") { Name = "nicke", Password = "hejsan" },
    //        new User("") { Name = "nick", Password = "hejsan" }
    //    };
    //    public User GetUserFromDatabase(string username)
    //    {
    //        foreach (User user in Users)
    //        {
    //            if (user.Name == username)
    //                return user;
    //        }

    //        return null;
    //    }
    //}
}
