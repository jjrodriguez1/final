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
    public partial class MenuMozo : Form
    {
        private static Operador _Operador = null;
        private IMesaRepository _MesaRepo = null;
        private ITempPedidoPorMesaRepository _MesaProd = null;
        private ILog _Log = null;

        public MenuMozo()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
        }

        public MenuMozo(Operador operador)
        {
            InitializeComponent();
            _Operador = operador;
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaProd = new TempPedidoPorMesaRepository(_Log);
            _MesaRepo = new MesaRepository(_Log);
            CargarMesasPorOperador(_Operador.Id);
            CargarListMesasOcupadas(_Operador.Id);
        }

        private void CargarMesasPorOperador(int id)
        {
            try
            {
                GenericCombo seleccion = new GenericCombo();
                seleccion.Id = -1;
                seleccion.Descripcion = "Seleccione";

                List<GenericCombo> list = new List<GenericCombo>();
                list.Add(seleccion);

                var listado = _MesaRepo.GetMesasDispOpe(id);

                foreach (var item in listado)
                {
                    list.Add(item);
                }

                cboMesasDispo.DataSource = new BindingSource(list, null);
                cboMesasDispo.DisplayMember = "Descripcion";
                cboMesasDispo.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuMozo CargarMesasPorOperador exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Close();
            Login form = new Login();
            form.Show();
        }

        private void btnOcupar_Click(object sender, EventArgs e)
        {
            try
            {
                var listado = _MesaRepo.OcuparMesa(int.Parse(cboMesasDispo.SelectedValue.ToString()));
                CargarListMesasOcupadas(_Operador.Id);
                CargarMesasPorOperador(_Operador.Id);
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuMozo btnOcupar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void CargarListMesasOcupadas(int id)
        {
            try
            {
                dgvMesas.DataSource = _MesaRepo.GetMesasOcupadasOperador(id);
                dgvMesas.Columns[0].Visible = false;
                dgvMesas.Columns[1].Visible = false;
                dgvMesas.Columns[2].Visible = false;

                dgvMesas.Visible = true;
                btnGestionar.Visible = true;
                btnCerrar.Visible = true;
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuMozo CargarListMesasOcupadas exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                MesasLista mesaDgv = (MesasLista)dgvMesas.CurrentRow.DataBoundItem;
                _MesaRepo.CerrarMesa(mesaDgv.MesaId);
                CargarMesasPorOperador(_Operador.Id);
                CargarListMesasOcupadas(_Operador.Id);
                _MesaProd.CerrarMesaPedidos(mesaDgv.MesaId);
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuMozo btnCerrar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnGestionar_Click(object sender, EventArgs e)
        {
            try
            {
                MesasLista mesaDgv = (MesasLista)dgvMesas.CurrentRow.DataBoundItem;
                GesationMesaOcupada form = new GesationMesaOcupada(mesaDgv.MesaId, mesaDgv.NroMesa, _Operador);
                form.Show();
                Close();
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuMozo btnGestionar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
            
        }
    }
}
