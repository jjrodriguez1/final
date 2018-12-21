using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class EditarAsignacionMesa : Form
    {
        private ILog _Log = null;
        private IMesaRepository _MesaRepo;
        private IOperadorRepository _OperadorRepo;
        private static int _IdMp;
        private static int _IdOperador;
        private static int _MesaId;
        private static Mesa _mesaStatica = null;
        private static Operador _operadorStatico = null;

        public EditarAsignacionMesa()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaRepo = new MesaRepository(_Log);
            _OperadorRepo = new OperadorRepository(_Log);
        }

        public EditarAsignacionMesa(int? IdMp, int? IdOperador, int MesaId)
        {
            //id de tabla Idmp
            //id operador
            //id de la mesa que se debe pasar a asignado
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaRepo = new MesaRepository(_Log);
            _OperadorRepo = new OperadorRepository(_Log);

            _MesaId = MesaId;
            _IdOperador = IdOperador ?? 0;
            _IdMp = IdMp ?? 0;
            _mesaStatica = _MesaRepo.GetMesa(MesaId);
        }

        private void EditarAsignacionMesa_Load(object sender, EventArgs e)
        {
            CargarOperadores(_IdOperador);
            CargarComboBusqueda();
            if (_IdOperador > 0)
            {
                _operadorStatico = _OperadorRepo.GetOperadorById(_IdOperador);
                lblResultadoOperador.Text = "Asignado a: " + _operadorStatico.Nombre;
                Text = "Edición de mesa nro: " + _mesaStatica.NroMesa;
            }
            else
            {
                Text = "Asignación para mesa nro: " + _mesaStatica.NroMesa;
                lblResultadoOperador.Text = "Asignado a: N//A";
            }
        }

        private void CargarComboBusqueda()
        {
            try
            {
                List<GenericCombo> list = new List<GenericCombo>();
                GenericCombo seleccion = new GenericCombo();

                seleccion.Id = -1;
                seleccion.Descripcion = "Seleccione";
                list.Add(seleccion);

                seleccion = new GenericCombo();
                seleccion.Id = 1;
                seleccion.Descripcion = "Nombre";
                list.Add(seleccion);

                seleccion = new GenericCombo();
                seleccion.Id = 2;
                seleccion.Descripcion = "CI";
                list.Add(seleccion);

                cboFiltroOperador.DataSource = new BindingSource(list, null);
                cboFiltroOperador.DisplayMember = "Descripcion";
                cboFiltroOperador.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                _Log.Error($"EditAsignarMesa CargarComboBusqueda exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void CargarOperadores(int id)
        {
            try
            {
                var operador = _OperadorRepo.GetOperadores();
                SetOperador(operador);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Cargar operador.");
                Close();
            }
        }

        private void SetOperador(List<OperadorLista> lista)
        {
            try
            {
                try
                {
                    dgvOperadores.DataSource = lista;
                    dgvOperadores.Columns[0].Visible = false;
                    dgvOperadores.Columns[2].Visible = false;
                    dgvOperadores.Columns[3].Visible = false;
                    dgvOperadores.Columns[4].Visible = false;
                    dgvOperadores.Columns[6].Visible = false;
                    dgvOperadores.Columns[7].Visible = false;
                }
                catch (Exception ex)
                {
                    _Log.Error($"CargarGrilla SetOperador Exception: {ex}");
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"Error: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            GestionMesas form = new GestionMesas();
            form.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboFiltroOperador.SelectedIndex > 0 || string.IsNullOrEmpty(txtFiltroOperador.Text.TrimStart().TrimEnd().Trim()))
            {
                if (cboFiltroOperador.SelectedIndex == 1)
                {
                    var operador = _OperadorRepo.GetByName(txtFiltroOperador.Text);
                    SetOperador(operador);
                }
                else if (cboFiltroOperador.SelectedIndex == 2)
                {
                    if (txtFiltroOperador.Text.Length > 20)
                    {
                        var operador = _OperadorRepo.GetByDocument(txtFiltroOperador.Text.Substring(0, 19));
                        SetOperador(operador);
                    }
                    else
                    {
                        var operador = _OperadorRepo.GetByDocument(txtFiltroOperador.Text);
                        SetOperador(operador);
                    }
                }
            }
            else
            {
                var operador = _OperadorRepo.GetOperadores();
                SetOperador(operador);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            OperadorLista operador = (OperadorLista)dgvOperadores.CurrentRow.DataBoundItem;

            DialogResult result = MessageBox.Show($"Desea asignar mesa nro: {_mesaStatica.NroMesa} a {operador.Nombre}", "Asignado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result.Equals(DialogResult.OK))
            {
                bool respuesta = _MesaRepo.AsignarMesa(_IdMp, _mesaStatica.Id, operador.Id);

                if (respuesta)
                {
                    MessageBox.Show("Asignación exitosa");
                }
                else
                {
                    MessageBox.Show("No se pudo asignar mesa");
                }

                Close();
                GestionMesas form = new GestionMesas();
                form.Show();
            }
            else
            {
                var operadores = _OperadorRepo.GetOperadores();
                SetOperador(operadores);
            }
        }
    }
}
