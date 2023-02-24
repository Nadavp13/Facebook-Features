using CefSharp.DevTools.IndexedDB;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal interface IObserver
    {
        void Update(NotifierUserSubject subject, int key);
    }
    internal class NotifiedUser:IObserver
    {
        public string m_Name { get; set; }
        public List<int> m_Keys {get; set; }

        public NotifiedUser()
        {

        }
        public void Update(NotifierUserSubject i_Notifier, int i_Key)
        {
            if (this.m_Keys == null)
            {
                m_Keys = new List<int>();
            }
            m_Keys.Add(i_Key);
        }

    }
}
