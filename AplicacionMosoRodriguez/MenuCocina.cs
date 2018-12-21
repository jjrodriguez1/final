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
    public partial class MenuCocina : Form
    {
        private ILog _Log = null;
        private static Operador _Operador = null;
        private IComandaRepository _ComandaRepo = null;

        public MenuCocina()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
        }

        public MenuCocina(Operador operador)
        {
            InitializeComponent();
            _Operador = operador;
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _ComandaRepo = new ComandaRepository(_Log);
            CargarComandasInicio();
            CargarComandasProceso();
        }

        private void CargarComandasProceso()
        {
            try
            {
                dgvComandasProceso.DataSource = _ComandaRepo.GetComandasDos();
                dgvComandasProceso.Columns[0].Visible = false;
                dgvComandasProceso.Columns[2].Visible = false;
                //dgvComandasProceso.Columns[3].Visible = false;
                dgvComandasProceso.Columns[4].Visible = false;
                dgvComandasProceso.Columns[5].Visible = false;

                if (dgvComandasProceso.RowCount > 0)
                {
                    btnFinalizar.Enabled = true;
                }
                else
                {
                    btnFinalizar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuCocina CargarComandasProceso exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void CargarComandasInicio()
        {
            try
            {
                dgvComandasInicio.DataSource = _ComandaRepo.GetComandasUno();
                dgvComandasInicio.Columns[0].Visible = false;
                //dgvComandasInicio.Columns[2].Visible = false;
                dgvComandasInicio.Columns[3].Visible = false;
                dgvComandasInicio.Columns[4].Visible = false;
                dgvComandasInicio.Columns[5].Visible = false;
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuCocina CargarComandasInicio exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            try
            {
                Comanda ComandaDgv = (Comanda)dgvComandasInicio.CurrentRow.DataBoundItem;
                _ComandaRepo.CambiarEstado(ComandaDgv, "Tomada");
                CargarComandasProceso();
                CargarComandasInicio();
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuCocina btnTake_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Close();
            Login form = new Login();
            form.Show();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                Comanda ComandaDgv = (Comanda)dgvComandasProceso.CurrentRow.DataBoundItem;
                _ComandaRepo.CambiarEstado(ComandaDgv, "Finalizado");
                CargarComandasProceso();
                CargarComandasInicio();
            }
            catch (Exception ex)
            {
                _Log.Error($"MenuCocina btnTake_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }
    }
}
