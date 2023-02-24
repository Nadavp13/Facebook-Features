using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal class ImageCollenctionProxy: IImageCollectionInterface
    {
        private ImageCollection m_Collection;
        private string m_WantedPermissionName;
   
        public ImageCollenctionProxy(ImageCollection collection, string name)
        {
            m_Collection = collection;
            m_WantedPermissionName = name;
        }

        public bool HasAccess()
        {
            bool hasAccess = false;
            if ((m_Collection.m_AllowedFriends.Contains(m_WantedPermissionName)|| m_Collection.m_UserName.Equals(m_WantedPermissionName)) && (m_Collection.m_ImagesArray.Count > 0))
            {
                hasAccess = true;
            }

            return hasAccess;
        }
    }
}
