using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        public static string OPFpth, OPTpth, DEVpth, DEEPpth;

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //LibOPF button
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //folderBrowserDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OPFpth = folderBrowserDialog1.SelectedPath;
                textBox1.Text = OPFpth;
            }
        }

        private void button2_Click(object sender, EventArgs e) //LibOPT
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
            //folderBrowserDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (folderBrowserDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OPTpth = folderBrowserDialog2.SelectedPath;
                textBox2.Text = OPTpth;
            }
        }

        private void button3_Click(object sender, EventArgs e) //LibDev
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            FolderBrowserDialog folderBrowserDialog3 = new FolderBrowserDialog();
            //folderBrowserDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (folderBrowserDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DEVpth = folderBrowserDialog3.SelectedPath;
                textBox3.Text = DEVpth;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            FolderBrowserDialog folderBrowserDialog4 = new FolderBrowserDialog();
            //folderBrowserDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (folderBrowserDialog4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DEEPpth = folderBrowserDialog4.SelectedPath;
                textBox4.Text = DEEPpth;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Selecione um caminho para LibOPF", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Selecione um caminho para LibOPT", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                }
                else
                {
                    if (textBox3.Text == "")
                    {
                        MessageBox.Show("Selecione um caminho para LibDEV", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Focus();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }

        }
    }
}
