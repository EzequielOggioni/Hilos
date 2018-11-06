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
  public partial class Form1 : Form
  {
    Thread TMov;
    Thread TTiempo;
    public int segundos;

    Random random;

    public Form1()
    {
      random = new Random(new DateTime().Millisecond);
      InitializeComponent();
      TTiempo = new Thread(tiempo);
      TMov = new Thread(mover);
      segundos = 120;
    }

    public void tiempo()
    {
      while (segundos > 0)
      {
        Thread.Sleep(1000);
        actualizaSegundo(((int)segundos / 60).ToString() + ":" + ((int)segundos % 60).ToString("00"));
        segundos--;
      }

      if (TMov.IsAlive)
      {
        TMov.Abort();
      }



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
      if (TMov.IsAlive)
        TMov.Abort();

      MessageBox.Show("Tardo " + this.lblSeg.Text + " Segundos ");
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
      TTiempo.Start();
      TMov.Start();
    }

    public void mover()
    {
      while (segundos > 0)
      {
        Thread.Sleep(1500);
        moverpieza();
      }
    }

    void moverpieza()
    {
      if (this.pictureBox1.InvokeRequired)
      {
        pictureBox1.BeginInvoke((MethodInvoker)delegate
        {
          
          this.pictureBox1.Top = random.Next(399);
          this.pictureBox1.Left = random.Next(753);
        });

      }    
      else
      {
          Random random = new Random(new DateTime().Millisecond);
          this.pictureBox1.Top = random.Next(399);
          this.pictureBox1.Left = random.Next(753);
      }

    }

    private void Form1_Click(object sender, EventArgs e)
    {
      this.segundos--;
    }

    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
     

    }
  }
}
