# 🎵 MediaPlayer

Reproductor multimedia desarrollado en **C# y Windows Forms**, utilizando **LibVLCSharp** para la reproducción de audio y video.

El proyecto incluye funciones como playlist, ecualizador, visualizador de audio, pantalla completa y mini reproductor.

## ✨ Características

- ▶️ Reproducción de audio y video
- 📂 Playlist
- 🖱️ Drag & Drop de archivos y carpetas
- ⏮️ Anterior / siguiente
- 🔀 Reproducción aleatoria
- 🔁 Repetición
- 🔊 Control de volumen
- 🎚️ Ecualizador de 10 bandas
- 📊 Visualizador de audio
- ⏩ Barra de progreso y búsqueda
- 🖥️ Pantalla completa
- ⌨️ Atajos de teclado
- 🪟 Mini reproductor
- 📌 Mini reproductor automático al minimizar la aplicación

## 🛠️ Tecnologías

- **C#**
- **.NET 8**
- **Windows Forms**
- **LibVLCSharp**
- **VideoLAN.LibVLC.Windows**
- **NAudio**

## 🎧 Formatos

El reproductor utiliza **LibVLC** para ofrecer compatibilidad con una amplia variedad de formatos de audio y video.

Algunos ejemplos:

- MP4
- MKV
- AVI
- MOV
- WMV
- WebM
- MP3
- WAV
- FLAC
- M4A
- OGG
- AAC
- WMA

## 🎚️ Ecualizador

Incluye un ecualizador de **10 bandas**:

- 31 Hz
- 62 Hz
- 125 Hz
- 250 Hz
- 500 Hz
- 1 kHz
- 2 kHz
- 4 kHz
- 8 kHz
- 16 kHz

También cuenta con diferentes presets:

- Normal
- Rock
- Pop
- Clásica
- Electrónica
- Voz
- Bass Boost

## 📊 Visualizador

El reproductor incluye un visualizador de espectro de audio que muestra barras de frecuencia en tiempo real.

## ⌨️ Atajos de teclado

Durante la reproducción se pueden utilizar diferentes teclas para controlar el reproductor, incluyendo:

- `Space` — Reproducir / Pausar
- `←` — Retroceder
- `→` — Adelantar
- `↑` — Subir volumen
- `↓` — Bajar volumen
- `F11` — Pantalla completa
- `Esc` — Salir de pantalla completa

## 🖥️ Mini reproductor

Al minimizar la ventana principal, el reproductor puede mostrar automáticamente un mini reproductor en la esquina inferior derecha de Windows.

Desde el mini reproductor se puede:

- Reproducir / pausar
- Cambiar de canción
- Controlar el volumen
- Cambiar la posición de reproducción
- Restaurar la ventana principal
- Ocultar el mini reproductor

## 📁 Estructura del proyecto

```text
MediaPlayer
│
├── Forms
│   ├── MainForm.cs
│   ├── MiniPlayerForm.cs
│   ├── EqualizerForm.cs
│   
│
├── Models
│   └── MediaItem.cs
│
├── Services
│   └── AudioSpectrumService.cs
│
├── Controls
│   └── AudioVisualizer.cs
│
├── Assets
│
└── Program.cs
