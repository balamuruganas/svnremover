using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SVN_Remover
{
	public partial class Form1 : Form
	{
		int removedNum;
	
		public Form1()
		{
			InitializeComponent();
		}

		private void selectButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folder = new FolderBrowserDialog();
			if(folder.ShowDialog() == DialogResult.OK)
			{
				dirTextBox.Text = folder.SelectedPath;
			}
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			if(dirTextBox.Text != null && dirTextBox.Text.Length > 0)
			{
				try
				{
					removedNum = 0;
					FindSVNFolders(dirTextBox.Text);
					removalText.Text = removedNum.ToString();
				}
				catch(Exception ex) {MessageBox.Show(ex.Message);}
			}
		}

        void FindSVNFolders(string rootDir)
		{
            string[] directories = Directory.GetDirectories(rootDir);
            foreach (string dir in directories)
			{
                DirectoryInfo di = new DirectoryInfo(dir);
				if(di.Name == ".svn")
				{
                    DeleteFolderTree(dir);
                    Directory.Delete(dir);
				    removedNum ++;
				}
                else FindSVNFolders(dir);
			}
		}

        void DeleteFolderTree(string rootDir)
		{
            string[] files = Directory.GetFiles(rootDir);
			foreach(string file in files)
			{
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
			}

            string[] directories = Directory.GetDirectories(rootDir);
            foreach (string loop in directories)
			{
				DeleteFolderTree(loop);
				Directory.Delete(loop);
			}
		}
	}
}
