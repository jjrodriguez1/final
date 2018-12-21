using DLL;
using Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class TempPedidoPorMesaRepository : GenericRepository, ITempPedidoPorMesaRepository
    {
        public TempPedidoPorMesaRepository(ILog log): base (log)
        { }
        
        public bool InsertTempPedidoMesa(int idmesa, int idprod)
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
                        cm.CommandText = "SP_InsertPedidosMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@IdMesa", SqlDbType.Int)).Value = idmesa;
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
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaProducto Exception: {ex}");
            }

            return resultado;
        }

        public List<TempPedidoPorMesa> GetAllByIdMesa(int id)
        {
            List<TempPedidoPorMesa> resultado = new List<TempPedidoPorMesa>();

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
                        cm.CommandText = "SP_GetAllPedidosMesa";
                        cm.Connection = cn;
                        cm.Parameters.Add(new SqlParameter($"@IdMesa", SqlDbType.Int)).Value = id;
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

                        SqlDataReader lector = cm.ExecuteReader();
                        TempPedidoPorMesa reg;

                        while (lector.Read())
                        {
                            reg = new TempPedidoPorMesa();
                            reg.Id = 0;
                            reg.IdMesa = id;
                            reg.IdProducto = lector.GetInt32(0);
                            reg.Descripcion = lector.GetString(2);
                            reg.Cantidad = lector.GetInt32(1);
                            reg.Subtotal = lector.GetInt32(1) * lector.GetDecimal(3);
                            reg.StockDisp = lector.GetInt32(4);

                            resultado.Add(reg);
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
                _Log.Error($"EjecutarProcedure Exception: {ex}");
            }

            return resultado;
        }

        public void CerrarMesaPedidos(int idmesa)
        {
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
                        cm.CommandText = "SP_CloseAllPedidosMesa";
                        cm.Connection = cn;
                        cm.Parameters.Add(new SqlParameter($"@IdMesa", SqlDbType.Int)).Value = idmesa;
                        
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
                    #endregion
                    cn.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure Exception: {ex}");
            }
        }

        public void RemoverItem(TempPedidoPorMesa item)
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
                        cm.CommandText = "SP_RemovePedidosMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@IdMesa", SqlDbType.Int)).Value = item.IdMesa;
                        cm.Parameters.Add(new SqlParameter($"@IdProducto", SqlDbType.Int)).Value = item.IdProducto;

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
                _Log.Error($"EjecutarProcedure AltaProducto Exception: {ex}");
            }
        }
    }
}
