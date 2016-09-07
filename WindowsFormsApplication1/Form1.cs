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
        string path = null, OPFpath = null, OPTpath = null, DEVpath = null, DEEPpath = null, extension = null, fileName = null, fileDirec = null, fileDirecSh = null;
		int ext = -1, intCar;								
		int[] bestF;
		float Ptrain = 0, Ptest = 0, trainPSO=0, evalPSO=0;

        public Form1()
        {
            InitializeComponent();
            //muda a "," do número real para "."
			System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
			customCulture.NumberFormat.NumberDecimalSeparator = ".";
			System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

		//executa comando no terminal
        private static void ExecuteCommand(string fileDirecSh, string command)
		{
			Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = "/bin/bash";
			proc.StartInfo.WorkingDirectory = fileDirecSh;
			proc.StartInfo.Arguments = "-c \" " + command + " \"";
			proc.StartInfo.UseShellExecute = false; 
			proc.StartInfo.RedirectStandardOutput = true;
			proc.Start ();
			proc.WaitForExit();

			while (!proc.StandardOutput.EndOfStream) {
				Console.WriteLine (proc.StandardOutput.ReadLine ());
			}
		}

		//informa qual comando de terminal deve ser executado
		public static void ExecuteOPF(string OPFpath, string fileDirecSh, string fileDirec, string fileData, int ext, float Ptrain, float Ptest)
		{
			ExecuteCommand(fileDirecSh, "./runOPF.sh " + ext + " " + OPFpath + " " + fileDirec + " " + fileDirecSh + " " + fileData + " " + Ptrain + " " + Ptest);
		}

		public static void ExecutePSO(string OPFpath, string OPTpath, string DEVpath, string DEEPpath, string fileDirecSh, string fileDirec, string fileData, int ext, float Ptrain, float Ptest)
        {
			ExecuteCommand(fileDirecSh, "./runPSO.sh " + ext + " " + OPFpath + " " + fileDirec + " " + fileDirecSh + " " + fileData + " " + Ptrain + " " + Ptest + " " + OPTpath + " " + DEVpath + " " + DEEPpath);
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

		//botão iniciar realiza toda operação
        private void button1_Click(object sender, EventArgs e) // botão iniciar
        {
			richTextBox1.Clear();
            //caso nenhum arquivo tenha sido selecionado
            if (textBox1.Text == "")
            {
                MessageBox.Show("Selecione um arquivo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            else
            {
                fileDirec = System.IO.Path.GetDirectoryName(path); //diretório da base de dados
                fileName = System.IO.Path.GetFileName(path); //nome da base de dados
                extension = System.IO.Path.GetExtension(path); //extensão da base de dados
                //diretório do sh padrão
                string cntDir = System.IO.Directory.GetCurrentDirectory();
                fileDirecSh = System.IO.Directory.GetParent(cntDir).FullName;
                fileDirecSh = System.IO.Directory.GetParent(fileDirecSh).FullName;
                fileDirecSh = fileDirecSh + "/sh";
                //verifica extensão da base
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

                // 0 - OPF | 1 - OPF + PSO
                //erro caso nenhum processo eseja selecionado
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Selecione um processo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                }
                else
                {
                    Ptrain = Convert.ToInt16(textBox3.Text);
                    Ptest = Convert.ToInt16(textBox4.Text);
                    Ptrain = Ptrain / 100; //tranformação de porcentagem (0-1)
                    Ptest = Ptest / 100; //tranformação de porcentagem (0-1)
                    float sum = Ptrain + Ptest;
                    //teste se soma é 100%
                    if (sum != 1.0)
                    {
                        MessageBox.Show("Os valores de treino e teste devem somar 100%.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox3.Focus();
                        richTextBox1.Clear();
                    }
                    else
                    {
                        //se selecionado OPF (op. 0)
                        if (comboBox1.SelectedIndex == 0)
                        {
                            if (OPFpath == null)
                            {
                                //abre a janela para selecionar o caminho da LibOPF 
                                Form winOPF = new Form4();
                                winOPF.ShowDialog();
                                OPFpath = Form4.pth;
                            }
                            //executa comando terminal
                            ExecuteOPF(OPFpath, fileDirecSh, fileDirec, fileName, ext, Ptrain, Ptest);
                            //impressão de resultados I
                            richTextBox1.Text = "Rodando OPF...";
                            //verifica se processo já foi finalizado
                            while (!System.IO.File.Exists(fileDirecSh + "/testing.dat.acc"))
                            {
                                richTextBox1.Text += "...";
                                System.Threading.Thread.Sleep(100);
                            }
                            //impressão de resultados II
                            richTextBox1.Text += "OK\nBase de Dados: " + fileName + "\nPorcentagem de Treinamento: " + (Ptrain * 100) + "%" + "\nPorcentagem de Teste: " + (Ptest * 100) + "%";
                            string getName = System.IO.Path.GetFileNameWithoutExtension(path);//pega nome da base
                                                                                              //utiliza o base.txt
                            string filePath = fileDirecSh + "/" + getName + ".txt";
                            using (System.IO.StreamReader ln = new System.IO.StreamReader(filePath))
                            {
                                string line = ln.ReadLine();
                                string[] words = line.Split();
                                //impressão de resultados III
                                richTextBox1.Text += "\nNúmero de amostras: " + words[0];
                                richTextBox1.Text += "\nNúmero de classes: " + words[1];
                                richTextBox1.Text += "\nNúmero de características: " + words[2];
                            }
                            //impressão de resultados IV
                            richTextBox1.Text += "\nAcurácia (%): " + System.IO.File.ReadAllText(fileDirecSh + "/testing.dat.acc");
                            richTextBox1.Text += "Tempo de treinamento (s): " + System.IO.File.ReadAllText(fileDirecSh + "/testing.dat.time");
                            richTextBox1.Text += "Tempo de teste (s): " + System.IO.File.ReadAllText(fileDirecSh + "/training.dat.time");
                            //remoção dos arquivos gerados
                            ExecuteCommand(fileDirecSh, "rm *.out *.acc *.time classifier.opf *.dat " + getName + ".txt");
                        } //end if (combobox = 0)
                        else
                        {
                            if (comboBox1.SelectedIndex == 1)
                            {
                                button5.Visible = true;
								if (OPFpath == null || (OPTpath == null && DEVpath == null && DEEPpath == null))
                                {
                                    Form winLib = new Form5();
                                    winLib.ShowDialog();
                                    OPFpath = Form5.OPFpth;
                                    OPTpath = Form5.OPTpth;
                                    DEVpath = Form5.DEVpth;
                                    DEEPpath = Form5.DEEPpth;
                                }
                                //Informações PSo e criação do PSO_info.txt
                                Form winPSO = new Form6(fileDirecSh);
                                winPSO.ShowDialog();
								//winPSO.Close();
                             //   MessageBox.Show("Processo ainda não implementado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             //   comboBox1.Focus();
								trainPSO = Ptrain/2;
								Console.WriteLine (trainPSO);
								Console.WriteLine (Ptest);
                                //executa comando terminal
								ExecuteOPF(OPFpath, fileDirecSh, fileDirec, fileName, ext, Ptrain, Ptest);
								ExecutePSO(OPFpath, OPTpath, DEVpath, DEEPpath, fileDirecSh, fileDirec, fileName, ext, trainPSO, Ptest);
                                //impressão de resultados I
								richTextBox1.Text = "Rodando...";
                                //verifica se processo já foi finalizado
								while (!System.IO.File.Exists(fileDirecSh + "/final_accuracy.txt"))
                                {
                                    richTextBox1.Text += "...";
                                    System.Threading.Thread.Sleep(100);
                                }
                                //impressão de resultados II
                                richTextBox1.Text += "OK\nBase de Dados: " + fileName + "\nPorcentagem de Treinamento: " + (Ptrain * 100) + "%" + "\nPorcentagem de Teste: " + (Ptest * 100) + "%";
                                string getName = System.IO.Path.GetFileNameWithoutExtension(path);//pega nome da base
                                                                                                  //utiliza o base.txt
                                string filePath = fileDirecSh + "/" + getName + ".txt";
                                //impressão de resultados base Original
                                richTextBox1.Text += "\n\nResultados Base Original: ";
								string CaracOr = null;
                                using (System.IO.StreamReader ln = new System.IO.StreamReader(filePath))
                                {
                                    string line = ln.ReadLine();
                                    string[] words = line.Split();
                                    richTextBox1.Text += "\nNúmero de amostras: " + words[0];
                                    richTextBox1.Text += "\nNúmero de classes: " + words[1];
                                    richTextBox1.Text += "\nNúmero de características: " + words[2];
									CaracOr = words [2];
                                }
                                richTextBox1.Text += "\nAcurácia (%): " + System.IO.File.ReadAllText(fileDirecSh + "/testing.dat.acc");
                                richTextBox1.Text += "Tempo de treinamento (s): " + System.IO.File.ReadAllText(fileDirecSh + "/testing.dat.time");
                                richTextBox1.Text += "Tempo de teste (s): " + System.IO.File.ReadAllText(fileDirecSh + "/training.dat.time");
                                
                                //impressão de resultados base Otimizada
                                richTextBox1.Text += "\nResultados Base Otimizada: ";
								//ler informações do pso_infos
								string filePSO = fileDirecSh + "/pso_infos.txt";
                                using (System.IO.StreamReader ln = new System.IO.StreamReader(filePSO))
                                {
                                    string line = ln.ReadLine();
                                    string[] words = line.Split();
                                    richTextBox1.Text += "\nNúmero de agentes: " + words[0];
                                    richTextBox1.Text += "\nNúmero de iterações: " + words[2];
                                }
								//transforma base otimizada em .txt
								ExecuteCommand(fileDirecSh, OPFpath + "/tools/opf2txt training.PSO.dat dataPSO.txt");
								string Carac = null;
								string dataPSO = fileDirecSh + "/dataPSO.txt";
								using (System.IO.StreamReader ln = new System.IO.StreamReader(dataPSO))
								{
									string line = ln.ReadLine();
									string[] words = line.Split();
									Carac = words[2];
								}
								using (System.IO.StreamReader ln = new System.IO.StreamReader(filePath))
								{
									string line = ln.ReadLine();
									string[] words = line.Split();
									richTextBox1.Text += "\nNúmero de amostras: " + words[0];
									richTextBox1.Text += "\nNúmero de classes: " + words[1];
									richTextBox1.Text += "\nNúmero de características: " + Carac;
								}
								using (System.IO.StreamReader ln = new System.IO.StreamReader (fileDirecSh + "/final_accuracy.txt")) {
									string line = ln.ReadLine ();
									string[] words = line.Split();
									double accPSO = Convert.ToDouble(words[0])*100;
									richTextBox1.Text += "\nAcurácia (%): " + accPSO;
								}
								using (System.IO.StreamReader ln = new System.IO.StreamReader (fileDirecSh + "/optimization.time")) {
									string line = ln.ReadLine ();
									string[] words = line.Split();
									richTextBox1.Text += "\nTempo de otimização (s): " + words[0];
								}
								// armazenamento do best features
								intCar = Convert.ToInt16(CaracOr);
								bestF = new int[intCar];
								string bestFeat = fileDirecSh + "/best_feats.txt";
								using (System.IO.StreamReader ln = new System.IO.StreamReader(bestFeat))
								{
									string line = ln.ReadLine();
									string[] feat = line.Split();
									for (int i = 0; i < intCar; i++) {
										bestF [i] = Convert.ToInt16(feat[i]);
									}
								}
                                MessageBox.Show("Otimização realizada, para ver as melhores características clique no botão binário.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // remoção de arquivos
								ExecuteCommand(fileDirecSh, "rm *.out *.time *.acc classifier.opf best_feats.txt final_accuracy.txt pso_infos.txt dataPSO.txt *.dat " + getName + ".txt");
							  
                            }
                        }
                    }// end else sum
                }//end else combobox1.selectedindex
            }//end else textbox1.text
        }// end botão iniciar

		// botão de ajuda - nova janela
        private void button2_Click(object sender, EventArgs e) 
        {
            Form winHelp = new Form3();
            winHelp.Show();
        }

		// botão de info - noja janela
        private void button3_Click(object sender, EventArgs e)
        {
            Form winInfo= new Form2();
            winInfo.Show();
        }

		// botão salvar
        private void button4_Click(object sender, EventArgs e) 
        {
			if (richTextBox1.Text == ""){
				MessageBox.Show("Caixa de texto vazia. Execute um procedimento para gerar dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else{
				
			// Create a SaveFileDialog to request a path and file name to save to.
			SaveFileDialog saveFile1 = new SaveFileDialog();

			// Initialize the SaveFileDialog to specify the RTF extention for the file.
			saveFile1.DefaultExt = "*.txt";
			saveFile1.Filter = "Text Files|*.txt";

			// Determine whether the user selected a file name from the saveFileDialog.
			    if(saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
				    saveFile1.FileName.Length > 0) 
			    {
				    // Save the contents of the RichTextBox into the file.
				    richTextBox1.SaveFile(saveFile1.FileName);
			    }
			}
        }

		//botão de selecionar arquivo
        private void button6_Click(object sender, EventArgs e) 
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
            Form winCar = new Form7(intCar, bestF);
            winCar.Show();
        }


    }
}
