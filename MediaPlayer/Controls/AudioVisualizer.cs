using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MediaPlayer.Controls
{
    public class AudioVisualizer : Control
    {
        private const int NumeroBarras = 32;

        private readonly float[] _spectrum =
            new float[NumeroBarras];

        private readonly float[] _alturas =
            new float[NumeroBarras];

        private readonly float[] _objetivos =
            new float[NumeroBarras];

        private readonly float[] _suavizado =
            new float[NumeroBarras];

        private readonly System.Windows.Forms.Timer _timer;

        private bool _reproduciendo;

        public bool Reproduciendo
        {
            get => _reproduciendo;

            set
            {
                _reproduciendo = value;

                if (!_reproduciendo)
                {
                    for (int i = 0; i < NumeroBarras; i++)
                    {
                        _objetivos[i] = 0;
                    }
                }

                Invalidate();
            }
        }

        public AudioVisualizer()
        {
            DoubleBuffered = true;

            BackColor =
                Color.FromArgb(18, 18, 20);

            ForeColor =
                Color.White;

            ResizeRedraw = true;

            _timer =
                new System.Windows.Forms.Timer
                {
                    Interval = 30
                };

            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        // =========================================================
        // FFT REAL
        // =========================================================

        public void SetSpectrum(float[] spectrum)
        {
            if (spectrum == null)
            {
                return;
            }

            int cantidad =
                Math.Min(
                    spectrum.Length,
                    NumeroBarras);

            // Copiar datos recibidos
            for (int i = 0; i < cantidad; i++)
            {
                float valor =
                    spectrum[i];

                if (float.IsNaN(valor) ||
                    float.IsInfinity(valor))
                {
                    valor = 0;
                }

                valor =
                    Math.Max(
                        0,
                        Math.Min(
                            100,
                            valor));

                _spectrum[i] =
                    valor;
            }

            // =====================================================
            // SUAVIZADO MUY LIGERO
            // =====================================================

            for (int i = 0; i < cantidad; i++)
            {
                float izquierda =
                    i > 0
                        ? _spectrum[i - 1]
                        : _spectrum[i];

                float centro =
                    _spectrum[i];

                float derecha =
                    i < cantidad - 1
                        ? _spectrum[i + 1]
                        : _spectrum[i];

                _suavizado[i] =
                    centro * 0.70f +
                    izquierda * 0.15f +
                    derecha * 0.15f;
            }

            // =====================================================
            // REALCE MUY LIGERO DE GRAVES
            // =====================================================

            for (int i = 0; i < cantidad; i++)
            {
                float valor =
                    _suavizado[i];

                if (i < 4)
                {
                    valor *= 1.18f;
                }
                else if (i < 8)
                {
                    valor *= 1.08f;
                }

                _objetivos[i] =
                    Math.Min(
                        100,
                        valor);
            }

            for (int i = cantidad;
                 i < NumeroBarras;
                 i++)
            {
                _spectrum[i] = 0;
                _suavizado[i] = 0;
                _objetivos[i] = 0;
            }

            Invalidate();
        }

        // =========================================================
        // ANIMACIÓN
        // =========================================================

        private void Timer_Tick(
            object sender,
            EventArgs e)
        {
            for (int i = 0;
                 i < NumeroBarras;
                 i++)
            {
                float objetivo =
                    _objetivos[i];

                float actual =
                    _alturas[i];

                // Las barras suben rápido.
                if (objetivo > actual)
                {
                    actual +=
                        (objetivo - actual) *
                        0.55f;
                }
                else
                {
                    // Las barras bajan lentamente.
                    actual +=
                        (objetivo - actual) *
                        0.18f;
                }

                if (Math.Abs(
                    actual - objetivo) < 0.5f)
                {
                    actual = objetivo;
                }

                _alturas[i] =
                    Math.Max(
                        0,
                        Math.Min(
                            100,
                            actual));
            }

            Invalidate();
        }

        // =========================================================
        // DIBUJAR
        // =========================================================

        protected override void OnPaint(
            PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g =
                e.Graphics;

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

            g.PixelOffsetMode =
                PixelOffsetMode.HighQuality;

            int ancho =
                ClientSize.Width;

            int alto =
                ClientSize.Height;

            if (ancho <= 0 ||
                alto <= 0)
            {
                return;
            }

            // =====================================================
            // FONDO
            // =====================================================

            using (LinearGradientBrush fondo =
                new LinearGradientBrush(
                    ClientRectangle,
                    Color.FromArgb(
                        10,
                        10,
                        14),
                    Color.FromArgb(
                        24,
                        20,
                        32),
                    90f))
            {
                g.FillRectangle(
                    fondo,
                    ClientRectangle);
            }

            // =====================================================
            // TITULO
            // =====================================================

            using (Font fuente =
                new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold))
            {
                using (Brush pincel =
                    new SolidBrush(
                        Color.FromArgb(
                            235,
                            235,
                            245)))
                {
                    g.DrawString(
                        "AUDIO VISUALIZER",
                        fuente,
                        pincel,
                        24,
                        22);
                }
            }

            // =====================================================
            // ESTADO
            // =====================================================

            using (Font fuente =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Regular))
            {
                using (Brush pincel =
                    new SolidBrush(
                        _reproduciendo
                            ? Color.FromArgb(
                                120,
                                180,
                                255)
                            : Color.FromArgb(
                                130,
                                130,
                                145)))
                {
                    g.DrawString(
                        _reproduciendo
                            ? "● Reproduciendo"
                            : "● Pausado",
                        fuente,
                        pincel,
                        26,
                        50);
                }
            }

            // =====================================================
            // ÁREA DE BARRAS
            // =====================================================

            int margenIzquierdo = 24;
            int margenDerecho = 24;

            int arriba = 92;
            int abajo = 38;

            int areaAncho =
                ancho -
                margenIzquierdo -
                margenDerecho;

            int areaAlto =
                alto -
                arriba -
                abajo;

            if (areaAncho <= 0 ||
                areaAlto <= 0)
            {
                return;
            }

            float espacio = 5f;

            float anchoBarra =
                (
                    areaAncho -
                    ((NumeroBarras - 1) *
                     espacio)
                ) /
                NumeroBarras;

            if (anchoBarra < 1)
            {
                anchoBarra = 1;
            }

            float lineaY =
                arriba +
                areaAlto;

            // =====================================================
            // LÍNEA BASE
            // =====================================================

            using (Pen linea =
                new Pen(
                    Color.FromArgb(
                        50,
                        80,
                        80,
                        100),
                    1))
            {
                g.DrawLine(
                    linea,
                    margenIzquierdo,
                    lineaY,
                    ancho - margenDerecho,
                    lineaY);
            }

            // =====================================================
            // BARRAS
            // =====================================================

            for (int i = 0;
                 i < NumeroBarras;
                 i++)
            {
                float porcentaje =
                    _alturas[i] / 100f;

                porcentaje =
                    Math.Max(
                        0,
                        Math.Min(
                            1,
                            porcentaje));

                float altura =
                    porcentaje *
                    areaAlto;

                if (_reproduciendo &&
                    porcentaje > 0.01f &&
                    altura < 3)
                {
                    altura = 3;
                }

                float x =
                    margenIzquierdo +
                    i *
                    (anchoBarra + espacio);

                float y =
                    lineaY -
                    altura;

                if (altura > 0)
                {
                    RectangleF rectangulo =
                        new RectangleF(
                            x,
                            y,
                            anchoBarra,
                            altura);

                    // =================================================
                    // BARRA
                    // =================================================

                    using (LinearGradientBrush barra =
                        new LinearGradientBrush(
                            rectangulo,
                            Color.FromArgb(
                                70,
                                130,
                                255),
                            Color.FromArgb(
                                180,
                                70,
                                255),
                            90f))
                    {
                        g.FillRoundedRectangle(
                            barra,
                            rectangulo,
                            3f);
                    }

                    // =================================================
                    // BRILLO SUPERIOR
                    // =================================================

                    if (altura >= 4)
                    {
                        float brilloAlto =
                            Math.Min(
                                3f,
                                altura);

                        RectangleF brillo =
                            new RectangleF(
                                x,
                                y,
                                anchoBarra,
                                brilloAlto);

                        using (Brush pincel =
                            new SolidBrush(
                                Color.FromArgb(
                                    210,
                                    220,
                                    235,
                                    255)))
                        {
                            g.FillRoundedRectangle(
                                pincel,
                                brillo,
                                2f);
                        }
                    }

                    // =================================================
                    // REFLEJO
                    // =================================================

                    float reflejoAltura =
                        Math.Min(
                            altura * 0.25f,
                            areaAlto * 0.20f);

                    if (reflejoAltura > 1)
                    {
                        RectangleF reflejo =
                            new RectangleF(
                                x,
                                lineaY + 2,
                                anchoBarra,
                                reflejoAltura);

                        using (LinearGradientBrush reflejoBrush =
                            new LinearGradientBrush(
                                reflejo,
                                Color.FromArgb(
                                    45,
                                    100,
                                    130,
                                    255),
                                Color.FromArgb(
                                    0,
                                    100,
                                    130,
                                    255),
                                90f))
                        {
                            g.FillRectangle(
                                reflejoBrush,
                                reflejo);
                        }
                    }
                }
            }

            // =====================================================
            // ETIQUETAS
            // =====================================================

            using (Font fuente =
                new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Regular))
            {
                using (Brush pincel =
                    new SolidBrush(
                        Color.FromArgb(
                            95,
                            95,
                            110)))
                {
                    string textoGraves =
                        "GRAVES";

                    string textoMedios =
                        "MEDIOS";

                    string textoAgudos =
                        "AGUDOS";

                    g.DrawString(
                        textoGraves,
                        fuente,
                        pincel,
                        margenIzquierdo,
                        alto - 27);

                    SizeF tamañoMedios =
                        g.MeasureString(
                            textoMedios,
                            fuente);

                    g.DrawString(
                        textoMedios,
                        fuente,
                        pincel,
                        (ancho -
                         tamañoMedios.Width) /
                        2,
                        alto - 27);

                    SizeF tamañoAgudos =
                        g.MeasureString(
                            textoAgudos,
                            fuente);

                    g.DrawString(
                        textoAgudos,
                        fuente,
                        pincel,
                        ancho -
                        margenDerecho -
                        tamañoAgudos.Width,
                        alto - 27);
                }
            }
        }

        // =========================================================
        // DISPOSE
        // =========================================================

        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }

    // =============================================================
    // EXTENSIONES PARA RECTÁNGULOS REDONDEADOS
    // =============================================================

    internal static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            RectangleF rectangle,
            float radius)
        {
            using (GraphicsPath path =
                CreateRoundedRectanglePath(
                    rectangle,
                    radius))
            {
                graphics.FillPath(
                    brush,
                    path);
            }
        }

        private static GraphicsPath
            CreateRoundedRectanglePath(
                RectangleF rectangle,
                float radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            float diametro =
                radius * 2;

            if (diametro >
                rectangle.Width)
            {
                diametro =
                    rectangle.Width;
            }

            if (diametro >
                rectangle.Height)
            {
                diametro =
                    rectangle.Height;
            }

            path.AddArc(
                rectangle.X,
                rectangle.Y,
                diametro,
                diametro,
                180,
                90);

            path.AddArc(
                rectangle.Right -
                diametro,
                rectangle.Y,
                diametro,
                diametro,
                270,
                90);

            path.AddArc(
                rectangle.Right -
                diametro,
                rectangle.Bottom -
                diametro,
                diametro,
                diametro,
                0,
                90);

            path.AddArc(
                rectangle.X,
                rectangle.Bottom -
                diametro,
                diametro,
                diametro,
                90,
                90);

            path.CloseFigure();

            return path;
        }
    }
}