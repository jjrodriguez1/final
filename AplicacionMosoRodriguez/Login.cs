using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class Login : Form
    {
        private ILog _Log = null;
        private IOperadorRepository _OperadorRepo;
        public Login()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _OperadorRepo = new OperadorRepository(_Log);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Log.Info($"Iniciando sesion usuario: {txtUsuario.Text}");

            try
            {
                if (ValidarIngresos())
                {
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(txtPassword.Text);
                    string basepass = Convert.ToBase64String(plainTextBytes);

                    Operador resultado = _OperadorRepo.LoginOperador(txtUsuario.Text, basepass);

                    if (resultado != null && resultado.Id != 0)
                    {
                        switch (resultado.IdTipoOperador)
                        {
                            case 1:
                                Hide();
                                MenuGerente formUno = new MenuGerente(resultado);
                                formUno.Show();
                                break;
                            case 2:
                                Hide();
                                MenuMozo formDos = new MenuMozo(resultado);
                                formDos.Show();
                                break;
                            case 3:
                                Hide();
                                MenuCocina formTres = new MenuCocina(resultado);
                                formTres.Show();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña invalida.");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese usuario y contraseña.");
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"button1_Click exception: {ex}");
                MessageBox.Show("No se puede ingresar en este momento. Favor de contactar a sistemas.");
            }
        }

        private bool ValidarIngresos()
        {
            bool retorno = true;

            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text))
                    retorno = false;

                if (string.IsNullOrEmpty(txtPassword.Text))
                    retorno = false;
            }
            catch (Exception ex)
            {
                _Log.Error($"ValidarIngresos exception: {ex}");
                retorno = false;
                MessageBox.Show("No se puede ingresar en este momento. Favor de contactar a sistemas.");
            }

            return retorno;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NADA
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //NADA
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, e);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
