using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace BasicFacebookFeatures
{
    internal class SerializeContext
    {
        public IStrategySerializeHelper m_Serilizer;

        public SerializeContext(IStrategySerializeHelper i_Serlizier)
        {
            SetSerilizer(i_Serlizier);
        }
        public void SetSerilizer(IStrategySerializeHelper i_Serlizier)
        {
            m_Serilizer = i_Serlizier;
        }
        public void ExecuteSerilizerStrategy(string i_Path)
        {
            m_Serilizer.SerializeJsonFileWhenSaving(i_Path);
        }

        public void ExecuteDeserilizerStrategy(string i_Path)
        {
            m_Serilizer.DeserializeJsonFile(i_Path);
        }
    }
}
