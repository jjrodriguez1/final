using DLL;
using Interfaces;
using log4net;
using log4net.Config;
using Repositorios;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class AltaProducto : Form
    {
        private ILog _Log = null;
        private IProductoRepository _ProductoRepo;
        private static bool isUpdate = false;
        private static int idP;

        public AltaProducto()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _ProductoRepo = new ProductoRepository(_Log);
        }

        public AltaProducto(int idUpdate)
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            _Log = LogManager.GetLogger("MainLogger");
            _ProductoRepo = new ProductoRepository(_Log);

            if (idUpdate > 0)
            {
                idP = idUpdate;
                isUpdate = true;
                Text = "Modificación Producto";
            }
        }

        private void CargarProductoId(int id)
        {
            try
            {
                var operador = _ProductoRepo.GetOperadorById(id);
                SetProducto(operador);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al modificar operador.");
                Close();
            }
        }

        private void SetProducto(Producto producto)
        {
            txtPrecio.Text = producto.Precio.ToString();
            txtStock.Text = producto.Stock.ToString();
            txtDescripcion.Text = producto.Descripcion;
            txtSku.Text = producto.Sku;
            
        }

        private string ValidarCampos()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                    return "Ingrese Descripcion de producto.";

                if (string.IsNullOrEmpty(txtSku.Text.Trim()))
                    return "Ingrese SKU de producto.";

                if (string.IsNullOrEmpty(txtStock.Text.Trim()))
                    return "Ingrese stock de producto.";

                if (string.IsNullOrEmpty(txtPrecio.Text.Trim()))
                    return "Ingrese Precio de producto.";
            }
            catch (Exception ex)
            {
                _Log.Error($"Producto Validar Campos Exception: {ex}");
                return "Se produjo un error, intente mas tarde.";
            }

            return string.Empty;
        }

        private void btnAltaProducto_Click(object sender, EventArgs e)
        {
            try
            {
                bool resultado = false;
                string mensaje = ValidarCampos();

                if (string.IsNullOrEmpty(mensaje))
                {
                    Producto nuevo = new Producto();

                    nuevo.Descripcion = txtDescripcion.Text;
                    nuevo.Sku = txtSku.Text;
                    nuevo.Precio = decimal.Parse(txtPrecio.Text);
                    nuevo.Stock = int.Parse(txtStock.Text);

                    if (!isUpdate)
                    {
                        resultado = _ProductoRepo.AltaProducto(nuevo);
                    }
                    else
                    {
                        nuevo.Id = idP;
                        resultado = _ProductoRepo.ModificarProducto(nuevo);
                    }

                    if (resultado)
                    {
                        if (!isUpdate)
                            MessageBox.Show("ALTA EXITOSA.");
                        else
                            MessageBox.Show("MODIFICACIÓN EXITOSA.");

                        Close();
                    }
                    else
                        MessageBox.Show("Se produjo un error, intente mas tarde.");
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AltaProducto_Load_1(object sender, EventArgs e)
        {
            if (isUpdate)
                CargarProductoId(idP);
        }
    }
}
