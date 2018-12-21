using DLL;
using Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repositorios
{
//Espera 1
//Finalizado 2
//Tomada 3
    public class ComandaRepository : GenericRepository, IComandaRepository
    {
        public ComandaRepository(ILog log) : base(log)
        { }

        public void CambiarEstado(Comanda com, string estado)
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
                        cm.CommandText = "SP_CambiarEstadoComanda";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Descripcion", SqlDbType.VarChar, 20)).Value = estado;
                        cm.Parameters.Add(new SqlParameter($"@Id", SqlDbType.BigInt)).Value = com.Id;

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

                        cm.ExecuteReader();
                        
                        cm.Dispose();
                    }
                    #endregion
                    cn.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure CambiarEstado Exception: {ex}");
            }
        }

        public List<Comanda> GetComandasDos()
        {
            List<Comanda> resultado = new List<Comanda>();
            Comanda registro;

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
                        cm.CommandText = "SP_GetComandasTipoDos";
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
                            registro = new Comanda();
                            registro.Id =  rd.GetInt64(0);
                            registro.Menu = rd.GetString(1);
                            registro.FechaInicio = rd.GetDateTime(2);
                            registro.FechaFin = rd.IsDBNull(3) ? DateTime.Now : rd.GetDateTime(3);
                            registro.IdEstado = rd.GetInt32(4);
                            registro.IdOperador = rd.GetInt32(5);
                            
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

        public List<Comanda> GetComandasUno()
        {
            List<Comanda> resultado = new List<Comanda>();
            Comanda registro;

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
                        cm.CommandText = "SP_GetComandasTipoUno";
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
                            registro = new Comanda();
                            registro.Id = rd.GetInt64(0);
                            registro.Menu = rd.GetString(1);
                            registro.FechaInicio = rd.GetDateTime(2);
                            registro.FechaFin = rd.IsDBNull(3) ? DateTime.Now : rd.GetDateTime(3);
                            registro.IdEstado = rd.GetInt32(4);
                            registro.IdOperador = rd.GetInt32(5);

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

        public void InsertarTicket(int idmesa, int nromesa, Operador operador, decimal total, string descripcion)
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
                        cm.CommandText = "InsertTicket";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@IdMesa", SqlDbType.Int)).Value = idmesa;
                        cm.Parameters.Add(new SqlParameter($"@IdOperador", SqlDbType.Int)).Value = operador.Id;
                        cm.Parameters.Add(new SqlParameter($"@NroMesa", SqlDbType.Int)).Value = nromesa;
                        cm.Parameters.Add(new SqlParameter($"@Operador", SqlDbType.VarChar, 100)).Value = operador.Nombre;
                        cm.Parameters.Add(new SqlParameter($"@Total", SqlDbType.Money)).Value = total;
                        cm.Parameters.Add(new SqlParameter($"@Fecha", SqlDbType.DateTime)).Value = DateTime.Now;
                        cm.Parameters.Add(new SqlParameter($"@Desc", SqlDbType.VarChar,1000)).Value = DateTime.Now;

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

        public bool InsertComanda(Comanda comanda)
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
                        cm.CommandText = "SP_InsertComanda";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Menu", SqlDbType.Int)).Value = comanda.Menu;
                        cm.Parameters.Add(new SqlParameter($"@FechaInicio", SqlDbType.Int)).Value = comanda.FechaInicio;
                        cm.Parameters.Add(new SqlParameter($"@IdEstado", SqlDbType.Int)).Value = comanda.IdEstado;
                        cm.Parameters.Add(new SqlParameter($"@IdOperador", SqlDbType.Int)).Value = comanda.IdOperador;

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

        public bool InsertComandaOperador(int IdOperador, int IdComanda)
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
                        cm.CommandText = "SP_InsertComandaOperador";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@IdOperador", SqlDbType.Int)).Value = IdOperador;
                        cm.Parameters.Add(new SqlParameter($"@IdComanda", SqlDbType.Int)).Value = IdComanda;

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

        public List<ComandaReport> ComandaReporte(DateTime desde, DateTime hasta)
        {
            List<ComandaReport> lista = new List<ComandaReport>();

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
                        cm.CommandText = "SP_Comandas_Ventas";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@FechaDesde", SqlDbType.DateTime)).Value = desde;
                        cm.Parameters.Add(new SqlParameter($"@FechaHasta", SqlDbType.DateTime)).Value = hasta;

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

                        ComandaReport registro;

                        while (rd.Read())
                        {
                            registro = new ComandaReport();

                            registro.Menu = rd.GetString(0);
                            registro.Fedate = rd.GetDateTime(1).ToString();
                            registro.Nombre = rd.GetString(2);
                            
                            lista.Add(registro);
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
                _Log.Error($"EjecutarProcedure ComandaReporte Exception: {ex}");
            }

            return lista;
        }
    }
}
