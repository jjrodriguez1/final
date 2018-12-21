using DLL;
using Interfaces;
using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repositorios
{
    public class MenuRepository: GenericRepository, IMenuRepository
    {
        public MenuRepository(ILog log) : base(log)
        {

        }

        public bool AltaMenu(Menu menu)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = ConfigurationManager.AppSettings[""];
                    cn.Open();

                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "SP_AltaMenu";
                        cm.Connection = cn;

                        cm.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.VarChar, 100)).Value = menu.IdProducto;
                        cm.Parameters.Add(new SqlParameter("@DescripcionMenu", SqlDbType.VarChar, 200)).Value = menu.DescripcionMenu;

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
                _Log.Error($"EjecutarProcedure AltaMenu Exception: {ex}");
            }

            return resultado;
        }
    }
}
