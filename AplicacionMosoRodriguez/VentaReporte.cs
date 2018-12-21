using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class VentaReporte : Form
    {
        private ILog _Log = null;
        private IOperadorRepository _OpRepo = null;

        public VentaReporte()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _OpRepo = new OperadorRepository(_Log);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Validar()))
                {
                    var lista = _OpRepo.VentasReporte(DateTime.Parse(dtpDesde.Value.ToString("yyyy-MM-dd")), DateTime.Parse(dtpFechaHasta.Value.ToString("yyyy-MM-dd")));

                    if (lista.Count > 0)
                    {
                        string mensaje = GenerarMensaje(lista);
                        SendEmail(ConfigurationManager.AppSettings["EmailGerente"], mensaje);
                        Close();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"VentaReporte exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private string Validar()
        {
            string retorno = string.Empty;

            try
            {
                DateTime desde = DateTime.Parse(dtpDesde.Value.ToString("yyyy-MM-dd"));
                DateTime hasta = DateTime.Parse(dtpFechaHasta.Value.ToString("yyyy-MM-dd"));

                if (desde.Year > DateTime.Now.Year || desde.Month > DateTime.Now.Month || desde.Day > DateTime.Now.Day)
                {
                    retorno = "Fecha Desde es mayor al dia de la fecha";
                }

                if (hasta.Year > DateTime.Now.Year || hasta.Month > DateTime.Now.Month || hasta.Day > DateTime.Now.Day)
                {
                    retorno = "Fecha Hasta es mayor al dia de la fecha";
                }

                if (hasta.Year < desde.Year || hasta.Month < desde.Month || hasta.Day < desde.Day)
                {
                    retorno = "Fecha Hasta es menor a la fecha de inicio";
                }

            }
            catch (Exception ex)
            {
                _Log.Error($"ComandaReporte Validar exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
                retorno = "Error en validacion";
            }

            return retorno;
        }

        private string GenerarMensaje(List<VentaReport> lista)
        {
            string retorno = string.Empty;

            try
            {
                foreach (var item in lista)
                {
                    retorno += String.Format("Operador de Venta: {0}{1}{0}. Total facturado:{0}{2}{0}. Fecha:{0}{3}{0}.", Environment.NewLine, item.Operador, item.Ventas.ToString(), item.Fedate);
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EnvioEmail GenerarMensaje exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }

            return retorno;
        }

        private bool SendEmail(string email, string mensaje)
        {
            bool retorno = false;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                mail.To.Add(email);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["from"]);
                mail.Subject = "Ticket no fiscal.";
                mail.Body = mensaje;
                //https://myaccount.google.com/lesssecureapps
                client.Port = 587;//25;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usr"], ConfigurationManager.AppSettings["psw"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mail);
                retorno = true;
            }
            catch (Exception ex)
            {
                _Log.Error($"EnvioEmail SendEmail exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }

            return retorno;
        }
    }
}
