using CefSharp.DevTools.IndexedDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{

    internal class NotifierUserSubject
    {
        public List<NotifiedUser> m_FriendsToNotify = new List<NotifiedUser>();

        public void Attach(string i_NotifiedUser)
        {
            NotifiedUser toNotifyFriend = new NotifiedUser();
            List<string> allNames = m_FriendsToNotify.Select(user => user.m_Name).ToList();
            if ((allNames.ToList().Contains(i_NotifiedUser)))
            {
                toNotifyFriend = m_FriendsToNotify.Find((user => user.m_Name == i_NotifiedUser));
            }
            else
            {
                toNotifyFriend.m_Name = i_NotifiedUser;
            }
            m_FriendsToNotify.Add(toNotifyFriend);
        }

        public void Detach(NotifiedUser i_User)
        {
            m_FriendsToNotify.Remove(i_User);
        }

        public void Notify(int i_Key)
        {
            foreach (NotifiedUser observer in m_FriendsToNotify)
            {
                observer.Update(this, i_Key);
            }
        }
    }
}
