using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    internal interface IAdapterInterface
    {
        bool SearchCollection(FormMain i_Form, string i_KeyToCheck);
        void FetchFriends(User i_User, ListBox io_ListBoxOfFriends);
        void AddViewerToList(ListBox i_ListBoxOfFriends);
        void MoveToPreviousImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel);
        void MoveToNextImage(PictureBox io_displayPictureBox, Label io_currentImageVotesLabel);
        void UploadPost(string i_PostComment);
        void SearchPosts(string i_SearchKeyWord, ListBox io_postsListBox, BindingSource i_postBindingSource);
        void ShowAllPosts(string i_MaxPosts, ListBox io_postsListBox);

    }
}
