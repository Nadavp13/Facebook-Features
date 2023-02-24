using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal class PostValidator
    {
        private ValidationCheck m_ValidationChecks;

        public PostValidator()
        {
            m_ValidationChecks = new ProfanityCheck();
            m_ValidationChecks.AddLast(new LengthCheck());
            m_ValidationChecks.AddLast(new PictureTypeCheck());
            m_ValidationChecks.AddLast(new TimeCheck());
        }

        public bool Validate(ScheduledPost i_post)
        {
            return m_ValidationChecks.Check(i_post);
        }
    }
}
