using FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static BasicFacebookFeatures.UniqueExceptions;

namespace BasicFacebookFeatures
{
    internal class SecondFeature
    {
        public static List<string> m_FriendsList { get; private set; }
        public static int m_Key { get; private set; }
        public static List<string> m_Images { get; private set; }
        private static int[] m_ValidKeys;
        private static int m_CurrentImage = 0;
        public static ImageCollection m_CurrentImageCollection { get; private set; }
        public static List<ImageCollection> m_AllImageCollection { get; private set; }
        public static SingeltonPathInitializer m_Path = SingeltonPathInitializer.getPath;
        public static NotifierUserSubject m_Notifier = new NotifierUserSubject();
        public static SerializeContext m_SerializeImageCollection = new SerializeContext(new SerializeHelperImageCollection());
        public static SerializeContext m_SerializeObservers = new SerializeContext(new SerializeHelperNotifiedUser());
        public static SerializeHelperImageCollection m_ParseImageCollectionSerializer = (m_SerializeImageCollection.m_Serilizer as SerializeHelperImageCollection);
        public static SerializeHelperNotifiedUser m_ParseObserverSerilizer = (m_SerializeObservers.m_Serilizer as SerializeHelperNotifiedUser);

        public SecondFeature() { }

        internal static void fetchFriends(User i_User, ListBox io_ListBoxOfFriends)
        {
            try
            {
                foreach (User friend in i_User.Friends)
                {
                    io_ListBoxOfFriends.Items.Add(friend.Name);
                }
                io_ListBoxOfFriends.Items.Add("Nicky Goldstein");
                io_ListBoxOfFriends.Items.Add("Avi Ron");
                io_ListBoxOfFriends.Items.Add("Eli Coppter");
                io_ListBoxOfFriends.Items.Add("Tiki Por");
                io_ListBoxOfFriends.Items.Add("Amit Galeah");
                io_ListBoxOfFriends.Items.Add("Ish Im Otu");
                io_ListBoxOfFriends.Items.Add("Gila Zahav");
                io_ListBoxOfFriends.Items.Add("Moti Lohim");
            }
            catch (Exception ex)
            {
                throw ex; 
            }

            if (io_ListBoxOfFriends.Items.Count == 0)
            {
                throw new FriendNotFoundException();
            }
        }

        internal static void CreateEvent()
        {
            m_FriendsList = new List<string>();
            m_Images = new List<string>();
            m_Key = new Random().Next(1000, 10000);
        }

        internal static bool SearchCollection(FormMain i_Form, string i_KeyToCheck)
        {
            bool canShowCollectionToUser = false;
            m_SerializeImageCollection.ExecuteDeserilizerStrategy(m_Path.m_DirectoryPath);
            m_AllImageCollection = m_ParseImageCollectionSerializer.m_ImageCollection;
            m_ValidKeys = ImageCollection.GetListOfKeys(m_AllImageCollection);
            bool isNumber = Int32.TryParse(i_KeyToCheck, out int imageIndex);
            if (isNumber && m_ValidKeys.Contains(imageIndex))
            {
                m_CurrentImageCollection = m_AllImageCollection[Array.IndexOf(m_ValidKeys, imageIndex)];
                ImageCollenctionProxy icProxcy = new ImageCollenctionProxy(m_CurrentImageCollection, FormMain.m_LoggedInUser.Name);
                if (icProxcy.HasAccess()) 
                {
                    m_CurrentImage = 0;
                    canShowCollectionToUser = true;
                }
                
                else
                {
                    throw new NoAccessToCollectionException();
                }
            }
            else
            {
                throw new NoCollectionFoundException();
            }

            return canShowCollectionToUser;
        }

        internal static void updateCollectionData(Label io_UserNameLabel, Label io_KeyLabel, PictureBox io_DisplayPicureBox, Label io_currentImageVotesLabel)
        {
            io_UserNameLabel.Text = m_CurrentImageCollection.m_UserName.ToString();
            io_KeyLabel.Text = m_CurrentImageCollection.m_Key.ToString();
            io_DisplayPicureBox.ImageLocation = m_Path.m_ImageDirectoryPath + m_CurrentImageCollection.m_ImagesArray[m_CurrentImage];
            io_DisplayPicureBox.Load();
            VotesUpdate(io_currentImageVotesLabel);
        }

