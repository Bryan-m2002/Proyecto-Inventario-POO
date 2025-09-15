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
using Proyecto; 

namespace Proyecto
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCargarProductos_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();

                // Aquí ya no hay código del botón

                AccesoDatos.ProductoDAO productoDAO = new AccesoDatos.ProductoDAO();
                var productos = productoDAO.ObtenerProductos();

                dataGridView1.DataSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

        private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            // Creamos una instancia del formulario de registro de productos
            frmRegistroProductos formularioRegistro = new frmRegistroProductos();

            // Lo mostramos
            formularioRegistro.ShowDialog();
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            // Verificamos si hay alguna fila seleccionada en la tabla
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtenemos el ID del producto de la fila seleccionada
                int idProducto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdProducto"].Value);

                // Confirmamos la eliminación con el usuario
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    // Instanciamos la clase de acceso a datos y llamamos al método para eliminar
                    ProductoDAO productoDAO = new ProductoDAO();
                    bool exito = productoDAO.EliminarProducto(idProducto);

                    if (exito)
                    {
                        MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Actualizamos la tabla para que el producto ya no aparezca
                        btnCargarProductos_Click_1(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
