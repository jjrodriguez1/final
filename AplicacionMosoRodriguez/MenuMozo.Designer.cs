namespace AplicacionMosoRodriguez
{
    partial class MenuMozo
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
            this.cboMesasDispo = new System.Windows.Forms.ComboBox();
            this.btnOcupar = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.dgvMesas = new System.Windows.Forms.DataGridView();
            this.btnGestionar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMesasDispo
            // 
            this.cboMesasDispo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMesasDispo.FormattingEnabled = true;
            this.cboMesasDispo.Location = new System.Drawing.Point(12, 12);
            this.cboMesasDispo.Name = "cboMesasDispo";
            this.cboMesasDispo.Size = new System.Drawing.Size(243, 21);
            this.cboMesasDispo.TabIndex = 0;
            // 
            // btnOcupar
            // 
            this.btnOcupar.Location = new System.Drawing.Point(261, 10);
            this.btnOcupar.Name = "btnOcupar";
            this.btnOcupar.Size = new System.Drawing.Size(75, 23);
            this.btnOcupar.TabIndex = 1;
            this.btnOcupar.Text = "Ocupar";
            this.btnOcupar.UseVisualStyleBackColor = true;
            this.btnOcupar.Click += new System.EventHandler(this.btnOcupar_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(261, 258);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 11;
            this.btnLogOut.Text = "LogOut";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // dgvMesas
            // 
            this.dgvMesas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMesas.Location = new System.Drawing.Point(12, 63);
            this.dgvMesas.MultiSelect = false;
            this.dgvMesas.Name = "dgvMesas";
            this.dgvMesas.ReadOnly = true;
            this.dgvMesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMesas.Size = new System.Drawing.Size(243, 173);
            this.dgvMesas.TabIndex = 12;
            // 
            // btnGestionar
            // 
            this.btnGestionar.Location = new System.Drawing.Point(12, 258);
            this.btnGestionar.Name = "btnGestionar";
            this.btnGestionar.Size = new System.Drawing.Size(75, 23);
            this.btnGestionar.TabIndex = 14;
            this.btnGestionar.Text = "Gestionar";
            this.btnGestionar.UseVisualStyleBackColor = true;
            this.btnGestionar.Visible = false;
            this.btnGestionar.Click += new System.EventHandler(this.btnGestionar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(135, 258);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 15;
            this.btnCerrar.Text = "Cerrar Mesa";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // MenuMozo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 289);
            this.ControlBox = false;
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnGestionar);
            this.Controls.Add(this.dgvMesas);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnOcupar);
            this.Controls.Add(this.cboMesasDispo);
            this.Name = "MenuMozo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuMozo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMesasDispo;
        private System.Windows.Forms.Button btnOcupar;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.DataGridView dgvMesas;
        private System.Windows.Forms.Button btnGestionar;
        private System.Windows.Forms.Button btnCerrar;
    }
}