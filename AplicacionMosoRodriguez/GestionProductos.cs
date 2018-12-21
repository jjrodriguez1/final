using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class GestionProductos : Form
    {
        private ILog _Log = null;
        private IProductoRepository _ProductoRepo = null;

        public GestionProductos()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _ProductoRepo = new ProductoRepository(_Log);
            CargarGrilla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CargarGrilla()
        {
            try
            {
                dgvProductos.DataSource = _ProductoRepo.GetProductos();
                dgvProductos.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                _Log.Error($"CargarGrilla Exception: {ex}");
            }
            
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            try
            {
                ProductosLista operador = (ProductosLista)dgvProductos.CurrentRow.DataBoundItem;
                AltaProducto form = new AltaProducto(operador.Id);
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
