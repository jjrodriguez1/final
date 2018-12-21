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
    public partial class AltaMesa : Form
    {
        private ILog _Log = null;
        private IMesaRepository _MesaRepo = null;

        public AltaMesa()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaRepo = new MesaRepository(_Log);
        }

        private void btnCrearMesa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMesaNumero.Text))
                {
                    if (!ValidarMesaNro())
                    {
                        Mesa mesa = new Mesa();

                        mesa.NroMesa = int.Parse(txtMesaNumero.Text);
                        mesa.EstadoId = 2;
                        mesa.Asignada = 0;

                        if (_MesaRepo.AltaMesa(mesa))
                            MessageBox.Show($"Alta de mesa exitosa.");
                        else
                            MessageBox.Show("No se pudo generar el alta de mesa. Intente mas tarde.");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Numero de mesa existente.");
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar numero de mesa.");
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"Alta Mesa btnCrearMesa_Click Exception: {ex}");
                MessageBox.Show("No se pudo generar el alta de mesa. Intente mas tarde.");
            }
        }

        private bool ValidarMesaNro()
        {

            try
            {
                return _MesaRepo.ExisteMesa(int.Parse(txtMesaNumero.Text));
            }
            catch (Exception ex)
            {
                _Log.Error($"Alta Mesa ValidarMesaNro Exception: {ex}");
                MessageBox.Show("No se pudo generar el alta de mesa. Intente mas tarde.");
                return true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
