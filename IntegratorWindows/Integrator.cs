using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

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

        public SqlDataReader GetRepositories()
        {
            string sqlconnectionstring = "Database=integrator;Server=(local);uid=integrator;pwd=Compuware1!;";
            SqlConnection con = new SqlConnection(sqlconnectionstring);
            string sp = "[dbo].[spGetRespositories]";
            SqlCommand com = new SqlCommand(sp, con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = null;
            try
            {
                con.Open();
                reader = com.ExecuteReader();
            }
            catch (System.Exception ex)
            {
                string exmessage = ex.Message;
                //log the exception
            }
            return reader;
        }

        public string InsertCommit(string RepositoryId,string Hash,string AuthorName,string AuthorEmail,string Message,string CommitDate)
        {
            string sqlconnectionstring = "Database=integrator;Server=(local);uid=integrator;pwd=Compuware1!;";
            SqlConnection con = new SqlConnection(sqlconnectionstring);
            string sp = "[dbo].[spInsertCommit]";
            SqlCommand com = new SqlCommand(sp, con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                com.Parameters.AddWithValue("@RepositoryId",RepositoryId);
                com.Parameters.AddWithValue("@Hash", Hash);
                com.Parameters.AddWithValue("@AuthorName", AuthorName);
                com.Parameters.AddWithValue("@AuthorEmail", AuthorEmail);
                com.Parameters.AddWithValue("@Message", Message);
                com.Parameters.AddWithValue("@CommitDate", CommitDate);
                com.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                string exmessage = ex.Message;
                //log the exception
            }
            return sp;
        }

        public bool PollRepositories()
        {

            SqlDataReader reader = null;

            try
            {
                reader = GetRepositories();
                while (reader.Read())
                {
                    var repositoryid = reader["RepositoryId"];
                    string repositoryidstring = repositoryid.ToString();
                    var repositorylocation = reader["RepositoryLocation"];
                    string repositorylocationstring = repositorylocation.ToString();

                    var git_url = GitSharp.Repository.FindRepository(repositorylocationstring);
                    if (git_url == null || !GitSharp.Repository.IsValid(git_url))
                    {
                        Console.Write("Given path doesn't seem to refer to a git repository: " + repositorylocation);
                        return false;
                    }
                    var repo = new GitSharp.Repository(git_url);
                    PollCommitments(repo, repositoryidstring);
                }
            }
            catch (System.Exception ex)
            {
                string exmessage = ex.Message;
                //log the exception
            }
            return true;
        }

        private void PollCommitments(GitSharp.Repository repo, string RepositoryId)
        {

            int i = 0;
            string name = string.Empty;
            string hash = string.Empty;
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
                    hash = c1.Hash;
                    authorname = c1.Author.Name;
                    authoremail = c1.Author.EmailAddress;
                    message = c1.Message;
                    commitdate = c1.CommitDate;
                    InsertCommit(RepositoryId,hash,authorname,authoremail,message,commitdate.DateTime.ToString());
                }
                catch
                {
                    loopcontrol = false;
                }
            }
        }

    }
}
