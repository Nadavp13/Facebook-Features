using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

namespace BasicFacebookFeatures
{
    internal abstract class ValidationCheck
    {
        public ValidationCheck m_NextValidation;
        private static ValidationCheck m_LastCheck;

        private void setNext(ValidationCheck i_NextValidation)
        {
            this.m_NextValidation = i_NextValidation;
        }

        public void AddLast(ValidationCheck i_Validation)
        {
            if(m_LastCheck == null)
            {
                m_LastCheck = this;
            }
            m_LastCheck.setNext(i_Validation);
            m_LastCheck = i_Validation;
        }

        public bool Check(ScheduledPost i_post)
        {
            return CheckNext(i_post, SpecificCheck(i_post));
        }

        public bool CheckNext(ScheduledPost i_post, bool i_result)
        {
            if (m_NextValidation != null)
            {
                i_result = i_result && m_NextValidation.Check(i_post);
            }

            return i_result;
        }

        internal abstract bool SpecificCheck(ScheduledPost i_post);
    }

    class ProfanityCheck : ValidationCheck
    {
        internal override bool SpecificCheck(ScheduledPost i_post)
        {
            bool result = true;
            if(i_post.m_Text != null && i_post.m_Text.Contains("BIBI"))
            {
                result = false;
            }

            return result;
        }
    }

    class LengthCheck : ValidationCheck
    {
        internal override bool SpecificCheck(ScheduledPost i_post)
        {
            bool result = true;
            if (i_post.m_Text.Length > 140)
            {
                result = false;
            }

            return result;
        }
    }

    class PictureTypeCheck : ValidationCheck
    {
        internal override bool SpecificCheck(ScheduledPost i_post)
        {
            bool result = true;
            if(i_post.m_PictureURL != null)
            {
                if (!(i_post.m_PictureURL.EndsWith(".png") || i_post.m_PictureURL.EndsWith(".jpeg") || i_post.m_PictureURL.Equals("")))
                {
                    result = false;
                }
            }

            return result;
        }
    }

    class TimeCheck : ValidationCheck
    {
        internal override bool SpecificCheck(ScheduledPost i_post)
        {
            bool result = true;
            if ((i_post.m_TimeToPost - DateTime.Now).TotalSeconds < 0)
            {
                result = false;
            }

            return result;
        }
    }
}