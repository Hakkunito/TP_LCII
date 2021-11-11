﻿using System;
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
        public void CargarUnDatagrid(string consulta,string texto)
        {
            // cambiar el codigo de la conexion 
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-CBD3HB7\SQLEXPRESS;Initial Catalog=Fabrica;Integrated Security=True");
            try
            {
                
                cn.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(consulta, cn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                dgvPrincipal.DataSource = dt;                                    
                cn.Close();

                lblConsulta.Text = texto;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }
        // faltan las consultas


        private void btn_Consulta2_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid("","");
        }

        private void btn_Consulta3_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid("","");
        }

        private void btn_Consulta4_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid("","");
        }

        private void btn_Consulta1_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid("select * from paises","paises");
        }

        private void btn_Consulta5_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid("","");
        }
    }

}
