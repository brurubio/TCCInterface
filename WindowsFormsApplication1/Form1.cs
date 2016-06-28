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
    public partial class Form1 : Form
    {
        string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // combobox selecionar processo
        {

        }

        private void button1_Click(object sender, EventArgs e) // botão iniciar
        {
            string extension, fileName, strCmdText;
            int ext = -1, Ptrain = 0, Ptest = 0;
            if (textBox1. Text == "")
            {
                MessageBox.Show("Selecione um arquivo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            extension = System.IO.Path.GetExtension(path);
            fileName = System.IO.Path.GetFileName(path);
            //Console.WriteLine(path);
            //Console.WriteLine(fileName);
            //Console.WriteLine(extension);
            if (String.Compare(extension, ".txt", true) == 0)
            {
                ext = 0;
            }
            else
            {
                if (String.Compare(extension, ".dat", true) == 0)
                {
                    ext = 1;
                }
            }
            //Console.WriteLine(ext);
            // 0 - OPF | 1 - OPF + PSO
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um processo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
            }
            Ptrain = Convert.ToInt16(textBox3.Text);
            Ptest = Convert.ToInt16(textBox4.Text);
            //Console.WriteLine(comboBox1.SelectedIndex); 
            //Console.WriteLine(Ptrain);
            //Console.WriteLine(Ptest);
            if ((Ptrain + Ptest) != 100)
            {
                MessageBox.Show("Os valores de treino e teste devem somar 100", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
            }
            if (comboBox1.SelectedIndex == 0)
            {
                strCmdText = "./testTerminal.sh";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText + ext + fileName + Ptrain + Ptest);
            }

        }

        private void button2_Click(object sender, EventArgs e) // botão de ajuda
        {
            Form winHelp = new Form3();
            winHelp.Show();
        }

        private void button3_Click(object sender, EventArgs e) // botão de info
        {
            Form winInfo= new Form2();
            winInfo.Show();
        }

        private void button4_Click(object sender, EventArgs e) // botão salvar
        {

        }

        private void button6_Click(object sender, EventArgs e) //botão de selecionar arquivo
        {
            
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "Text files (*.txt)|*.txt| Data files (*.dat)|*.dat ";
            openFileDialog1.Filter = "OPF files (*.txt,*.dat)|*.txt;*.dat ";
            openFileDialog1.Title = "Select a Database File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                textBox1.Text = path;
            }
        }

        private void button5_Click(object sender, EventArgs e) // botão database
        {

        }
    }
}
