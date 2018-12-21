using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class GestionOperador : Form
    {
        private ILog _Log = null;
        private IOperadorRepository _OperadorRepo = null;

        public GestionOperador()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _OperadorRepo = new OperadorRepository(_Log);
            CargarGrilla();
        }


        private void CargarGrilla()
        {
            //todo
            dgvOperadores.DataSource = _OperadorRepo.GetOperadores();
            dgvOperadores.Columns[0].Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                OperadorLista operador = (OperadorLista)dgvOperadores.CurrentRow.DataBoundItem;
                AltaOperador form = new AltaOperador(operador.Id);
                form.ShowDialog();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                _Log.Error($"btnEditar_Click Exception: {ex}");
                CargarGrilla();
            }
        }
    }
}
