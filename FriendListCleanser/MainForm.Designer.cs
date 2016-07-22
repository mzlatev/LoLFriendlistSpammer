using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendListCleanser
{
	public class MainForm : MaterialForm
	{
		private readonly MaterialSkinManager materialSkinManager;

		private readonly List<Account> Accounts = new List<Account>();

		private readonly List<Account> DeletedAccounts = new List<Account>();

		private bool Deleting = false;

		private IContainer components = null;

		private ProgressBar progressBar1;

		private MaterialDivider materialDivider1;

		private MaterialLabel label1;

		private MaterialFlatButton button1;

		private MaterialFlatButton button2;

		private ComboBox comboBox1;

		private OpenFileDialog openFileDialog1;

		private DataGridView dataGridView1;

		private GroupBox groupBox1;

		public MainForm()
		{
			this.InitializeComponent();
			string[] strArrays = new string[] { "BR", "EUN", "EUW", "NA", "KR", "OCE", "RU", "TR", "LAN", "LAS" };
			this.comboBox1.Items.AddRange(strArrays);
			this.button2.Enabled = false;
			this.dataGridView1.ColumnCount = 3;
			this.dataGridView1.Columns[0].Name = "Account";
			this.dataGridView1.Columns[1].Name = "Friends";
			this.dataGridView1.Columns[2].Name = "Status";
			this.button1.Click += new EventHandler(this.button1OnClick);
			this.button2.Click += new EventHandler(this.button2OnClick);
			this.materialSkinManager = MaterialSkinManager.Instance;
			this.materialSkinManager.AddFormToManage(this);
			this.materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			this.materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
		}

		private void button1OnClick(object sender, EventArgs args)
		{
			if (this.comboBox1.SelectedItem != null)
			{
				this.Accounts.Clear();
				this.DeletedAccounts.Clear();
				this.dataGridView1.Rows.Clear();
				OpenFileDialog openFileDialog = new OpenFileDialog()
				{
					Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
				};
				if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string fileName = openFileDialog.FileName;
					if (File.Exists(fileName))
					{
						StreamReader streamReader = new StreamReader(fileName);
						while (true)
						{
							string str = streamReader.ReadLine();
							string str1 = str;
							if (str == null)
							{
								break;
							}
							string[] strArrays = str1.Split(new char[] { ':' });
							if ((strArrays.Count<string>() < 2 ? false : !this.Accounts.Any<Account>((Account h) => h.User == strArrays[0])))
							{
								Account account = new Account(strArrays[0], strArrays[1], 0, "Not Checked");
								this.Accounts.Add(account);
							}
						}
						this.button1.Enabled = false;
						this.button2.Enabled = true;
						this.comboBox1.Enabled = false;
						this.UpdateForms();
					}
				}
			}
			else
			{
				this.label1.Text = "Select Server first";
			}
		}

		private void button2OnClick(object sender, EventArgs args)
		{
			this.progressBar1.Value = 1;
			this.DeleteAccountHander();
		}

		private async Task<Account> DeleteAccount(Account account)
		{
			ChatClient chatClient = new ChatClient(account.User, account.Pass, this.GetServer());
			await chatClient.IsCompleted.Task;
			if (chatClient.Status != "Failed Login")
			{
				await chatClient.DeleteFriends();
			}
			chatClient.Disconect();
			Account account1 = new Account(account.User, account.Pass, chatClient.FriendCount, chatClient.Status);
			return account1;
		}

		private async void DeleteAccountHander()
		{
			this.Deleting = true;
			this.button2.Enabled = false;
			while (true)
			{
				List<Account> accounts = this.Accounts;
				if (!accounts.Any<Account>((Account h) => h.Status == "Not Checked"))
				{
					break;
				}
				this.progressBar1.Minimum = 1;
				this.progressBar1.Maximum = this.Accounts.Count + 1;
				List<Account> accounts1 = this.Accounts;
				Account account = accounts1.FirstOrDefault<Account>((Account h) => h.Status == "Not Checked");
				if (account != null)
				{
					int num = this.Accounts.FindIndex((Account h) => h.User == account.User);
					List<Account> accounts2 = this.Accounts;
					accounts2[num] = await this.DeleteAccount(account);
					this.DeletedAccounts.Add(account);
					this.UpdateForms();
					this.progressBar1.PerformStep();
				}
			}
			this.Deleting = false;
			this.button1.Enabled = true;
			this.comboBox1.Enabled = true;
			List<Account> accounts3 = this.Accounts;
			int num1 = accounts3.Sum<Account>((Account h) => h.FriendCount);
			MessageBox.Show(string.Concat("You Deleted ", num1, " Friends"));
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private string GetServer()
		{
			string str;
			if (this.comboBox1.SelectedItem != null)
			{
				string str1 = this.comboBox1.SelectedItem.ToString();
				if (str1 != null)
				{
					switch (str1)
					{
						case "BR":
						{
							str = "br";
							return str;
						}
						case "EUN":
						{
							str = "eun1";
							return str;
						}
						case "EUW":
						{
							str = "euw1";
							return str;
						}
						case "NA":
						{
							str = "na2";
							return str;
						}
						case "KR":
						{
							str = "kr";
							return str;
						}
						case "OCE":
						{
							str = "oc1";
							return str;
						}
						case "RU":
						{
							str = "ru";
							return str;
						}
						case "TR":
						{
							str = "tr";
							return str;
						}
						case "LAN":
						{
							str = "la1";
							return str;
						}
						case "LAS":
						{
							str = "la2";
							return str;
						}
					}
				}
				str = "";
			}
			else
			{
				str = "";
			}
			return str;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.progressBar1 = new ProgressBar();
			this.materialDivider1 = new MaterialDivider();
			this.label1 = new MaterialLabel();
			this.button1 = new MaterialFlatButton();
			this.button2 = new MaterialFlatButton();
			this.comboBox1 = new ComboBox();
			this.openFileDialog1 = new OpenFileDialog();
			this.dataGridView1 = new DataGridView();
			this.groupBox1 = new GroupBox();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.progressBar1.Location = new Point(0, 64);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(653, 10);
			this.progressBar1.Step = 1;
			this.progressBar1.Style = ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 1;
			this.materialDivider1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.materialDivider1.BackColor = Color.FromArgb(31, 0, 0, 0);
			this.materialDivider1.Depth = 0;
			this.materialDivider1.Location = new Point(1, 277);
			this.materialDivider1.Margin = new System.Windows.Forms.Padding(0);
			this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
			this.materialDivider1.Name = "materialDivider1";
			this.materialDivider1.Size = new System.Drawing.Size(651, 1);
			this.materialDivider1.TabIndex = 17;
			this.materialDivider1.Text = "materialDivider1";
			this.label1.AutoSize = true;
			this.label1.Depth = 0;
			this.label1.Font = new System.Drawing.Font("Roboto", 11f);
			this.label1.ForeColor = Color.FromArgb(222, 0, 0, 0);
			this.label1.Location = new Point(14, 292);
			this.label1.MouseState = MaterialSkin.MouseState.HOVER;
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 19);
			this.label1.TabIndex = 18;
			this.label1.Text = "Status";
			this.button1.AutoSize = true;
			this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button1.Depth = 0;
			this.button1.Location = new Point(575, 282);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.button1.MouseState = MaterialSkin.MouseState.HOVER;
			this.button1.Name = "button1";
			this.button1.Primary = false;
			this.button1.Size = new System.Drawing.Size(67, 36);
			this.button1.TabIndex = 20;
			this.button1.Text = "Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button2.AutoSize = true;
			this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button2.Depth = 0;
			this.button2.Location = new Point(507, 282);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.button2.MouseState = MaterialSkin.MouseState.HOVER;
			this.button2.Name = "button2";
			this.button2.Primary = false;
			this.button2.Size = new System.Drawing.Size(60, 36);
			this.button2.TabIndex = 21;
			this.button2.Text = "Delete";
			this.button2.UseVisualStyleBackColor = true;
			this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox1.FlatStyle = FlatStyle.System;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.ItemHeight = 13;
			this.comboBox1.Location = new Point(427, 290);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(73, 21);
			this.comboBox1.TabIndex = 22;
			this.openFileDialog1.FileName = "openFileDialog1";
			this.dataGridView1.BackgroundColor = SystemColors.HighlightText;
			this.dataGridView1.BorderStyle = BorderStyle.None;
			this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.GridColor = SystemColors.Control;
			this.dataGridView1.Location = new Point(6, 19);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(617, 163);
			this.dataGridView1.TabIndex = 0;
			this.groupBox1.Controls.Add(this.dataGridView1);
			this.groupBox1.Location = new Point(12, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(629, 188);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Accounts";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(653, 323);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.materialDivider1);
			base.Controls.Add(this.progressBar1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "MainForm";
			base.Sizable = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "FriendList Cleanser";
			base.FormClosed += new FormClosedEventHandler(this.MainForm_FormClosed);
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			base.Hide();
			Credit credit = new Credit();
			credit.ShowDialog();
			credit.Close();
		}

		private void UpdateForms()
		{
			int num = this.Accounts.Sum<Account>((Account h) => h.FriendCount);
			MaterialLabel materialLabel = this.label1;
			object[] count = new object[] { "Deleted: ", this.DeletedAccounts.Count, "/", this.Accounts.Count, "(", num, ")" };
			materialLabel.Text = string.Concat(count);
			foreach (Account account in this.Accounts)
			{
				bool flag = false;
				foreach (DataGridViewRow row in (IEnumerable)this.dataGridView1.Rows)
				{
					if (row.Cells[0].Value == account.User)
					{
						flag = true;
						if (row.Cells[2].Value != account.Status)
						{
							row.Cells[2].Value = account.Status;
						}
						if (row.Cells[1].Value != account.FriendCount.ToString())
						{
							row.Cells[1].Value = account.FriendCount;
						}
					}
				}
				if (!flag)
				{
					string[] user = new string[] { account.User, null, null };
					user[1] = account.FriendCount.ToString();
					user[2] = account.Status;
					this.dataGridView1.Rows.Add(user);
				}
			}
		}
	}
}