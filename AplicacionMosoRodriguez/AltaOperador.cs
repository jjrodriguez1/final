using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class AltaOperador : Form
    {
        private ILog _Log = null;
        private IOperadorRepository _OperadorRepo;
        private IEstadoOperadorRepository _EstadoRepo;
        private ITipoOperadorRepository _TipoRepo;
        private static bool isUpdate = false;
        private static int idOp;
        private static string stringPass = string.Empty;

        public AltaOperador()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _OperadorRepo = new OperadorRepository(_Log);
            _EstadoRepo = new EstadoOperadorRepository(_Log);
            _TipoRepo = new TipoOperadorRepository(_Log);
        }


        public AltaOperador(int idUpdate)
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _OperadorRepo = new OperadorRepository(_Log);
            _EstadoRepo = new EstadoOperadorRepository(_Log);
            _TipoRepo = new TipoOperadorRepository(_Log);

            if (idUpdate > 0)
            {
                idOp = idUpdate;
                isUpdate = true;
                Text = "Modificación Operador";
            }
        }

        private void AltaOperador_Load(object sender, EventArgs e)
        {
            CargarTipoOperador();
            CargarEstados();

            if (isUpdate)
                CargarOperadorId(idOp);
        }

        private void CargarOperadorId(int id)
        {
            try
            {
                var operador = _OperadorRepo.GetOperadorById(id);
                SetearOperador(operador);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al modificar operador.");
                this.Close();
            }
        }

        private void SetearOperador(Operador operador)
        {
            txtNombre.Text = operador.Nombre;
            txtEmail.Text = operador.Email;
            txtDireccion.Text = operador.Direccion;
            txtDocumento.Text = operador.Documento;
            txtUsuario.Text = operador.Usuario;
            txtPassword.Text = stringPass = operador.Password;

            cboEstado.SelectedValue = operador.EstadoId;
            cboTipoOperador.SelectedIndex = operador.IdTipoOperador;
        }

        private void CargarTipoOperador()
        {
            try
            {
                TipoOperador seleccion = new TipoOperador();
                seleccion.Id = -1;
                seleccion.Descripcion = "Seleccione";

                List<TipoOperador> list = new List<TipoOperador>();
                list.Add(seleccion);

                var listado = _TipoRepo.GetAllTipoOperador();

                foreach (var item in listado)
                {
                    list.Add(item);
                }

                cboTipoOperador.DataSource = new BindingSource(list, null);
                cboTipoOperador.DisplayMember = "Descripcion";
                cboTipoOperador.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                _Log.Error($"AltaOperador CargarTipoOperador exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void CargarEstados()
        {
            try
            {
                EstadoOperador seleccion = new EstadoOperador();
                seleccion.Id = -1;
                seleccion.Descripcion = "Seleccione";

                List<EstadoOperador> list = new List<EstadoOperador>();
                list.Add(seleccion);

                var listado = _EstadoRepo.GetAllEstados();

                foreach (var item in listado)
                {
                    list.Add(item);
                }

                cboEstado.DataSource = new BindingSource(list, null);
                cboEstado.DisplayMember = "Descripcion";
                cboEstado.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                _Log.Error($"AltaOperador CargarEstados exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ValidarCampos()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    return "Ingrese Nombre.";

                if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    return "Ingrese Email.";

                if (string.IsNullOrEmpty(txtDocumento.Text.Trim()))
                    return "Ingrese Documento.";

                if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
                    return "Ingrese Direccion.";

                if (string.IsNullOrEmpty(txtUsuario.Text.Trim()))
                    return "Ingrese Usuario.";

                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    return "Ingrese Password.";

                if (cboEstado.SelectedIndex == 0)
                    return "Seleccione Estado Inicial.";

                if (cboTipoOperador.SelectedIndex == 0)
                    return "Seleccione Tipo de Operador.";

            }
            catch (Exception ex)
            {
                _Log.Error($"AltaOperador ValidarCampos exception: {ex}");
                return "Ops, se produjo un inconveniente al generar el alta. Intente nuevamente.";
            }

            return string.Empty;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = ValidarCampos();
                if (string.IsNullOrEmpty(mensaje))
                {
                    Operador nuevo = new Operador();

                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Email = txtEmail.Text;
                    nuevo.Direccion = txtDireccion.Text;
                    nuevo.Documento = txtDocumento.Text;
                    nuevo.Usuario = txtUsuario.Text;
                    string basepass = string.Empty;

                    if (!isUpdate)
                    {
                        basepass = Encode(txtPassword.Text);
                    }
                    else if (string.Compare(txtPassword.Text, stringPass) == 0)
                    {
                        basepass = txtPassword.Text;
                        nuevo.Id = idOp;
                    }
                    else
                    {
                        basepass = Encode(txtPassword.Text);
                        nuevo.Id = idOp;
                    }

                    nuevo.Password = basepass;
                    nuevo.EstadoId = (int)cboEstado.SelectedValue;
                    nuevo.IdTipoOperador = (int)cboTipoOperador.SelectedValue;

                    bool resultado = false;

                    if(!isUpdate)
                        resultado = _OperadorRepo.AltaOperador(nuevo);
                    else
                        resultado = _OperadorRepo.ModificarOperador(nuevo);

                    if (resultado)
                    {
                        if (!isUpdate)
                            MessageBox.Show("ALTA EXITOSA.");
                        else
                            MessageBox.Show("MODIFICACIÓN EXITOSA.");

                        this.Close();
                    }
                    else
                        MessageBox.Show("Se produjo un error.");
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"btnAceptar_Click Alta/Modificacion operador: {ex}");
                MessageBox.Show("Se produjo un error, intente mas tarde.");
            }
        }

        private string Encode(string pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(txtPassword.Text);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
