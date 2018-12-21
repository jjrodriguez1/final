using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class EnvioEmail : Form
    {
        private static Comanda _Comanda = null;
        private static Operador _Operador = null;
        private static int _IdMesa = 0;
        private static decimal _Total = 0;

        private ITempPedidoPorMesaRepository _MesaProRepo = null;
        private IMesaRepository _MesaRepo = null;

        private ILog _Log = null;

        public EnvioEmail()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaProRepo = new TempPedidoPorMesaRepository(_Log);
            _MesaRepo = new MesaRepository(_Log);
        }

        public EnvioEmail(Comanda com, Operador op, int idmesa, decimal total)
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaProRepo = new TempPedidoPorMesaRepository(_Log);
            _MesaRepo = new MesaRepository(_Log);

            _IdMesa = idmesa;
            _Operador = op;
            _Comanda = com;
            _Total = total;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                MenuMozo form = new MenuMozo(_Operador);
                _MesaProRepo.CerrarMesaPedidos(_IdMesa);
                _MesaRepo.CerrarMesa(_IdMesa);
                Close();
                form.Show();
            }
            catch (Exception ex)
            {
                _Log.Error($"EnvioEmail btnCancelar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDir.Text) && !string.IsNullOrEmpty(txtServ.Text))
                {
                    string message = GenerarMensaje(_Comanda, _Total);
                    SendEmail($"{txtDir.Text}@{txtServ.Text}", message);

                    _MesaProRepo.CerrarMesaPedidos(_IdMesa);
                    _MesaRepo.CerrarMesa(_IdMesa);
                    MenuMozo form = new MenuMozo(_Operador);
                    Close();
                    form.Show();

                }
                else
                {
                    MessageBox.Show("Debe completar la dirección de envio");
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"EnvioEmail btnCancelar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private string GenerarMensaje(Comanda comanda, decimal total)
        {
            string retorno = string.Empty;

            try
            {
                retorno = String.Format("Detalle de consumo: {0}{1}{0}. Total facturado:{0}{2}{0}. Gracias Por Elegirnos. Juan TP", Environment.NewLine, comanda.Menu, total.ToString());
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
