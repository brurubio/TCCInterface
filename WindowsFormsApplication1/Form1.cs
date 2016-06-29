using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string path, OPFpath, extension, fileName, fileDirec;
		int ext = -1;
		float Ptrain = 0, Ptest = 0;

        public Form1()
        {
            InitializeComponent();
			System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
			customCulture.NumberFormat.NumberDecimalSeparator = ".";
			System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            Form winOPF = new Form4();
            winOPF.ShowDialog();
            //this.OPFpath = Form4.pth;
            OPFpath = Form4.pth;
			//winOPF.Close();
            //Console.WriteLine(OPFpath);
        }

        private static void ExecuteCommand(string fileDirec, string command)
		{
			//Console.WriteLine(command);
			Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = "/bin/bash";
			//proc.StartInfo.Arguments = command;
			proc.StartInfo.WorkingDirectory = fileDirec;
			proc.StartInfo.Arguments = "-c \" " + command + " \"";
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.Start ();
			proc.WaitForExit();

			while (!proc.StandardOutput.EndOfStream) {
				Console.WriteLine (proc.StandardOutput.ReadLine ());
			}
		}

		public static void Executeterminal(string binPath, string fileDirec, string fileData, int ext, float Ptrain, float Ptest)
		{
			//Console.WriteLine(fileDirec);
			//Console.WriteLine(fileData);
			//Console.WriteLine(ext);
			//Console.WriteLine(Ptrain);
			//Console.WriteLine(Ptest);
			//ExecuteCommand(fileDirec, "opf2txt boat.dat cy.txt");
			ExecuteCommand(fileDirec, "./testTerminal.sh " + ext + " " + binPath + " " + fileData + " " + Ptrain + " " + Ptest);
			//ExecuteCommand("gnome-terminal -x bash -ic 'cd $fileDirec; ./testTerminal.sh $ext $fileName $Ptrain $Ptest;bash'");
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
            if(textBox1. Text == "")
            {
                MessageBox.Show("Selecione um arquivo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
			fileDirec = System.IO.Path.GetDirectoryName(path);
            fileName = System.IO.Path.GetFileName(path);
			extension = System.IO.Path.GetExtension(path);
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
			Ptrain = Ptrain/100;
			Ptest = Ptest/100;
			float sum = Ptrain + Ptest;
            //Console.WriteLine(comboBox1.SelectedIndex); 
			if (sum != 1.0) {
				MessageBox.Show ("Os valores de treino e teste devem somar 100%.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				textBox3.Focus ();
				richTextBox1.Clear ();
			} else {
				if (comboBox1.SelectedIndex == 0) {
					Executeterminal (OPFpath, fileDirec, fileName, ext, Ptrain, Ptest);
					richTextBox1.Text = "Rodando OPF...";
					while (!System.IO.File.Exists (fileDirec + "/testing.dat.acc")) {
						//richTextBox1.Text += "...";
						System.Threading.Thread.Sleep (100);
					}
					string filePath = fileDirec + "/" + System.IO.Path.GetFileNameWithoutExtension (path) + ".txt";
					using (System.IO.StreamReader ln = new System.IO.StreamReader (filePath)) {
						string line = ln.ReadLine ();
						string[] words = line.Split ();
						richTextBox1.Text += "OK\nNúmero de amostras: " + words [0];
						richTextBox1.Text += "\nNúmero de classes: " + words [1];
						richTextBox1.Text += "\nNúmero de características: " + words [2];
					}
					richTextBox1.Text += "\nAcurácia (%): " + System.IO.File.ReadAllText (fileDirec + "/testing.dat.acc");
					richTextBox1.Text += "Tempo de treinamento (s): " + System.IO.File.ReadAllText (fileDirec + "/testing.dat.time");
					richTextBox1.Text += "Tempo de teste (s): " + System.IO.File.ReadAllText (fileDirec + "/training.dat.time");
					//richTextBox1.Text = " \n";
					//strCmdText = "./testTerminal.sh";
					ExecuteCommand (fileDirec, "rm *.out *.acc *.time classifier.opf");
					//System.Diagnostics.Process.Start("CMD.exe", strCmdText + ext + fileName + Ptrain + Ptest);
				}
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
