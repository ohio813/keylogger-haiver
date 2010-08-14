namespace Restringidor_Acceso_a_Aplicaciones
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.l_tiempoTranscurrido = new System.Windows.Forms.Label();
            this.l_tiempoRestante = new System.Windows.Forms.Label();
            this.lMensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tiempo Gastado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tiempo Restante";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // l_tiempoTranscurrido
            // 
            this.l_tiempoTranscurrido.AutoSize = true;
            this.l_tiempoTranscurrido.Location = new System.Drawing.Point(161, 26);
            this.l_tiempoTranscurrido.Name = "l_tiempoTranscurrido";
            this.l_tiempoTranscurrido.Size = new System.Drawing.Size(35, 13);
            this.l_tiempoTranscurrido.TabIndex = 4;
            this.l_tiempoTranscurrido.Text = "label4";
            // 
            // l_tiempoRestante
            // 
            this.l_tiempoRestante.AutoSize = true;
            this.l_tiempoRestante.Location = new System.Drawing.Point(161, 52);
            this.l_tiempoRestante.Name = "l_tiempoRestante";
            this.l_tiempoRestante.Size = new System.Drawing.Size(35, 13);
            this.l_tiempoRestante.TabIndex = 5;
            this.l_tiempoRestante.Text = "label4";
            // 
            // lMensaje
            // 
            this.lMensaje.AutoSize = true;
            this.lMensaje.ForeColor = System.Drawing.Color.Red;
            this.lMensaje.Location = new System.Drawing.Point(12, 126);
            this.lMensaje.Name = "lMensaje";
            this.lMensaje.Size = new System.Drawing.Size(0, 13);
            this.lMensaje.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 157);
            this.Controls.Add(this.lMensaje);
            this.Controls.Add(this.l_tiempoRestante);
            this.Controls.Add(this.l_tiempoTranscurrido);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Control Acceso - Redes Sociales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label l_tiempoTranscurrido;
        private System.Windows.Forms.Label l_tiempoRestante;
        private System.Windows.Forms.Label lMensaje;
    }
}

