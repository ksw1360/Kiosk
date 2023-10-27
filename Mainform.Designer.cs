
namespace Kiosk
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnPrePtnt = new System.Windows.Forms.Button();
            this.btnNewPtnt = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbFirst = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSecond = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrePtnt
            // 
            this.btnPrePtnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnPrePtnt.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.btnPrePtnt.ForeColor = System.Drawing.Color.White;
            this.btnPrePtnt.Image = global::Kiosk.Properties.Resources.내원한적있음_버튼;
            this.btnPrePtnt.Location = new System.Drawing.Point(118, 596);
            this.btnPrePtnt.Name = "btnPrePtnt";
            this.btnPrePtnt.Size = new System.Drawing.Size(819, 250);
            this.btnPrePtnt.TabIndex = 12;
            this.btnPrePtnt.UseVisualStyleBackColor = false;
            this.btnPrePtnt.Click += new System.EventHandler(this.btnPrePtnt_Click);
            // 
            // btnNewPtnt
            // 
            this.btnNewPtnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnNewPtnt.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.btnNewPtnt.ForeColor = System.Drawing.Color.White;
            this.btnNewPtnt.Image = global::Kiosk.Properties.Resources.처음내원_버튼;
            this.btnNewPtnt.Location = new System.Drawing.Point(118, 276);
            this.btnNewPtnt.Name = "btnNewPtnt";
            this.btnNewPtnt.Size = new System.Drawing.Size(819, 250);
            this.btnNewPtnt.TabIndex = 11;
            this.btnNewPtnt.UseVisualStyleBackColor = false;
            this.btnNewPtnt.Click += new System.EventHandler(this.btnNewPtnt_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 531);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 40);
            this.button2.TabIndex = 13;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.btnPreview);
            this.panel4.Controls.Add(this.btnNext);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel4.Location = new System.Drawing.Point(0, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1064, 53);
            this.panel4.TabIndex = 21;
            // 
            // btnPreview
            // 
            this.btnPreview.AutoSize = true;
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreview.BackgroundImage")));
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPreview.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPreview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("맑은 고딕", 26F);
            this.btnPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreview.Location = new System.Drawing.Point(0, 0);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(150, 53);
            this.btnPreview.TabIndex = 15;
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click_2);
            // 
            // btnNext
            // 
            this.btnNext.AutoSize = true;
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("맑은 고딕", 30F);
            this.btnNext.ForeColor = System.Drawing.Color.Gray;
            this.btnNext.Location = new System.Drawing.Point(914, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 53);
            this.btnNext.TabIndex = 17;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Visible = false;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1064, 36);
            this.panel7.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel2.Controls.Add(this.lbFirst);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1064, 113);
            this.panel2.TabIndex = 23;
            // 
            // lbFirst
            // 
            this.lbFirst.AutoSize = true;
            this.lbFirst.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbFirst.Font = new System.Drawing.Font("Noto Sans KR Bold", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirst.Location = new System.Drawing.Point(0, 0);
            this.lbFirst.Name = "lbFirst";
            this.lbFirst.Size = new System.Drawing.Size(420, 118);
            this.lbFirst.TabIndex = 0;
            this.lbFirst.Text = "안녕하세요";
            this.lbFirst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.lbSecond);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 63);
            this.panel1.TabIndex = 22;
            // 
            // lbSecond
            // 
            this.lbSecond.AutoSize = true;
            this.lbSecond.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbSecond.Font = new System.Drawing.Font("Noto Sans KR Regular", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSecond.Location = new System.Drawing.Point(0, 0);
            this.lbSecond.Name = "lbSecond";
            this.lbSecond.Size = new System.Drawing.Size(165, 47);
            this.lbSecond.TabIndex = 0;
            this.lbSecond.Text = "안녕하세요";
            this.lbSecond.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1064, 1061);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPrePtnt);
            this.Controls.Add(this.btnNewPtnt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button btnPrePtnt;
        public System.Windows.Forms.Button btnNewPtnt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label lbFirst;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lbSecond;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
    }
}

