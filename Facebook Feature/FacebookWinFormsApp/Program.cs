using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FacebookWrapper;

// $G$ THE-001 (-2) Grade: 98 on patterns selection / accuracy in implementation / description / document / diagrams (50%) (see comments in document).
// $G$ DSN-999 (-10) Program crashes on second feature when try to click 'Add' before 'fetchFriends'. See comment on SecondFeature.cs (line 125).
// $G$ CSS-999 (-10) Coding Standards and StyleCop errors.

namespace BasicFacebookFeatures
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FacebookService.s_UseForamttedToStrings = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
