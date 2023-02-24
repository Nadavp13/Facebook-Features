# Facebook-Features
New features for facebook using C# .NET and Design Patterns.  
There are plenty (23) of Design Patterns, we chosed 6 that mentioned later on. This project is more about prove of concept than actual funcionality as some where unavailable to accomplish as mentioned later on.  

## How to run
To run the application properly you need to create new Facebook application ID.   
To do so you go https://developers.facebook.com/ , My Apps and Create. Then in "FormMain.cs" search the comment where I state about the application ID and change the 00000 to yours.  
Please notice that some of the features are unavailable as the Terms Of Use of Facebook does not allow it for user's safety and privacy (Such reach your friend list or post something for you).  

## The Features
### Posts Filter
Here you can look at all of your posts (Might take some time to load) or the last <NUMBER OF YOUR CHOSE> posts and filter them by keywords. In addition, you can re-filter as recursive searchs works.  

### Which Picture Should I Upload?
Sharing this application with friends you can upload several pictures as you like and create a friend list. Each friend in that list can login to his own account and search your pictures by a key number. Then the friends voted for what picture they liked the most and after they're done you can easily upload the most liked one by your friends directly to facebook with a comment of your chose.  
*Please note as mentioned before, Facebook does not allow to actual post without some more serious approvals so it is more for the concept itself.  

### Post Scheduler
Add a text and a picture [optional], set time and day and the application will post it for you whenever you chose.  
*Again, terms of use and you need to make sure your application is running in the time and day of the post for it to actually happen.

## Design Patterns Used
Observer - To notify if someone added you to a new image collection in the second feature.  
Stratrgy - Serialize/Desirialize functionallity.  
Chain Of Responsbility - Third feature validity checks.  
(Protected) Proxy - Working with json file (User privacy in second feature).  
Singelton - Creating just one file of each.  
Adapter - Working with generic exceptions and turning them to MessegeBox.  