        internal static void SaveCollection() 
        {
            m_SerializeImageCollection.ExecuteDeserilizerStrategy(m_Path.m_DirectoryPath);
            m_AllImageCollection = m_ParseImageCollectionSerializer.m_ImageCollection;
            m_AllImageCollection.Add(new ImageCollection() { m_UserName = FormMain.m_LoggedInUser.Name, m_Key = m_Key, m_ImagesArray = m_Images, m_ImagesVotes = new int[m_Images.Count], m_AllowedFriends = m_FriendsList });
            m_ParseImageCollectionSerializer.m_ImageCollection = m_AllImageCollection;
            m_SerializeImageCollection.ExecuteSerilizerStrategy(m_Path.m_DirectoryPath);
            m_Notifier.Notify(m_Key);
            m_ParseObserverSerilizer.m_Observers = m_Notifier.m_FriendsToNotify;
            m_SerializeObservers.ExecuteSerilizerStrategy( m_Path.m_NotifiedPath);
        }

        internal static void AddViewerToList(ListBox i_ListBoxOfFriends)
        {
            /// Selected item might return null.
            string selectedUser = i_ListBoxOfFriends.SelectedItem.ToString();
            m_FriendsList.Add(selectedUser);
            MessageBox.Show($"{selectedUser} Added to Viewers List!");
            m_Notifier.Attach(selectedUser);
        }

        internal static void AddPictureToCollection(Image i_ImageToAdd)
        {
            SerializeHelperImageCollection.SavePictureLocally(m_Key, i_ImageToAdd, m_Images.Count, m_Path.m_ImageDirectoryPath);
            m_Images.Add($"\\{m_Key}_picture{m_Images.Count}.jpg");
        }

        internal static void MoveToPreviousImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel) 
        {
            if (m_CurrentImage == 0)
            {
                throw new NoMoreImagesException();
            }
            m_CurrentImage--;
            io_displayPictureBox.ImageLocation = m_Path.m_ImageDirectoryPath + m_CurrentImageCollection.m_ImagesArray[m_CurrentImage];
            io_displayPictureBox.Load();
            VotesUpdate(io_currentImageVotesLabel);
        }

        internal static void MoveToNextImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel)
        {
            if (m_CurrentImage == (m_CurrentImageCollection.m_ImagesArray.Count - 1))
            {
                throw new NoMoreImagesException();
            }
            m_CurrentImage++;
            io_displayPictureBox.ImageLocation = m_Path.m_ImageDirectoryPath + m_CurrentImageCollection.m_ImagesArray[m_CurrentImage];
            io_displayPictureBox.Load();
            VotesUpdate(io_currentImageVotesLabel);
        }

        internal static void VotesUpdate(Label io_currentImageVotesLabel)
        {
            if (m_CurrentImageCollection.m_UserName.Equals(FormMain.m_LoggedInUser.Name))
            {
                io_currentImageVotesLabel.Visible = true;
                io_currentImageVotesLabel.Text = $"current votes: {m_CurrentImageCollection.m_ImagesVotes[m_CurrentImage]}";
            }
        }

        internal static void VoteForPicture()
        {
            m_CurrentImageCollection.m_ImagesVotes[m_CurrentImage]++;
        }

        internal static void SaveVotesForCollection()
        {
            m_ParseImageCollectionSerializer.m_ImageCollection = m_AllImageCollection;
            m_SerializeImageCollection.ExecuteSerilizerStrategy(m_Path.m_DirectoryPath);
        }

        internal static void UploadPost(string i_PostComment)
        {
            int mostLikedPictureVotesAmount = m_CurrentImageCollection.m_ImagesVotes.Max();
            int mostLikedPictureIndex = Array.IndexOf(m_CurrentImageCollection.m_ImagesVotes, mostLikedPictureVotesAmount);
            string imagePath = m_Path.m_ImageDirectoryPath + (($"\\{m_CurrentImageCollection.m_Key}_picture{mostLikedPictureIndex}.jpg"));
            try
            {
                FormMain.m_LoggedInUser.PostPhoto(imagePath, i_PostComment);
            }
            catch (Facebook.FacebookOAuthException ex)
            {
                throw ex;
            }
        }
    }
}
