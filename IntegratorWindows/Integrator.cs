using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntegratorWindows
{
    public partial class Integrator : Form
    {
        public Integrator()
        {
            InitializeComponent();
        }

        private void btnProcessRepositories_Click(object sender, EventArgs e)
        {
            ProcessRepositories();
        }
        public void ProcessRepositories()
        {
            PollRepositories();
        }

        public bool PollRepositories()
        {

            string url;

            url = "d:\\projects\\bradstest\\.git";

            var git_url = GitSharp.Repository.FindRepository(url);
            if (git_url == null || !GitSharp.Repository.IsValid(git_url))
            {
                Console.Write("Given path doesn't seem to refer to a git repository: " + url);
                return false;
            }
            var repo = new GitSharp.Repository(git_url);
            PollCommitments(repo);
            return true;
        }

        private void PollCommitments(GitSharp.Repository repo)
        {

            int i = 0;
            string name = string.Empty;
            string authorname = string.Empty;
            string authoremail = string.Empty;
            string message = string.Empty;
            System.DateTimeOffset commitdate;
            bool loopcontrol = true;
            while (loopcontrol)
            {
                if (i == 0)
                {
                    name = "HEAD";
                    i++;
                }
                else
                {
                    name = name + "^";
                }
                var c1 = new GitSharp.Commit(repo, name);
                try
                {
                    authorname = c1.Author.Name;
                    authoremail = c1.Author.EmailAddress;
                    message = c1.Message;
                    commitdate = c1.CommitDate;
                    //Load to Database
                }
                catch
                {
                    loopcontrol = false;
                }
            }
        }

    }
}
