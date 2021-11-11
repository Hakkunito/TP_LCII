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
        public void CargarUnDatagrid(string consulta)
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
            CargarUnDatagrid(@"select TOP(5) td.nombre + ' nro : ' + c.nro_doc as 'Identificacion',
c.nombre 'Nombre',
c.[e - mail] 'E-mail',
c.telefono 'Telefono',
sum(df.cantidad * df.precio) / count(distinct f.nro_factura) 'Importe Promedio',
tp.nombre 'Tipo de plan'
from clientes c join tipos_documento td on c.id_tipo_doc = td.id_tipo_doc
join facturas f on c.id_cliente = f.id_cliente
join detalles_factura df on f.nro_factura = df.nro_factura
join tipos_plan tp on f.id_plan = tp.id_plan
join tipos_cliente tc on c.id_tipo_cliente = tc.id_tipo_cliente
--join productos p on df.id_producto = p.id_producto
--join automoviles au on p.id_automovil = au.id_automovil
where exists(
select a.id_automovil
from automoviles a join productos p on p.id_automovil = a.id_automovil
join detalles_factura df on p.id_producto = df.id_producto
)
and year(df.fecha) = year(GETDATE())
and LOWER(tc.nombre) like 'mayorista'
group by 5
");
        }

        private void btn_Consulta3_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid(@"select ap.descripcion 'Descripcion',
sum(df.cantidad) 'Total Vendido',
ap.stock 'Stock',
year(ap.fecha_fabricacion) 'Año de fabricacion'
from autopartes ap join productos p on p.id_autoparte=ap.id_autoparte
join detalles_factura df on p.id_producto = df.id_producto
group by 4
union
select am.modelo 'Descripcion',
sum(df.cantidad) 'Total Vendido',
am.stock 'Stock',
year(am.fecha_fabricacion) 'Año de fabricacion'
from automoviles am join productos p on p.id_automovil=am.id_automovil
join detalles_factura df on p.id_producto =
df.id_producto
group by 4");
        }

        private void btn_Consulta4_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid(@"select op.id_orden_pedido 'Id Orden:',
op.id_producto 'Id Prod:',
ap.descripcion 'Descripcion:',
sum(op.cantidad) / count(distinct df.nro_factura) 'AVG(Cant x Factura):'
from ordenes_pedidos op join facturas f on op.id_orden_pedido = f.id_orden_pedido
join productos p on op.id_producto = p.id_producto
join autopartes ap on p.id_autoparte = ap.id_autoparte
join automoviles am on p.id_automovil = am.id_automovil
join detalles_factura df on f.nro_factura = df.nro_factura
where month(df.fecha) between MONTH(GETDATE()) and MONTH(GETDATE() - 2)
union
select op.id_orden_pedido 'Id Orden:',
op.id_producto 'Id Prod:',
am.modelo 'Descripcion:',
count(op.cantidad) / count(distinct df.nro_factura) 'AVG(Cant x Factura):'
from ordenes_pedidos op join facturas f on op.id_orden_pedido = f.id_orden_pedido
join productos p on op.id_producto = p.id_producto
join autopartes ap on p.id_autoparte = ap.id_autoparte
join automoviles am on p.id_automovil = am.id_automovil
join detalles_factura df on f.nro_factura = df.nro_factura
where df.fecha between MONTH(GETDATE()) and MONTH(GETDATE() - 2)");
        }

        private void btn_Consulta1_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid(@"select c.id_tipo_cliente as Codigo, tc.nombre as
Nombre, str(count(c.id_tipo_cliente) * 100 / (select count(*) from facturas)) + ' %' as Porcentaje
from facturas f join clientes c on f.id_cliente = c.id_cliente join tipos_cliente tc on
  c.id_tipo_cliente = tc.id_tipo_cliente
group by c.id_tipo_cliente,tc.nombre");
        }

        private void btn_Consulta5_Click(object sender, EventArgs e)
        {
            CargarUnDatagrid(@"select count(p.id_automovil) as Vendidos, a.modelo Modelo
from detalles_factura d join productos p on d.id_producto=p.id_producto join automoviles a
on p.id_automovil=a.id_automovil
group by p.id_automovil,a.modelo
order by 1 desc");
        }
    }

}
