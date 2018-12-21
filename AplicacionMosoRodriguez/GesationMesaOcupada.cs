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
    public partial class GesationMesaOcupada : Form
    {
        private static Operador _Operador = null;
        private static List<TempPedidoPorMesa> regs;
        private IProductoRepository _ProdRepo = null;
        private ITempPedidoPorMesaRepository _MesaProRepo = null;
        private IComandaRepository _ComandaRepo = null;
        private ITempPedidoPorMesaRepository _MesaProd = null;
        private IMesaRepository _MesaRepo = null;
        private static int _IdMesa = 0;
        private static decimal _Total = 0;
        private static int _NroMesa = 0;
        private ILog _Log = null;

        public GesationMesaOcupada()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _MesaProRepo = new TempPedidoPorMesaRepository(_Log);
            _ComandaRepo = new ComandaRepository(_Log);
            _MesaProd = new TempPedidoPorMesaRepository(_Log);
            _MesaRepo = new MesaRepository(_Log);
        }

        public GesationMesaOcupada(int idMesa, int nroMesa, Operador operador)
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _ProdRepo = new ProductoRepository(_Log);
            _MesaProRepo = new TempPedidoPorMesaRepository(_Log);
            _ComandaRepo = new ComandaRepository(_Log);
            _MesaProd = new TempPedidoPorMesaRepository(_Log);
            _MesaRepo = new MesaRepository(_Log);
            _NroMesa = nroMesa;
            Text = $"Gestion de mesa nro: {nroMesa}";
            _IdMesa = idMesa;
            _Operador = operador;
            regs = new List<TempPedidoPorMesa>();
            CargarGrillaProductos();
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            try
            {
                CargarListadoPedido();
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada HabilitarBotones exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void CargarListadoPedido()
        {
            try
            {
                regs = _MesaProRepo.GetAllByIdMesa(_IdMesa);
                dgvProdList.DataSource = regs;
                dgvProdList.Columns[0].Visible = false;
                dgvProdList.Columns[1].Visible = false;
                dgvProdList.Columns[2].Visible = false;
                //dgvProdList.Columns[5].Visible = false;
                dgvProdList.Columns[6].Visible = false;

                if (dgvProdList.RowCount > 0)
                {
                    btnEmail.Enabled = true;
                    btnRemover.Enabled = true;
                    _Total = CalcularTotal(regs);
                    lblTotal.Text = $"Total: ${_Total}";
                }
                else
                {
                    lblTotal.Text = $"Total: $0";
                    btnEmail.Enabled = false;
                    btnRemover.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada CargarListadoPedido exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private decimal CalcularTotal(List<TempPedidoPorMesa> regs)
        {
            decimal retorno = 0;

            try
            {
                foreach (var item in regs)
                {
                    retorno += item.Subtotal;
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada CalcularTotal exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }

            return retorno;
        }

        private void CargarGrillaProductos()
        {
            try
            {
                dgvProducto.DataSource = _ProdRepo.GetProductos();
                dgvProducto.Columns[0].Visible = false;
                dgvProducto.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada CargarGrillaProductos exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            MenuMozo form = new MenuMozo(_Operador);
            Close();
            form.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //todo _MesaProRepo
                ProductosLista prodDgv = (ProductosLista)dgvProducto.CurrentRow.DataBoundItem;
                _MesaProRepo.InsertTempPedidoMesa(_IdMesa, prodDgv.Id);
                CargarListadoPedido();
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada btnAgregar_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                TempPedidoPorMesa prodDgv = (TempPedidoPorMesa)dgvProdList.CurrentRow.DataBoundItem;
                _MesaProRepo.RemoverItem(prodDgv);
                CargarListadoPedido();
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada btnRemover_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChechStock())
                {
                    Comanda com = GenerarComanda(regs);
                    _ComandaRepo.InsertComanda(com);
                    _ComandaRepo.InsertComandaOperador(_IdMesa, _Operador.Id);
                    DescontarStock(regs);
                    _ComandaRepo.InsertarTicket(_IdMesa, _NroMesa, _Operador, _Total, com.Menu);
                    DialogResult result = MessageBox.Show($"Desea recibir el detalle de cuenta en su email?", "Email", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result.Equals(DialogResult.OK))
                    {
                        EnvioEmail form = new EnvioEmail(com, _Operador, _IdMesa, _Total);
                        Close();
                        form.Show();
                    }
                    else
                    {
                        _MesaProRepo.CerrarMesaPedidos(_IdMesa);
                        _MesaRepo.CerrarMesa(_IdMesa);
                        MenuMozo form = new MenuMozo(_Operador);
                        Close();
                        form.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada btnEmail_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private void DescontarStock(List<TempPedidoPorMesa> regs)
        {
            try
            {
                foreach (var r in regs)
                {
                    int cant = r.StockDisp - r.Cantidad;
                    _ProdRepo.DescontarCantidad(cant, r.IdProducto);
                }
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada btnRemover_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }
        }

        private Comanda GenerarComanda(List<TempPedidoPorMesa> regs)
        {
            Comanda retorno = new Comanda();

            try
            {
                string pedido = string.Empty;

                foreach (var it in regs)
                {
                    pedido += $"{it.Descripcion} x {it.Cantidad}, ";
                }

                pedido = pedido.Substring(0, pedido.Length - 2);

                retorno.FechaInicio = DateTime.Now;
                retorno.IdEstado = 1;
                retorno.IdOperador = _Operador.Id;
                retorno.Menu = pedido;
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada GenerarComanda exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }

            return retorno;
        }

        private bool ChechStock()
        {
            bool retorno = false;

            try
            {
                foreach (var r in regs)
                {
                    if (r.Cantidad > r.StockDisp)
                    {
                        MessageBox.Show($"{r.Descripcion} excede la cantidad de stock disponible: {r.StockDisp}");
                        return retorno;
                    }
                }

                retorno = true;
            }
            catch (Exception ex)
            {
                _Log.Error($"GesationMesaOcupada btnRemover_Click exception: {ex}");
                MessageBox.Show("Ops, se produjo un inconveniente. Contacte con sistemas.");
            }

            return retorno;
        }
    }
}
