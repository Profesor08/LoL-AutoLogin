using System.Collections.Generic;
using System.IO;
using NAudio.Vorbis;
using System.Threading;

namespace LoL_AutoLogin
{
    class Audio
    {

        private static List<Thread> threads = new List<Thread>();

        public static void PlayButtonHover()
        {
            Play(new MemoryStream(Properties.Resources.audio_play_hover));
        }

        public static void PlayButtonClick()
        {
            Play(new MemoryStream(Properties.Resources.audio_play_click));
        }

        public static void ButtonClick()
        {
            Play(new MemoryStream(Properties.Resources.audio_button_click));
        }

        public static void ButtonHover()
        {
            Play(new MemoryStream(Properties.Resources.audio_button_hover));
        }

        public static void InputFocus()
        {
            Play(new MemoryStream(Properties.Resources.audio_input_focus));
        }






        /// <summary>
        /// Playing .ogg audio stream in separate thread, maximum 6 threads
        /// </summary>
        /// <param name="audioStream"></param>
        private static void Play(Stream audioStream)
        {
            if (threads.Count >= 6)
            {
                if (threads[0].IsAlive)
                {
                    try
                    {
                        threads[0].Abort();
                    }
                    catch { }
                }

                threads.RemoveAt(0);
            }

            var thread = new Thread(() =>
            {
                PlayAudio(audioStream);
            });

            thread.Start();

            threads.Add(thread);
        }

        /// <summary>
        /// Playing an .ogg audio stream
        /// </summary>
        /// <param name="audioStream"></param>
        private static void PlayAudio(Stream audioStream)
        {
            using (var vorbisStream = new VorbisWaveReader(audioStream))
            using (var waveOut = new NAudio.Wave.WaveOutEvent())
            {
                waveOut.Init(vorbisStream);
                waveOut.Volume = 0.5f;
                waveOut.Play();
                Thread.Sleep(vorbisStream.TotalTime);
            }
        }
    }
}
