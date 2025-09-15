using System;

namespace Proyecto.Modelos
{
    // ProductoQuimico también hereda de Producto.
    public class ProductoQuimico : Producto
    {
        public string NivelToxicidad { get; set; }

        // Sobrescribimos el método para este tipo de producto.
        public override string MostrarDetalles()
        {
            return $"{base.MostrarDetalles()}, Nivel de Toxicidad: {NivelToxicidad}";
        }
    }
}