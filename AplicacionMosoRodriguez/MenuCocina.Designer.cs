namespace AplicacionMosoRodriguez
{
    partial class MenuCocina
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
            this.dgvComandasInicio = new System.Windows.Forms.DataGridView();
            this.dgvComandasProceso = new System.Windows.Forms.DataGridView();
            this.btnTake = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComandasInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComandasProceso)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvComandasInicio
            // 
            this.dgvComandasInicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvComandasInicio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComandasInicio.Location = new System.Drawing.Point(12, 30);
            this.dgvComandasInicio.MultiSelect = false;
            this.dgvComandasInicio.Name = "dgvComandasInicio";
            this.dgvComandasInicio.ReadOnly = true;
            this.dgvComandasInicio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComandasInicio.Size = new System.Drawing.Size(243, 173);
            this.dgvComandasInicio.TabIndex = 13;
            // 
            // dgvComandasProceso
            // 
            this.dgvComandasProceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvComandasProceso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComandasProceso.Location = new System.Drawing.Point(366, 30);
            this.dgvComandasProceso.MultiSelect = false;
            this.dgvComandasProceso.Name = "dgvComandasProceso";
            this.dgvComandasProceso.ReadOnly = true;
            this.dgvComandasProceso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComandasProceso.Size = new System.Drawing.Size(243, 173);
            this.dgvComandasProceso.TabIndex = 14;
            // 
            // btnTake
            // 
            this.btnTake.Location = new System.Drawing.Point(270, 30);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(75, 23);
            this.btnTake.TabIndex = 15;
            this.btnTake.Text = "Realizar";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.Location = new System.Drawing.Point(366, 219);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(101, 23);
            this.btnFinalizar.TabIndex = 16;
            this.btnFinalizar.Text = "Finalizar Comanda";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(534, 219);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 17;
            this.btnLogOut.Text = "LogOut";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // MenuCocina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 254);
            this.ControlBox = false;
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.dgvComandasProceso);
            this.Controls.Add(this.dgvComandasInicio);
            this.Name = "MenuCocina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuCocina";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComandasInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComandasProceso)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvComandasInicio;
        private System.Windows.Forms.DataGridView dgvComandasProceso;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnLogOut;
    }
}