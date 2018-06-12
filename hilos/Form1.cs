using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hilos
{
    public partial class Form1 : Form
    {

        List<Thread> hilos = new List<Thread>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Numero n1 = new Numero(1);
            Numero n2 = new Numero(2);
            Numero n3 = new Numero(3);
            Numero n4 = new Numero(4);
            n1.MuestraNumeroEvent += mostrar;
            n2.MuestraNumeroEvent += mostrar;
            n3.MuestraNumeroEvent += mostrar;
            n4.MuestraNumeroEvent += mostrar;

            
            hilos.Add(new Thread(n1.sumar));
            hilos.Add(new Thread(n2.sumar));
            hilos.Add(new Thread(n3.sumar));
            hilos.Add(new Thread(n4.sumar));

            foreach (Thread t in hilos)
            {
                
                t.Start();
            }
        }

       public void mostrar(int num, int posicion)
        {
            
            switch (posicion)
            {
                case 1:
                    if (this.txt1.InvokeRequired)
                    {
                        this.txt1.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.txt1.Text = num.ToString();
                        });

                    }
                    else
                        this.txt1.Text = num.ToString();
                    break;
                case 2:
                    if (this.txt2.InvokeRequired)
                    {
                        this.txt2.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.txt2.Text = num.ToString();
                        });

                    }
                    else
                        this.txt2.Text = num.ToString();
                    break;
                case 3:
                    if (this.txt3.InvokeRequired)
                    {
                        this.txt3.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.txt3.Text = num.ToString();
                        });

                    }
                    else
                        this.txt3.Text = num.ToString();
                    break;
                case 4:
                    if (this.txt4.InvokeRequired)
                    {
                        this.txt4.BeginInvoke((MethodInvoker)delegate()
                        {
                            this.txt4.Text = num.ToString();
                        });

                    }
                    else
                        this.txt4.Text = num.ToString();
                    break;
            }


        }

       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
       {
           foreach (Thread t in hilos)
           {
               if (t.IsAlive)
                   t.Abort();
           }
       }

       private void button2_Click(object sender, EventArgs e)
       {
           
       }
    }
}
