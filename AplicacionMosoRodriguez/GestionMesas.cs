using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class GestionMesas : Form
    {
        private ILog _Log = null;
        private IMesaRepository _MesasRepo = null;

        public GestionMesas()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesasRepo = new MesaRepository(_Log);
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                dgvMesas.DataSource = _MesasRepo.GetMesas();
                dgvMesas.Columns[0].Visible = false;
                dgvMesas.Columns[1].Visible = false;
                dgvMesas.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                _Log.Error($"CargarGrilla Exception: {ex}");
            }

        }

        private void GestionMesas_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                MesasLista mesas = (MesasLista)dgvMesas.CurrentRow.DataBoundItem;
                bool resultado = _MesasRepo.EliminarMesa(mesas.MesaId);

                if (resultado)
                    MessageBox.Show("Mesa eliminada");
                else
                    MessageBox.Show("Mesa no pudo ser eliminada");

                CargarGrilla();
            }
            catch (Exception ex)
            {
                _Log.Error($"btnEditar_Click Exception: {ex}");
                CargarGrilla();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //TODO
            MesasLista mesas = (MesasLista)dgvMesas.CurrentRow.DataBoundItem;
            EditarAsignacionMesa form = new EditarAsignacionMesa(mesas.IdMp, mesas.IdOperador, mesas.MesaId);
            form.Show();
            Close();
        }
    }
}
