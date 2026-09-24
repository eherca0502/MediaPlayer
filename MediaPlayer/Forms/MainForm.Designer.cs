using System.Drawing;
using System.Windows.Forms;

namespace MediaPlayer.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelVideo;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private Panel panelAudioVisualizer;
        private Panel panelControles;

        private Button btnAbrir;
        private Button btnPlayPause;
        private Button btnStop;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Button btnPantallaCompleta;
        private Button btnRepetir;
        private Button btnAleatorio;
        private Button btnEcualizador;

        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnLimpiar;

        private TrackBar trackBarProgreso;
        private TrackBar trackBarVolumen;

        private Label lblTiempoActual;
        private Label lblDuracion;
        private Label lblVolumen;
        private Label lblPlaylist;

        private ListBox lstPlaylist;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            panelVideo = new Panel();
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            panelAudioVisualizer = new Panel();

            panelControles = new Panel();

            btnAbrir = new Button();
            btnPlayPause = new Button();
            btnStop = new Button();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            btnPantallaCompleta = new Button();
            btnRepetir = new Button();
            btnAleatorio = new Button();
            btnEcualizador = new Button();

            btnAgregar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();

            trackBarProgreso = new TrackBar();
            trackBarVolumen = new TrackBar();

            lblTiempoActual = new Label();
            lblDuracion = new Label();
            lblVolumen = new Label();
            lblPlaylist = new Label();

            lstPlaylist = new ListBox();

            panelVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();

            panelAudioVisualizer.SuspendLayout();

            panelControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarProgreso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolumen).BeginInit();

            SuspendLayout();

            panelVideo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            panelVideo.BackColor =
                Color.Black;

            panelVideo.Controls.Add(
                panelAudioVisualizer);

            panelVideo.Controls.Add(
                videoView1);

            panelVideo.Location =
                new Point(0, 0);

            panelVideo.Name =
                "panelVideo";

            panelVideo.Size =
                new Size(690, 505);

            panelVideo.TabIndex = 0;

            videoView1.BackColor =
                Color.Black;

            videoView1.Dock =
                DockStyle.Fill;

            videoView1.Location =
                new Point(0, 0);

            videoView1.MediaPlayer =
                null;

            videoView1.Name =
                "videoView1";

            videoView1.Size =
                new Size(690, 505);

            videoView1.TabIndex = 0;

            videoView1.TabStop = false;

            videoView1.Text =
                "videoView1";

            panelAudioVisualizer.BackColor =
                Color.FromArgb(18, 18, 20);

            panelAudioVisualizer.Dock =
                DockStyle.Fill;

            panelAudioVisualizer.Location =
                new Point(0, 0);

            panelAudioVisualizer.Name =
                "panelAudioVisualizer";

            panelAudioVisualizer.Size =
                new Size(690, 505);

            panelAudioVisualizer.TabIndex = 1;

            panelAudioVisualizer.Visible =
                false;

            panelControles.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            panelControles.BackColor =
                Color.FromArgb(30, 30, 33);

            panelControles.Location =
                new Point(0, 505);

            panelControles.Name =
                "panelControles";

            panelControles.Size =
                new Size(690, 105);

            panelControles.TabIndex = 1;

            lblTiempoActual.AutoSize =
                true;

            lblTiempoActual.ForeColor =
                Color.White;

            lblTiempoActual.Location =
                new Point(10, 8);

            lblTiempoActual.Name =
                "lblTiempoActual";

            lblTiempoActual.Size =
                new Size(40, 15);

            lblTiempoActual.TabIndex = 0;

            lblTiempoActual.Text =
                "00:00";

            trackBarProgreso.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            trackBarProgreso.AutoSize =
                false;

            trackBarProgreso.Location =
                new Point(55, 3);

            trackBarProgreso.Maximum =
                1000;

            trackBarProgreso.Name =
                "trackBarProgreso";

            trackBarProgreso.Size =
                new Size(575, 25);

            trackBarProgreso.TabIndex = 1;

            trackBarProgreso.TickStyle =
                TickStyle.None;

            trackBarProgreso.Scroll +=
                trackBarProgreso_Scroll;

            lblDuracion.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblDuracion.AutoSize =
                true;

            lblDuracion.ForeColor =
                Color.White;

            lblDuracion.Location =
                new Point(635, 8);

            lblDuracion.Name =
                "lblDuracion";

            lblDuracion.Size =
                new Size(40, 15);

            lblDuracion.TabIndex = 2;

            lblDuracion.Text =
                "00:00";

            btnAbrir.BackColor =
                Color.FromArgb(45, 45, 48);

            btnAbrir.FlatAppearance.BorderSize =
                0;

            btnAbrir.FlatStyle =
                FlatStyle.Flat;

            btnAbrir.ForeColor =
                Color.White;

            btnAbrir.Location =
                new Point(15, 50);

            btnAbrir.Name =
                "btnAbrir";

            btnAbrir.Size =
                new Size(42, 38);

            btnAbrir.TabIndex = 3;

            btnAbrir.Text =
                "📂";

            btnAbrir.UseVisualStyleBackColor =
                false;

            btnAbrir.Click +=
                btnAbrir_Click;

            btnAnterior.BackColor =
                Color.FromArgb(45, 45, 48);

            btnAnterior.FlatAppearance.BorderSize =
                0;

            btnAnterior.FlatStyle =
                FlatStyle.Flat;

            btnAnterior.ForeColor =
                Color.White;

            btnAnterior.Location =
                new Point(65, 50);

            btnAnterior.Name =
                "btnAnterior";

            btnAnterior.Size =
                new Size(42, 38);

            btnAnterior.TabIndex = 4;

            btnAnterior.Text =
                "⏮";

            btnAnterior.UseVisualStyleBackColor =
                false;

            btnAnterior.Click +=
                btnAnterior_Click;

            btnPlayPause.BackColor =
                Color.FromArgb(55, 55, 58);

            btnPlayPause.FlatAppearance.BorderSize =
                0;

            btnPlayPause.FlatStyle =
                FlatStyle.Flat;

            btnPlayPause.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            btnPlayPause.ForeColor =
                Color.White;

            btnPlayPause.Location =
                new Point(115, 47);

            btnPlayPause.Name =
                "btnPlayPause";

            btnPlayPause.Size =
                new Size(48, 44);

            btnPlayPause.TabIndex = 5;

            btnPlayPause.Text =
                "▶";

            btnPlayPause.UseVisualStyleBackColor =
                false;

            btnPlayPause.Click +=
                btnPlayPause_Click;

            btnStop.BackColor =
                Color.FromArgb(45, 45, 48);

            btnStop.FlatAppearance.BorderSize =
                0;

            btnStop.FlatStyle =
                FlatStyle.Flat;

            btnStop.ForeColor =
                Color.White;

            btnStop.Location =
                new Point(173, 50);

            btnStop.Name =
                "btnStop";

            btnStop.Size =
                new Size(42, 38);

            btnStop.TabIndex = 6;

            btnStop.Text =
                "⏹";

            btnStop.UseVisualStyleBackColor =
                false;

            btnStop.Click +=
                btnStop_Click;

            btnSiguiente.BackColor =
                Color.FromArgb(45, 45, 48);

            btnSiguiente.FlatAppearance.BorderSize =
                0;

            btnSiguiente.FlatStyle =
                FlatStyle.Flat;

            btnSiguiente.ForeColor =
                Color.White;

            btnSiguiente.Location =
                new Point(223, 50);

            btnSiguiente.Name =
                "btnSiguiente";

            btnSiguiente.Size =
                new Size(42, 38);

            btnSiguiente.TabIndex = 7;

            btnSiguiente.Text =
                "⏭";

            btnSiguiente.UseVisualStyleBackColor =
                false;

            btnSiguiente.Click +=
                btnSiguiente_Click;

            btnPantallaCompleta.BackColor =
                Color.FromArgb(45, 45, 48);

            btnPantallaCompleta.FlatAppearance.BorderSize =
                0;

            btnPantallaCompleta.FlatStyle =
                FlatStyle.Flat;

            btnPantallaCompleta.ForeColor =
                Color.White;

            btnPantallaCompleta.Location =
                new Point(273, 50);

            btnPantallaCompleta.Name =
                "btnPantallaCompleta";

            btnPantallaCompleta.Size =
                new Size(42, 38);

            btnPantallaCompleta.TabIndex = 8;

            btnPantallaCompleta.Text =
                "⛶";

            btnPantallaCompleta.UseVisualStyleBackColor =
                false;

            btnPantallaCompleta.Click +=
                btnPantallaCompleta_Click;

            btnRepetir.BackColor =
                Color.FromArgb(45, 45, 48);

            btnRepetir.FlatAppearance.BorderSize =
                0;

            btnRepetir.FlatStyle =
                FlatStyle.Flat;

            btnRepetir.ForeColor =
                Color.White;

            btnRepetir.Location =
                new Point(323, 50);

            btnRepetir.Name =
                "btnRepetir";

            btnRepetir.Size =
                new Size(42, 38);

            btnRepetir.TabIndex = 9;

            btnRepetir.Text =
                "🔁";

            btnRepetir.UseVisualStyleBackColor =
                false;

            btnRepetir.Click +=
                btnRepetir_Click;

            btnAleatorio.BackColor =
                Color.FromArgb(45, 45, 48);

            btnAleatorio.FlatAppearance.BorderSize =
                0;

            btnAleatorio.FlatStyle =
                FlatStyle.Flat;

            btnAleatorio.ForeColor =
                Color.White;

            btnAleatorio.Location =
                new Point(373, 50);

            btnAleatorio.Name =
                "btnAleatorio";

            btnAleatorio.Size =
                new Size(42, 38);

            btnAleatorio.TabIndex = 10;

            btnAleatorio.Text =
                "🔀";

            btnAleatorio.UseVisualStyleBackColor =
                false;

            btnAleatorio.Click +=
                btnAleatorio_Click;

            btnEcualizador.BackColor =
                Color.FromArgb(45, 45, 48);

            btnEcualizador.FlatAppearance.BorderSize =
                0;

            btnEcualizador.FlatStyle =
                FlatStyle.Flat;

            btnEcualizador.ForeColor =
                Color.White;

            btnEcualizador.Location =
                new Point(423, 50);

            btnEcualizador.Name =
                "btnEcualizador";

            btnEcualizador.Size =
                new Size(40, 38);

            btnEcualizador.TabIndex = 11;

            btnEcualizador.Text =
                "🎚";

            btnEcualizador.UseVisualStyleBackColor =
                false;

            btnEcualizador.Click +=
                btnEcualizador_Click;

            lblVolumen.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblVolumen.AutoSize =
                true;

            lblVolumen.ForeColor =
                Color.White;

            lblVolumen.Location =
                new Point(520, 61);

            lblVolumen.Name =
                "lblVolumen";

            lblVolumen.Size =
                new Size(27, 15);

            lblVolumen.TabIndex = 11;

            lblVolumen.Text =
                "🔊";

            trackBarVolumen.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            trackBarVolumen.AutoSize =
                false;

            trackBarVolumen.Location =
                new Point(550, 51);

            trackBarVolumen.Maximum =
                100;

            trackBarVolumen.Name =
                "trackBarVolumen";

            trackBarVolumen.Size =
                new Size(125, 35);

            trackBarVolumen.TabIndex = 12;

            trackBarVolumen.TickStyle =
                TickStyle.None;

            trackBarVolumen.Value =
                100;

            trackBarVolumen.Scroll +=
                trackBarVolumen_Scroll;

            panelControles.Controls.Add(
                lblTiempoActual);

            panelControles.Controls.Add(
                trackBarProgreso);

            panelControles.Controls.Add(
                lblDuracion);

            panelControles.Controls.Add(
                btnAbrir);

            panelControles.Controls.Add(
                btnAnterior);

            panelControles.Controls.Add(
                btnPlayPause);

            panelControles.Controls.Add(
                btnStop);

            panelControles.Controls.Add(
                btnSiguiente);

            panelControles.Controls.Add(
                btnPantallaCompleta);

            panelControles.Controls.Add(
                btnRepetir);

            panelControles.Controls.Add(
                btnAleatorio);

            panelControles.Controls.Add(
                btnEcualizador);

            panelControles.Controls.Add(
                lblVolumen);

            panelControles.Controls.Add(
                trackBarVolumen);

            lblPlaylist.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblPlaylist.AutoSize =
                true;

            lblPlaylist.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            lblPlaylist.ForeColor =
                Color.White;

            lblPlaylist.Location =
                new Point(720, 15);

            lblPlaylist.Name =
                "lblPlaylist";

            lblPlaylist.Size =
                new Size(68, 20);

            lblPlaylist.TabIndex = 13;

            lblPlaylist.Text =
                "Playlist";

            lstPlaylist.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            lstPlaylist.BackColor =
                Color.FromArgb(35, 35, 38);

            lstPlaylist.BorderStyle =
                BorderStyle.FixedSingle;

            lstPlaylist.ForeColor =
                Color.White;

            lstPlaylist.FormattingEnabled =
                true;

            lstPlaylist.HorizontalScrollbar =
                true;

            lstPlaylist.ItemHeight =
                25;

            lstPlaylist.Location =
                new Point(715, 45);

            lstPlaylist.Name =
                "lstPlaylist";

            lstPlaylist.Size =
                new Size(265, 450);

            lstPlaylist.TabIndex = 14;

            lstPlaylist.DoubleClick +=
                lstPlaylist_DoubleClick;

            btnAgregar.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            btnAgregar.BackColor =
                Color.FromArgb(45, 45, 48);

            btnAgregar.FlatAppearance.BorderSize =
                0;

            btnAgregar.FlatStyle =
                FlatStyle.Flat;

            btnAgregar.ForeColor =
                Color.White;

            btnAgregar.Location =
                new Point(715, 560);

            btnAgregar.Name =
                "btnAgregar";

            btnAgregar.Size =
                new Size(80, 35);

            btnAgregar.TabIndex = 15;

            btnAgregar.Text =
                "➕";

            btnAgregar.UseVisualStyleBackColor =
                false;

            btnAgregar.Click +=
                btnAgregar_Click;

            btnEliminar.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            btnEliminar.BackColor =
                Color.FromArgb(45, 45, 48);

            btnEliminar.FlatAppearance.BorderSize =
                0;

            btnEliminar.FlatStyle =
                FlatStyle.Flat;

            btnEliminar.ForeColor =
                Color.White;

            btnEliminar.Location =
                new Point(805, 560);

            btnEliminar.Name =
                "btnEliminar";

            btnEliminar.Size =
                new Size(80, 35);

            btnEliminar.TabIndex = 16;

            btnEliminar.Text =
                "🗑";

            btnEliminar.UseVisualStyleBackColor =
                false;

            btnEliminar.Click +=
                btnEliminar_Click;

            btnLimpiar.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            btnLimpiar.BackColor =
                Color.FromArgb(45, 45, 48);

            btnLimpiar.FlatAppearance.BorderSize =
                0;

            btnLimpiar.FlatStyle =
                FlatStyle.Flat;

            btnLimpiar.ForeColor =
                Color.White;

            btnLimpiar.Location =
                new Point(895, 560);

            btnLimpiar.Name =
                "btnLimpiar";

            btnLimpiar.Size =
                new Size(85, 35);

            btnLimpiar.TabIndex = 17;

            btnLimpiar.Text =
                "🧹";

            btnLimpiar.UseVisualStyleBackColor =
                false;

            btnLimpiar.Click +=
                btnLimpiar_Click;

            AutoScaleDimensions =
                new SizeF(
                    7F,
                    15F);

            AutoScaleMode =
                AutoScaleMode.Font;

            BackColor =
                Color.FromArgb(
                    30,
                    30,
                    30);

            ClientSize =
                new Size(
                    1000,
                    630);

            Controls.Add(
                panelVideo);

            Controls.Add(
                panelControles);

            Controls.Add(
                lblPlaylist);

            Controls.Add(
                lstPlaylist);

            Controls.Add(
                btnAgregar);

            Controls.Add(
                btnEliminar);

            Controls.Add(
                btnLimpiar);

            MinimumSize =
                new Size(
                    900,
                    600);

            Name =
                "MainForm";

            StartPosition =
                FormStartPosition.CenterScreen;

            Text =
                "MediaPlayer";

            panelVideo.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                videoView1).EndInit();

            panelAudioVisualizer.ResumeLayout(false);

            panelControles.ResumeLayout(false);

            panelControles.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)
                trackBarProgreso).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                trackBarVolumen).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }


}
