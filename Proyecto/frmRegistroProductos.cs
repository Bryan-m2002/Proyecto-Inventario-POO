using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto.AccesoDatos;
using Proyecto.Modelos;

namespace Proyecto
{
    public partial class frmRegistroProductos : Form
    {
        public frmRegistroProductos()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Ocultamos los campos específicos al inicio
            dtpFechaCaducidad.Visible = false;
            txtNivelToxicidad.Visible = false;
            // Configuramos los tipos de productos en el ComboBox
            cboTipoProducto.Items.Add("General");
            cboTipoProducto.Items.Add("Alimento");
            cboTipoProducto.Items.Add("Quimico");
            cboTipoProducto.SelectedIndex = 0;
            // Manejamos el evento para mostrar los campos correctos
            cboTipoProducto.SelectedIndexChanged += cboTipoProducto_SelectedIndexChanged;
        }

        private void cboTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpFechaCaducidad.Visible = (cboTipoProducto.SelectedItem.ToString() == "Alimento");
            txtNivelToxicidad.Visible = (cboTipoProducto.SelectedItem.ToString() == "Quimico");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Instanciamos la clase de acceso a datos para productos
                ProductoDAO productoDAO = new ProductoDAO();
                bool exito = false;

                // Creamos el objeto según el tipo de producto seleccionado (Polimorfismo en acción)
                if (cboTipoProducto.SelectedItem.ToString() == "Alimento")
                {
                    ProductoAlimento nuevoAlimento = new ProductoAlimento
                    {
                        IdProducto = int.Parse(txtIdProducto.Text),
                        Nombre = txtNombre.Text,
                        StockActual = int.Parse(txtStock.Text),
                        Precio = decimal.Parse(txtPrecio.Text),
                        FechaCaducidad = dtpFechaCaducidad.Value
                    };
                    exito = productoDAO.InsertarProducto(nuevoAlimento);
                }
                else if (cboTipoProducto.SelectedItem.ToString() == "Quimico")
                {
                    ProductoQuimico nuevoQuimico = new ProductoQuimico
                    {
                        IdProducto = int.Parse(txtIdProducto.Text),
                        Nombre = txtNombre.Text,
                        StockActual = int.Parse(txtStock.Text),
                        Precio = decimal.Parse(txtPrecio.Text),
                        NivelToxicidad = txtNivelToxicidad.Text
                    };
                    exito = productoDAO.InsertarProducto(nuevoQuimico);
                }
                else
                {
                    Producto nuevoProducto = new Producto
                    {
                        IdProducto = int.Parse(txtIdProducto.Text),
                        Nombre = txtNombre.Text,
                        StockActual = int.Parse(txtStock.Text),
                        Precio = decimal.Parse(txtPrecio.Text)
                    };
                    exito = productoDAO.InsertarProducto(nuevoProducto);
                }

                if (exito)
                {
                    MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Cierra el formulario después de guardar
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Asegúrate de llenar todos los campos correctamente. Detalles: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
   }
