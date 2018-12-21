namespace AplicacionMosoRodriguez
{
    partial class EditarAsignacionMesa
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
            this.dgvOperadores = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFiltroOperador = new System.Windows.Forms.ComboBox();
            this.txtFiltroOperador = new System.Windows.Forms.TextBox();
            this.lblResultadoOperador = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperadores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOperadores
            // 
            this.dgvOperadores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOperadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperadores.Location = new System.Drawing.Point(12, 12);
            this.dgvOperadores.MultiSelect = false;
            this.dgvOperadores.Name = "dgvOperadores";
            this.dgvOperadores.ReadOnly = true;
            this.dgvOperadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOperadores.Size = new System.Drawing.Size(243, 194);
            this.dgvOperadores.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Filtrar Operador Por:";
            // 
            // cboFiltroOperador
            // 
            this.cboFiltroOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroOperador.FormattingEnabled = true;
            this.cboFiltroOperador.Location = new System.Drawing.Point(268, 29);
            this.cboFiltroOperador.Name = "cboFiltroOperador";
            this.cboFiltroOperador.Size = new System.Drawing.Size(121, 21);
            this.cboFiltroOperador.TabIndex = 11;
            // 
            // txtFiltroOperador
            // 
            this.txtFiltroOperador.Location = new System.Drawing.Point(417, 29);
            this.txtFiltroOperador.MaxLength = 100;
            this.txtFiltroOperador.Name = "txtFiltroOperador";
            this.txtFiltroOperador.Size = new System.Drawing.Size(173, 20);
            this.txtFiltroOperador.TabIndex = 13;
            // 
            // lblResultadoOperador
            // 
            this.lblResultadoOperador.AutoSize = true;
            this.lblResultadoOperador.Location = new System.Drawing.Point(417, 61);
            this.lblResultadoOperador.Name = "lblResultadoOperador";
            this.lblResultadoOperador.Size = new System.Drawing.Size(10, 13);
            this.lblResultadoOperador.TabIndex = 15;
            this.lblResultadoOperador.Text = " ";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(521, 183);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 16;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(616, 183);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(616, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // EditarAsignacionMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 219);
            this.ControlBox = false;
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblResultadoOperador);
            this.Controls.Add(this.txtFiltroOperador);
            this.Controls.Add(this.cboFiltroOperador);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvOperadores);
            this.Name = "EditarAsignacionMesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditarAsignacionMesa";
            this.Load += new System.EventHandler(this.EditarAsignacionMesa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOperadores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFiltroOperador;
        private System.Windows.Forms.TextBox txtFiltroOperador;
        private System.Windows.Forms.Label lblResultadoOperador;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnBuscar;
    }
}