using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CefSharp.DevTools.Debugger;
using System.Text.Json;
using System.IO;
using CefSharp.DevTools.IndexedDB;
using CefSharp.Web;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        public static User m_LoggedInUser { get; private set; }
        private LoginResult m_LoginResult;
        private ExceptionToMessageBoxAdapter m_AdapterToMessage = new ExceptionToMessageBoxAdapter();
        private SerializeContext m_SerializeObservers = new SerializeContext(new SerializeHelperNotifiedUser());

        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;
        }

        private void loginAndInit()
        {
            m_LoginResult = FacebookService.Login("0000", // To run it you need to make new facebook application ID
					"email",
                    "public_profile",
                    "user_friends",
                    "user_photos",
                    "user_posts");
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(m_LoggedInUser != null)
            {
                return;
            }
            loginAndInit();
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                this.tabsBox.Visible = true;
                buttonLogin.Text = $"Logged in as {m_LoggedInUser.Name}";
                m_SerializeObservers.ExecuteDeserilizerStrategy(SingeltonPathInitializer.getPath.m_NotifiedPath);
                List<NotifiedUser> observers = (m_SerializeObservers.m_Serilizer as SerializeHelperNotifiedUser).m_Observers;
                NotifiedUser notifiedUser = observers.Find(user => user.m_Name == (m_LoggedInUser.FirstName + " " + m_LoggedInUser.LastName));
                if (notifiedUser != null)
                {
                    StringBuilder keys = new StringBuilder("");
                    foreach(int key in notifiedUser.m_Keys){
                        keys.Append(key + ", ");
                    }
                    if(keys.Length > 0)
                    {
                        keys.Remove(keys.Length - 2, 2);
                    }
                    MessageBox.Show($"You Have Been Added To The Following collections: {keys}");
                    observers.Remove(notifiedUser);
                    (m_SerializeObservers.m_Serilizer as SerializeHelperNotifiedUser).m_Observers = observers;
                    m_SerializeObservers.ExecuteSerilizerStrategy(SingeltonPathInitializer.getPath.m_NotifiedPath);
                }
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
			FacebookService.LogoutWithUI();
			buttonLogin.Text = "Login";
            this.tabsBox.Visible = false;
            m_LoggedInUser = null;
            m_LoginResult = null;
        }

        private void linkLabelFetchFriends_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_AdapterToMessage.FetchFriends(m_LoggedInUser, listBoxOfFriends);
        }

        private void listBoxOfFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxOfFriends.SelectedItems.Count == 1)
            {
                User selectedUser = listBoxOfFriends.SelectedItem as User;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog formDialog = new OpenFileDialog();
            formDialog.Title = "Select Pictures To Upload";
            formDialog.InitialDirectory = @"c:\";
            formDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            formDialog.FilterIndex = 2;
            formDialog.RestoreDirectory = true;
            if (formDialog.ShowDialog() == DialogResult.OK)
            {
                if (!displayCurrentPictureBox.Visible)
                {
                    displayCurrentPictureBox.Visible = true;
                }
                displayCurrentPictureBox.ImageLocation = formDialog.FileName;
                displayCurrentPictureBox.Load();
            }
        }

        private void createEventButton_Click(object sender, EventArgs e)
        {
            SecondFeature.CreateEvent();
            displayCurrentPictureBox.Visible = false;
            listBoxOfFriends.Items.Clear();
            listBoxOfFriends.DisplayMember = "Name";
            this.newCollectionPanel.Visible = true;
            this.votingPanel.Visible = false;
            this.keyLabel.Text = $"Your collection key is: {SecondFeature.m_Key}";
            this.createEventButton.Enabled = false;
        }

        private void searchEvent_Click(object sender, EventArgs e)
        {
            if(m_AdapterToMessage.SearchCollection(this, this.searchByKeyTextBox.Text))
            {
                this.showCollection();
            }
        }

        private void showCollection()
        {
            this.newCollectionPanel.Visible = false;
            this.votingPanel.Visible = true;
            this.createEventButton.Enabled = true;
            currentImageVotesLabel.Visible = false;
            SecondFeature.updateCollectionData(userNameLabel, keyNumberLabel, displayPictureBox, currentImageVotesLabel);
            showVotesIfOwner();
        }

        private void showVotesIfOwner()
        {
            if (SecondFeature.m_CurrentImageCollection.m_UserName.Equals(m_LoggedInUser.Name))
            {
                commentTextBox.Visible = true;
                uploadPostButton.Visible = true;
            }
            else
            {
                commentTextBox.Visible = false;
                uploadPostButton.Visible = false;
            }
        }

        private void saveCollection_Click(object sender, EventArgs e)
        {
            SecondFeature.SaveCollection();
            this.newCollectionPanel.Visible = false;
            MessageBox.Show($"Collection Saved Successfully! Collection No.{SecondFeature.m_Key}");
        }

        private void addFriendToCollection_Click(object sender, EventArgs e)
        {
            m_AdapterToMessage.AddViewerToList(listBoxOfFriends);
        }

        private void addPictureToOptionsButton_Click(object sender, EventArgs e)
        {
            SecondFeature.AddPictureToCollection(displayCurrentPictureBox.Image);
            MessageBox.Show("Pictured Added Successfully!");
        }

        private void previousImageButton_Click(object sender, EventArgs e)
        {
            m_AdapterToMessage.MoveToPreviousImage(displayPictureBox, currentImageVotesLabel);
        }

        private void nextImageButton_Click(object sender, EventArgs e)
        {
            m_AdapterToMessage.MoveToNextImage(displayPictureBox, currentImageVotesLabel);
        }

        private void votePictureButton_Click(object sender, EventArgs e)
        {
            SecondFeature.VoteForPicture();
            MessageBox.Show("Your vote been counted!");
        }

        private void saveVotesButton_Click(object sender, EventArgs e)
        {
            SecondFeature.SaveVotesForCollection();
            MessageBox.Show("Your votes been saved!");
        }

        private void uploadPostButton_Click(object sender, EventArgs e)
        {
            m_AdapterToMessage.UploadPost(commentTextBox.Text.ToString());
        }

        private void searchPostsButton_Click(object sender, EventArgs e)
        {
            m_AdapterToMessage.SearchPosts(searchTextBox.Text, postsListBox, postBindingSource);
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            FirstFeature.FetchPosts(m_LoggedInUser, numberOfPostsToShowTextBox, postBindingSource, postsListBox);
        }

        private void postsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (postsListBox.SelectedItems.Count == 1)
            {
                FirstFeature.ShowPostsPicture(postsListBox, postPictureBox);
                dataBindingPanel.Visible = true;
            }
        }

        private void createScheduledPostButton_Click(object sender, EventArgs e)
        {
            ScheduledPost excuteScheduledPost = new ScheduledPost(scheduledPostTextBox, scheduledPostPictureBox, dateTimePicker.Value, hoursTextBox, minutesTextBox);
            ThirdFeature executeFeature = new ThirdFeature();
            executeFeature.SchedulePost(excuteScheduledPost);
        }

        private void thirdFeatureBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog formDialog = new OpenFileDialog();
            formDialog.Title = "Select Pictures To Upload";
            formDialog.InitialDirectory = @"c:\";
            formDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            formDialog.FilterIndex = 2;
            formDialog.RestoreDirectory = true;
            if (formDialog.ShowDialog() == DialogResult.OK)
            {
                if (!scheduledPostPictureBox.Visible)
                {
                    scheduledPostPictureBox.Visible = true;
                }
                scheduledPostPictureBox.ImageLocation = formDialog.FileName;
                scheduledPostPictureBox.Load();
            }
        }
    }
}
