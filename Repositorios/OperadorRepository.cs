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
    public class OperadorRepository : GenericRepository, IOperadorRepository
    {
        public OperadorRepository(ILog log) : base(log)
        {

        }

        public bool AltaOperador(Operador operador)
        {
            bool resultado = false;

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
                        cm.CommandText = "SP_CreateNewOperador";
                        cm.Connection = cn;


                        cm.Parameters.Add(new SqlParameter($"@Nombre", SqlDbType.VarChar, 100)).Value = operador.Nombre;
                        cm.Parameters.Add(new SqlParameter($"@Email", SqlDbType.VarChar, 100)).Value = operador.Email;
                        cm.Parameters.Add(new SqlParameter($"@EstadoId", SqlDbType.Int)).Value = operador.EstadoId;
                        cm.Parameters.Add(new SqlParameter($"@IdTipoOperador", SqlDbType.Int)).Value = operador.IdTipoOperador;
                        cm.Parameters.Add(new SqlParameter($"@Documento", SqlDbType.VarChar, 20)).Value = operador.Documento;
                        cm.Parameters.Add(new SqlParameter($"@Direccion", SqlDbType.VarChar, 100)).Value = operador.Direccion;
                        cm.Parameters.Add(new SqlParameter($"@Usuario", SqlDbType.VarChar, 20)).Value = operador.Usuario;
                        cm.Parameters.Add(new SqlParameter($"@Password", SqlDbType.VarChar, 20)).Value = operador.Password;

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
                    resultado = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaOperador Exception: {ex}");
            }

            return resultado;
        }

        public List<OperadorLista> GetByDocument(string documento)
        {
            List<OperadorLista> resultado = new List<OperadorLista>();
            OperadorLista registro;

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
                        cm.CommandText = "SP_GetAllOperadoresByDocument";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Documento", SqlDbType.VarChar, 20)).Value = documento.ToUpper();

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
                            registro = new OperadorLista();
                            registro.Id = rd.GetInt32(0);
                            registro.Nombre = rd.GetString(1);
                            registro.Email = string.IsNullOrEmpty(rd.GetString(2)) ? string.Empty : rd.GetString(2);
                            registro.Estado = rd.GetString(3);
                            registro.TipoOperador = rd.GetString(4);
                            registro.Documento = rd.GetString(5);
                            registro.Direccion = rd.GetString(6);
                            registro.Usuario = rd.GetString(7);

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
                _Log.Error($"EjecutarProcedure GetByDocument Exception: {ex}");
            }

            return resultado;
        }

        public List<OperadorLista> GetByName(string nombre)
        {
            List<OperadorLista> resultado = new List<OperadorLista>();
            OperadorLista registro;

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
                        cm.CommandText = "SP_GetAllOperadoresByName";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Nombre", SqlDbType.VarChar, 100)).Value = nombre.ToUpper();

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
                            registro = new OperadorLista();
                            registro.Id = rd.GetInt32(0);
                            registro.Nombre = rd.GetString(1);
                            registro.Email = string.IsNullOrEmpty(rd.GetString(2)) ? string.Empty : rd.GetString(2);
                            registro.Estado = rd.GetString(3);
                            registro.TipoOperador = rd.GetString(4);
                            registro.Documento = rd.GetString(5);
                            registro.Direccion = rd.GetString(6);
                            registro.Usuario = rd.GetString(7);

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
                _Log.Error($"EjecutarProcedure GetByName Exception: {ex}");
            }

            return resultado;
        }

        public Operador GetOperadorById(int id)
        {
            Operador registro = new Operador();

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
                        cm.CommandText = "SP_GetOperadorById";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@id", SqlDbType.Int)).Value = id;

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
                            registro.Nombre = rd.GetString(1);
                            registro.Email = string.IsNullOrEmpty(rd.GetString(2)) ? string.Empty : rd.GetString(2);
                            registro.EstadoId = rd.GetInt32(3);
                            registro.IdTipoOperador = rd.GetInt32(4);
                            registro.Documento = rd.GetString(5);
                            registro.Direccion = rd.GetString(6);
                            registro.Usuario = rd.GetString(7);
                            registro.Password = rd.GetString(8);
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

            return registro;
        }

        public List<OperadorLista> GetOperadores()
        {
            List<OperadorLista> resultado = new List<OperadorLista>();
            OperadorLista registro;

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
                        cm.CommandText = "SP_GetAllOperadores";
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
                            registro = new OperadorLista();
                            registro.Id = rd.GetInt32(0);
                            registro.Nombre = rd.GetString(1);
                            registro.Email = string.IsNullOrEmpty(rd.GetString(2)) ? string.Empty : rd.GetString(2);
                            registro.Estado = rd.GetString(3);
                            registro.TipoOperador = rd.GetString(4);
                            registro.Documento = rd.GetString(5);
                            registro.Direccion = rd.GetString(6);
                            registro.Usuario = rd.GetString(7);

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

        public Operador LoginOperador(string usuario, string password)
        {
            Operador resultado = new Operador();

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
                        cm.CommandText = "SP_GetOperadorByUserPass";
                        cm.Connection = cn;


                        cm.Parameters.Add(new SqlParameter($"@usuario", SqlDbType.VarChar, 20)).Value = usuario;
                        cm.Parameters.Add(new SqlParameter($"@password", SqlDbType.VarChar, 20)).Value = password;

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

                        while (lector.Read())
                        {
                            resultado.Id = lector.GetInt32(0);
                            resultado.Nombre = lector.GetString(1);
                            resultado.Email = lector.GetString(2);
                            resultado.FechaAlta = lector.GetDateTime(3);
                            resultado.EstadoId = lector.GetInt32(4);
                            resultado.IdTipoOperador = lector.GetInt32(5);
                            resultado.Documento = lector.GetString(6);
                            resultado.Direccion = lector.GetString(7);
                            resultado.Usuario = lector.GetString(8);   
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
                _Log.Error($"EjecutarProcedure LoginOperador Exception: {ex}");
            }

            return resultado;
        }

        public bool ModificarOperador(Operador operador)
        {
            bool resultado = false;

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
                        cm.CommandText = "SP_ModificarOperador";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter($"@Id", SqlDbType.VarChar, 100)).Value = operador.Id;
                        cm.Parameters.Add(new SqlParameter($"@Nombre", SqlDbType.VarChar, 100)).Value = operador.Nombre;
                        cm.Parameters.Add(new SqlParameter($"@Email", SqlDbType.VarChar, 100)).Value = operador.Email;
                        cm.Parameters.Add(new SqlParameter($"@EstadoId", SqlDbType.Int)).Value = operador.EstadoId;
                        cm.Parameters.Add(new SqlParameter($"@IdTipoOperador", SqlDbType.Int)).Value = operador.IdTipoOperador;
                        cm.Parameters.Add(new SqlParameter($"@Documento", SqlDbType.VarChar, 20)).Value = operador.Documento;
                        cm.Parameters.Add(new SqlParameter($"@Direccion", SqlDbType.VarChar, 100)).Value = operador.Direccion;
                        cm.Parameters.Add(new SqlParameter($"@Usuario", SqlDbType.VarChar, 20)).Value = operador.Usuario;
                        cm.Parameters.Add(new SqlParameter($"@Password", SqlDbType.VarChar, 20)).Value = operador.Password;

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

                        int afectado = cm.ExecuteNonQuery();

                        cm.Dispose();
                    }
                    #endregion
                    cn.Close();
                    resultado = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaOperador Exception: {ex}");
            }

            return resultado;
        }

        public List<VentaReport> VentasReporte(DateTime desde, DateTime hasta)
        {
            List<VentaReport> lista = new List<VentaReport>();

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
                        cm.CommandText = "SP_Top_Ventas";
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

                        VentaReport registro;

                        while (rd.Read())
                        {
                            registro = new VentaReport();

                            registro.Operador = rd.GetString(0);
                            registro.Ventas = rd.GetDecimal(1);
                            registro.Fedate = rd.GetDateTime(2).ToString();
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
                _Log.Error($"EjecutarProcedure VentasReporte Exception: {ex}");
            }

            return lista;
        }
    }
}
