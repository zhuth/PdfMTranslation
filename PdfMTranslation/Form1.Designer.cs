namespace PdfMTranslation
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPageNum = new System.Windows.Forms.ToolStripTextBox();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTranslatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTranslatedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.picCursor = new System.Windows.Forms.PictureBox();
            this.pic = new System.Windows.Forms.PictureBox();
            this.txtTranslated = new System.Windows.Forms.TextBox();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toEnglishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toChineseSimpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCursor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.prevToolStripMenuItem,
            this.txtPageNum,
            this.nextToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.copyTranslatedToolStripMenuItem,
            this.txtSearch,
            this.darkToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(726, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // prevToolStripMenuItem
            // 
            this.prevToolStripMenuItem.Name = "prevToolStripMenuItem";
            this.prevToolStripMenuItem.Size = new System.Drawing.Size(45, 23);
            this.prevToolStripMenuItem.Text = "&Prev";
            this.prevToolStripMenuItem.Click += new System.EventHandler(this.prevToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.nextToolStripMenuItem.Text = "&Next";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // txtPageNum
            // 
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(100, 23);
            this.txtPageNum.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPageNum_KeyUp);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // copyTranslatedToolStripMenuItem
            // 
            this.copyTranslatedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyOriginalToolStripMenuItem,
            this.copyTranslatedToolStripMenuItem1});
            this.copyTranslatedToolStripMenuItem.Name = "copyTranslatedToolStripMenuItem";
            this.copyTranslatedToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.copyTranslatedToolStripMenuItem.Text = "&Copy";
            // 
            // copyOriginalToolStripMenuItem
            // 
            this.copyOriginalToolStripMenuItem.Name = "copyOriginalToolStripMenuItem";
            this.copyOriginalToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyOriginalToolStripMenuItem.Text = "Copy &Original";
            this.copyOriginalToolStripMenuItem.Click += new System.EventHandler(this.copyOriginalToolStripMenuItem_Click);
            // 
            // copyTranslatedToolStripMenuItem1
            // 
            this.copyTranslatedToolStripMenuItem1.Name = "copyTranslatedToolStripMenuItem1";
            this.copyTranslatedToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.copyTranslatedToolStripMenuItem1.Text = "Copy &Translated";
            this.copyTranslatedToolStripMenuItem1.Click += new System.EventHandler(this.copyTranslatedToolStripMenuItem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 23);
            this.txtSearch.ToolTipText = "搜索";
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
            this.darkToolStripMenuItem.Text = "&Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtOriginal);
            this.splitContainer1.Panel1.Controls.Add(this.picCursor);
            this.splitContainer1.Panel1.Controls.Add(this.pic);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtTranslated);
            this.splitContainer1.Size = new System.Drawing.Size(726, 373);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 2;
            // 
            // picCursor
            // 
            this.picCursor.Image = global::PdfMTranslation.Properties.Resources.icon_003190_256;
            this.picCursor.Location = new System.Drawing.Point(0, 0);
            this.picCursor.Name = "picCursor";
            this.picCursor.Size = new System.Drawing.Size(32, 32);
            this.picCursor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCursor.TabIndex = 2;
            this.picCursor.TabStop = false;
            this.picCursor.Visible = false;
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(332, 373);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // txtTranslated
            // 
            this.txtTranslated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTranslated.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranslated.Location = new System.Drawing.Point(0, 0);
            this.txtTranslated.Multiline = true;
            this.txtTranslated.Name = "txtTranslated";
            this.txtTranslated.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslated.Size = new System.Drawing.Size(390, 373);
            this.txtTranslated.TabIndex = 3;
            this.txtTranslated.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtTranslated_MouseUp);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toEnglishToolStripMenuItem,
            this.toChineseSimpToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(77, 23);
            this.languageToolStripMenuItem.Text = "&Language";
            // 
            // toEnglishToolStripMenuItem
            // 
            this.toEnglishToolStripMenuItem.Name = "toEnglishToolStripMenuItem";
            this.toEnglishToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.toEnglishToolStripMenuItem.Text = "To English";
            this.toEnglishToolStripMenuItem.Click += new System.EventHandler(this.toEnglishToolStripMenuItem_Click);
            // 
            // toChineseSimpToolStripMenuItem
            // 
            this.toChineseSimpToolStripMenuItem.Name = "toChineseSimpToolStripMenuItem";
            this.toChineseSimpToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.toChineseSimpToolStripMenuItem.Text = "To Chinese (Simp)";
            this.toChineseSimpToolStripMenuItem.Click += new System.EventHandler(this.toChineseSimpToolStripMenuItem_Click);
            // 
            // txtOriginal
            // 
            this.txtOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOriginal.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginal.Location = new System.Drawing.Point(0, 0);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOriginal.Size = new System.Drawing.Size(332, 373);
            this.txtOriginal.TabIndex = 4;
            this.txtOriginal.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 400);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PdfMTranslation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCursor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prevToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.ToolStripTextBox txtPageNum;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.PictureBox picCursor;
        private System.Windows.Forms.ToolStripMenuItem copyTranslatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripMenuItem copyOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTranslatedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.TextBox txtTranslated;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toEnglishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toChineseSimpToolStripMenuItem;
        private System.Windows.Forms.TextBox txtOriginal;
    }
}

