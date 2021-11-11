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


namespace TP_LCII
{
    public partial class Form1 : Form
    {
        public SqlConnection conexion;
        public SqlCommand comando;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_Consulta_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Consulta1_Click(object sender, EventArgs e)
        {
            conexion = new SqlConnection(@"Data Source=DESKTOP-JT4OIKA;Initial Catalog=Fabrica;Integrated Security=True");
            conexion.Open();
            DataTable Tabla = new DataTable();
            comando = new SqlCommand();
            comando.CommandText = "Select * from automoviles";
            comando.Connection = conexion;
            Tabla.Load(comando.ExecuteReader());
            conexion.Close();

            dataGridView1.ClearSelection();
            dataGridView1.DataSource = Tabla;
        }
    }
}
