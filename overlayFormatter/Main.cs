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
using System.Xml.Linq;

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
        List<Overlay> overlays = new List<Overlay>();

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
                    actionsLog.AppendText("\r\n");
            }

            actionsLog.AppendText(msg);
        }

        private void GetOverlayItems(XDocument document, string fileName)
        {
            if (!document.Root.IsEmpty && document.Root.HasElements)
            {
                if (document.Root.Name.LocalName == "PedDecorationCollection")
                {
                    XElement items = document.Root.Element("presets");

                    if (!items.IsEmpty && items.HasElements)
                    {
                        try
                        {
                            overlays.AddRange(items.Elements("Item").Select(x => new Overlay(
                                x.Element("nameHash").Value,
                                (Zone)Enum.Parse(typeof(Zone), x.Element("zone").Value),
                                (Type)Enum.Parse(typeof(Type), x.Element("type").Value),
                                (Faction)Enum.Parse(typeof(Faction), x.Element("faction").Value),
                                (Gender)Enum.Parse(typeof(Gender), x.Element("gender").Value)
                            )));

                            LogAction(">> " + fileName + " formatted successfully");
                        }
                        catch
                        {
                            LogAction(">> " + fileName + " an error occured, skipping...");
                        }
                    }
                    else
                    {
                        LogAction(">> " + fileName + " has no items, skipping...");
                    }
                }
                else
                {
                    LogAction(">> " + fileName + " is not a PedDecorationCollection, skipping...");
                }
            }
            else
            {
                LogAction(">> " + fileName + " is empty, skipping...");
            }
        }

        private void GetShopItems(XDocument document, string fileName)
        {

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
                    if (fileName.Contains(".xml") && fileName.Contains("_overlays"))
                        selectedFiles.Add(fileName);
                }

                LogAction("Looking for .xml files");

                if (selectedFiles.Count > 0)
                {
                    selectedPathTxt.Text = directory;
                    formatBtn.Enabled = true;

                    for (int i = 0; i < selectedFiles.Count; i++)
                    {
                        LogAction("> " + Path.GetFileName(selectedFiles[i]));
                    }

                    LogAction("Found " + selectedFiles.Count + " files");
                } else
                {
                    MessageBox.Show("No .xml files were found in the directory!", "overlayFormatter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogAction("Unable to find any .meta files");
                }
            }
        }

        private void formatBtn_Click(object sender, EventArgs e)
        {
            overlays.Clear();

            LogAction("Formatting..");

            for (int i = 0; i < selectedFiles.Count; i++)
            {
                string fileName = Path.GetFileName(selectedFiles[i]);

                LogAction("> " + fileName);

                XDocument document = XDocument.Load(selectedFiles[i]);

                GetOverlayItems(document, fileName);
            }

            LogAction("Formatted " + selectedFiles.Count + " files");
        }
    }
}
