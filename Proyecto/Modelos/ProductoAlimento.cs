using System;

namespace Proyecto.Modelos
{
    // ProductoAlimento hereda de Producto.
    public class ProductoAlimento : Producto
    {
        // Esta propiedad es específica para los alimentos.
        public DateTime FechaCaducidad { get; set; }

        // Sobrescribimos el método de la clase padre (polimorfismo).
        public override string MostrarDetalles()
        {
            return $"{base.MostrarDetalles()}, Fecha de Caducidad: {FechaCaducidad.ToShortDateString()}";
        }
    }
}