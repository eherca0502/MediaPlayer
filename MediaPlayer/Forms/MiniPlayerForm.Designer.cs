namespace MediaPlayer.Forms
{
    partial class MiniPlayerForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Label lblVolumen;

        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnCerrar;

        private System.Windows.Forms.TrackBar trackBarProgreso;
        private System.Windows.Forms.TrackBar trackBarVolumen;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitulo = new System.Windows.Forms.Label();
            lblTiempo = new System.Windows.Forms.Label();
            lblVolumen = new System.Windows.Forms.Label();

            btnAnterior = new System.Windows.Forms.Button();
            btnPlayPause = new System.Windows.Forms.Button();
            btnSiguiente = new System.Windows.Forms.Button();
            btnRestaurar = new System.Windows.Forms.Button();
            btnCerrar = new System.Windows.Forms.Button();

            trackBarProgreso = new System.Windows.Forms.TrackBar();
            trackBarVolumen = new System.Windows.Forms.TrackBar();

            ((System.ComponentModel.ISupportInitialize)trackBarProgreso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumen).BeginInit();

            SuspendLayout();

            // FORM
            AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            BackColor =
                System.Drawing.Color.FromArgb(25, 25, 28);

            ClientSize =
                new System.Drawing.Size(430, 138);

            FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MiniPlayerForm";
            ShowInTaskbar = false;
            StartPosition =
                System.Windows.Forms.FormStartPosition.Manual;

            Text = "MediaPlayer Mini";
            TopMost = true;

            // TITULO
            lblTitulo.AutoEllipsis = true;
            lblTitulo.Font =
                new System.Drawing.Font(
                    "Segoe UI Semibold",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            lblTitulo.ForeColor =
                System.Drawing.Color.White;

            lblTitulo.Location =
                new System.Drawing.Point(12, 8);

            lblTitulo.Size =
                new System.Drawing.Size(335, 23);

            lblTitulo.Text = "MediaPlayer";

            // RESTAURAR
            btnRestaurar.BackColor =
                System.Drawing.Color.FromArgb(45, 45, 48);

            btnRestaurar.FlatAppearance.BorderSize = 0;
            btnRestaurar.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btnRestaurar.ForeColor =
                System.Drawing.Color.White;

            btnRestaurar.Location =
                new System.Drawing.Point(350, 7);

            btnRestaurar.Size =
                new System.Drawing.Size(30, 27);

            btnRestaurar.Text = "↗";
            btnRestaurar.UseVisualStyleBackColor = false;

            btnRestaurar.Click +=
                btnRestaurar_Click;

            // CERRAR MINI
            btnCerrar.BackColor =
                System.Drawing.Color.FromArgb(45, 45, 48);

            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btnCerrar.ForeColor =
                System.Drawing.Color.White;

            btnCerrar.Location =
                new System.Drawing.Point(387, 7);

            btnCerrar.Size =
                new System.Drawing.Size(30, 27);

            btnCerrar.Text = "✕";
            btnCerrar.UseVisualStyleBackColor = false;

            btnCerrar.Click +=
                btnCerrar_Click;

            // PROGRESO
            trackBarProgreso.AutoSize = false;

            trackBarProgreso.Location =
                new System.Drawing.Point(10, 35);

            trackBarProgreso.Size =
                new System.Drawing.Size(315, 23);

            trackBarProgreso.Maximum = 1000;
            trackBarProgreso.Minimum = 0;
            trackBarProgreso.TickStyle =
                System.Windows.Forms.TickStyle.None;

            trackBarProgreso.MouseDown +=
                trackBarProgreso_MouseDown;

            trackBarProgreso.MouseMove +=
                trackBarProgreso_MouseMove;

            // TIEMPO
            lblTiempo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    7.5F);

            lblTiempo.ForeColor =
                System.Drawing.Color.FromArgb(
                    180, 180, 185);

            lblTiempo.Location =
                new System.Drawing.Point(328, 35);

            lblTiempo.Size =
                new System.Drawing.Size(90, 23);

            lblTiempo.Text =
                "00:00 / 00:00";

            lblTiempo.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // ANTERIOR
            btnAnterior.BackColor =
                System.Drawing.Color.FromArgb(
                    45, 45, 48);

            btnAnterior.FlatAppearance.BorderSize = 0;
            btnAnterior.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btnAnterior.ForeColor =
                System.Drawing.Color.White;

            btnAnterior.Location =
                new System.Drawing.Point(90, 77);

            btnAnterior.Size =
                new System.Drawing.Size(40, 35);

            btnAnterior.Text = "⏮";
            btnAnterior.UseVisualStyleBackColor = false;

            btnAnterior.Click +=
                btnAnterior_Click;

            // PLAY
            btnPlayPause.BackColor =
                System.Drawing.Color.FromArgb(
                    0, 120, 215);

            btnPlayPause.FlatAppearance.BorderSize = 0;
            btnPlayPause.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btnPlayPause.ForeColor =
                System.Drawing.Color.White;

            btnPlayPause.Location =
                new System.Drawing.Point(137, 74);

            btnPlayPause.Size =
                new System.Drawing.Size(48, 41);

            btnPlayPause.Text = "▶";
            btnPlayPause.UseVisualStyleBackColor = false;

            btnPlayPause.Click +=
                btnPlayPause_Click;

            // SIGUIENTE
            btnSiguiente.BackColor =
                System.Drawing.Color.FromArgb(
                    45, 45, 48);

            btnSiguiente.FlatAppearance.BorderSize = 0;
            btnSiguiente.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btnSiguiente.ForeColor =
                System.Drawing.Color.White;

            btnSiguiente.Location =
                new System.Drawing.Point(193, 77);

            btnSiguiente.Size =
                new System.Drawing.Size(40, 35);

            btnSiguiente.Text = "⏭";
            btnSiguiente.UseVisualStyleBackColor = false;

            btnSiguiente.Click +=
                btnSiguiente_Click;

            // VOLUMEN
            trackBarVolumen.AutoSize = false;

            trackBarVolumen.Location =
                new System.Drawing.Point(244, 78);

            trackBarVolumen.Size =
                new System.Drawing.Size(105, 28);

            trackBarVolumen.Maximum = 100;
            trackBarVolumen.Minimum = 0;
            trackBarVolumen.Value = 100;

            trackBarVolumen.TickStyle =
                System.Windows.Forms.TickStyle.None;

            trackBarVolumen.Scroll +=
                trackBarVolumen_Scroll;

            // LABEL VOLUMEN
            lblVolumen.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    7.5F);

            lblVolumen.ForeColor =
                System.Drawing.Color.FromArgb(
                    190, 190, 195);

            lblVolumen.Location =
                new System.Drawing.Point(350, 80);

            lblVolumen.Size =
                new System.Drawing.Size(68, 24);

            lblVolumen.Text =
                "🔊 100%";

            lblVolumen.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            Controls.Add(lblTitulo);
            Controls.Add(btnRestaurar);
            Controls.Add(btnCerrar);
            Controls.Add(trackBarProgreso);
            Controls.Add(lblTiempo);
            Controls.Add(btnAnterior);
            Controls.Add(btnPlayPause);
            Controls.Add(btnSiguiente);
            Controls.Add(trackBarVolumen);
            Controls.Add(lblVolumen);

            ((System.ComponentModel.ISupportInitialize)trackBarProgreso).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumen).EndInit();

            ResumeLayout(false);
        }
    }
}
