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
    public partial class Form4 : Form
    {
        public static string pth;

        public Form4()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //folderBrowserDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pth = folderBrowserDialog1.SelectedPath;
                textBox1.Text = pth;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //Form winMain = new Form1();
            this.Close();
            //winMain.Show();
        }
    }
}
