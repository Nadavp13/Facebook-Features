using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
     interface IStrategySerializeHelper
    {
         void SerializeJsonFileWhenSaving(string i_Path);
         void DeserializeJsonFile(string i_Path);

    }
    internal class SerializeHelperImageCollection : IStrategySerializeHelper
    {
        public List<ImageCollection> m_ImageCollection { get; set; }

        public SerializeHelperImageCollection()
        {

        }

        public SerializeHelperImageCollection(List<ImageCollection> i_ImageCollection)
        {
            this.m_ImageCollection = i_ImageCollection;
        }
        public void SerializeJsonFileWhenSaving(string i_FileLocation)

        {
            string jsonString = JsonSerializer.Serialize(m_ImageCollection);
            File.WriteAllText(i_FileLocation, jsonString);
        }

        public void DeserializeJsonFile(string i_DirectoryPath)
        {
            string jsonString = File.ReadAllText(i_DirectoryPath);
            m_ImageCollection = JsonSerializer.Deserialize<List<ImageCollection>>(jsonString);
        }

        public static void SavePictureLocally(int i_Key, Image i_Image, int i_ImageIndex, string i_FileLocation)
        {
            string jsonFileName = i_FileLocation + ($"\\{i_Key}_picture{i_ImageIndex}.jpg");
            i_Image.Save(jsonFileName);
        }
    }

    internal class SerializeHelperNotifiedUser : IStrategySerializeHelper
    {
        public List<NotifiedUser> m_Observers { get; set; }

        public SerializeHelperNotifiedUser()
        {
            this.m_Observers = new List<NotifiedUser>();
        }

        public SerializeHelperNotifiedUser(List<NotifiedUser> i_NotifiedUsers)
        {
            this.m_Observers= i_NotifiedUsers;
        }
        public void SerializeJsonFileWhenSaving( string i_FileLocation)
        {
            string jsonString = JsonSerializer.Serialize(this.m_Observers);
            File.WriteAllText(i_FileLocation, jsonString);
        }

        public void DeserializeJsonFile(string i_DirectoryPath)
        {
            string jsonString = File.ReadAllText(i_DirectoryPath);
            this.m_Observers = JsonSerializer.Deserialize<List<NotifiedUser>>(jsonString);
        }
    }
}
