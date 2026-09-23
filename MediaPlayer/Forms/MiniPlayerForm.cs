using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaPlayer.Forms
{
    public partial class MiniPlayerForm : Form
    {
        private readonly Action _anterior;
        private readonly Action _playPause;
        private readonly Action _siguiente;
        private readonly Action<int> _volumen;
        private readonly Action<int> _progreso;
        private readonly Action _restaurar;

        private bool _actualizando;

        public MiniPlayerForm(
            Action anterior,
            Action playPause,
            Action siguiente,
            Action<int> volumen,
            Action<int> progreso,
            Action restaurar)
        {
            _anterior = anterior;
            _playPause = playPause;
            _siguiente = siguiente;
            _volumen = volumen;
            _progreso = progreso;
            _restaurar = restaurar;

            InitializeComponent();
        }

        public void ActualizarEstado(
            string titulo,
            bool reproduciendo)
        {
            lblTitulo.Text =
                string.IsNullOrWhiteSpace(titulo)
                    ? "MediaPlayer"
                    : titulo;

            btnPlayPause.Text =
                reproduciendo ? "⏸" : "▶";
        }

        public void ActualizarVolumen(int volumen)
        {
            volumen = Math.Max(0, Math.Min(100, volumen));

            _actualizando = true;

            try
            {
                trackBarVolumen.Value = volumen;
            }
            finally
            {
                _actualizando = false;
            }

            lblVolumen.Text =
                "🔊 " + volumen + "%";
        }

        public void ActualizarProgreso(
            long actual,
            long total)
        {
            if (total <= 0)
            {
                _actualizando = true;

                try
                {
                    trackBarProgreso.Value = 0;
                }
                finally
                {
                    _actualizando = false;
                }

                lblTiempo.Text =
                    "00:00 / 00:00";

                return;
            }

            actual = Math.Max(0, Math.Min(total, actual));

            int valor =
                (int)((actual * 1000L) / total);

            valor = Math.Max(0, Math.Min(1000, valor));

            _actualizando = true;

            try
            {
                trackBarProgreso.Value = valor;
            }
            finally
            {
                _actualizando = false;
            }

            lblTiempo.Text =
                FormatearTiempo(actual) +
                " / " +
                FormatearTiempo(total);
        }

        public void MostrarEnEsquinaNotificaciones(
            Form owner)
        {
            Rectangle areaTrabajo =
                Screen.FromControl(owner).WorkingArea;

            int margen = 10;

            Location = new Point(
                areaTrabajo.Right -
                Width -
                margen,
                areaTrabajo.Bottom -
                Height -
                margen);
        }

        private string FormatearTiempo(long milliseconds)
        {
            TimeSpan tiempo =
                TimeSpan.FromMilliseconds(milliseconds);

            if (tiempo.TotalHours >= 1)
                return tiempo.ToString(@"h\:mm\:ss");

            return tiempo.ToString(@"mm\:ss");
        }

        private void btnAnterior_Click(
            object sender,
            EventArgs e)
        {
            _anterior?.Invoke();
        }

        private void btnPlayPause_Click(
            object sender,
            EventArgs e)
        {
            _playPause?.Invoke();
        }

        private void btnSiguiente_Click(
            object sender,
            EventArgs e)
        {
            _siguiente?.Invoke();
        }

        private void trackBarVolumen_Scroll(
            object sender,
            EventArgs e)
        {
            if (_actualizando)
                return;

            lblVolumen.Text =
                "🔊 " +
                trackBarVolumen.Value +
                "%";

            _volumen?.Invoke(
                trackBarVolumen.Value);
        }

        private void trackBarProgreso_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MoverProgreso(e.X);
        }

        private void trackBarProgreso_MouseMove(
            object sender,
            MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MoverProgreso(e.X);
        }

        private void MoverProgreso(int mouseX)
        {
            if (_actualizando)
                return;

            int ancho =
                trackBarProgreso.ClientSize.Width;

            if (ancho <= 0)
                return;

            int posicion =
                Math.Max(0, Math.Min(ancho, mouseX));

            int valor =
                (int)((posicion /
                (double)ancho) * 1000);

            valor =
                Math.Max(0, Math.Min(1000, valor));

            trackBarProgreso.Value = valor;

            _progreso?.Invoke(valor);
        }

        private void btnRestaurar_Click(
            object sender,
            EventArgs e)
        {
            _restaurar?.Invoke();
        }

        private void btnCerrar_Click(
            object sender,
            EventArgs e)
        {
            Hide();
        }

        public void CerrarDefinitivamente()
        {
            base.Close();
        }

        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            if (e.CloseReason ==
                CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            base.OnFormClosing(e);
        }
    }
}
