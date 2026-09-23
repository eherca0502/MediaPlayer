using System;
using System.Collections.Generic;
using System.Numerics;
using NAudio.Wave;

namespace MediaPlayer.Services
{
    public class AudioSpectrumService : IDisposable
    {
        private const int FftSize = 1024;
        private const int NumeroBarras = 32;
        private WasapiLoopbackCapture _capture;
        private readonly object _lock = new object();
        private readonly List<float> _sampleBuffer = new List<float>(FftSize * 4);
        private bool _running;
        private int _sampleRate = 48000;
        private int _channels = 2;
        private DateTime _ultimoCalculo = DateTime.MinValue;

        public event EventHandler<float[]> SpectrumAvailable;

        public void Start()
        {
            if (_running) return;
            try
            {
                Stop();
                _capture = new WasapiLoopbackCapture();
                _sampleRate = _capture.WaveFormat.SampleRate;
                _channels = Math.Max(1, _capture.WaveFormat.Channels);
                _capture.DataAvailable += Capture_DataAvailable;
                _capture.RecordingStopped += Capture_RecordingStopped;
                lock (_lock) _sampleBuffer.Clear();
                _running = true;
                _capture.StartRecording();
            }
            catch
            {
                _running = false;
                if (_capture != null)
                {
                    _capture.DataAvailable -= Capture_DataAvailable;
                    _capture.RecordingStopped -= Capture_RecordingStopped;
                    _capture.Dispose();
                    _capture = null;
                }
            }
        }

        public void Stop()
        {
            _running = false;
            WasapiLoopbackCapture capture = _capture;
            _capture = null;
            if (capture != null)
            {
                try
                {
                    capture.DataAvailable -= Capture_DataAvailable;
                    capture.RecordingStopped -= Capture_RecordingStopped;
                    capture.StopRecording();
                }
                catch { }
                try { capture.Dispose(); } catch { }
            }
            lock (_lock) _sampleBuffer.Clear();
        }

        private void Capture_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (!_running || e.BytesRecorded <= 0) return;
            try
            {
                ConvertToMonoSamples(e.Buffer, e.BytesRecorded);
                while (true)
                {
                    float[] bloque;
                    lock (_lock)
                    {
                        if (_sampleBuffer.Count < FftSize) break;
                        bloque = _sampleBuffer.GetRange(0, FftSize).ToArray();
                        _sampleBuffer.RemoveRange(0, FftSize / 2);
                    }

                    DateTime ahora = DateTime.UtcNow;
                    if ((ahora - _ultimoCalculo).TotalMilliseconds < 25) continue;
                    _ultimoCalculo = ahora;
                    float[] spectrum = CalcularSpectrum(bloque);
                    SpectrumAvailable?.Invoke(this, spectrum);
                }
            }
            catch { }
        }

