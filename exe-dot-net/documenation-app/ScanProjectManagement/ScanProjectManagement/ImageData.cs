using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanProjectManagement
{
    public partial class ImageData : Form
    {
        public ImageData()
        {
            InitializeComponent();
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                DialogResult dr = ofd.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    if (ofd.CheckFileExists)
                    {
                        FileInfo f = new FileInfo(ofd.FileName);
                        string data = FileData.GetFileData(f);
                        imageDataText.Text = data;
                    }
                }
            }
        }
    }
    public static class FileData
    {
        const string ContentType = "Content Type";
        public static string GetFileData(FileInfo file)
        {
            StringBuilder data = new StringBuilder();
            data.Append("data: ");
            if (File.Exists(file.FullName))
            {
                data.Append(GetMimeType(file.FullName));
                data.Append("; base64,");
                data.Append(Convert.ToBase64String(File.ReadAllBytes(file.FullName)));
            }
            return data.ToString();
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = null;
            string extension = Path.GetExtension(fileName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(extension);
            if (regKey != null && regKey.GetValue(ContentType) != null)
                mimeType = regKey.GetValue(ContentType).ToString();
            return string.IsNullOrEmpty(mimeType) ? "application/unknown" : mimeType;
        }
    }
}
