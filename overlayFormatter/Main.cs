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
        string github = "https://github.com/FrazzIe";
        CommonOpenFileDialog folderPicker = new CommonOpenFileDialog
        {
            EnsurePathExists = true,
            IsFolderPicker = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "Select folder with overlays",
        }; //folder picker used to select a directory

        List<string> overlayFiles = new List<string>(); //stores filenames of all (x)_overlays.xml found in the directory
        List<string> shopFiles = new List<string>(); //stores filenames of all shop_tattoo.meta found in the directory
        List<Overlay> overlays = new List<Overlay>(); //stores all the data selected from the (x)_overlays.xml & shop_tattoo.meta

        public Main()
        {
            InitializeComponent();

            exportBtn.Enabled = false;
            formatBtn.Enabled = false;
            overlayRadioButton.Enabled = false;
            tattooRadioButton.Enabled = false;
            hairRadioButton.Enabled = false;
            decalRadioButton.Enabled = false; //Disable buttons
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void LogAction(string msg, bool newLine = true) //tells the user what the program is doing
        {
            if (newLine)
            {
                if (actionsLog.Text != "")
                    actionsLog.AppendText("\r\n");
            }

            actionsLog.AppendText(msg);
        }

        private void GetOverlayItems(XDocument document, string fileName, ref int count) //Fetches select data in all (x)_overlays.xml files
        {
            if (!document.Root.IsEmpty && document.Root.HasElements) //Check if document is empty
            {
                if (document.Root.Name.LocalName == "PedDecorationCollection") //Check if document is an overlays.xml file
                {
                    XElement items = document.Root.Element("presets"); //Selects the element which holds all the overlay items

                    if (!items.IsEmpty && items.HasElements) //Checks if there are overlay items available
                    {
                        try
                        {
                            foreach (XElement item in items.Elements("Item"))
                            {
                                try
                                {
                                    overlays.Add(new Overlay(
                                        item.Element("nameHash").Value,
                                        (Zone)Enum.Parse(typeof(Zone), item.Element("zone").Value),
                                        (Type)Enum.Parse(typeof(Type), item.Element("type").Value),
                                        (Faction)Enum.Parse(typeof(Faction), item.Element("faction").Value),
                                        (Gender)Enum.Parse(typeof(Gender), item.Element("gender").Value)
                                    ));
                                }
                                catch (Exception e) //Catches any errors that occur in the add process and notifies the user
                                {
                                    LogAction(">> " + fileName + " an error occured, skipping item...");
                                    if (!errorCheckBox.Checked)
                                        MessageBox.Show(e.Message, "An error occured with an item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            LogAction(">> " + fileName + " formatted successfully");

                            count++; //updates the number of successfully formatted files
                        }
                        catch (Exception e) //Catches any errors that occur during formatting of a file
                        {
                            LogAction(">> " + fileName + " an error occured, skipping...");
                            if (!errorCheckBox.Checked)
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

        private void GetShopItems(XDocument document, string fileName, ref int count) //Fetches select data in all shop_tattoo.meta files
        {
            if (!document.Root.IsEmpty && document.Root.HasElements) //Check if document is empty
            {
                if (document.Root.Name.LocalName == "TattooShopItemArray") //Check if document is an shop_tattoo.meta file
                {
                    XElement items = document.Root.Element("TattooShopItems"); //Selects the element which holds all the overlay items

                    if (!items.IsEmpty && items.HasElements) //Checks if there are overlay items available
                    {
                        try
                        {
                            List<Shop> shopItems = new List<Shop>();

                            foreach (XElement item in items.Elements("Item"))
                            {
                                try
                                {
                                    shopItems.Add(new Shop(
                                        item.Element("textLabel").Value,
                                        item.Element("collection").Value,
                                        item.Element("preset").Value
                                    ));
                                }
                                catch (Exception e) //Catches any errors that occur in the add process and notifies the user
                                {
                                    LogAction(">> " + fileName + " an error occured, skipping item...");
                                    if (!errorCheckBox.Checked)
                                        MessageBox.Show(e.Message, "An error occured with an item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            shopItems.ForEach(x =>
                            {
                                int idx = overlays.FindIndex(y => y.name.ToLower() == x.name.ToLower());

                                if (idx > -1) {
                                    overlays[(int)idx].label = x.label;
                                    overlays[(int)idx].collection = x.collection;
                                }
                            }); //Loops through every overlay found and looks for a match in the main overlays list and adds the text label and collection

                            LogAction(">> " + fileName + " formatted successfully");

                            count++; //updates the number of successfully formatted files
                        }
                        catch (Exception e) //Catches any errors that occur in the selection process and notifies the user
                        {
                            LogAction(">> " + fileName + " an error occured, skipping...");
                            if (!errorCheckBox.Checked)
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
            CommonFileDialogResult dialogResult = folderPicker.ShowDialog(); //Opens the folder picker

            if (dialogResult == CommonFileDialogResult.Ok) //Checks if a folder was selected
            {
                string directory = folderPicker.FileName; //Gets the folder path
                string[] files = Directory.GetFiles(directory); //Gets all files in the folder

                overlayFiles.Clear(); //Remove any existing files from the last run
                exportBtn.Enabled = false;
                formatBtn.Enabled = false;
                overlayRadioButton.Enabled = false;
                tattooRadioButton.Enabled = false;
                hairRadioButton.Enabled = false;
                decalRadioButton.Enabled = false; //Disable buttons

                foreach (string fileName in files) //Loop through every file
                {
                    if (fileName.Contains(".xml") && fileName.Contains("_overlays")) //Check if file is an (x)_overlays.xml file
                        overlayFiles.Add(fileName);
                    if (fileName.Contains(".meta") && fileName.Contains("shop_tattoo")) //Check if file is a shop_tattoo.meta file
                        shopFiles.Add(fileName);
                }

                LogAction("Looking for overlay files");

                if (overlayFiles.Count > 0) //If any overlays were found
                {
                    selectedPathTxt.Text = directory; //Show the user the selected directory

                    for (int i = 0; i < overlayFiles.Count; i++)
                    {
                        LogAction("> " + Path.GetFileName(overlayFiles[i]));
                    } //Show the user all found (x)_overlays.xml files

                    LogAction("Found " + overlayFiles.Count + " overlay file(s)");

                    LogAction("Looking for shop files");

                    for (int i = 0; i < shopFiles.Count; i++)
                    {
                        LogAction("> " + Path.GetFileName(shopFiles[i]));
                    } //Show the user all found shop_tattoo.meta files

                    LogAction("Found " + shopFiles.Count + " shop file(s)");

                    formatBtn.Enabled = true; //Enable buttons now that files were found
                } else //Show a warning
                {
                    MessageBox.Show("No overlay files were found in the directory!", "overlayFormatter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogAction("Unable to find any overlay files");
                }
            }
        }

        private void formatBtn_Click(object sender, EventArgs e)
        {
            exportBtn.Enabled = false; //Disable export button
            overlayRadioButton.Enabled = false;
            tattooRadioButton.Enabled = false;
            hairRadioButton.Enabled = false;
            decalRadioButton.Enabled = false; //Disable radio buttons

            overlays.Clear(); //Remove any existing overlays from the last run

            LogAction("Formatting overlay files...");

            int overlayCount = 0;
            int shopCount = 0; //Set formatted file counters

            for (int i = 0; i < overlayFiles.Count; i++) //Loop through each overlay file and select data
            {
                string fileName = Path.GetFileName(overlayFiles[i]);

                LogAction("> " + fileName);

                XDocument document = XDocument.Load(overlayFiles[i]);

                GetOverlayItems(document, fileName, ref overlayCount);
            }

            LogAction("Formatted " + overlayCount + " overlay file(s)");

            if (shopFiles.Count > 0) //Check if any shop files were found
            {
                LogAction("Formating shop files..");

                for (int i = 0; i < shopFiles.Count; i++) //Loop through each shop file and select data
                {
                    string fileName = Path.GetFileName(shopFiles[i]);

                    LogAction("> " + fileName);

                    XDocument document = XDocument.Load(shopFiles[i]);

                    GetShopItems(document, fileName, ref shopCount);
                }

                LogAction("Formatted " + shopCount + " overlay file(s)");
            } else
            {
                LogAction("If you want labels and collection add shop_tattoo.meta files");
            }

            exportBtn.Enabled = true; //Allow exporting
            overlayRadioButton.Enabled = true;
            tattooRadioButton.Enabled = true;
            hairRadioButton.Enabled = true;
            decalRadioButton.Enabled = true; //Enable radio buttons
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            List<Overlay> exportOverlays = new List<Overlay>();
            string fileName = "overlays_";
            int count = 1;
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //Get application directory
            string filePath = "";

            LogAction("Exporting...");

            exportOverlays.AddRange(overlays);

            if (tattooRadioButton.Checked) //Checks if only tattoo overlays are being kept
            {
                exportOverlays.RemoveAll(x => x.type != 0); //Removes all non-tattoo overlays
                fileName += "tattoo";
            }
            else if (hairRadioButton.Checked) //Checks if only hair overlays are being kept
            {
                exportOverlays.RemoveAll(x =>
                {
                    if (x.type != 0)
                        return true;
                    if (x.name.ToLower().Contains("hair"))
                        return false;

                    return true;
                }); //Removes all non-hair overlays
                fileName += "hair";
            }
            else if (decalRadioButton.Checked) //Checks if only decal overlays are being kept
            {
                exportOverlays.RemoveAll(x => x.type != 1); //Removes all non-decal overlays
                fileName += "decals";
            } 
            else
            {
                fileName += "all";
            }

            if (File.Exists(currentDir + "\\" + fileName + ".json")) { //Check if exported file already exists
                LogAction("Getting suitable filename...");

                while (File.Exists(currentDir + "\\" + fileName + "_" + count + ".json")) //Find a unused file name
                {
                    count++;
                }

                filePath = currentDir + "\\" + fileName + "_" + count + ".json"; //Set path to new file
            } else
            {
                filePath = currentDir + "\\" + fileName + ".json"; //Set path to new file
            }

            using (StreamWriter outputFile = File.CreateText(filePath)) //Create file
            {
                outputFile.WriteLine(JsonConvert.SerializeObject(exportOverlays, Formatting.Indented)); //Convert list to JSON and write to file
            }

            LogAction("Successfully exported to: " + Path.GetFileName(filePath));

            System.Diagnostics.Process.Start(currentDir); //Open exported file location in explorer
        }

        private void creditLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(github);
        }
    }
}
