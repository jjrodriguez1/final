using System;
using System.Collections.Generic;
using DLL;
using Interfaces;
using log4net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Repositorios
{
    public class EstadoOperadorRepository: GenericRepository, IEstadoOperadorRepository
    {
        public EstadoOperadorRepository(ILog log) : base(log)
        {

        }

        public List<EstadoOperador> GetAllEstados()
        {
            List<EstadoOperador> resultado = new List<EstadoOperador>();

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
                        cm.CommandText = "SP_GetAllEstadoOperador";
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

                        SqlDataReader lector = cm.ExecuteReader();
                        EstadoOperador reg;

                        while (lector.Read())
                        {
                            reg = new EstadoOperador();
                            reg.Id = lector.GetInt32(0);
                            reg.Descripcion = lector.GetString(1);

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
    }
}
