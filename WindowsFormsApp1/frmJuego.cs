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

namespace WindowsFormsApp1
{
    public partial class frmJuego : Form
    {

        const int SEGUNDOS = 120; 
        Thread TMov;
        Thread TTiempo;
        public int segundos;

        Random random;

        public frmJuego()
        {
            random = new Random(new DateTime().Millisecond);
            InitializeComponent();
            
        }

        public void tiempo()
        {
            while (segundos > 0)
            {
                Thread.Sleep(1000);
                segundos--;
                actualizaSegundo(((int)segundos / 60).ToString() + ":" + ((int)segundos % 60).ToString("00"));

            }

            if (TMov.IsAlive)
                TMov.Abort();

            if (this.btnComenzar.InvokeRequired)
            {
                this.btnComenzar.BeginInvoke((MethodInvoker)delegate
                {
                    this.btnComenzar.Visible = true;
                });
            }
            else
                this.btnComenzar.Visible = true;

        }

        public void actualizaSegundo(string texto)
        {

            if (lblSeg.InvokeRequired)
            {
                lblSeg.BeginInvoke((MethodInvoker)delegate
                {
                    lblSeg.Text = texto;
                });

            }
            else
                lblSeg.Text = texto;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (TMov.IsAlive)
                TMov.Abort();
            if (TTiempo.IsAlive)
                TTiempo.Abort();

            this.btnComenzar.Visible = true;
            frmRecords frm = new frmRecords();
            frm.tiempo = lblSeg.Text;
            frm.segundos = segundos;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TMov.IsAlive)
                TMov.Abort();
            if (TTiempo.IsAlive)
                TTiempo.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void mover()
        {
            while (segundos > 0)
            {
                Thread.Sleep(500);
                moverpieza();
            }
        }

        void moverpieza()
        {
            if (this.pictureBox1.InvokeRequired)
            {
                pictureBox1.BeginInvoke((MethodInvoker)delegate
                {
                    int y = random.Next(399);
                    int x = random.Next(753);
                    this.pictureBox1.Top = y;
                    this.pictureBox1.Left = x;
                });

            }
            else
            {
                Random random = new Random(new DateTime().Millisecond);

                int y = random.Next(399);
                int x = random.Next(753);
                this.pictureBox1.Top = y;
                this.pictureBox1.Left = x;
            }

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.segundos--;
            actualizaSegundo(((int)segundos / 60).ToString() + ":" + ((int)segundos % 60).ToString("00"));
        }


        private void btnComenzar_Click(object sender, EventArgs e)
        {
            TTiempo = new Thread(tiempo);
            TMov = new Thread(mover);
            segundos = SEGUNDOS;
            this.btnComenzar.Visible = false;
            this.segundos = SEGUNDOS;
            TTiempo.Start();
            TMov.Start();
        }
    }
}
