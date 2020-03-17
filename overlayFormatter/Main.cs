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
using Newtonsoft.Json;

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

        List<string> overlayFiles = new List<string>();
        List<string> shopFiles = new List<string>();
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

        private void GetOverlayItems(XDocument document, string fileName, ref int count)
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

                            if (!hairCheckBox.Checked)
                            {
                                overlays.RemoveAll(x => x.name.ToLower().Contains("hair"));
                            }

                            LogAction(">> " + fileName + " formatted successfully");

                            count++;
                        }
                        catch (Exception e)
                        {
                            LogAction(">> " + fileName + " an error occured, skipping...");
                            MessageBox.Show(e.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void GetShopItems(XDocument document, string fileName, ref int count)
        {
            if (!document.Root.IsEmpty && document.Root.HasElements)
            {
                if (document.Root.Name.LocalName == "TattooShopItemArray")
                {
                    XElement items = document.Root.Element("TattooShopItems");

                    if (!items.IsEmpty && items.HasElements)
                    {
                        try
                        {
                            List<Shop> shopItems = items.Elements("Item").Select(x => new Shop(
                                x.Element("textLabel").Value,
                                x.Element("collection").Value,
                                x.Element("preset").Value
                            )).ToList();

                            shopItems.ForEach(x =>
                            {
                                int idx = overlays.FindIndex(y => y.name == x.name);

                                if (idx > -1) {
                                    overlays[(int)idx].label = x.label;
                                    overlays[(int)idx].collection = x.collection;
                                }
                            });

                            LogAction(">> " + fileName + " formatted successfully");

                            count++;
                        }
                        catch (Exception e)
                        {
                            LogAction(">> " + fileName + " an error occured, skipping...");
                            MessageBox.Show(e.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        LogAction(">> " + fileName + " has no items, skipping...");
                    }
                }
                else
                {
                    LogAction(">> " + fileName + " is not a TattooShopItemArray, skipping...");
                }
            }
            else
            {
                LogAction(">> " + fileName + " is empty, skipping...");
            }
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            CommonFileDialogResult dialogResult = folderPicker.ShowDialog();

            if (dialogResult == CommonFileDialogResult.Ok)
            {
                string directory = folderPicker.FileName;
                string[] files = Directory.GetFiles(directory);

                overlayFiles.Clear();
                exportBtn.Enabled = false;
                formatBtn.Enabled = false;
                hairCheckBox.Enabled = false;

                foreach (string fileName in files)
                {
                    if (fileName.Contains(".xml") && fileName.Contains("_overlays"))
                        overlayFiles.Add(fileName);
                    if (fileName.Contains(".meta") && fileName.Contains("shop_tattoo"))
                        shopFiles.Add(fileName);
                }

                LogAction("Looking for overlay files");

                if (overlayFiles.Count > 0)
                {
                    selectedPathTxt.Text = directory;

                    for (int i = 0; i < overlayFiles.Count; i++)
                    {
                        LogAction("> " + Path.GetFileName(overlayFiles[i]));
                    }

                    LogAction("Found " + overlayFiles.Count + " overlay files");

                    LogAction("Looking for shop files");

                    for (int i = 0; i < shopFiles.Count; i++)
                    {
                        LogAction("> " + Path.GetFileName(shopFiles[i]));
                    }

                    LogAction("Found " + shopFiles.Count + " shop files");

                    formatBtn.Enabled = true;
                    hairCheckBox.Enabled = true;
                } else
                {
                    MessageBox.Show("No overlay files were found in the directory!", "overlayFormatter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogAction("Unable to find any overlay files");
                }
            }
        }

        private void formatBtn_Click(object sender, EventArgs e)
        {
            exportBtn.Enabled = false;
            overlays.Clear();

            LogAction("Formatting overlay files...");

            int overlayCount = 0;
            int shopCount = 0;

            for (int i = 0; i < overlayFiles.Count; i++)
            {
                string fileName = Path.GetFileName(overlayFiles[i]);

                LogAction("> " + fileName);

                XDocument document = XDocument.Load(overlayFiles[i]);

                GetOverlayItems(document, fileName, ref overlayCount);
            }

            LogAction("Formatted " + overlayCount + " overlay files");

            if (shopFiles.Count > 0)
            {
                LogAction("Formating shop files..");

                for (int i = 0; i < shopFiles.Count; i++)
                {
                    string fileName = Path.GetFileName(shopFiles[i]);

                    LogAction("> " + fileName);

                    XDocument document = XDocument.Load(shopFiles[i]);

                    GetShopItems(document, fileName, ref shopCount);
                }

                LogAction("Formatted " + shopCount + " overlay files");
            } else
            {
                LogAction("If you want labels and collection add shop_tattoo.meta files");
            }

            exportBtn.Enabled = true;
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            string fileName = "overlays";
            int count = 1;
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = "";

            LogAction("Exporting...");

            if (File.Exists(currentDir + "\\" + fileName + ".json")) {
                LogAction("Getting suitable filename...");

                while (File.Exists(currentDir + "\\" + fileName + "_" + count + ".json"))
                {
                    count++;
                }

                filePath = currentDir + "\\" + fileName + "_" + count + ".json";
            } else
            {
                filePath = currentDir + "\\" + fileName + ".json";
            }

            using (StreamWriter outputFile = File.CreateText(filePath))
            {
                outputFile.WriteLine(JsonConvert.SerializeObject(overlays, Formatting.Indented));
            }

            LogAction("Successfully exported to: " + Path.GetFileName(filePath));

            System.Diagnostics.Process.Start(currentDir);
        }
    }
}
