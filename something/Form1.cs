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

namespace something
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string stpa;
            StreamReader sr = new StreamReader("save.txt");
            stpa = sr.ReadLine();
            sr.Close();
            FileSystemWatcher watcher = new FileSystemWatcher(stpa);
            DirectoryInfo dir = new DirectoryInfo(stpa);
            foreach (var item in dir.GetDirectories())
            {
                TreeNode tn = new TreeNode { Text = item.Name, Name = item.FullName };
                treeView1.Nodes.Add(tn);
            }
            foreach (var item in dir.GetFiles())
            {
                listView1.Items.Add(item.Name);
            }
            fileSystemWatcher1.Path = dir.ToString();
            textBox1.Text = dir.ToString();
            treeView1.ContextMenuStrip = contextMenuStrip1;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (button2.Enabled == false)
            {

                try
                {
                    DirectoryInfo dir = new DirectoryInfo(e.Node.Name);
                    
                    dir.GetDirectories();
                    FileSystemWatcher watcher = new FileSystemWatcher(e.Node.Name);
                    treeView1.Nodes.Clear();
                    foreach (var item in dir.GetDirectories())
                    {
                        TreeNode tn = new TreeNode { Text = item.Name, Name = item.FullName };
                        treeView1.Nodes.Add(tn);
                    }
                    listView1.Items.Clear();
                    foreach (var item in dir.GetFiles())
                    {
                        listView1.Items.Add(item.Name);
                    }
                    fileSystemWatcher1.Path = dir.ToString();
                    textBox1.Text = dir.ToString();

                }
                catch { };
                button2.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == true)
            {
                string path = textBox1.Text;
                int i = path.Length - 1;
                while (path[i] != '\\')
                {
                    path = path.Remove(i);
                    i--;
                }
                if (path != @"C:\") path = path.Remove(i);
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemWatcher watcher = new FileSystemWatcher(path);
                dir.GetDirectories();
                treeView1.Nodes.Clear();
                foreach (var item in dir.GetDirectories())
                {
                    TreeNode tn = new TreeNode { Text = item.Name, Name = item.FullName };
                    treeView1.Nodes.Add(tn);
                }
                listView1.Items.Clear();
                foreach (var item in dir.GetFiles())
                {
                    listView1.Items.Add(item.Name);
                }
                fileSystemWatcher1.Path = dir.ToString();
                textBox1.Text = dir.ToString();
            }
            else button2.Enabled = true;
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            notifyIcon1.Icon = SystemIcons.WinLogo;
            notifyIcon1.BalloonTipText = e.FullPath;
            notifyIcon1.BalloonTipTitle = e.ChangeType.ToString();
            notifyIcon1.ShowBalloonTip(3);

            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            FileSystemWatcher watcher = new FileSystemWatcher(textBox1.Text);
            dir.GetDirectories();
            treeView1.Nodes.Clear();
            foreach (var item in dir.GetDirectories())
            {
                TreeNode tn = new TreeNode { Text = item.Name, Name = item.FullName };
                treeView1.Nodes.Add(tn);
            }
            listView1.Items.Clear();
            foreach (var item in dir.GetFiles())
            {
                listView1.Items.Add(item.Name);
            }
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            notifyIcon1.Icon = SystemIcons.WinLogo;
            notifyIcon1.BalloonTipText = e.FullPath;
            notifyIcon1.BalloonTipTitle = e.ChangeType.ToString();
            notifyIcon1.ShowBalloonTip(3);

            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            FileSystemWatcher watcher = new FileSystemWatcher(textBox1.Text);
            dir.GetDirectories();
            treeView1.Nodes.Clear();
            foreach (var item in dir.GetDirectories())
            {
                TreeNode tn = new TreeNode { Text = item.Name, Name = item.FullName };
                treeView1.Nodes.Add(tn);
            }
            listView1.Items.Clear();
            foreach (var item in dir.GetFiles())
            {
                listView1.Items.Add(item.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = null;
            button2.Enabled = false;

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
                Directory.Delete(treeView1.SelectedNode.Name, true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter save = new StreamWriter("save.txt");
            save.WriteLine(textBox1.Text);
            save.Close();
        }
    }
}

