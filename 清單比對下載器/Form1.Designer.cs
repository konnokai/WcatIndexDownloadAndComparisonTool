namespace 清單比對下載器
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txt_Save = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.GB_SV = new System.Windows.Forms.GroupBox();
            this.rb_KR = new System.Windows.Forms.RadioButton();
            this.rb_JP = new System.Windows.Forms.RadioButton();
            this.rb_TW = new System.Windows.Forms.RadioButton();
            this.btn_DLIndex = new System.Windows.Forms.Button();
            this.lib_IndexID = new System.Windows.Forms.ListBox();
            this.lib_DownloadID = new System.Windows.Forms.ListBox();
            this.rtb_Log = new System.Windows.Forms.RichTextBox();
            this.btn_DLFile = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_MakeList = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.GB_Index = new System.Windows.Forms.GroupBox();
            this.rb_Item = new System.Windows.Forms.RadioButton();
            this.rb_Event = new System.Windows.Forms.RadioButton();
            this.rb_Area = new System.Windows.Forms.RadioButton();
            this.rb_Card = new System.Windows.Forms.RadioButton();
            this.btn_UseErrorIndex = new System.Windows.Forms.Button();
            this.GB_SV.SuspendLayout();
            this.GB_Index.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Save
            // 
            this.txt_Save.Location = new System.Drawing.Point(12, 12);
            this.txt_Save.Name = "txt_Save";
            this.txt_Save.ReadOnly = true;
            this.txt_Save.Size = new System.Drawing.Size(775, 22);
            this.txt_Save.TabIndex = 0;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(793, 11);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(24, 23);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "...";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // GB_SV
            // 
            this.GB_SV.Controls.Add(this.rb_KR);
            this.GB_SV.Controls.Add(this.rb_JP);
            this.GB_SV.Controls.Add(this.rb_TW);
            this.GB_SV.Location = new System.Drawing.Point(362, 42);
            this.GB_SV.Name = "GB_SV";
            this.GB_SV.Size = new System.Drawing.Size(106, 60);
            this.GB_SV.TabIndex = 2;
            this.GB_SV.TabStop = false;
            this.GB_SV.Text = "伺服器";
            // 
            // rb_KR
            // 
            this.rb_KR.AutoSize = true;
            this.rb_KR.Location = new System.Drawing.Point(5, 43);
            this.rb_KR.Name = "rb_KR";
            this.rb_KR.Size = new System.Drawing.Size(47, 16);
            this.rb_KR.TabIndex = 2;
            this.rb_KR.Text = "韓版";
            this.rb_KR.UseVisualStyleBackColor = true;
            // 
            // rb_JP
            // 
            this.rb_JP.AutoSize = true;
            this.rb_JP.Location = new System.Drawing.Point(58, 21);
            this.rb_JP.Name = "rb_JP";
            this.rb_JP.Size = new System.Drawing.Size(47, 16);
            this.rb_JP.TabIndex = 1;
            this.rb_JP.Text = "日版";
            this.rb_JP.UseVisualStyleBackColor = true;
            // 
            // rb_TW
            // 
            this.rb_TW.AutoSize = true;
            this.rb_TW.Checked = true;
            this.rb_TW.Location = new System.Drawing.Point(5, 21);
            this.rb_TW.Name = "rb_TW";
            this.rb_TW.Size = new System.Drawing.Size(47, 16);
            this.rb_TW.TabIndex = 0;
            this.rb_TW.TabStop = true;
            this.rb_TW.Text = "台版";
            this.rb_TW.UseVisualStyleBackColor = true;
            // 
            // btn_DLIndex
            // 
            this.btn_DLIndex.Location = new System.Drawing.Point(362, 172);
            this.btn_DLIndex.Name = "btn_DLIndex";
            this.btn_DLIndex.Size = new System.Drawing.Size(103, 23);
            this.btn_DLIndex.TabIndex = 3;
            this.btn_DLIndex.Text = "下載Index";
            this.btn_DLIndex.UseVisualStyleBackColor = true;
            this.btn_DLIndex.Click += new System.EventHandler(this.btn_DLIndex_Click);
            // 
            // lib_IndexID
            // 
            this.lib_IndexID.FormattingEnabled = true;
            this.lib_IndexID.HorizontalScrollbar = true;
            this.lib_IndexID.ItemHeight = 12;
            this.lib_IndexID.Location = new System.Drawing.Point(12, 42);
            this.lib_IndexID.Name = "lib_IndexID";
            this.lib_IndexID.Size = new System.Drawing.Size(344, 268);
            this.lib_IndexID.TabIndex = 4;
            // 
            // lib_DownloadID
            // 
            this.lib_DownloadID.FormattingEnabled = true;
            this.lib_DownloadID.HorizontalScrollbar = true;
            this.lib_DownloadID.ItemHeight = 12;
            this.lib_DownloadID.Location = new System.Drawing.Point(473, 42);
            this.lib_DownloadID.Name = "lib_DownloadID";
            this.lib_DownloadID.Size = new System.Drawing.Size(344, 268);
            this.lib_DownloadID.TabIndex = 5;
            // 
            // rtb_Log
            // 
            this.rtb_Log.DetectUrls = false;
            this.rtb_Log.Location = new System.Drawing.Point(12, 316);
            this.rtb_Log.Name = "rtb_Log";
            this.rtb_Log.ReadOnly = true;
            this.rtb_Log.Size = new System.Drawing.Size(805, 221);
            this.rtb_Log.TabIndex = 6;
            this.rtb_Log.Text = "";
            this.rtb_Log.WordWrap = false;
            // 
            // btn_DLFile
            // 
            this.btn_DLFile.Enabled = false;
            this.btn_DLFile.Location = new System.Drawing.Point(362, 229);
            this.btn_DLFile.Name = "btn_DLFile";
            this.btn_DLFile.Size = new System.Drawing.Size(103, 23);
            this.btn_DLFile.TabIndex = 7;
            this.btn_DLFile.Text = "下載檔案";
            this.btn_DLFile.UseVisualStyleBackColor = true;
            this.btn_DLFile.Click += new System.EventHandler(this.btn_DLPic_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Enabled = false;
            this.btn_Stop.Location = new System.Drawing.Point(362, 287);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(103, 23);
            this.btn_Stop.TabIndex = 8;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_MakeList
            // 
            this.btn_MakeList.Enabled = false;
            this.btn_MakeList.Location = new System.Drawing.Point(362, 258);
            this.btn_MakeList.Name = "btn_MakeList";
            this.btn_MakeList.Size = new System.Drawing.Size(103, 23);
            this.btn_MakeList.TabIndex = 9;
            this.btn_MakeList.Text = "製作清單";
            this.btn_MakeList.UseVisualStyleBackColor = true;
            this.btn_MakeList.Click += new System.EventHandler(this.btn_MakeList_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "文字文件|*.txt";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 543);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(805, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 548);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "0 KB / 0 KB";
            // 
            // GB_Index
            // 
            this.GB_Index.Controls.Add(this.rb_Item);
            this.GB_Index.Controls.Add(this.rb_Event);
            this.GB_Index.Controls.Add(this.rb_Area);
            this.GB_Index.Controls.Add(this.rb_Card);
            this.GB_Index.Location = new System.Drawing.Point(362, 107);
            this.GB_Index.Name = "GB_Index";
            this.GB_Index.Size = new System.Drawing.Size(106, 60);
            this.GB_Index.TabIndex = 3;
            this.GB_Index.TabStop = false;
            this.GB_Index.Text = "Index";
            // 
            // rb_Item
            // 
            this.rb_Item.AutoSize = true;
            this.rb_Item.Location = new System.Drawing.Point(59, 43);
            this.rb_Item.Name = "rb_Item";
            this.rb_Item.Size = new System.Drawing.Size(44, 16);
            this.rb_Item.TabIndex = 3;
            this.rb_Item.Text = "Item";
            this.rb_Item.UseVisualStyleBackColor = true;
            // 
            // rb_Event
            // 
            this.rb_Event.AutoSize = true;
            this.rb_Event.Location = new System.Drawing.Point(6, 43);
            this.rb_Event.Name = "rb_Event";
            this.rb_Event.Size = new System.Drawing.Size(50, 16);
            this.rb_Event.TabIndex = 2;
            this.rb_Event.Text = "Event";
            this.rb_Event.UseVisualStyleBackColor = true;
            // 
            // rb_Area
            // 
            this.rb_Area.AutoSize = true;
            this.rb_Area.Location = new System.Drawing.Point(59, 21);
            this.rb_Area.Name = "rb_Area";
            this.rb_Area.Size = new System.Drawing.Size(45, 16);
            this.rb_Area.TabIndex = 1;
            this.rb_Area.Text = "Area";
            this.rb_Area.UseVisualStyleBackColor = true;
            // 
            // rb_Card
            // 
            this.rb_Card.AutoSize = true;
            this.rb_Card.Checked = true;
            this.rb_Card.Location = new System.Drawing.Point(6, 21);
            this.rb_Card.Name = "rb_Card";
            this.rb_Card.Size = new System.Drawing.Size(46, 16);
            this.rb_Card.TabIndex = 0;
            this.rb_Card.TabStop = true;
            this.rb_Card.Text = "Card";
            this.rb_Card.UseVisualStyleBackColor = true;
            // 
            // btn_UseErrorIndex
            // 
            this.btn_UseErrorIndex.Location = new System.Drawing.Point(362, 200);
            this.btn_UseErrorIndex.Name = "btn_UseErrorIndex";
            this.btn_UseErrorIndex.Size = new System.Drawing.Size(103, 23);
            this.btn_UseErrorIndex.TabIndex = 12;
            this.btn_UseErrorIndex.Text = "使用錯誤清單";
            this.btn_UseErrorIndex.UseVisualStyleBackColor = true;
            this.btn_UseErrorIndex.Click += new System.EventHandler(this.btn_UseErrorIndex_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 572);
            this.Controls.Add(this.btn_UseErrorIndex);
            this.Controls.Add(this.GB_Index);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_MakeList);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_DLFile);
            this.Controls.Add(this.rtb_Log);
            this.Controls.Add(this.lib_DownloadID);
            this.Controls.Add(this.lib_IndexID);
            this.Controls.Add(this.btn_DLIndex);
            this.Controls.Add(this.GB_SV);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "清單比對下載器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.GB_SV.ResumeLayout(false);
            this.GB_SV.PerformLayout();
            this.GB_Index.ResumeLayout(false);
            this.GB_Index.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txt_Save;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox GB_SV;
        private System.Windows.Forms.RadioButton rb_KR;
        private System.Windows.Forms.RadioButton rb_JP;
        private System.Windows.Forms.RadioButton rb_TW;
        private System.Windows.Forms.Button btn_DLIndex;
        private System.Windows.Forms.ListBox lib_IndexID;
        private System.Windows.Forms.ListBox lib_DownloadID;
        private System.Windows.Forms.RichTextBox rtb_Log;
        private System.Windows.Forms.Button btn_DLFile;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_MakeList;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_Event;
        private System.Windows.Forms.RadioButton rb_Area;
        private System.Windows.Forms.RadioButton rb_Card;
        private System.Windows.Forms.GroupBox GB_Index;
        private System.Windows.Forms.RadioButton rb_Item;
        private System.Windows.Forms.Button btn_UseErrorIndex;
    }
}

