using FriendListCleanser.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FriendListCleanser
{
	public class Credit : MaterialForm
	{
		private readonly MaterialSkinManager materialSkinManager;

		private IContainer components = null;

		private PictureBox pictureBox1;

		private PictureBox pictureBox2;

		private PictureBox pictureBox3;

		public Credit()
		{
			this.InitializeComponent();
			this.pictureBox2.Click += new EventHandler(this.DonateOnClick);
			this.materialSkinManager = MaterialSkinManager.Instance;
			this.materialSkinManager.AddFormToManage(this);
			this.materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
			this.materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void DonateOnClick(object sender, EventArgs args)
		{
			Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=K4DTAWBJ622VL");
			base.TopMost = false;
		}

		private void InitializeComponent()
		{
			this.pictureBox3 = new PictureBox();
			this.pictureBox2 = new PictureBox();
			this.pictureBox1 = new PictureBox();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox3.BackColor = Color.Transparent;
			this.pictureBox3.Image = Resources.Soft_White_Text_Effect;
			this.pictureBox3.Location = new Point(41, 32);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(409, 178);
			this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 2;
			this.pictureBox3.TabStop = false;
			this.pictureBox2.BackColor = Color.Transparent;
			this.pictureBox2.Cursor = Cursors.Hand;
			this.pictureBox2.Image = Resources.Paypal_button1;
			this.pictureBox2.Location = new Point(367, 233);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(118, 64);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
			this.pictureBox1.BackColor = Color.Transparent;
			this.pictureBox1.BackgroundImageLayout = ImageLayout.None;
			this.pictureBox1.Image = Resources.logo_v2;
			this.pictureBox1.Location = new Point(12, 233);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(280, 64);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(497, 309);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.pictureBox3);
			base.Controls.Add(this.pictureBox2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Credit";
			base.ShowInTaskbar = false;
			base.Sizable = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.TopMost = true;
			((ISupportInitialize)this.pictureBox3).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}