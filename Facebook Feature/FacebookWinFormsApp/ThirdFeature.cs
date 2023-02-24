using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    internal class ThirdFeature
    {
        private readonly PostValidator r_Validator;

        public ThirdFeature()
        {
            r_Validator = new PostValidator();
        }

        public bool SchedulePost(ScheduledPost i_post)
        {
            bool postedSuccefully = false;
            if (r_Validator.Validate(i_post))
            {
                postedSuccefully = true;
                MessageBox.Show("The post will be upload :)");
            }
            else
            {
                i_post.StopTimer();
            }

            return postedSuccefully;
        }
    }
}
