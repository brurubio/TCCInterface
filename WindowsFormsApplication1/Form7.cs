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
    public partial class Form7 : Form
    {
        int[] bestFeat;
        int nCar;
        

        public Form7(int num, int[] vet)
        {
            InitializeComponent();
            nCar = num;
            bestFeat = new int[nCar];
            bestFeat = vet;
            for (int i = 0; i < nCar; i++)
            {
				richTextBox1.Text += (i+1) + ": " + bestFeat[i] + "\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Caixa de texto vazia. Execute um procedimento para gerar dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                // Create a SaveFileDialog to request a path and file name to save to.
                SaveFileDialog saveFile1 = new SaveFileDialog();

                // Initialize the SaveFileDialog to specify the RTF extention for the file.
                saveFile1.DefaultExt = "*.txt";
                saveFile1.Filter = "Text Files|*.txt";

                // Determine whether the user selected a file name from the saveFileDialog.
                if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                    saveFile1.FileName.Length > 0)
                {
                    // Save the contents of the RichTextBox into the file.
                    richTextBox1.SaveFile(saveFile1.FileName);
                }
            }
        }
    }
}
