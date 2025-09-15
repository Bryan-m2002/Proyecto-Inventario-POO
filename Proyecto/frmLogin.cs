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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // una instancia de la clase para acceder a los datos de usuario
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            // Verificamos las credenciales del usuario
            string nombreUsuario = txtUsuario.Text;
            string contrasenaUsuario = txtContrasena.Text;

            Usuario usuario = usuarioDAO.ObtenerUsuario(nombreUsuario, contrasenaUsuario);

            if (usuario != null)
            {
                // Si el usuario existe, oculta el formulario de login y muestra el menú principal
                this.Hide();
                frmMenuPrincipal menuPrincipal = new frmMenuPrincipal();
                menuPrincipal.ShowDialog();
                this.Close(); // Cierra este formulario cuando el principal se cierre
            }
            else
            {
                // Si el usuario no existe, muestra un mensaje de error
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
