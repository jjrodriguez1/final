using DLL;
using System;
using System.Windows.Forms;

namespace AplicacionMosoRodriguez
{
    public partial class MenuGerente : Form
    {
        private static Operador _Operador = null;

        public MenuGerente()
        {
            InitializeComponent();
        }

        public MenuGerente(Operador operador)
        {
            InitializeComponent();
            _Operador = operador;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AltaOperador form = new AltaOperador();
            form.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
            Login form = new Login();
            form.Show();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            AltaProducto form = new AltaProducto();
            form.Show();
        }

        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            AltaMesa form = new AltaMesa();
            form.Show();
        }

        private void btnGestionarOperador_Click(object sender, EventArgs e)
        {
            GestionOperador form = new GestionOperador();
            form.Show();
        }

        private void btnGestionarProducto_Click(object sender, EventArgs e)
        {
            GestionProductos form = new GestionProductos();
            form.Show();
        }

        private void btnGestionarMesa_Click(object sender, EventArgs e)
        {
            GestionMesas form = new GestionMesas();
            form.Show();
        }

        private void btnRepVenta_Click(object sender, EventArgs e)
        {
            VentaReporte form = new VentaReporte();
            form.Show();
        }

        private void btnRepCom_Click(object sender, EventArgs e)
        {
            ComandaReporte form = new ComandaReporte();
            form.Show();
        }
    }
}
