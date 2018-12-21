using DLL;
using Interfaces;
using log4net;
using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Repositorios
{
    public class ProductoRepository : GenericRepository, IProductoRepository
    {
        public ProductoRepository(ILog log) : base(log)
        {

        }

        public bool AltaProducto(Producto producto)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings["TPPROG"];
                    cn.Open();

                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_CrearProducto";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Descripcion", SqlDbType.VarChar, 200)).Value = producto.Descripcion;
                        cm.Parameters.Add(new SqlParameter($"@SKU", SqlDbType.VarChar, 20)).Value = producto.Sku;
                        cm.Parameters.Add(new SqlParameter($"@Stock", SqlDbType.Int)).Value = producto.Stock;
                        cm.Parameters.Add(new SqlParameter($"@Precio", SqlDbType.Money)).Value = producto.Precio;

                        #region Log
                        string log = $"EXEC {cm.CommandText} ";
                        foreach (SqlParameter i in cm.Parameters)
                        {
                            switch (i.SqlDbType)
                            {
                                case SqlDbType.VarChar:
                                    log += String.Format("'{0}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                case SqlDbType.DateTime:
                                    log += String.Format("'{0:yyyyMMdd HH:mm:ss.fff}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                default:
                                    log += String.Format("{0},", (i.Value == null ? "NULL" : i.Value));
                                    break;
                            }

                        }

                        log = log.Substring(0, log.Length - 1);
                        _Log.Info(log);
                        #endregion

                        cm.ExecuteNonQuery();

                        cm.Dispose();
                    }

                    cn.Close();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaProducto Exception: {ex}");
            }

            return resultado;
        }

        public void DescontarCantidad(int cant, int idprod)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings["TPPROG"];
                    cn.Open();

                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_DescontarCantidad";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Cantidad", SqlDbType.Int)).Value = cant;
                        cm.Parameters.Add(new SqlParameter($"@IdProducto", SqlDbType.Int)).Value = idprod;

                        #region Log
                        string log = $"EXEC {cm.CommandText} ";
                        foreach (SqlParameter i in cm.Parameters)
                        {
                            switch (i.SqlDbType)
                            {
                                case SqlDbType.VarChar:
                                    log += String.Format("'{0}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                case SqlDbType.DateTime:
                                    log += String.Format("'{0:yyyyMMdd HH:mm:ss.fff}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                default:
                                    log += String.Format("{0},", (i.Value == null ? "NULL" : i.Value));
                                    break;
                            }

                        }

                        log = log.Substring(0, log.Length - 1);
                        _Log.Info(log);
                        #endregion

                        cm.ExecuteNonQuery();

                        cm.Dispose();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure DescontarCantidad Exception: {ex}");
                throw ex;
            }
        }

        public Producto GetOperadorById(int Id)
        {
            Producto registro = new Producto();

            try
            {
                #region Using SQL Connection
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings["TPPROG"];
                    cn.Open();

                    #region Using Sql Command
                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_GetProductoById";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@id", SqlDbType.Int)).Value = Id;

                        #region Log
                        string log = $"EXEC {cm.CommandText} ";
                        foreach (SqlParameter i in cm.Parameters)
                        {
                            switch (i.SqlDbType)
                            {
                                case SqlDbType.VarChar:
                                    log += String.Format("'{0}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                case SqlDbType.DateTime:
                                    log += String.Format("'{0:yyyyMMdd HH:mm:ss.fff}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                default:
                                    log += String.Format("{0},", (i.Value == null ? "NULL" : i.Value));
                                    break;
                            }

                        }

                        log = log.Substring(0, log.Length - 1);
                        _Log.Info(log);
                        #endregion

                        SqlDataReader rd = cm.ExecuteReader();

                        while (rd.Read())
                        {
                            registro.Id = rd.GetInt32(0);
                            registro.Precio = rd.GetDecimal(1);
                            registro.Stock = rd.GetInt32(2);
                            registro.Descripcion = string.IsNullOrEmpty(rd.GetString(3)) ? string.Empty : rd.GetString(3);
                            registro.Sku = string.IsNullOrEmpty(rd.GetString(4)) ? string.Empty : rd.GetString(4);
                        }

                        cm.Dispose();
                    }
                    #endregion
                    cn.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure GETPRODUCTOID Exception: {ex}");
            }

            return registro;
        }

        public List<ProductosLista> GetProductos()
        {
            List<ProductosLista> resultado = new List<ProductosLista>();
            ProductosLista registro;

            try
            {
                #region Using SQL Connection
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings["TPPROG"];
                    cn.Open();

                    #region Using Sql Command
                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_GetAllProductos";
                        cm.Connection = cn;

                        #region Log
                        string log = $"EXEC {cm.CommandText} ";
                        foreach (SqlParameter i in cm.Parameters)
                        {
                            switch (i.SqlDbType)
                            {
                                case SqlDbType.VarChar:
                                    log += String.Format("'{0}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                case SqlDbType.DateTime:
                                    log += String.Format("'{0:yyyyMMdd HH:mm:ss.fff}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                default:
                                    log += String.Format("{0},", (i.Value == null ? "NULL" : i.Value));
                                    break;
                            }

                        }

                        log = log.Substring(0, log.Length - 1);
                        _Log.Info(log);
                        #endregion

                        SqlDataReader rd = cm.ExecuteReader();

                        while (rd.Read())
                        {
                            registro = new ProductosLista();
                            registro.Id = rd.GetInt32(0);
                            registro.Precio = rd.GetDecimal(1);
                            registro.Stock = rd.GetInt32(2);
                            registro.Descripcion = string.IsNullOrEmpty(rd.GetString(3)) ? string.Empty : rd.GetString(3);
                            registro.Sku = string.IsNullOrEmpty(rd.GetString(4)) ? string.Empty : rd.GetString(4);

                            resultado.Add(registro);
                        }

                        cm.Dispose();
                    }
                    #endregion
                    cn.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaOperador Exception: {ex}");
            }

            return resultado;
        }

        public bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings["TPPROG"];
                    cn.Open();

                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_ModificarProducto";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Id", SqlDbType.Int)).Value = producto.Id;
                        cm.Parameters.Add(new SqlParameter($"@Descripcion", SqlDbType.VarChar, 200)).Value = producto.Descripcion;
                        cm.Parameters.Add(new SqlParameter($"@SKU", SqlDbType.VarChar, 20)).Value = producto.Sku;
                        cm.Parameters.Add(new SqlParameter($"@Stock", SqlDbType.Int)).Value = producto.Stock;
                        cm.Parameters.Add(new SqlParameter($"@Precio", SqlDbType.Money)).Value = producto.Precio;

                        #region Log
                        string log = $"EXEC {cm.CommandText} ";
                        foreach (SqlParameter i in cm.Parameters)
                        {
                            switch (i.SqlDbType)
                            {
                                case SqlDbType.VarChar:
                                    log += String.Format("'{0}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                case SqlDbType.DateTime:
                                    log += String.Format("'{0:yyyyMMdd HH:mm:ss.fff}',", (i.Value == null ? "NULL" : i.Value));
                                    break;
                                default:
                                    log += String.Format("{0},", (i.Value == null ? "NULL" : i.Value));
                                    break;
                            }

                        }

                        log = log.Substring(0, log.Length - 1);
                        _Log.Info(log);
                        #endregion

                        cm.ExecuteNonQuery();

                        cm.Dispose();
                    }

                    cn.Close();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaProducto Exception: {ex}");
            }

            return resultado;
        }
    }
}
