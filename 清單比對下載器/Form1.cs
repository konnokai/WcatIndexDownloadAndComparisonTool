using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using UnityExport;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace 清單比對下載器
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        private static extern void SetForegroundWindow(IntPtr hwnd);

        WebClient WCIndex = new WebClient(), WCPic = new WebClient();
        AutoResetEvent evtDownload = new AutoResetEvent(false);
        Dictionary<string, string> IndexData = new Dictionary<string, string>();
        string IndexText = "";
        byte[] resultData = null;
        bool Error = false, StopWork = false, CancelClose = false, isUseFromErrorList = false;

        #region 委派
        private void SetGroupBoxEnable(bool enable, GroupBox gb)
        {
            if (InvokeRequired) BeginInvoke(new Action(delegate { gb.Enabled = enable; }));
            else gb.Enabled = enable;
        }

        private void SetButtonEnable(bool enable, Button btn)
        {
            if (InvokeRequired) BeginInvoke(new Action(delegate { btn.Enabled = enable; }));
            else btn.Enabled = enable;
        }

        private void AddTextBoxText(string text, bool isNewLine = true)
        {
            if (isNewLine) text += "\r\n";
            if (InvokeRequired) BeginInvoke(new Action(delegate { rtb_Log.AppendText(text); }));
            else rtb_Log.AppendText(text);
        }

        private void AddListBoxItem(string Item, ListBox lib)
        {
            if (InvokeRequired) Invoke(new Action(delegate { lib.Items.Add(Item); lib.SelectedIndex = lib.Items.Count - 1; }));
            else { lib.Items.Add(Item); lib.SelectedIndex = lib.Items.Count - 1; }
        }

        private void AddListBoxItem(string[] Item, ListBox lib)
        {
            if (InvokeRequired) Invoke(new Action(delegate { lib.Items.AddRange(Item); }));
            else lib.Items.AddRange(Item);
        }

        private void DeleteListBoxItem(string Item, ListBox lib)
        {
            if (InvokeRequired) BeginInvoke(new Action(delegate { lib.Items.Remove(Item); }));
            else { lib.Items.Remove(Item); }
        }

        private void ClearListBoxItem(ListBox lib)
        {
            if (InvokeRequired) BeginInvoke(new Action(delegate { lib.Items.Clear(); }));
            else lib.Items.Clear();
        }

        private void ClearTextBoxText()
        {
            if (InvokeRequired) BeginInvoke(new Action(delegate { rtb_Log.Clear(); }));
            else rtb_Log.Clear();
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(Application.StartupPath + "\\Index")) Directory.CreateDirectory(Application.StartupPath + "\\Index");
            if (!Directory.Exists(Application.StartupPath + "\\ErrorIndex")) Directory.CreateDirectory(Application.StartupPath + "\\ErrorIndex");
            txt_Save.Text = Properties.Settings.Default.SavePath;
            WCIndex.DownloadDataCompleted += new DownloadDataCompletedEventHandler((sender, e) =>
            {
                if (e.Error == null)
                {
                    AddTextBoxText("完成!");
                    BundleFile BF = new BundleFile(new MemoryStream(e.Result));
                    AssetsFile AF = new AssetsFile("_Version_a_Card_txt", new EndianStream(BF.MemoryAssetsFileList[0].memStream, EndianType.BigEndian));
                    TextAsset TA = new TextAsset(AF.preloadTable.Values.First((AssetPreloadData a) => (a.Type2 == 49)), true);
                    IndexText = System.Text.Encoding.UTF8.GetString(TA.m_Script);
                    BF = null;
                    AF = null;
                    TA = null;
                }
                else { AddTextBoxText("失敗\r\n(" + e.Error.Message + ")"); Error = true; }
                evtDownload.Set();
            });
            WCPic.DownloadDataCompleted += new DownloadDataCompletedEventHandler((sender, e) =>
            {
                if (e.Error == null) { AddTextBoxText("完成!"); resultData = e.Result; }
                else { AddTextBoxText("失敗\r\n(" + e.Error.Message + ")"); Error = true; }
                evtDownload.Set();
            });
            WCIndex.DownloadProgressChanged += DownloadProgressChanged;
            WCPic.DownloadProgressChanged += DownloadProgressChanged;
            if (Properties.Settings.Default.FirstUse)
            {
                Properties.Settings.Default.FirstUse = false;
                Properties.Settings.Default.Save();
                MessageBox.Show(
                    "初次使用提示：\r\n" +
                    "   先選擇好儲存的目錄\r\n" +
                    "   然後再個別從各伺服器抓取檔案，以便建置好舊檔案清單\r\n" +
                    "抓取檔案方法：\r\n" +
                    "   選擇要抓取的檔案伺服器\r\n" +
                    "   然後執行\"下載Index\"\r\n" +
                    "   再來執行\"下載圖檔\"\r\n" +
                    "(檔案儲存目錄：選擇的目錄 + 伺服器版本)"
                    );
                btn_Save_Click(this, null);
                SetForegroundWindow(Handle);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CancelClose;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_Save.Text != "" && Directory.Exists(txt_Save.Text)) folderBrowserDialog1.SelectedPath = txt_Save.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_Save.Text = folderBrowserDialog1.SelectedPath;
                if (!txt_Save.Text.EndsWith("\\")) txt_Save.Text += "\\";
                Properties.Settings.Default.SavePath = txt_Save.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btn_DLIndex_Click(object sender, EventArgs e)
        {
            if (txt_Save.Text == "" || !Directory.Exists(txt_Save.Text)) return;

            string Url, ServerVer, Index;
            if (rb_TW.Checked) { Url = "http://img.wcproject.so-net.tw/assets/469/a/"; ServerVer = "台"; }
            else if (rb_JP.Checked) { Url = "http://i-wcat-colopl-jp.akamaized.net/assets/465/a/"; ServerVer = "日"; }
            else { Url = "http://i-wcat-colopl-kr.akamaized.net/assets/465/a/"; ServerVer = "韓"; }

            if (rb_Card.Checked) Index = "Card";
            else if (rb_Area.Checked) Index = "Area";
            else if (rb_Item.Checked) Index = "Item";
            else Index = "Event";
            Url += "_Version_a_" + Index + "_txt.unity3d?t=" + DateTime.Now.ToFileTime();
            StopWork = false;
            isUseFromErrorList = false;

            Dictionary<string, string> oldIndex = null;

            new Thread(new ThreadStart(delegate
            {
                WorkSwitch(false);
                Error = false;
                ClearListBoxItem(lib_IndexID);
                IndexData.Clear();

                if (File.Exists(Application.StartupPath + "\\Index\\" + ServerVer + Index + ".json"))
                {
                    AddTextBoxText("已偵測到舊Index清單! ");
                    oldIndex = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Application.StartupPath + "\\Index\\" + ServerVer + Index + ".json"));
                }

                AddTextBoxText(string.Format("下載: ({0})_Version_a_{1}_txt... ", ServerVer, Index), false);
                WCIndex.DownloadDataAsync(new Uri(Url));
                evtDownload.WaitOne();
                if (Error) return;
                
                BeginInvoke(new Action(delegate
                {
                    progressBar1.Value = 0;
                    progressBar1.Maximum = 0;
                    label1.Text = string.Format("{0} KB / {1} KB", 0, 0);
                    label1.Left = (ClientSize.Width - label1.Size.Width) / 2;
                }));

                AddTextBoxText("製作Index的檔案清單中...");
                foreach (string item in IndexText.Split(new char[] { '\n' }))
                {
                    if (item == "") continue;
                    string[] temp = item.Split(new char[] { ',' });
                    if (Index == "Card")
                    {
                        if (temp[0].EndsWith("_png") && !temp[0].StartsWith("Card_0_icon") && !temp[0].StartsWith("Card_1_bust") && !temp[0].StartsWith("Card_3_evol"))
                        {
                            if (!temp[0].EndsWith("1_2_png") && !temp[0].EndsWith("2_2_png")) IndexData.Add(temp[0], temp[1]);
                        }
                    }
                    else if (Index == "Area" || Index == "Item")
                    {
                        if (temp[0].EndsWith("_png")) IndexData.Add(temp[0], temp[1]);
                    }
                    else if (temp[0].EndsWith("_txt")) IndexData.Add(temp[0], temp[1]);
                }

                File.WriteAllText(Application.StartupPath + "\\Index\\" + ServerVer + Index + ".json", JsonConvert.SerializeObject(IndexData)); //序列化

                if (oldIndex != null)
                {
                    AddTextBoxText("比對新舊檔案中...");
                    Invoke(new Action(delegate { lib_IndexID.BeginUpdate(); }));
                    foreach (KeyValuePair<string, string> item in IndexData)
                    {
                        if (!oldIndex.ContainsKey(item.Key)) AddListBoxItem(item.Key, lib_IndexID);
                        else if (oldIndex.ContainsKey(item.Key) && oldIndex.First((x) => (x.Key == item.Key)).Value != item.Value) AddListBoxItem(item.Key, lib_IndexID);
                    }
                    Invoke(new Action(delegate { lib_IndexID.EndUpdate(); }));
                }
                else AddListBoxItem(IndexData.Keys.ToArray(), lib_IndexID);

                AddTextBoxText("完成!");
                WorkSwitch(true);

                if (lib_IndexID.Items.Count == 0) {
                    AddTextBoxText("沒有東西被新增");
                    SetButtonEnable(false, btn_DLFile);
                    SetButtonEnable(false, btn_MakeList);
                }
                else AddTextBoxText("新增了" + lib_IndexID.Items.Count.ToString() + "個檔案");
                GC.Collect();
            })).Start();
        }

        private void btn_UseErrorIndex_Click(object sender, EventArgs e)
        {
            ClearListBoxItem(lib_IndexID);

            string ServerVer, Index;
            if (rb_TW.Checked) ServerVer = "台";
            else if (rb_JP.Checked) ServerVer = "日";
            else ServerVer = "韓"; 

            if (rb_Card.Checked) Index = "Card";
            else if (rb_Area.Checked) Index = "Area";
            else if (rb_Item.Checked) Index = "Item";
            else Index = "Event";
            if (!File.Exists(Application.StartupPath + "\\ErrorIndex\\" + ServerVer + Index + ".json")) return;
            isUseFromErrorList = true;

            lib_IndexID.Items.AddRange(JsonConvert.DeserializeObject<string[]>(File.ReadAllText(Application.StartupPath + "\\ErrorIndex\\" + ServerVer + Index + ".json")));
            WorkSwitch(true);
        }

        private void btn_DLPic_Click(object sender, EventArgs e)
        {
            if (txt_Save.Text == "" || !Directory.Exists(txt_Save.Text) || lib_IndexID.Items.Count == 0) return;

            string Url, ServerVer, Index;
            if (rb_TW.Checked) { Url = "http://img.wcproject.so-net.tw/assets/469/a/"; ServerVer = "台"; }
            else if (rb_JP.Checked) { Url = "http://i-wcat-colopl-jp.akamaized.net/assets/465/a/"; ServerVer = "日"; }
            else { Url = "http://i-wcat-colopl-kr.akamaized.net/assets/465/a/"; ServerVer = "韓"; }

            if (rb_Card.Checked) Index = "Card";
            else if (rb_Area.Checked) Index = "Area";
            else if (rb_Item.Checked) Index = "Item";
            else Index = "Event";
            StopWork = false;

            new Thread(new ThreadStart(delegate
            {
                WorkSwitch(false);

                List<string> ErrorList = new List<string>();
                int i = 0;
                if (!Directory.Exists(txt_Save.Text + ServerVer + Index)) Directory.CreateDirectory(txt_Save.Text + ServerVer + Index);
                foreach (string item in lib_IndexID.Items)
                {
                    i++;
                    string DLUrl = Url + item + ".unity3d?t=" + DateTime.Now.ToFileTime();
                    AddTextBoxText(string.Format("[{0} / {1}]下載: {2}... ", i.ToString(), lib_IndexID.Items.Count, item), false);
                    Error = false;
                    WCPic.DownloadDataAsync(new Uri(DLUrl));
                    evtDownload.WaitOne();
                    if (!Error)
                    {
                        BundleFile BF = new BundleFile(new MemoryStream(resultData));
                        AssetsFile AF = new AssetsFile(item, new EndianStream(BF.MemoryAssetsFileList[0].memStream, EndianType.BigEndian));
                        if (Index != "Event")
                        {
                            string SavePath = txt_Save.Text + ServerVer + Index + "\\" + item + ".png";
                            Bitmap tempBitmap = new Texture2D(AF.preloadTable.Values.First((AssetPreloadData a) => (a.Type2 == 28)), true).ConvertToBitmap(true);
                            if (Index == "Card" && tempBitmap.Width == 1024 && tempBitmap.Height == 1024 && !item.Contains("std")) tempBitmap = ResizeImage(tempBitmap, 1024, 1331);
                            if (File.Exists(SavePath)) File.Delete(SavePath);
                            tempBitmap.Save(SavePath, ImageFormat.Png);
                            tempBitmap.Dispose();
                            tempBitmap = null;
                        }
                        else
                        {
                            string SavePath = txt_Save.Text + ServerVer + "Event\\" + item + ".txt";
                            TextAsset TA = new TextAsset(AF.preloadTable.Values.First((AssetPreloadData a) => (a.Type2 == 49)), true);
                            if (File.Exists(SavePath)) File.Delete(SavePath);
                            File.WriteAllText(SavePath, System.Text.Encoding.UTF8.GetString(TA.m_Script));
                            TA = null;
                        }
                        BF = null;
                        AF = null;
                        AddListBoxItem(item, lib_DownloadID);
                    }
                    else ErrorList.Add(item);
                    if (StopWork) break;
                }
                GC.Collect();

                if (ErrorList.Count >= 1)
                {
                    AddTextBoxText("寫入錯誤檔案清單中...");
                    try
                    {
                        if (!isUseFromErrorList && File.Exists(Application.StartupPath + "\\ErrorIndex\\" + ServerVer + Index + ".json"))
                            foreach (string item in JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Application.StartupPath + "\\ErrorIndex\\" + ServerVer + Index + ".json")))
                                if (!ErrorList.Contains(item)) ErrorList.Add(item);
                        File.WriteAllText(Application.StartupPath + "\\ErrorIndex\\" + ServerVer + Index + ".json", JsonConvert.SerializeObject(ErrorList));
                    }
                    catch (Exception ex) { AddTextBoxText("寫入錯誤\r\n" + ex.Message); }
                }

                AddTextBoxText("全部完成!");
                WorkSwitch(true);
            })).Start();
        }

        private void btn_MakeList_Click(object sender, EventArgs e)
        {
            if (txt_Save.Text != "" && Directory.Exists(txt_Save.Text)) saveFileDialog1.InitialDirectory = txt_Save.Text;

            string Index;
            if (rb_Card.Checked) Index = "Card";
            else if (rb_Area.Checked) Index = "Area";
            else if (rb_Item.Checked) Index = "Item";
            else Index = "Event";

            saveFileDialog1.FileName = Index + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WorkSwitch(false);
                foreach (string item in lib_IndexID.Items)
                {
                    using (StreamWriter SW = File.AppendText(saveFileDialog1.FileName))
                    {
                        AddTextBoxText("寫入: " + item);
                        SW.WriteLine(item);
                    }
                }
                AddTextBoxText("完成!");
                WorkSwitch(true);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            StopWork = true;
            CancelClose = false;
        }
        
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate
            {
                progressBar1.Maximum = (int)(e.TotalBytesToReceive / 1024);
                progressBar1.Value = (int)(e.BytesReceived / 1024);
                label1.Text = string.Format("{0} KB / {1} KB", (e.BytesReceived / 1024).ToString(), (e.TotalBytesToReceive / 1024).ToString());
                label1.Left = (ClientSize.Width - label1.Size.Width) / 2;
            }));
        }

        private void WorkSwitch(bool Switch)
        {
            SetButtonEnable(Switch, btn_Save);
            SetButtonEnable(Switch, btn_DLIndex);
            SetButtonEnable(Switch, btn_UseErrorIndex);
            SetButtonEnable(Switch, btn_DLFile);
            SetButtonEnable(Switch, btn_MakeList);
            SetButtonEnable(!Switch, btn_Stop);
            SetGroupBoxEnable(Switch, GB_SV);
            SetGroupBoxEnable(Switch, GB_Index);
            if (!Switch)
            {               
                ClearListBoxItem(lib_DownloadID);
                ClearTextBoxText();
            }
            CancelClose = !Switch;
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (ImageAttributes imageAttributes = new ImageAttributes())
                {
                    imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
            return bitmap;
        }
    }
}
