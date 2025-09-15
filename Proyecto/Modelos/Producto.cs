using System;
using System.Text;

namespace Proyecto.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int StockActual { get; set; }
        public decimal Precio { get; set; }

        // El método MostrarDetalles es virtual para permitir que las clases hijas lo sobrescriban.
        public virtual string MostrarDetalles()
        {
            return $"ID: {IdProducto}, Nombre: {Nombre}, Stock: {StockActual}, Precio: {Precio:C}";
        }
    }
}