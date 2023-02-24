using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    internal class ScheduledPost
    {
        public string m_Text { get; set; }
        public string m_PictureURL { get; set; }
        public DateTime m_TimeToPost { get; set; }
        public int m_Hours {get; set; }
        public int m_Minutes { get; set; }
        private System.Timers.Timer m_Timer;

        public ScheduledPost(TextBox i_Text, PictureBox i_PictureURL, DateTime i_TimeToPost, TextBox i_Hours, TextBox i_Minutes)
        {
            m_Text = "";
            m_PictureURL = "";
            m_TimeToPost = i_TimeToPost;
            m_Hours = 23;
            m_Minutes = 59;
            bool updated = validateAndUpdateParameters(i_Text, i_PictureURL, i_TimeToPost, i_Hours, i_Minutes);
            if (!updated)
            {
                MessageBox.Show("Notice some of your inputs are empty and been set to default values");
            }
        }

        private bool validateAndUpdateParameters(TextBox i_Text, PictureBox i_PictureURL, DateTime i_TimeToPost, TextBox i_Hours, TextBox i_Minutes) {
            bool validParameters = true;
            if (i_Text.Text != null) {
                m_Text = i_Text.Text;
            }
            else
            {
                validParameters = false;
            }
            if (i_Hours.Text != null && i_Minutes.Text != null) {
                parseAndValidateTime(i_Hours.Text, i_Minutes.Text);
            }
            else
            {
                validParameters = false;
            }
            if (i_PictureURL.Image != null)
            {
                m_PictureURL = i_PictureURL.Image.ToString();
            }
            else
            {
                validParameters = false;
            }
            setTimeOfPost();
            if ((this.m_TimeToPost - DateTime.Now).TotalMilliseconds < 0)
            {
                this.m_TimeToPost = DateTime.Now.Date + new TimeSpan(23, 59, 0);
                validParameters = false;
            }
            startTimer();

            return validParameters;
        }

        public void StopTimer()
        {
            m_Timer.Enabled = false;
            m_Timer.Stop();
        }

        private void parseAndValidateTime(string i_Hours, string i_Minutes)
        {
            m_Hours = 23;
            m_Minutes = 59;
            if(Int32.TryParse(i_Hours, out int hours))
            {
                m_Hours = hours;
            }
            if (Int32.TryParse(i_Minutes, out int minutes))
            {
                m_Minutes = minutes;
            }
            m_Hours = Math.Min(23, m_Hours);
            m_Hours = Math.Max(0, m_Hours);
            m_Minutes = Math.Min(59, m_Minutes);
            m_Minutes = Math.Max(0, m_Minutes);
        }

        private void setTimeOfPost()
        {
            TimeSpan timeSpan = new TimeSpan(m_Hours, m_Minutes, 0);
            m_TimeToPost = m_TimeToPost.Date + timeSpan;
        }

        private void startTimer()
        {
            m_Timer = new System.Timers.Timer();
            m_Timer.Interval = (this.m_TimeToPost - DateTime.Now).TotalMilliseconds;
            m_Timer.Elapsed += new ElapsedEventHandler(PostByScheldue);
            m_Timer.Enabled = true;
        }

        private void PostByScheldue(object source, ElapsedEventArgs e)
        {
           MessageBox.Show("Your Post Has Been Upload");
           StopTimer();
        }
    }
}
