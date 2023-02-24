using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public sealed class SingeltonPathInitializer
    {
        private static SingeltonPathInitializer m_SingeltonPath = null;
        public string m_DirectoryPath { get; private set; }
        public string m_NotifiedPath { get; private set; }
        public string m_ImageDirectoryPath { get; private set; }
        private static readonly object r_PathLock = new object();

        private SingeltonPathInitializer() {
            string basicPath = System.IO.Directory.GetCurrentDirectory();
            int basicPathOffSet = basicPath.IndexOf("bin", 0) - 1;
            basicPath = basicPath.Substring(0, basicPathOffSet);
            m_DirectoryPath = basicPath + ("\\allCollections.json");
            m_NotifiedPath = basicPath + ("\\toNotify.json");
            m_ImageDirectoryPath = basicPath + ("\\images");
        }

        public static SingeltonPathInitializer getPath
        {
            get
            {
                lock (r_PathLock) {
                    if (m_SingeltonPath == null)
                    {
                        m_SingeltonPath = new SingeltonPathInitializer();
                    }

                    return m_SingeltonPath;
                }  
            }
        }
    }
}
