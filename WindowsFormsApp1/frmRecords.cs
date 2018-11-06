using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class frmRecords : Form
    {
        public string tiempo;
        public int segundos;
        private int minSeg = 120;
        const string CONNSTR = "";
        public frmRecords()
        {
            InitializeComponent();
        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRecords_Load(object sender, EventArgs e)
        {
            minSeg = cargarRecords();

            this.txtNombre.Visible = (minSeg < segundos);
            this.btnGuardar.Visible = (minSeg < segundos);


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(CONNSTR);
            SqlCommand cm = new SqlCommand("insert into records (nombre, tiempo,segundos) values " +
         "('" + txtNombre.Text + "','" + tiempo + "'," + segundos + ")", cn);

            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cargarRecords();
        }


        private int cargarRecords()
        {

            SqlConnection cn = new SqlConnection(CONNSTR);
            SqlCommand cm = new SqlCommand("Select top 20 * from records order by segundos desc", cn);
            minSeg = 120;
            try
            {
                cn.Open();
                SqlDataReader da = cm.ExecuteReader();
                while (da.Read())
                {
                    this.rtxtPodio.AppendText(da["nombre"].ToString() + "  " + da["tiempo"].ToString() + "\n");
                    if ((int)da["segundos"] < minSeg)
                        minSeg = (int)da["segundos"];
                }
                cn.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return minSeg;
        }
    }
}
