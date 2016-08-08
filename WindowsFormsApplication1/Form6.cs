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
    public partial class Form6 : Form
    {
        int Agentes, Carac, Iter;
        string fileDirec;
        public Form6(string path)
        {
            InitializeComponent();
            //muda a "," do número real para "."
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            fileDirec = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double c1, c2, w, wmin, wmax;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Digite o número de partículas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Digite o número de características.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("Digite a quantidade máxima de iterações.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
            }
            Agentes = Convert.ToInt16(textBox1.Text);
            Carac = Convert.ToInt16(textBox2.Text);
            Iter = Convert.ToInt16(textBox3.Text);
            c1 = 1.7;
            c2 = 1.7;
            w = 0.7;
            wmin = 0.5;
            wmax = 1.5;

            //Declaração do método StreamWriter passando o caminho e nome do arquivo que deve ser salvo
            string file = fileDirec+"/pso_infos.txt";
            //string file = "C:/Arquivos Bruna/UNESP/7º Semestre/Projeto e Implementação de Sistemas I/Interface/TCC/WindowsFormsApplication1/sh/PSO_Infos.txt";
            Console.WriteLine(file);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(file);
            //Escrevendo o Arquivo e pulando uma linha
            //Escrevendo o Arquivo sem pular linha
            writer.WriteLine(Agentes + " " + Carac + " " + Iter);
            writer.WriteLine(c1 + " " + c2);
            writer.WriteLine(w + " " + wmin + " " + wmax);
            //Fechando o arquivo
            writer.Close();
            //Limpando a referencia dele da memória
            writer.Dispose();
            this.Close();
        }
    }
}
