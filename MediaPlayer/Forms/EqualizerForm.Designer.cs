using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MediaPlayer.Forms
{
    partial class EqualizerForm
    {
        private IContainer components = null;

        private Label lblTitulo;
        private Label lblPreset;
        private ComboBox cmbPresets;
        private Label lblPreamp;
        private TrackBar trackBarPreamp;
        private Label lblPreampValor;
        private Panel panelBandas;

        private TrackBar trackBar31;
        private TrackBar trackBar62;
        private TrackBar trackBar125;
        private TrackBar trackBar250;
        private TrackBar trackBar500;
        private TrackBar trackBar1K;
        private TrackBar trackBar2K;
        private TrackBar trackBar4K;
        private TrackBar trackBar8K;
        private TrackBar trackBar16K;

        private Label lblValor31;
        private Label lblValor62;
        private Label lblValor125;
        private Label lblValor250;
        private Label lblValor500;
        private Label lblValor1K;
        private Label lblValor2K;
        private Label lblValor4K;
        private Label lblValor8K;
        private Label lblValor16K;

        private Label lblFrecuencia31;
        private Label lblFrecuencia62;
        private Label lblFrecuencia125;
        private Label lblFrecuencia250;
        private Label lblFrecuencia500;
        private Label lblFrecuencia1K;
        private Label lblFrecuencia2K;
        private Label lblFrecuencia4K;
        private Label lblFrecuencia8K;
        private Label lblFrecuencia16K;

        private Button btnNormal;
        private Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            lblTitulo = new Label();
            lblPreset = new Label();
            cmbPresets = new ComboBox();
            lblPreamp = new Label();
            trackBarPreamp = new TrackBar();
            lblPreampValor = new Label();
            panelBandas = new Panel();

            trackBar31 = new TrackBar();
            trackBar62 = new TrackBar();
            trackBar125 = new TrackBar();
            trackBar250 = new TrackBar();
            trackBar500 = new TrackBar();
            trackBar1K = new TrackBar();
            trackBar2K = new TrackBar();
            trackBar4K = new TrackBar();
            trackBar8K = new TrackBar();
            trackBar16K = new TrackBar();

            lblValor31 = new Label();
            lblValor62 = new Label();
            lblValor125 = new Label();
            lblValor250 = new Label();
            lblValor500 = new Label();
            lblValor1K = new Label();
            lblValor2K = new Label();
            lblValor4K = new Label();
            lblValor8K = new Label();
            lblValor16K = new Label();

            lblFrecuencia31 = new Label();
            lblFrecuencia62 = new Label();
            lblFrecuencia125 = new Label();
            lblFrecuencia250 = new Label();
            lblFrecuencia500 = new Label();
            lblFrecuencia1K = new Label();
            lblFrecuencia2K = new Label();
            lblFrecuencia4K = new Label();
            lblFrecuencia8K = new Label();
            lblFrecuencia16K = new Label();

            btnNormal = new Button();
            btnCerrar = new Button();

            ((ISupportInitialize)trackBarPreamp).BeginInit();
            panelBandas.SuspendLayout();

            ((ISupportInitialize)trackBar31).BeginInit();
            ((ISupportInitialize)trackBar62).BeginInit();
            ((ISupportInitialize)trackBar125).BeginInit();
            ((ISupportInitialize)trackBar250).BeginInit();
            ((ISupportInitialize)trackBar500).BeginInit();
            ((ISupportInitialize)trackBar1K).BeginInit();
            ((ISupportInitialize)trackBar2K).BeginInit();
            ((ISupportInitialize)trackBar4K).BeginInit();
            ((ISupportInitialize)trackBar8K).BeginInit();
            ((ISupportInitialize)trackBar16K).BeginInit();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 27);
            ClientSize = new Size(850, 500);
            Controls.Add(btnCerrar);
            Controls.Add(btnNormal);
            Controls.Add(panelBandas);
            Controls.Add(lblPreampValor);
            Controls.Add(trackBarPreamp);
            Controls.Add(lblPreamp);
            Controls.Add(cmbPresets);
            Controls.Add(lblPreset);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EqualizerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ecualizador";
            ForeColor = Color.White;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(135, 30);
            lblTitulo.Text = "Ecualizador";
            lblPreset.AutoSize = true;
            lblPreset.ForeColor = Color.LightGray;
            lblPreset.Location = new Point(205, 27);
            lblPreset.Name = "lblPreset";
            lblPreset.Text = "Preset:";

            cmbPresets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPresets.FormattingEnabled = true;
            cmbPresets.Items.AddRange(new object[]
            {
                "Normal",
                "Rock",
                "Pop",
                "Clásica",
                "Electrónica",
                "Voz",
                "Bass Boost"
            });
            cmbPresets.Location = new Point(255, 23);
            cmbPresets.Name = "cmbPresets";
            cmbPresets.Size = new Size(180, 23);
            cmbPresets.TabIndex = 0;
            cmbPresets.SelectedIndexChanged += new EventHandler(cmbPresets_SelectedIndexChanged);
            lblPreamp.AutoSize = true;
            lblPreamp.ForeColor = Color.LightGray;
            lblPreamp.Location = new Point(470, 27);
            lblPreamp.Name = "lblPreamp";
            lblPreamp.Text = "Preamp:";

            trackBarPreamp.Location = new Point(535, 18);
            trackBarPreamp.Maximum = 12;
            trackBarPreamp.Minimum = -12;
            trackBarPreamp.Name = "trackBarPreamp";
            trackBarPreamp.Size = new Size(180, 45);
            trackBarPreamp.TabIndex = 1;
            trackBarPreamp.TickFrequency = 3;
            trackBarPreamp.Value = 0;
            trackBarPreamp.Scroll += new EventHandler(trackBarPreamp_Scroll);

            lblPreampValor.AutoSize = false;
            lblPreampValor.ForeColor = Color.White;
            lblPreampValor.Location = new Point(720, 24);
            lblPreampValor.Name = "lblPreampValor";
            lblPreampValor.Size = new Size(70, 23);
            lblPreampValor.Text = "0 dB";
            lblPreampValor.TextAlign = ContentAlignment.MiddleCenter;
            panelBandas.BackColor = Color.FromArgb(30, 30, 34);
            panelBandas.BorderStyle = BorderStyle.FixedSingle;
            panelBandas.Controls.Add(lblFrecuencia31);
            panelBandas.Controls.Add(lblFrecuencia62);
            panelBandas.Controls.Add(lblFrecuencia125);
            panelBandas.Controls.Add(lblFrecuencia250);
            panelBandas.Controls.Add(lblFrecuencia500);
            panelBandas.Controls.Add(lblFrecuencia1K);
            panelBandas.Controls.Add(lblFrecuencia2K);
            panelBandas.Controls.Add(lblFrecuencia4K);
            panelBandas.Controls.Add(lblFrecuencia8K);
            panelBandas.Controls.Add(lblFrecuencia16K);

            panelBandas.Controls.Add(lblValor31);
            panelBandas.Controls.Add(lblValor62);
            panelBandas.Controls.Add(lblValor125);
            panelBandas.Controls.Add(lblValor250);
            panelBandas.Controls.Add(lblValor500);
            panelBandas.Controls.Add(lblValor1K);
            panelBandas.Controls.Add(lblValor2K);
            panelBandas.Controls.Add(lblValor4K);
            panelBandas.Controls.Add(lblValor8K);
            panelBandas.Controls.Add(lblValor16K);

            panelBandas.Controls.Add(trackBar31);
            panelBandas.Controls.Add(trackBar62);
            panelBandas.Controls.Add(trackBar125);
            panelBandas.Controls.Add(trackBar250);
            panelBandas.Controls.Add(trackBar500);
            panelBandas.Controls.Add(trackBar1K);
            panelBandas.Controls.Add(trackBar2K);
            panelBandas.Controls.Add(trackBar4K);
            panelBandas.Controls.Add(trackBar8K);
            panelBandas.Controls.Add(trackBar16K);

            panelBandas.Location = new Point(20, 75);
            panelBandas.Name = "panelBandas";
            panelBandas.Size = new Size(810, 300);
            panelBandas.TabIndex = 2;

            ConfigurarBanda(trackBar31, lblValor31, lblFrecuencia31, 20, "trackBar31", "31 Hz");
            ConfigurarBanda(trackBar62, lblValor62, lblFrecuencia62, 98, "trackBar62", "62 Hz");
            ConfigurarBanda(trackBar125, lblValor125, lblFrecuencia125, 176, "trackBar125", "125 Hz");
            ConfigurarBanda(trackBar250, lblValor250, lblFrecuencia250, 254, "trackBar250", "250 Hz");
            ConfigurarBanda(trackBar500, lblValor500, lblFrecuencia500, 332, "trackBar500", "500 Hz");
            ConfigurarBanda(trackBar1K, lblValor1K, lblFrecuencia1K, 410, "trackBar1K", "1 kHz");
            ConfigurarBanda(trackBar2K, lblValor2K, lblFrecuencia2K, 488, "trackBar2K", "2 kHz");
            ConfigurarBanda(trackBar4K, lblValor4K, lblFrecuencia4K, 566, "trackBar4K", "4 kHz");
            ConfigurarBanda(trackBar8K, lblValor8K, lblFrecuencia8K, 644, "trackBar8K", "8 kHz");
            ConfigurarBanda(trackBar16K, lblValor16K, lblFrecuencia16K, 722, "trackBar16K", "16 kHz");
            btnNormal.BackColor = Color.FromArgb(45, 45, 48);
            btnNormal.FlatAppearance.BorderSize = 0;
            btnNormal.FlatStyle = FlatStyle.Flat;
            btnNormal.ForeColor = Color.White;
            btnNormal.Location = new Point(20, 405);
            btnNormal.Name = "btnNormal";
            btnNormal.Size = new Size(130, 40);
            btnNormal.TabIndex = 3;
            btnNormal.Text = "Restablecer";
            btnNormal.UseVisualStyleBackColor = false;
            btnNormal.Click += new EventHandler(btnNormal_Click);
            btnCerrar.BackColor = Color.FromArgb(45, 45, 48);
            btnCerrar.DialogResult = DialogResult.OK;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(700, 405);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(130, 40);
            btnCerrar.TabIndex = 4;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;

            AcceptButton = btnCerrar;
            CancelButton = btnCerrar;

            ((ISupportInitialize)trackBarPreamp).EndInit();

            ((ISupportInitialize)trackBar31).EndInit();
            ((ISupportInitialize)trackBar62).EndInit();
            ((ISupportInitialize)trackBar125).EndInit();
            ((ISupportInitialize)trackBar250).EndInit();
            ((ISupportInitialize)trackBar500).EndInit();
            ((ISupportInitialize)trackBar1K).EndInit();
            ((ISupportInitialize)trackBar2K).EndInit();
            ((ISupportInitialize)trackBar4K).EndInit();
            ((ISupportInitialize)trackBar8K).EndInit();
            ((ISupportInitialize)trackBar16K).EndInit();

            panelBandas.ResumeLayout(false);
        }

        private void ConfigurarBanda(
            TrackBar barra,
            Label valor,
            Label frecuencia,
            int x,
            string nombre,
            string textoFrecuencia)
        {
            valor.AutoSize = false;
            valor.ForeColor = Color.White;
            valor.Location = new Point(x - 8, 10);
            valor.Name = "lblValor" + nombre.Replace("trackBar", "");
            valor.Size = new Size(60, 22);
            valor.Text = "0 dB";
            valor.TextAlign = ContentAlignment.MiddleCenter;

            barra.Location = new Point(x, 38);
            barra.Maximum = 12;
            barra.Minimum = -12;
            barra.Name = nombre;
            barra.Orientation = Orientation.Vertical;
            barra.Size = new Size(45, 170);
            barra.SmallChange = 1;
            barra.LargeChange = 3;
            barra.TabIndex = 1;
            barra.TickFrequency = 3;
            barra.Value = 0;

            frecuencia.AutoSize = false;
            frecuencia.ForeColor = Color.LightGray;
            frecuencia.Location = new Point(x - 10, 215);
            frecuencia.Name = "lblFrecuencia" + nombre.Replace("trackBar", "");
            frecuencia.Size = new Size(65, 30);
            frecuencia.Text = textoFrecuencia;
            frecuencia.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
