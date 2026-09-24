using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using MediaPlayer.Models;

namespace MediaPlayer.Forms
{
    public partial class MainForm : Form
    {
        private LibVLC _libVLC;
        private LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        private System.Windows.Forms.Timer _timer;

        private System.Windows.Forms.Timer _audioVisualizerTimer;

        private readonly Random _visualizerRandom =
            new Random();

        private readonly int[] _visualizerBars =
            new int[32];

        private bool _reproduciendoAudio = false;

        private readonly List<MediaItem> _playlist =
            new List<MediaItem>();

        private int _currentIndex = -1;

        private bool _repetir = false;
        private bool _aleatorio = false;

        private MiniPlayerForm _miniPlayer;

        private bool _pantallaCompleta = false;

        private FormBorderStyle _formBorderStyleAnterior;
        private FormWindowState _formWindowStateAnterior;
        private Size _tamanoAnterior;
        private Point _ubicacionAnterior;

        private Rectangle _boundsPanelVideoAnterior;

        private Screen _pantallaAnterior;

        private Rectangle _boundsBtnAbrir;
        private Rectangle _boundsBtnAnterior;
        private Rectangle _boundsBtnPlayPause;
        private Rectangle _boundsBtnStop;
        private Rectangle _boundsBtnSiguiente;

        private Rectangle _boundsBtnPantallaCompleta;
        private Rectangle _boundsBtnRepetir;
        private Rectangle _boundsBtnAleatorio;

        private Rectangle _boundsTrackBarProgreso;
        private Rectangle _boundsTrackBarVolumen;

        private Rectangle _boundsLblTiempoActual;
        private Rectangle _boundsLblDuracion;
        private Rectangle _boundsLblVolumen;

        public MainForm()
        {
            InitializeComponent();

            CrearMiniPlayer();

            Core.Initialize();

            _libVLC = new LibVLC();

            _mediaPlayer =
                new LibVLCSharp.Shared.MediaPlayer(_libVLC);

            videoView1.MediaPlayer =
                _mediaPlayer;

            _timer =
                new System.Windows.Forms.Timer();

            _timer.Interval = 500;

            _timer.Tick += Timer_Tick;

            _timer.Start();

            _audioVisualizerTimer =
                new System.Windows.Forms.Timer();

            _audioVisualizerTimer.Interval = 80;

            _audioVisualizerTimer.Tick +=
                AudioVisualizerTimer_Tick;

            _mediaPlayer.EndReached +=
                MediaPlayer_EndReached;

            _mediaPlayer.Volume =
                trackBarVolumen.Value;

            ActualizarIconoVolumen();

            KeyPreview = true;

            KeyDown += MainForm_KeyDown;

            GuardarPosicionesOriginales();

            panelAudioVisualizer.Visible = false;

            panelAudioVisualizer.BackColor =
                Color.FromArgb(18, 18, 20);

            panelAudioVisualizer.Paint +=
                panelAudioVisualizer_Paint;
        }

        private void GuardarPosicionesOriginales()
        {
            _boundsBtnAbrir = btnAbrir.Bounds;
            _boundsBtnAnterior = btnAnterior.Bounds;
            _boundsBtnPlayPause = btnPlayPause.Bounds;
            _boundsBtnStop = btnStop.Bounds;
            _boundsBtnSiguiente = btnSiguiente.Bounds;

            _boundsBtnPantallaCompleta =
                btnPantallaCompleta.Bounds;

            _boundsBtnRepetir =
                btnRepetir.Bounds;

            _boundsBtnAleatorio =
                btnAleatorio.Bounds;

            _boundsTrackBarProgreso =
                trackBarProgreso.Bounds;

            _boundsTrackBarVolumen =
                trackBarVolumen.Bounds;

            _boundsLblTiempoActual =
                lblTiempoActual.Bounds;

            _boundsLblDuracion =
                lblDuracion.Bounds;

            _boundsLblVolumen =
                lblVolumen.Bounds;
        }