        private void Capture_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (ReferenceEquals(sender, _capture)) _running = false;
        }

        private void ConvertToMonoSamples(byte[] buffer, int bytesRecorded)
        {
            if (_capture == null) return;
            int bytesPerSample = Math.Max(1, _capture.WaveFormat.BitsPerSample / 8);
            int frameSize = bytesPerSample * _channels;
            if (frameSize <= 0) return;
            int frames = bytesRecorded / frameSize;

            lock (_lock)
            {
                for (int frame = 0; frame < frames; frame++)
                {
                    float suma = 0f;
                    for (int channel = 0; channel < _channels; channel++)
                    {
                        int offset = frame * frameSize + channel * bytesPerSample;
                        suma += LeerSample(buffer, offset, bytesPerSample, _capture.WaveFormat.Encoding);
                    }
                    _sampleBuffer.Add(suma / _channels);
                }

                if (_sampleBuffer.Count > FftSize * 8)
                    _sampleBuffer.RemoveRange(0, _sampleBuffer.Count - FftSize * 4);
            }
        }

        private float LeerSample(byte[] buffer, int offset, int bytesPerSample, WaveFormatEncoding encoding)
        {
            if (offset < 0 || offset + bytesPerSample > buffer.Length) return 0f;

            if (encoding == WaveFormatEncoding.IeeeFloat && bytesPerSample >= 4)
            {
                float valor = BitConverter.ToSingle(buffer, offset);
                if (float.IsNaN(valor) || float.IsInfinity(valor)) return 0f;
                return Math.Clamp(valor, -1f, 1f);
            }

            if (encoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSample == 2)
                    return BitConverter.ToInt16(buffer, offset) / 32768f;
                if (bytesPerSample == 4)
                    return BitConverter.ToInt32(buffer, offset) / 2147483648f;
                if (bytesPerSample == 3)
                {
                    int valor = buffer[offset] | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 16);
                    if ((valor & 0x800000) != 0) valor |= unchecked((int)0xFF000000);
                    return valor / 8388608f;
                }
            }
            return 0f;
        }

        private float[] CalcularSpectrum(float[] muestras)
        {
            Complex[] datos = new Complex[FftSize];
            for (int i = 0; i < FftSize; i++)
            {
                double ventana = 0.5 - 0.5 * Math.Cos(2.0 * Math.PI * i / (FftSize - 1));
                datos[i] = new Complex(muestras[i] * ventana, 0);
            }

            FFT(datos);
            float[] resultado = new float[NumeroBarras];
            double frecuenciaMinima = 35.0;
            double frecuenciaMaxima = _sampleRate / 2.0;
            double logMin = Math.Log10(frecuenciaMinima);
            double logMax = Math.Log10(frecuenciaMaxima);

            for (int barra = 0; barra < NumeroBarras; barra++)
            {
                double inicio = Math.Pow(10, logMin + (logMax - logMin) * barra / NumeroBarras);
                double fin = Math.Pow(10, logMin + (logMax - logMin) * (barra + 1) / NumeroBarras);
                int binInicio = Math.Max(1, (int)(inicio * FftSize / _sampleRate));
                int binFin = Math.Min(FftSize / 2, Math.Max(binInicio + 1, (int)(fin * FftSize / _sampleRate)));
                double suma = 0;
                int cantidad = 0;

                for (int bin = binInicio; bin < binFin; bin++)
                {
                    suma += datos[bin].Magnitude;
                    cantidad++;
                }

                double promedio = cantidad > 0 ? suma / cantidad : 0;
                double decibelios = 20.0 * Math.Log10(promedio + 0.0000001);
                double nivel = (decibelios + 80.0) / 70.0 * 100.0;
                nivel = Math.Clamp(nivel, 0, 100);
                if (barra < 6) nivel *= 1.12;
                resultado[barra] = (float)Math.Min(100, nivel);
            }
            return resultado;
        }

        private static void FFT(Complex[] buffer)
        {
            int n = buffer.Length;
            for (int i = 1, j = 0; i < n; i++)
            {
                int bit = n >> 1;
                while ((j & bit) != 0) { j ^= bit; bit >>= 1; }
                j ^= bit;
                if (i < j)
                {
                    Complex temporal = buffer[i];
                    buffer[i] = buffer[j];
                    buffer[j] = temporal;
                }
            }

            for (int longitud = 2; longitud <= n; longitud <<= 1)
            {
                double angulo = -2.0 * Math.PI / longitud;
                Complex factor = new Complex(Math.Cos(angulo), Math.Sin(angulo));
                for (int inicio = 0; inicio < n; inicio += longitud)
                {
                    Complex actual = Complex.One;
                    int mitad = longitud / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        Complex par = buffer[inicio + i];
                        Complex impar = actual * buffer[inicio + i + mitad];
                        buffer[inicio + i] = par + impar;
                        buffer[inicio + i + mitad] = par - impar;
                        actual *= factor;
                    }
                }
            }
        }

        public void Dispose() => Stop();
    }
}
