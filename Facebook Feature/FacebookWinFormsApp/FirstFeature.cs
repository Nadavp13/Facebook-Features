using FacebookWrapper.ObjectModel;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BasicFacebookFeatures.UniqueExceptions;

namespace BasicFacebookFeatures
{
    internal class FirstFeature
    {
        public static List<string> m_UserPosts { get; private set; }
        public static List<Post> m_UserPostsAfterSearch { get; private set; }
        public static FacebookObjectCollection<Post> m_PostsCollection { get; private set; }

        public FirstFeature() { }

        internal static void FetchPosts(int i_maxPosts, ListBox io_postsListBox)
        {
            m_UserPosts = new List<string>();
            m_UserPostsAfterSearch = new List<Post>();
            int i = 0;
            try
            {
                foreach (Post post in FormMain.m_LoggedInUser.Posts)
                {
                    if (i_maxPosts != 0 && i_maxPosts < i)
                    {
                        break;
                    }
                    i++;
                    m_UserPosts.Add(post.ToString());
                    io_postsListBox.Items.Add(post);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (io_postsListBox.Items.Count == 0)
            {
                throw new NoPostFoundException();
            }
        }

        internal static void SearchPosts(string i_SearchKeyWord, ListBox io_postsListBox, BindingSource i_postBindingSource)
        {
            if(m_PostsCollection == null)
            {
                return;
            }
            if (m_UserPostsAfterSearch.Count == 0)
            {
                foreach (Post post in m_PostsCollection)
                {
                    if (post.Message != null && post.Message.Contains(i_SearchKeyWord))
                    {
                        m_UserPostsAfterSearch.Add(post);
                    }
                }
                if (m_UserPostsAfterSearch.Count == 0)
                {
                    throw new NoPostsAfterSearchingException();
                }
            }
            else
            {
                List<Post> moreThanOneSearchList = new List<Post>();
                foreach (Post post in m_UserPostsAfterSearch)
                {
                    if (post.Message.Contains(i_SearchKeyWord))
                    {
                        moreThanOneSearchList.Add(post);
                    }
                }
                if (moreThanOneSearchList.Count() > 0)
                {
                    m_UserPostsAfterSearch = moreThanOneSearchList;
                }
                else
                {
                    throw new NoPostsAfterSearchingException();
                }
            }
            if (m_UserPostsAfterSearch.Count != 0)
            {
                i_postBindingSource.DataSource = m_UserPostsAfterSearch;
            }
            else
            {
                i_postBindingSource.DataSource = m_PostsCollection;
            }
        }

        internal static void FetchPosts(User i_LoggedInUser, TextBox i_numberOfPostsToShowTextBox, BindingSource i_postBindingSource, ListBox i_postsListBox) {
            bool hasLimit = Int32.TryParse(i_numberOfPostsToShowTextBox.Text, out int maxNumberOfPosts);
            if (m_UserPostsAfterSearch == null) 
            {
                m_UserPostsAfterSearch = new List<Post>();
            }
            if (m_PostsCollection == null)
            {
                m_PostsCollection = new FacebookObjectCollection<Post>();
                if (hasLimit)
                {
                    for (int i = 0; i <= maxNumberOfPosts; i++)
                    {
                        m_PostsCollection.Add(i_LoggedInUser.Posts[i]);
                    }
                }
                else
                {
                    foreach (Post post in i_LoggedInUser.Posts)
                    {
                        m_PostsCollection.Add(post);
                    }
                }
                i_postBindingSource.DataSource = m_PostsCollection;
            }
            else
            {
                if (hasLimit)
                {
                    if (maxNumberOfPosts > m_PostsCollection.Count)
                    {
                        m_PostsCollection = new FacebookObjectCollection<Post>();
                        for (int i = 0; i <= maxNumberOfPosts; i++)
                        {
                            m_PostsCollection.Add(i_LoggedInUser.Posts[i]);
                        }
                        i_postBindingSource.DataSource = m_PostsCollection;
                    }
                    else
                    {
                        FacebookObjectCollection<Post> tempPostsCollection = new FacebookObjectCollection<Post>();
                        for (int i = 0; i <= maxNumberOfPosts; i++)
                        {
                            tempPostsCollection.Add(m_PostsCollection[i]);
                        }
                        i_postBindingSource.DataSource = tempPostsCollection;
                    }
                }
                else
                {
                    i_postBindingSource.DataSource = m_PostsCollection;
                }
            }
        }

        internal static void ShowAllPosts(string i_MaxPosts, ListBox io_postsListBox)
        {
            bool parsedSuccesfully = Int32.TryParse(i_MaxPosts, out int maxPosts);
            if (m_UserPosts == null)
            {
                if (parsedSuccesfully)
                {
                    FetchPosts(maxPosts, io_postsListBox);
                }
                else
                {
                    throw new QuantityNotInFormatException();
                }
            }
            else if (parsedSuccesfully && m_UserPosts.Count != maxPosts)
            {
                FetchPosts(maxPosts, io_postsListBox);
            }
            else
            {
                io_postsListBox.Items.Clear();
                m_UserPostsAfterSearch = new List<Post>();
                foreach (string post in m_UserPosts)
                {
                    io_postsListBox.Items.Add(post);
                }
            }
        }

        internal static void ShowPostsPicture(ListBox io_postsListBox, PictureBox io_postPictureBox)
        {
            string post = io_postsListBox.SelectedItem.ToString();
            if (post != null && post.Contains("[Photo]"))
            {
                string imageURL = post.Substring(9, post.Length - 9);
                io_postPictureBox.Load(imageURL);
            }
            else
            {
                io_postPictureBox.Image = null;
            }
        }
    }
}
