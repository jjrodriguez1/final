namespace AplicacionMosoRodriguez
{
    partial class MenuGerente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnHacer = new System.Windows.Forms.Button();
            this.btnGestionarOperador = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnGestionarProducto = new System.Windows.Forms.Button();
            this.btnGestionarMesa = new System.Windows.Forms.Button();
            this.btnAgregarMesa = new System.Windows.Forms.Button();
            this.btnRepVenta = new System.Windows.Forms.Button();
            this.btnRepCom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHacer
            // 
            this.btnHacer.Location = new System.Drawing.Point(21, 34);
            this.btnHacer.Name = "btnHacer";
            this.btnHacer.Size = new System.Drawing.Size(163, 68);
            this.btnHacer.TabIndex = 0;
            this.btnHacer.Text = "Crear Operador";
            this.btnHacer.UseVisualStyleBackColor = true;
            this.btnHacer.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGestionarOperador
            // 
            this.btnGestionarOperador.Location = new System.Drawing.Point(190, 34);
            this.btnGestionarOperador.Name = "btnGestionarOperador";
            this.btnGestionarOperador.Size = new System.Drawing.Size(163, 68);
            this.btnGestionarOperador.TabIndex = 1;
            this.btnGestionarOperador.Text = "Gestionar Operador";
            this.btnGestionarOperador.UseVisualStyleBackColor = true;
            this.btnGestionarOperador.Click += new System.EventHandler(this.btnGestionarOperador_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(21, 358);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "LogOut";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Location = new System.Drawing.Point(21, 108);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(163, 68);
            this.btnAgregarProducto.TabIndex = 3;
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // btnGestionarProducto
            // 
            this.btnGestionarProducto.Location = new System.Drawing.Point(190, 108);
            this.btnGestionarProducto.Name = "btnGestionarProducto";
            this.btnGestionarProducto.Size = new System.Drawing.Size(163, 68);
            this.btnGestionarProducto.TabIndex = 4;
            this.btnGestionarProducto.Text = "Gestionar Productos";
            this.btnGestionarProducto.UseVisualStyleBackColor = true;
            this.btnGestionarProducto.Click += new System.EventHandler(this.btnGestionarProducto_Click);
            // 
            // btnGestionarMesa
            // 
            this.btnGestionarMesa.Location = new System.Drawing.Point(190, 182);
            this.btnGestionarMesa.Name = "btnGestionarMesa";
            this.btnGestionarMesa.Size = new System.Drawing.Size(163, 68);
            this.btnGestionarMesa.TabIndex = 8;
            this.btnGestionarMesa.Text = "Gestionar Mesa";
            this.btnGestionarMesa.UseVisualStyleBackColor = true;
            this.btnGestionarMesa.Click += new System.EventHandler(this.btnGestionarMesa_Click);
            // 
            // btnAgregarMesa
            // 
            this.btnAgregarMesa.Location = new System.Drawing.Point(21, 182);
            this.btnAgregarMesa.Name = "btnAgregarMesa";
            this.btnAgregarMesa.Size = new System.Drawing.Size(163, 68);
            this.btnAgregarMesa.TabIndex = 7;
            this.btnAgregarMesa.Text = "Agregar Mesa";
            this.btnAgregarMesa.UseVisualStyleBackColor = true;
            this.btnAgregarMesa.Click += new System.EventHandler(this.btnAgregarMesa_Click);
            // 
            // btnRepVenta
            // 
            this.btnRepVenta.Location = new System.Drawing.Point(21, 256);
            this.btnRepVenta.Name = "btnRepVenta";
            this.btnRepVenta.Size = new System.Drawing.Size(163, 68);
            this.btnRepVenta.TabIndex = 9;
            this.btnRepVenta.Text = "Reporte Ventas";
            this.btnRepVenta.UseVisualStyleBackColor = true;
            this.btnRepVenta.Click += new System.EventHandler(this.btnRepVenta_Click);
            // 
            // btnRepCom
            // 
            this.btnRepCom.Location = new System.Drawing.Point(190, 256);
            this.btnRepCom.Name = "btnRepCom";
            this.btnRepCom.Size = new System.Drawing.Size(163, 68);
            this.btnRepCom.TabIndex = 10;
            this.btnRepCom.Text = "Reporte Comandas";
            this.btnRepCom.UseVisualStyleBackColor = true;
            this.btnRepCom.Click += new System.EventHandler(this.btnRepCom_Click);
            // 
            // MenuGerente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 389);
            this.ControlBox = false;
            this.Controls.Add(this.btnRepCom);
            this.Controls.Add(this.btnRepVenta);
            this.Controls.Add(this.btnGestionarMesa);
            this.Controls.Add(this.btnAgregarMesa);
            this.Controls.Add(this.btnGestionarProducto);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnGestionarOperador);
            this.Controls.Add(this.btnHacer);
            this.MaximizeBox = false;
            this.Name = "MenuGerente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuGerente";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHacer;
        private System.Windows.Forms.Button btnGestionarOperador;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnGestionarProducto;
        private System.Windows.Forms.Button btnGestionarMesa;
        private System.Windows.Forms.Button btnAgregarMesa;
        private System.Windows.Forms.Button btnRepVenta;
        private System.Windows.Forms.Button btnRepCom;
    }
}