using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace overlayFormatter
{
    public partial class Main : Form
    {
        CommonOpenFileDialog folderPicker = new CommonOpenFileDialog
        {
            EnsurePathExists = true,
            IsFolderPicker = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "Select folder with overlays",
        };

        List<string> selectedFiles = new List<string>();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void LogAction(string msg, bool newLine = true)
        {
            if (newLine)
            {
                if (actionsLog.Text != "")
                    actionsLog.Text += "\r\n";
            }

            actionsLog.Text += msg;
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            CommonFileDialogResult dialogResult = folderPicker.ShowDialog();

            if (dialogResult == CommonFileDialogResult.Ok)
            {
                string directory = folderPicker.FileName;
                string[] files = Directory.GetFiles(directory);

                selectedFiles.Clear();
                formatBtn.Enabled = false;

                foreach (string fileName in files)
                {
                    if (fileName.Contains(".meta"))
                        selectedFiles.Add(fileName);
                }

                LogAction("Looking for .meta files");

                if (selectedFiles.Count > 0)
                {
                    selectedPathTxt.Text = directory;
                    formatBtn.Enabled = true;

                    for (int i = 0; i < selectedFiles.Count; i++)
                    {
                        LogAction(">" + Path.GetFileName(selectedFiles[i]));
                    }

                    LogAction("Found " + selectedFiles.Count + " files");
                } else
                {
                    MessageBox.Show("No .meta files were found in the directory!", "overlayFormatter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogAction("Unable to find any .meta files");
                }
            }
        }
    }
}