        private void btnAbrir_Click(
            object sender,
            EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                "Seleccionar archivo multimedia";

            dialog.Filter =
                "Archivos multimedia|" +
                "*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.webm;" +
                "*.mp3;*.wav;*.flac;*.m4a;*.ogg;*.aac|" +
                "Todos los archivos|*.*";

            dialog.Multiselect = false;

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                AgregarArchivo(
                    dialog.FileName,
                    true);
            }
        }

        private void btnAgregar_Click(
            object sender,
            EventArgs e)
        {
            using OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Title =
                "Agregar archivos a la playlist";

            dialog.Filter =
                "Archivos multimedia|" +
                "*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.webm;" +
                "*.mp3;*.wav;*.flac;*.m4a;*.ogg;*.aac|" +
                "Todos los archivos|*.*";

            dialog.Multiselect = true;

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    AgregarArchivo(
                        file,
                        false);
                }

                if (_currentIndex == -1 &&
                    _playlist.Count > 0)
                {
                    ReproducirIndice(0);
                }
            }
        }

        private void AgregarArchivo(
            string filePath,
            bool reproducir)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            if (!File.Exists(filePath))
                return;

            foreach (MediaItem item in _playlist)
            {
                if (string.Equals(
                    item.FilePath,
                    filePath,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            MediaItem mediaItem =
                new MediaItem(filePath);

            _playlist.Add(mediaItem);

            lstPlaylist.Items.Add(mediaItem);

            if (reproducir)
            {
                ReproducirIndice(
                    _playlist.Count - 1);
            }
        }

        private void lstPlaylist_DoubleClick(
            object sender,
            EventArgs e)
        {
            if (lstPlaylist.SelectedIndex < 0)
                return;

            ReproducirIndice(
                lstPlaylist.SelectedIndex);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_pantallaCompleta)
                return;

            if (WindowState ==
                FormWindowState.Minimized)
            {
                MostrarMiniPlayer();
            }
            else
            {
                OcultarMiniPlayer();
            }
        }

        private void ReproducirIndice(
            int index)
        {
            if (index < 0 ||
                index >= _playlist.Count)
            {
                return;
            }

            MediaItem item =
                _playlist[index];

            if (!File.Exists(item.FilePath))
            {
                MessageBox.Show(
                    "El archivo ya no existe:\n\n" +
                    item.FilePath,
                    "Archivo no encontrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {

                bool esAudio =
                    EsArchivoDeAudio(item.FilePath);

                _reproduciendoAudio =
                    esAudio;

                if (esAudio)
                {
                    MostrarVisualizadorAudio();
                }
                else
                {
                    MostrarVideo();
                }

                using var media =
                    new Media(
                        _libVLC,
                        new Uri(item.FilePath));

                _currentIndex = index;

                _mediaPlayer.Play(media);

                lstPlaylist.SelectedIndex =
                    index;

                btnPlayPause.Text =
                    "⏸";

                trackBarProgreso.Value =
                    0;

                lblTiempoActual.Text =
                    "00:00";

                lblDuracion.Text =
                    "00:00";

                Text =
                    "MediaPlayer - " +
                    item.FileName;

                if (esAudio)
                {
                    IniciarVisualizadorAudio();
                }
                else
                {
                    DetenerVisualizadorAudio();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo reproducir el archivo.\n\n" +
                    ex.Message,
                    "Error al reproducir",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool EsArchivoDeAudio(
            string filePath)
        {
            string extension =
                Path.GetExtension(filePath)
                .ToLowerInvariant();

            return extension == ".mp3" ||
                   extension == ".wav" ||
                   extension == ".flac" ||
                   extension == ".m4a" ||
                   extension == ".ogg" ||
                   extension == ".aac" ||
                   extension == ".wma";
        }

        private void MostrarVisualizadorAudio()
        {
            videoView1.Visible = false;

            panelAudioVisualizer.Visible = true;

            panelAudioVisualizer.BringToFront();

            panelAudioVisualizer.Invalidate();
        }

        private void MostrarVideo()
        {
            DetenerVisualizadorAudio();

            panelAudioVisualizer.Visible = false;

            videoView1.Visible = true;

            videoView1.BringToFront();
        }

        private void IniciarVisualizadorAudio()
        {
            if (panelAudioVisualizer == null)
                return;

            for (int i = 0;
                 i < _visualizerBars.Length;
                 i++)
            {
                _visualizerBars[i] =
                    _visualizerRandom.Next(10, 80);
            }

            panelAudioVisualizer.Visible = true;

            _audioVisualizerTimer.Start();

            panelAudioVisualizer.Invalidate();
        }

        private void DetenerVisualizadorAudio()
        {
            if (_audioVisualizerTimer != null)
            {
                _audioVisualizerTimer.Stop();
            }

            _reproduciendoAudio = false;

            if (panelAudioVisualizer != null)
            {
                panelAudioVisualizer.Invalidate();
            }
        }

        private void AudioVisualizerTimer_Tick(
            object sender,
            EventArgs e)
        {
            if (!_reproduciendoAudio)
                return;

            if (_mediaPlayer == null)
                return;

            if (!_mediaPlayer.IsPlaying)
                return;

            for (int i = 0;
                 i < _visualizerBars.Length;
                 i++)
            {
                int cambio =
                    _visualizerRandom.Next(
                        -25,
                        26);

                _visualizerBars[i] +=
                    cambio;

                if (_visualizerBars[i] < 15)
                    _visualizerBars[i] = 15;

                if (_visualizerBars[i] > 90)
                    _visualizerBars[i] = 90;
            }

            panelAudioVisualizer.Invalidate();
        }

        private void panelAudioVisualizer_Paint(
            object sender,
            PaintEventArgs e)
        {
            if (!_reproduciendoAudio)
                return;

            Graphics g =
                e.Graphics;

            g.Clear(
                Color.FromArgb(
                    18,
                    18,
                    20));

            int ancho =
                panelAudioVisualizer.ClientSize.Width;

            int alto =
                panelAudioVisualizer.ClientSize.Height;

            int cantidadBarras =
                _visualizerBars.Length;

            int espacio =
                5;

            int anchoBarra =
                (ancho -
                 ((cantidadBarras + 1) * espacio))
                / cantidadBarras;

            if (anchoBarra < 2)
                anchoBarra = 2;

            int centro =
                alto / 2;

            using Brush brush =
                new SolidBrush(
                    Color.FromArgb(
                        0,
                        160,
                        255));

            for (int i = 0;
                 i < cantidadBarras;
                 i++)
            {
                int altura =
                    (int)(
                        alto *
                        (_visualizerBars[i] / 100.0));

                if (altura < 10)
                    altura = 10;

                if (altura > alto - 40)
                    altura = alto - 40;

                int x =
                    espacio +
                    i *
                    (anchoBarra + espacio);

                int y =
                    centro -
                    altura / 2;

                Rectangle barra =
                    new Rectangle(
                        x,
                        y,
                        anchoBarra,
                        altura);

                g.FillRectangle(
                    brush,
                    barra);
            }
        }

        private void btnPlayPause_Click(
            object sender,
            EventArgs e)
        {
            if (_mediaPlayer == null)
                return;

            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();

                btnPlayPause.Text =
                    "▶";
            }
            else
            {
                if (_currentIndex == -1 &&
                    _playlist.Count > 0)
                {
                    ReproducirIndice(0);
                    return;
                }

                _mediaPlayer.Play();

                btnPlayPause.Text =
                    "⏸";

                if (_reproduciendoAudio)
                {
                    _audioVisualizerTimer.Start();
                }
            }
        }

        private void btnStop_Click(
            object sender,
            EventArgs e)
        {
            if (_mediaPlayer == null)
                return;

            _mediaPlayer.Stop();

            DetenerVisualizadorAudio();

            btnPlayPause.Text =
                "▶";

            trackBarProgreso.Value =
                0;

            lblTiempoActual.Text =
                "00:00";
        }

        private void btnAnterior_Click(
            object sender,
            EventArgs e)
        {
            if (_playlist.Count == 0)
                return;

            if (_currentIndex <= 0)
            {
                ReproducirIndice(
                    _playlist.Count - 1);
            }
            else
            {
                ReproducirIndice(
                    _currentIndex - 1);
            }
        }

        private void btnSiguiente_Click(
            object sender,
            EventArgs e)
        {
            if (_playlist.Count == 0)
                return;

            ReproducirSiguiente();
        }

        private void ReproducirSiguiente()
        {
            if (_playlist.Count == 0)
                return;

            int siguienteIndex;

            if (_repetir)
            {
                siguienteIndex =
                    _currentIndex;
            }
            else if (_aleatorio)
            {
                if (_playlist.Count == 1)
                {
                    siguienteIndex = 0;
                }
                else
                {
                    Random random =
                        new Random();

                    do
                    {
                        siguienteIndex =
                            random.Next(
                                0,
                                _playlist.Count);
                    }
                    while (
                        siguienteIndex ==
                        _currentIndex);
                }
            }
            else
            {
                if (_currentIndex <
                    _playlist.Count - 1)
                {
                    siguienteIndex =
                        _currentIndex + 1;
                }
                else
                {
                    btnPlayPause.Text =
                        "▶";

                    trackBarProgreso.Value =
                        0;

                    lblTiempoActual.Text =
                        "00:00";

                    DetenerVisualizadorAudio();

                    return;
                }
            }

            ReproducirIndice(
                siguienteIndex);
        }

        private void MediaPlayer_EndReached(
            object sender,
            EventArgs e)
        {
            if (IsDisposed ||
                !IsHandleCreated)
            {
                return;
            }

            BeginInvoke(new Action(() =>
            {
                if (IsDisposed ||
                    _playlist.Count == 0)
                {
                    return;
                }

                ReproducirSiguiente();
            }));
        }

        private void trackBarVolumen_Scroll(
            object sender,
            EventArgs e)
        {
            if (_mediaPlayer == null)
                return;

            _mediaPlayer.Volume =
                trackBarVolumen.Value;

            ActualizarIconoVolumen();
        }

        private void ActualizarIconoVolumen()
        {
            if (trackBarVolumen.Value == 0)
            {
                lblVolumen.Text =
                    "🔇";
            }
            else if (trackBarVolumen.Value < 50)
            {
                lblVolumen.Text =
                    "🔉";
            }
            else
            {
                lblVolumen.Text =
                    "🔊";
            }
        }

        private void Timer_Tick(
            object sender,
            EventArgs e)
        {
            if (_mediaPlayer == null)
                return;

            if (_mediaPlayer.Length > 0)
            {
                long currentTime =
                    _mediaPlayer.Time;

                long totalTime =
                    _mediaPlayer.Length;

                lblTiempoActual.Text =
                    FormatTime(currentTime);

                lblDuracion.Text =
                    FormatTime(totalTime);

                if (!trackBarProgreso.Focused)
                {
                    int progress =
                        (int)(
                            (currentTime * 1000)
                            / totalTime);

                    if (progress < 0)
                        progress = 0;

                    if (progress > 1000)
                        progress = 1000;

                    trackBarProgreso.Value =
                        progress;
                }
            }

            ActualizarMiniPlayer();
        }

        private string FormatTime(
            long milliseconds)
        {
            TimeSpan time =
                TimeSpan.FromMilliseconds(
                    milliseconds);

            if (time.TotalHours >= 1)
            {
                return time.ToString(
                    @"h\:mm\:ss");
            }

            return time.ToString(
                @"mm\:ss");
        }

        private void trackBarProgreso_Scroll(
            object sender,
            EventArgs e)
        {
            if (_mediaPlayer == null)
                return;

            if (_mediaPlayer.Length <= 0)
                return;

            long newTime =
                (_mediaPlayer.Length *
                trackBarProgreso.Value) / 1000;

            _mediaPlayer.Time =
                newTime;
        }

        private void btnEliminar_Click(
            object sender,
            EventArgs e)
        {
            int selectedIndex =
                lstPlaylist.SelectedIndex;

            if (selectedIndex < 0)
                return;

            bool eraActual =
                selectedIndex ==
                _currentIndex;

            _playlist.RemoveAt(
                selectedIndex);

            lstPlaylist.Items.RemoveAt(
                selectedIndex);

            if (_playlist.Count == 0)
            {
                _mediaPlayer.Stop();

                DetenerVisualizadorAudio();

                _currentIndex = -1;

                btnPlayPause.Text =
                    "▶";

                trackBarProgreso.Value =
                    0;

                lblTiempoActual.Text =
                    "00:00";

                lblDuracion.Text =
                    "00:00";

                Text =
                    "MediaPlayer";

                return;
            }

            if (eraActual)
            {
                _mediaPlayer.Stop();

                if (selectedIndex >=
                    _playlist.Count)
                {
                    selectedIndex =
                        _playlist.Count - 1;
                }

                _currentIndex = -1;

                ReproducirIndice(
                    selectedIndex);
            }
            else
            {
                if (selectedIndex <
                    _currentIndex)
                {
                    _currentIndex--;
                }
            }
        }

        private void btnLimpiar_Click(
            object sender,
            EventArgs e)
        {
            if (_playlist.Count == 0)
                return;

            DialogResult result =
                MessageBox.Show(
                    "¿Quieres eliminar todos los archivos de la playlist?",
                    "Limpiar playlist",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result !=
                DialogResult.Yes)
            {
                return;
            }

            _mediaPlayer.Stop();

            DetenerVisualizadorAudio();

            _playlist.Clear();

            lstPlaylist.Items.Clear();

            _currentIndex = -1;

            btnPlayPause.Text =
                "▶";

            trackBarProgreso.Value =
                0;

            lblTiempoActual.Text =
                "00:00";

            lblDuracion.Text =
                "00:00";

            Text =
                "MediaPlayer";
        }

        private void btnRepetir_Click(
            object sender,
            EventArgs e)
        {
            _repetir =
                !_repetir;

            if (_repetir)
            {
                btnRepetir.BackColor =
                    Color.FromArgb(
                        0,
                        120,
                        215);

                btnRepetir.Text =
                    "🔁✓";
            }
            else
            {
                btnRepetir.BackColor =
                    Color.FromArgb(
                        45,
                        45,
                        48);

                btnRepetir.Text =
                    "🔁";
            }
        }

        private void btnAleatorio_Click(
            object sender,
            EventArgs e)
        {
            _aleatorio =
                !_aleatorio;

            if (_aleatorio)
            {
                btnAleatorio.BackColor =
                    Color.FromArgb(
                        0,
                        120,
                        215);

                btnAleatorio.Text =
                    "🔀✓";
            }
            else
            {
                btnAleatorio.BackColor =
                    Color.FromArgb(
                        45,
                        45,
                        48);

                btnAleatorio.Text =
                    "🔀";
            }
        }


        private void btnPantallaCompleta_Click(
            object sender,
            EventArgs e)
        {
            CambiarPantallaCompleta();
        }

        private void CambiarPantallaCompleta()
        {
            if (!_pantallaCompleta)
            {

                _formBorderStyleAnterior =
                    FormBorderStyle;

                _formWindowStateAnterior =
                    WindowState;

                _tamanoAnterior =
                    Size;

                _ubicacionAnterior =
                    Location;

                _boundsPanelVideoAnterior =
                    panelVideo.Bounds;

                _pantallaAnterior =
                    Screen.FromControl(this);

                FormBorderStyle =
                    FormBorderStyle.None;

                WindowState =
                    FormWindowState.Normal;

                TopMost = true;

                Bounds =
                    _pantallaAnterior.Bounds;

                _pantallaCompleta =
                    true;

                lblPlaylist.Visible = false;
                lstPlaylist.Visible = false;

                btnAgregar.Visible = false;
                btnEliminar.Visible = false;
                btnLimpiar.Visible = false;

                panelControles.Visible = false;

                btnAbrir.Visible = false;
                btnAnterior.Visible = false;
                btnPlayPause.Visible = false;
                btnStop.Visible = false;
                btnSiguiente.Visible = false;

                btnPantallaCompleta.Visible = false;

                btnRepetir.Visible = false;
                btnAleatorio.Visible = false;

                trackBarProgreso.Visible = false;
                trackBarVolumen.Visible = false;

                lblTiempoActual.Visible = false;
                lblDuracion.Visible = false;
                lblVolumen.Visible = false;

                panelVideo.Dock =
                    DockStyle.Fill;

                panelVideo.Anchor =
                    AnchorStyles.None;

                panelVideo.Location =
                    new Point(0, 0);

                panelVideo.Size =
                    ClientSize;

                videoView1.Dock =
                    DockStyle.Fill;

                panelAudioVisualizer.Dock =
                    DockStyle.Fill;

                if (_reproduciendoAudio)
                {
                    panelAudioVisualizer.Visible =
                        true;

                    panelAudioVisualizer.BringToFront();
                }
                else
                {
                    videoView1.Visible = true;

                    videoView1.BringToFront();
                }

                panelVideo.BringToFront();

                videoView1.TabStop = false;

                Focus();
            }
            else
            {
                SalirPantallaCompleta();
            }
        }

        private void SalirPantallaCompleta()
        {
            if (!_pantallaCompleta)
                return;

            TopMost = false;

            FormBorderStyle =
                _formBorderStyleAnterior;

            WindowState =
                FormWindowState.Normal;

            Bounds =
                new Rectangle(
                    _ubicacionAnterior,
                    _tamanoAnterior);

            if (_formWindowStateAnterior ==
                FormWindowState.Maximized)
            {
                WindowState =
                    FormWindowState.Maximized;
            }

            _pantallaCompleta =
                false;

            panelVideo.Dock =
                DockStyle.None;

            panelVideo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            panelVideo.Bounds =
                _boundsPanelVideoAnterior;

            videoView1.Dock =
                DockStyle.Fill;

            panelAudioVisualizer.Dock =
                DockStyle.Fill;

            lblPlaylist.Visible = true;
            lstPlaylist.Visible = true;

            btnAgregar.Visible = true;
            btnEliminar.Visible = true;
            btnLimpiar.Visible = true;

            panelControles.Visible =
                true;

            RestaurarPosicionesOriginales();

            MostrarControlesNormales();

            btnPantallaCompleta.Text =
                "⛶";

            if (_reproduciendoAudio)
            {
                videoView1.Visible = false;

                panelAudioVisualizer.Visible =
                    true;

                panelAudioVisualizer.BringToFront();
            }
            else
            {
                panelAudioVisualizer.Visible = false;

                videoView1.Visible =
                    true;

                videoView1.BringToFront();
            }

            Focus();
        }

        private void RestaurarPosicionesOriginales()
        {
            btnAbrir.Bounds =
                _boundsBtnAbrir;

            btnAnterior.Bounds =
                _boundsBtnAnterior;

            btnPlayPause.Bounds =
                _boundsBtnPlayPause;

            btnStop.Bounds =
                _boundsBtnStop;

            btnSiguiente.Bounds =
                _boundsBtnSiguiente;

            btnPantallaCompleta.Bounds =
                _boundsBtnPantallaCompleta;

            btnRepetir.Bounds =
                _boundsBtnRepetir;

            btnAleatorio.Bounds =
                _boundsBtnAleatorio;

            trackBarProgreso.Bounds =
                _boundsTrackBarProgreso;

            trackBarVolumen.Bounds =
                _boundsTrackBarVolumen;

            lblTiempoActual.Bounds =
                _boundsLblTiempoActual;

            lblDuracion.Bounds =
                _boundsLblDuracion;

            lblVolumen.Bounds =
                _boundsLblVolumen;
        }

        private void MostrarControlesNormales()
        {
            btnAbrir.Visible = true;

            btnAnterior.Visible = true;

            btnPlayPause.Visible = true;

            btnStop.Visible = true;

            btnSiguiente.Visible = true;

            btnPantallaCompleta.Visible = true;

            btnRepetir.Visible = true;

            btnAleatorio.Visible = true;

            trackBarProgreso.Visible = true;

            trackBarVolumen.Visible = true;

            lblTiempoActual.Visible = true;

            lblDuracion.Visible = true;

            lblVolumen.Visible = true;
        }

        private void MainForm_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            ProcesarTecla(
                e.KeyCode);

            e.SuppressKeyPress = true;
        }

        private void ProcesarTecla(
            Keys tecla)
        {
            if (tecla == Keys.F11)
            {
                CambiarPantallaCompleta();
                return;
            }
            if (tecla == Keys.Escape)
            {
                if (_pantallaCompleta)
                {
                    SalirPantallaCompleta();
                }

                return;
            }

            if (tecla == Keys.Space)
            {
                btnPlayPause.PerformClick();
                return;
            }
            if (tecla == Keys.Right)
            {
                Adelantar();
                return;
            }
            if (tecla == Keys.Left)
            {
                Retroceder();
                return;
            }
            if (tecla == Keys.Up)
            {
                CambiarVolumen(5);
                return;
            }
            if (tecla == Keys.Down)
            {
                CambiarVolumen(-5);
                return;
            }
        }


        protected override bool ProcessCmdKey(
            ref Message msg,
            Keys keyData)
        {
            Keys tecla =
                keyData & Keys.KeyCode;

            if (tecla == Keys.F11 ||
                tecla == Keys.Escape ||
                tecla == Keys.Space ||
                tecla == Keys.Left ||
                tecla == Keys.Right ||
                tecla == Keys.Up ||
                tecla == Keys.Down)
            {
                ProcesarTecla(tecla);

                return true;
            }

            return base.ProcessCmdKey(
                ref msg,
                keyData);
        }

        private void Adelantar()
        {
            if (_mediaPlayer == null)
                return;

            if (_mediaPlayer.Length <= 0)
                return;

            long nuevoTiempo =
                _mediaPlayer.Time + 10000;

            if (nuevoTiempo >
                _mediaPlayer.Length)
            {
                nuevoTiempo =
                    _mediaPlayer.Length;
            }

            _mediaPlayer.Time =
                nuevoTiempo;
        }


        private void Retroceder()
        {
            if (_mediaPlayer == null)
                return;

            if (_mediaPlayer.Length <= 0)
                return;

            long nuevoTiempo =
                _mediaPlayer.Time - 10000;

            if (nuevoTiempo < 0)
            {
                nuevoTiempo = 0;
            }

            _mediaPlayer.Time =
                nuevoTiempo;
        }


        private void CambiarVolumen(
            int cantidad)
        {
            if (_mediaPlayer == null)
                return;

            int nuevoVolumen =
                trackBarVolumen.Value +
                cantidad;

            if (nuevoVolumen < 0)
                nuevoVolumen = 0;

            if (nuevoVolumen > 100)
                nuevoVolumen = 100;

            trackBarVolumen.Value =
                nuevoVolumen;

            _mediaPlayer.Volume =
                nuevoVolumen;

            ActualizarIconoVolumen();
        }


        private void CrearMiniPlayer()
        {
            _miniPlayer =
                new MiniPlayerForm(
                    AccionMiniAnterior,
                    AccionMiniPlayPause,
                    AccionMiniSiguiente,
                    AccionMiniVolumen,
                    AccionMiniProgreso,
                    AccionMiniRestaurar);
        }

        private void ActualizarMiniPlayer()
        {
            if (_miniPlayer == null ||
                _miniPlayer.IsDisposed)
            {
                return;
            }

            string titulo = "MediaPlayer";

            if (_currentIndex >= 0 &&
                _currentIndex < _playlist.Count)
            {
                titulo =
                    _playlist[_currentIndex].FileName;
            }

            bool reproduciendo =
                _mediaPlayer != null &&
                _mediaPlayer.IsPlaying;

            _miniPlayer.ActualizarEstado(
                titulo,
                reproduciendo);

            if (_mediaPlayer != null)
            {
                _miniPlayer.ActualizarVolumen(
                    trackBarVolumen.Value);

                _miniPlayer.ActualizarProgreso(
                    _mediaPlayer.Time,
                    _mediaPlayer.Length);
            }
        }

        private void MostrarMiniPlayer()
        {
            if (_pantallaCompleta)
                return;

            if (_miniPlayer == null ||
                _miniPlayer.IsDisposed)
            {
                CrearMiniPlayer();
            }

            ActualizarMiniPlayer();

            _miniPlayer.MostrarEnEsquinaNotificaciones(
                this);

            _miniPlayer.Show();
            _miniPlayer.BringToFront();
            _miniPlayer.Activate();
        }

        private void OcultarMiniPlayer()
        {
            if (_miniPlayer != null &&
                !_miniPlayer.IsDisposed)
            {
                _miniPlayer.Hide();
            }
        }

        private void RestaurarDesdeMiniPlayer()
        {
            OcultarMiniPlayer();

            if (WindowState ==
                FormWindowState.Minimized)
            {
                WindowState =
                    FormWindowState.Normal;
            }

            Activate();
            BringToFront();
        }

        private void AccionMiniAnterior()
        {
            btnAnterior.PerformClick();
            ActualizarMiniPlayer();
        }

        private void AccionMiniPlayPause()
        {
            btnPlayPause.PerformClick();
            ActualizarMiniPlayer();
        }

        private void AccionMiniSiguiente()
        {
            btnSiguiente.PerformClick();
            ActualizarMiniPlayer();
        }

        private void AccionMiniVolumen(
            int volumen)
        {
            if (volumen < 0)
                volumen = 0;

            if (volumen > 100)
                volumen = 100;

            trackBarVolumen.Value =
                volumen;

            if (_mediaPlayer != null)
            {
                _mediaPlayer.Volume =
                    volumen;
            }

            ActualizarIconoVolumen();
            ActualizarMiniPlayer();
        }

        private void AccionMiniProgreso(
            int valor)
        {
            if (_mediaPlayer == null ||
                _mediaPlayer.Length <= 0)
            {
                return;
            }

            if (valor < 0)
                valor = 0;

            if (valor > 1000)
                valor = 1000;

            long nuevoTiempo =
                (_mediaPlayer.Length * valor) /
                1000;

            _mediaPlayer.Time =
                nuevoTiempo;
        }


        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            Cursor.Show();

            if (_miniPlayer != null &&
                !_miniPlayer.IsDisposed)
            {
                _miniPlayer.CerrarDefinitivamente();
                _miniPlayer = null;
            }

            _timer?.Stop();

            _audioVisualizerTimer?.Stop();

            if (_mediaPlayer != null)
            {
                _mediaPlayer.EndReached -=
                    MediaPlayer_EndReached;

                _mediaPlayer.Stop();

                _mediaPlayer.Dispose();
            }

            _libVLC?.Dispose();

            base.OnFormClosing(e);
        }
    }
}