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
                            overlays.AddRange(items.Elements("Item").Select(x => new Overlay(
                                x.Element("nameHash").Value,
                                (Zone)Enum.Parse(typeof(Zone), x.Element("zone").Value),
                                (Type)Enum.Parse(typeof(Type), x.Element("type").Value),
                                (Faction)Enum.Parse(typeof(Faction), x.Element("faction").Value),
                                (Gender)Enum.Parse(typeof(Gender), x.Element("gender").Value)
                            ))); //Selects 4 elements from every overlay and stores it in a list

                            if (!hairCheckBox.Checked) //Checks if hair overlays are to be removed
                            {
                                overlays.RemoveAll(x => x.name.ToLower().Contains("hair")); //Removes all hair overlays
                            }

                            LogAction(">> " + fileName + " formatted successfully");

                            count++; //updates the number of successfully formatted files
                        }
                        catch (Exception e) //Catches any errors that occur in the selection process and notifies the user
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
                            List<Shop> shopItems = items.Elements("Item").Select(x => new Shop(
                                x.Element("textLabel").Value,
                                x.Element("collection").Value,
                                x.Element("preset").Value
                            )).ToList(); //Selects 3 elements from every overlay and stores it in a list

                            shopItems.ForEach(x =>
                            {
                                int idx = overlays.FindIndex(y => y.name == x.name);

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
                hairCheckBox.Enabled = false; //Disable buttons

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

                    formatBtn.Enabled = true;
                    hairCheckBox.Enabled = true; //Enable buttons now that files were found
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
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            string fileName = "overlays";
            int count = 1;
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); //Get application directory
            string filePath = "";

            LogAction("Exporting...");

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
                outputFile.WriteLine(JsonConvert.SerializeObject(overlays, Formatting.Indented)); //Convert list to JSON and write to file
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
