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
using System.Collections;
using System.Text.RegularExpressions;

namespace PZ_39
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
           // this.TopMost = true;

            openFileDialog1.Filter = "Документ Microsfot Word (*.doc)|*.doc|text files (*.txt)|*.txt|All files|*.*";
            saveFileDialog1.Filter = "Документ Microsfot Word (*.doc)|*.doc|text files (*.txt)|*.txt|All files|*.*";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "txt";
        }

        bool change = false;

        private void SetFormTitle()
        {
            FileInfo Fileinfo = new FileInfo(openFileDialog1.FileName);
            this.Text = Fileinfo.Name + " - Text Editor"; 
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            change = true;
        }

        private void OpenFile()
        {
            try
            {
                richTextBox1.Clear();
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            change = false;
        }

        private void SaveFile()
        {
            saveFileDialog1.FileName = string.Empty;
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text, Encoding.Default);
            }
            change = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void NewFile()
        {
            if(change == true)
            {
                SaveFile();
                richTextBox1.Clear();
                change = false;
            }else
            {
                richTextBox1.Clear();
            }
            this.Text = "document";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text.Length > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }


        bool Exiting = false;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Exiting) return;
            Exit();
            Exiting = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Exiting) return;
            if (!Exit()) e.Cancel = true;
            Exiting = false;
        }

        private bool Exit()
        {

            if (change == true)
            {
                DialogResult result = MessageBox.Show("Файл не сохранён",
                    "Сохранить?", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                switch (result)
                {
                    case DialogResult.Cancel:
                        return  false;
                        break;
                    case DialogResult.Yes:
                        SaveFile();
                        Exiting = true;
                        Application.Exit();
                        return true;
                        break;
                    case DialogResult.No:
                        Exiting = true;
                        Application.Exit();
                        return true;
                        break;
                    default: break;
                }
            }

            else Application.Exit();
            return true;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;

            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.DeselectAll();

            MatchCollection coll = Regex.Matches(richTextBox1.Text.ToLower().Trim(), textBox1.Text.ToLower().Trim());

            foreach(Match mathc in coll)
            {
                richTextBox1.Select(mathc.Index, mathc.Length);
                richTextBox1.SelectionColor = Color.Red;
            }
            
        }
    }
}
