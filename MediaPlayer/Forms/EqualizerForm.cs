using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;

namespace MediaPlayer.Forms
{
    public partial class EqualizerForm : Form
    {
        private readonly LibVLCSharp.Shared.MediaPlayer _mediaPlayer;
        private readonly Equalizer _equalizer;

        private readonly TrackBar[] _bandas =
            new TrackBar[10];

        private readonly Label[] _labelsValor =
            new Label[10];

        private bool _actualizando = false;

        // =========================================================
        // FRECUENCIAS
        // =========================================================

        private readonly string[] _frecuencias =
        {
            "31 Hz",
            "62 Hz",
            "125 Hz",
            "250 Hz",
            "500 Hz",
            "1 kHz",
            "2 kHz",
            "4 kHz",
            "8 kHz",
            "16 kHz"
        };

        // =========================================================
        // PRESETS
        // =========================================================

        private readonly float[][] _presets =
        {
            // Normal
            new float[]
            {
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0
            },

            // Rock
            new float[]
            {
                4, 3, 2, 0, -1,
                1, 3, 4, 3, 2
            },

            // Pop
            new float[]
            {
                2, 1, 0, -1, -2,
                0, 2, 3, 2, 1
            },

            // Clásica
            new float[]
            {
                3, 2, 1, 0, -1,
                -1, 0, 2, 3, 4
            },

            // Electrónica
            new float[]
            {
                5, 4, 2, 0, -1,
                1, 3, 4, 5, 4
            },

            // Voz
            new float[]
            {
                -3, -2, -1, 2, 4,
                4, 3, 1, 0, -1
            },

            // Bass Boost
            new float[]
            {
                8, 7, 5, 3, 1,
                0, 0, 1, 1, 0
            }
        };

        private readonly float[] _preampPresets =
        {
            0,   // Normal
            -2,  // Rock
            -1,  // Pop
            -1,  // Clásica
            -2,  // Electrónica
            -1,  // Voz
            -4   // Bass Boost
        };

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public EqualizerForm(
            LibVLCSharp.Shared.MediaPlayer mediaPlayer,
            Equalizer equalizer)
        {
            _mediaPlayer = mediaPlayer;
            _equalizer = equalizer;

            InitializeComponent();

            ConfigurarBandas();

            CargarValoresActuales();
        }

        // =========================================================
        // CONFIGURAR BANDAS
        // =========================================================

        private void ConfigurarBandas()
        {
            TrackBar[] barras =
            {
                trackBar31,
                trackBar62,
                trackBar125,
                trackBar250,
                trackBar500,
                trackBar1K,
                trackBar2K,
                trackBar4K,
                trackBar8K,
                trackBar16K
            };

            Label[] valores =
            {
                lblValor31,
                lblValor62,
                lblValor125,
                lblValor250,
                lblValor500,
                lblValor1K,
                lblValor2K,
                lblValor4K,
                lblValor8K,
                lblValor16K
            };

            for (int i = 0; i < 10; i++)
            {
                _bandas[i] = barras[i];

                _labelsValor[i] = valores[i];

                _bandas[i].Tag = i;

                _bandas[i].Scroll +=
                    Banda_Scroll;
            }
        }

        // =========================================================
        // CARGAR VALORES ACTUALES
        // =========================================================

        private void CargarValoresActuales()
        {
            _actualizando = true;

            try
            {
                for (uint i = 0; i < 10; i++)
                {
                    int valor =
                        (int)Math.Round(
                            _equalizer.Amp(i));

                    if (valor < -12)
                        valor = -12;

                    if (valor > 12)
                        valor = 12;

                    _bandas[i].Value =
                        valor;

                    _labelsValor[i].Text =
                        FormatearDb(valor);
                }

                int preamp =
                    (int)Math.Round(
                        _equalizer.Preamp);

                if (preamp < -12)
                    preamp = -12;

                if (preamp > 12)
                    preamp = 12;

                trackBarPreamp.Value =
                    preamp;

                lblPreampValor.Text =
                    FormatearDb(preamp);
            }
            finally
            {
                _actualizando = false;
            }
        }

        // =========================================================
        // CAMBIAR BANDA
        // =========================================================

        private void Banda_Scroll(
            object sender,
            EventArgs e)
        {
            if (_actualizando)
                return;

            TrackBar barra =
                sender as TrackBar;

            if (barra == null)
                return;

            int indice =
                (int)barra.Tag;

            _equalizer.SetAmp(
                barra.Value,
                (uint)indice);

            _mediaPlayer.SetEqualizer(
                _equalizer);

            _labelsValor[indice].Text =
                FormatearDb(
                    barra.Value);

            // Si modificamos manualmente
            // ya no es un preset.
            cmbPresets.SelectedIndex = -1;
        }

        // =========================================================
        // PREAMP
        // =========================================================

        private void trackBarPreamp_Scroll(
            object sender,
            EventArgs e)
        {
            if (_actualizando)
                return;

            _equalizer.SetPreamp(
                trackBarPreamp.Value);

            _mediaPlayer.SetEqualizer(
                _equalizer);

            lblPreampValor.Text =
                FormatearDb(
                    trackBarPreamp.Value);

            cmbPresets.SelectedIndex = -1;
        }

        // =========================================================
        // CAMBIAR PRESET
        // =========================================================

        private void cmbPresets_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (_actualizando)
                return;

            int indice =
                cmbPresets.SelectedIndex;

            if (indice < 0 ||
                indice >= _presets.Length)
            {
                return;
            }

            AplicarPreset(indice);
        }

        // =========================================================
        // APLICAR PRESET
        // =========================================================

        private void AplicarPreset(
            int indice)
        {
            _actualizando = true;

            try
            {
                float[] valores =
                    _presets[indice];

                for (int i = 0; i < 10; i++)
                {
                    int valor =
                        (int)valores[i];

                    _bandas[i].Value =
                        valor;

                    _equalizer.SetAmp(
                        valor,
                        (uint)i);

                    _labelsValor[i].Text =
                        FormatearDb(
                            valor);
                }

                int preamp =
                    (int)_preampPresets[indice];

                trackBarPreamp.Value =
                    preamp;

                _equalizer.SetPreamp(
                    preamp);

                lblPreampValor.Text =
                    FormatearDb(
                        preamp);
            }
            finally
            {
                _actualizando = false;
            }

            _mediaPlayer.SetEqualizer(
                _equalizer);
        }

        // =========================================================
        // BOTÓN NORMAL
        // =========================================================

        private void btnNormal_Click(
            object sender,
            EventArgs e)
        {
            _actualizando = true;

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    _bandas[i].Value = 0;

                    _labelsValor[i].Text =
                        "0 dB";

                    _equalizer.SetAmp(
                        0,
                        (uint)i);
                }

                trackBarPreamp.Value =
                    0;

                lblPreampValor.Text =
                    "0 dB";

                _equalizer.SetPreamp(
                    0);

                cmbPresets.SelectedIndex =
                    0;
            }
            finally
            {
                _actualizando = false;
            }

            _mediaPlayer.SetEqualizer(
                _equalizer);
        }


        private string FormatearDb(
            int valor)
        {
            if (valor > 0)
            {
                return "+" +
                       valor +
                       " dB";
            }

            return valor +
                   " dB";
        }
    }
}