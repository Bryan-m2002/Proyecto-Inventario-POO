using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Proyecto.Modelos;

namespace Proyecto.AccesoDatos
{
    public class ProductoDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["inventario_db_con"].ConnectionString;

        public List<Producto> ObtenerProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT IdProducto, Nombre, StockActual, Precio, FechaCaducidad, NivelToxicidad FROM productos";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = null;

                        // Comprobamos si el producto es un alimento o un quimico
                        if (!reader.IsDBNull(reader.GetOrdinal("FechaCaducidad")))
                        {
                            ProductoAlimento alimento = new ProductoAlimento
                            {
                                IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                FechaCaducidad = reader.GetDateTime(reader.GetOrdinal("FechaCaducidad"))
                            };
                            producto = alimento;
                        }
                        else if (!reader.IsDBNull(reader.GetOrdinal("NivelToxicidad")))
                        {
                            ProductoQuimico quimico = new ProductoQuimico
                            {
                                IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                NivelToxicidad = reader.GetString(reader.GetOrdinal("NivelToxicidad"))
                            };
                            producto = quimico;
                        }
                        else
                        {
                            producto = new Producto
                            {
                                IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                            };
                        }

                        if (producto != null)
                        {
                            listaProductos.Add(producto);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return listaProductos;
        }

        public bool InsertarProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO productos (IdProducto, Nombre, StockActual, Precio, FechaCaducidad, NivelToxicidad) VALUES (@IdProducto, @Nombre, @StockActual, @Precio, @FechaCaducidad, @NivelToxicidad)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                command.Parameters.AddWithValue("@StockActual", producto.StockActual);
                command.Parameters.AddWithValue("@Precio", producto.Precio);

                // Uso de polimorfismo y operadores 'as' e 'is'
                var productoAlimento = producto as ProductoAlimento;
                var productoQuimico = producto as ProductoQuimico;

                if (productoAlimento != null)
                {
                    command.Parameters.AddWithValue("@FechaCaducidad", productoAlimento.FechaCaducidad);
                    command.Parameters.AddWithValue("@NivelToxicidad", DBNull.Value);
                }
                else if (productoQuimico != null)
                {
                    command.Parameters.AddWithValue("@NivelToxicidad", productoQuimico.NivelToxicidad);
                    command.Parameters.AddWithValue("@FechaCaducidad", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@FechaCaducidad", DBNull.Value);
                    command.Parameters.AddWithValue("@NivelToxicidad", DBNull.Value);
                }

                try
                {
                    connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar producto: " + ex.Message);
                    return false;
                }
            }
        }
        public bool EliminarProducto(int idProducto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Productos WHERE IdProducto = @IdProducto";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdProducto", idProducto);

                try
                {
                    connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el producto: " + ex.Message);
                    return false;
                }
            }
        }
    }
}