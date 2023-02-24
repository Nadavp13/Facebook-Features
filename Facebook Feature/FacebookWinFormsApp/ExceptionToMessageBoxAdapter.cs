using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BasicFacebookFeatures.UniqueExceptions;

namespace BasicFacebookFeatures
{
    public class ExceptionToMessageBoxAdapter:IAdapterInterface
    {
        public bool SearchCollection(FormMain i_Form, string i_KeyToCheck)
        {
            bool collectionExists = false;
            try
            {
                collectionExists = SecondFeature.SearchCollection(i_Form, i_KeyToCheck);
            }
            catch (NoCollectionFoundException)
            {
                MessageBox.Show("No collection found :(");
            }
            catch (NoAccessToCollectionException)
            {
                MessageBox.Show("You have no acceses to this collection :(");
            }
            return collectionExists;
        }
        public void FetchFriends(User i_User, ListBox io_ListBoxOfFriends)
        {
            try
            {
                SecondFeature.fetchFriends(i_User, io_ListBoxOfFriends);
            }
            catch (FriendNotFoundException)
            {
                MessageBox.Show("No friends to retrieve :(");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddViewerToList(ListBox i_ListBoxOfFriends)
        {
            try
            {
                SecondFeature.AddViewerToList(i_ListBoxOfFriends);
            }
            catch (NotExactlyOneException)
            {
                MessageBox.Show("Could not add a friend, More than one friend is seleced.");
            }
        }

        public void MoveToPreviousImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel)
        {
            try
            {
                SecondFeature.MoveToPreviousImage(io_displayPictureBox, io_currentImageVotesLabel);
            }
            catch (NoMoreImagesException)
            {
                MessageBox.Show("No More Pictures!");
            }
        }

        public void MoveToNextImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel)
        {
            try
            {
                SecondFeature.MoveToNextImage(io_displayPictureBox, io_currentImageVotesLabel);
            }
            catch (NoMoreImagesException)
            {
                MessageBox.Show("No More Pictures!");
            }
        }

        public void UploadPost(string i_PostComment)
        {
            try
            {
                SecondFeature.UploadPost(i_PostComment);
            }
            catch (Facebook.FacebookOAuthException)
            {
                MessageBox.Show("Facebook did not gave us premission to really post it :(");
            }

        }

        public void SearchPosts(string i_SearchKeyWord, ListBox io_postsListBox, BindingSource i_postBindingSource)
        {
            try
            {
                FirstFeature.SearchPosts(i_SearchKeyWord,io_postsListBox, i_postBindingSource);
            }
            catch (NoPostsAfterSearchingException)
            {
                MessageBox.Show("No posts match to the search, Showing last search :(");
            }
        }

        public void ShowAllPosts(string i_MaxPosts, ListBox io_postsListBox)
        {
            try
            {
                FirstFeature.ShowAllPosts(i_MaxPosts, io_postsListBox);
            }
            catch (QuantityNotInFormatException)
            {
                MessageBox.Show("Invalid amount of posts");
            }
            catch (Facebook.FacebookOAuthException e)
            {
                MessageBox.Show(e.Message);
            }

            catch (NoPostFoundException)
            {
                MessageBox.Show("No posts to retrieve :(");
            }
        }
    }
}
