using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    internal class ImageCollection : IImageCollectionInterface
    {
        public string m_UserName { get; set; }
        public int m_Key { get; set; }
        public List<string> m_ImagesArray { get; set; }
        public int[] m_ImagesVotes { get; set; }
        public List<string> m_AllowedFriends { get; set; }

        public ImageCollection() { }

        public static int[] GetListOfKeys(List<ImageCollection> i_AllImageCollections)
        {
            int[] keys = new int[i_AllImageCollections.Count];
            int i = 0;
            foreach (ImageCollection item in i_AllImageCollections)
            {
                keys[i] = item.m_Key;
                i++;
            }

            return keys;
        }
        public Boolean HasAccess()
        {
            return false;
        }

    }
}
