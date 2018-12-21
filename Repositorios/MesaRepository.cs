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
    public class MesaRepository : GenericRepository, IMesaRepository
    {
        public MesaRepository(ILog log) : base(log)
        {

        }

        public bool AltaMesa(Mesa mesa)
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
                        cm.CommandText = "SP_AltaMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@NroMesa", SqlDbType.Int)).Value = mesa.NroMesa;
                        cm.Parameters.Add(new SqlParameter("@EstadoId", SqlDbType.Int)).Value = mesa.EstadoId;
                        cm.Parameters.Add(new SqlParameter("@Asignada", SqlDbType.Int)).Value = mesa.Asignada;

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
                _Log.Error($"EjecutarProcedure AltaMesa Exception: {ex}");
            }

            return resultado;
        }

        public bool EliminarMesa(int id)
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
                        cm.CommandText = "SP_EliminarMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;

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

                        SqlDataReader rd;

                        rd = cm.ExecuteReader();

                        resultado = true;

                        cm.Dispose();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaMesa Exception: {ex}");
            }

            return resultado;
        }

        public bool ExisteMesa(int nroMesa)
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
                        cm.CommandText = "SP_ExisteNroMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@NroMesa", SqlDbType.Int)).Value = nroMesa;

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

                        SqlDataReader rd;

                        rd = cm.ExecuteReader();

                        while (rd.Read())
                        {
                            resultado = true;
                        }

                        cm.Dispose();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EjecutarProcedure AltaMesa Exception: {ex}");
            }

            return resultado;
        }

        public Mesa GetMesa(int Id)
        {
            Mesa registro = new Mesa();

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
                        cm.CommandText = "SP_GetMesaById";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

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
                            registro.NroMesa = rd.GetInt32(1);
                            registro.EstadoId = rd.IsDBNull(2) ? 0 : rd.GetInt32(2);
                            registro.Asignada = rd.GetInt32(3);
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

        public List<MesasLista> GetMesas()
        {
            List<MesasLista> resultado = new List<MesasLista>();
            MesasLista registro;

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
                        cm.CommandText = "SP_GetAllMesas";
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
                            registro = new MesasLista();
                            registro.IdMp = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                            registro.MesaId = rd.GetInt32(1);
                            registro.IdOperador = rd.IsDBNull(2) ? 0 : rd.GetInt32(2);
                            registro.NroMesa = rd.GetInt32(3);
                            registro.Usuario = rd.IsDBNull(4) ? "Sin Asignar" : rd.GetString(4);

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

        public bool AsignarMesa(int idMesaOp, int idMesa, int idOp)
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

                        if (idMesaOp != 0)
                            cm.CommandText = "SP_AsignarMesa";
                        else
                            cm.CommandText = "SP_CreateAsignarMesa";

                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdMesa", SqlDbType.Int)).Value = idMesa;
                        cm.Parameters.Add(new SqlParameter("@IdOperador", SqlDbType.Int)).Value = idOp;
                        cm.Parameters.Add(new SqlParameter("@IdMesaOperador", SqlDbType.Int)).Value = idMesaOp;

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
                _Log.Error($"EjecutarProcedure AltaMesa Exception: {ex}");
            }

            return resultado;
        }

        public List<GenericCombo> GetMesasDispOpe(int idOp)
        {
            List<GenericCombo> resultado = new List<GenericCombo>();
            GenericCombo registro;

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
                        cm.CommandText = "Sp_GetMesasOpUnassign";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdOperador", SqlDbType.Int)).Value = idOp;

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
                            registro = new GenericCombo();
                            registro.Id = rd.GetInt32(0);
                            registro.Descripcion = "Mesa Nro: " + rd.GetInt32(1).ToString();

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
                _Log.Error($"EjecutarProcedure Sp_GetMesasOpUnassign Exception: {ex}");
            }

            return resultado;
        }

        public bool OcuparMesa(int idMesa)
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
                        cm.CommandText = "Sp_OcuparMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdMesa", SqlDbType.Int)).Value = idMesa;

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

                        SqlDataReader rd;

                        rd = cm.ExecuteReader();

                        resultado = true;

                        cm.Dispose();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"OcuparMesa exception: {ex.Message}");
                throw ex;
            }

            return resultado;
        }

        public List<MesasLista> GetMesasOcupadasOperador(int id)
        {
            List<MesasLista> resultado = new List<MesasLista>();
            MesasLista registro;

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
                        cm.CommandText = "SP_GetAllMesasAsigOperador";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdOperador", SqlDbType.Int)).Value = id;

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
                            registro = new MesasLista();
                            registro.IdMp = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                            registro.MesaId = rd.GetInt32(1);
                            registro.IdOperador = id;
                            registro.NroMesa = rd.GetInt32(3);
                            registro.Usuario = rd.GetString(4);

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

        public bool CerrarMesa(int idMesa)
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
                        cm.CommandText = "Sp_CerrarMesa";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdMesa", SqlDbType.Int)).Value = idMesa;

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

                        SqlDataReader rd;

                        rd = cm.ExecuteReader();

                        resultado = true;

                        cm.Dispose();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"CerrarMesa exception: {ex.Message}");
                throw ex;
            }

            return resultado;
        }
    }
}
