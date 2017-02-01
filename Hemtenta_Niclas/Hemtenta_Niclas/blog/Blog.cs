using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        public bool UserIsLoggedIn { get; private set; }

        public void LoginUser(User u)
        {
            if (u == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(u.Name) || string.IsNullOrWhiteSpace(u.Password))
                throw new NullReferenceException();

            UserIsLoggedIn = true;
        }

        public void LogoutUser(User u)
        {
            if (u == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(u.Name) || string.IsNullOrWhiteSpace(u.Password))
                throw new NullReferenceException();

            UserIsLoggedIn = false;
        }

        public bool PublishPage(Page p)
        {
            if (string.IsNullOrWhiteSpace(p.Content) || string.IsNullOrWhiteSpace(p.Title))
                throw new Exception();
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
}
