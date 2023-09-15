using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Sxe.Engine
{
    /// <summary>
    /// Interface used by the AudioManager to look up the position
    /// and velocity of entities that can emit 3D sounds.
    /// </summary>
    public interface IAudioEmitter
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Up { get; }
        Vector3 Velocity { get; }
    }

    /// <summary>
    /// Audio manager keeps track of what 3D sounds are playing, updating
    /// their settings as the camera and entities move around the world,
    /// and automatically disposing cue instances after they finish playing.
    /// </summary>
    public class AudioManager : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields


        // XACT objects.
        AudioEngine audioEngine;
        WaveBank waveBank;
        public SoundBank soundBank;
        Stack<Cue> musicStack = new Stack<Cue>();


        // The listener describes the ear which is hearing 3D sounds.
        // This is usually set to match the camera.
        public AudioListener Listener
        {
            get { return listener; }
        }

        AudioListener listener = new AudioListener();


        // The emitter describes an entity which is making a 3D sound.
        AudioEmitter emitter = new AudioEmitter();


        // Keep track of all the 3D sounds that are currently playing.
        List<Cue3D> activeCues = new List<Cue3D>();


        // Keep track of spare Cue3D instances, so we can reuse them.
        // Otherwise we would have to allocate new instances each time
        // a sound was played, which would create unnecessary garbage.
        Stack<Cue3D> cuePool = new Stack<Cue3D>();

        string basePath;
        string engineName;

        #endregion


        public AudioManager(Game game, string inBasePath, string inEngineName)
            : base(game)
        {
            basePath = inBasePath;
            engineName = inEngineName;
        }


        /// <summary>
        /// Loads the XACT data.
        /// </summary>
        public override void Initialize()
        {
            audioEngine = new AudioEngine(basePath + "/" + engineName + ".xgs");
            waveBank = new WaveBank(audioEngine, basePath + "/" + "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine,  basePath + "/" + "Sound Bank.xsb");

            base.Initialize();
        }


        /// <summary>
        /// Unloads the XACT data.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    soundBank.Dispose();
                    waveBank.Dispose();
                    audioEngine.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }


        /// <summary>
        /// Updates the state of the 3D audio system.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Loop over all the currently playing 3D sounds.
            int index = 0;

            while (index < activeCues.Count)
            {
                Cue3D cue3D = activeCues[index];

                if (cue3D.Cue.IsStopped)
                {
                    // If the cue has stopped playing, dispose it.
                    cue3D.Cue.Dispose();

                    // Store the Cue3D instance for future reuse.
                    cuePool.Push(cue3D);

                    // Remove it from the active list.
                    activeCues.RemoveAt(index);
                }
                else
                {
                    // If the cue is still playing, update its 3D settings.
                    Apply3D(cue3D);

                    index++;
                }
            }

            // Update the XACT engine.
            audioEngine.Update();

            base.Update(gameTime);
        }


        public void PlayCue(string sz)
        {
            soundBank.PlayCue(sz);
        }


        public void PushMusic(string cueName)
        {
            //TODO : Get rid of this once we get our music back
            //return;

            //Do we have an existing cue?
            this.PauseMusic();

            Cue newCue = soundBank.GetCue(cueName);
            newCue.Play();
            musicStack.Push(newCue);
        }

        public void PopMusic()
        {
            if (musicStack.Count > 0)
            {
                Cue cue = musicStack.Pop();
                if(cue.IsPlaying)
                cue.Stop(AudioStopOptions.Immediate);
                cue.Dispose();
            }

            if (musicStack.Count > 0)
            {
                Cue currentCue = musicStack.Peek();
                if (currentCue != null && !currentCue.IsDisposed)
                    currentCue.Resume();
            }
        }

        public void PauseMusic()
        {
            if (musicStack.Count > 0)
            {
                if (musicStack.Peek().IsPlaying)
                    musicStack.Peek().Pause();
            }
        }

        public void ResumeMusic()
        {
            if (musicStack.Count > 0)
            {
                if (musicStack.Peek().IsPaused)
                    musicStack.Peek().Resume();
            }
        }




        /// <summary>
        /// Triggers a new 3D sound.
        /// </summary>
        public Cue Play3DCue(string cueName, IAudioEmitter emitter)
        {

            Cue3D cue3D;

            if (cuePool.Count > 0)
            {
                // If possible, reuse an existing Cue3D instance.
                cue3D = cuePool.Pop();
            }
            else
            {
                // Otherwise we have to allocate a new one.
                cue3D = new Cue3D();
            }

            // Fill in the cue and emitter fields.
            cue3D.Cue = soundBank.GetCue(cueName);
            cue3D.Emitter = emitter;

            // Set the 3D position of this cue, and then play it.
            Apply3D(cue3D);

            cue3D.Cue.Play();

            // Remember that this cue is now active.
            activeCues.Add(cue3D);

            return cue3D.Cue;
        }


        /// <summary>
        /// Updates the position and velocity settings of a 3D cue.
        /// </summary>
        private void Apply3D(Cue3D cue3D)
        {
            emitter.Position = cue3D.Emitter.Position;
            emitter.Forward = cue3D.Emitter.Forward;
            emitter.Up = cue3D.Emitter.Up;
            emitter.Velocity = cue3D.Emitter.Velocity;

            cue3D.Cue.Apply3D(listener, emitter);
        }


        /// <summary>
        /// Internal helper class for keeping track of an active 3D cue,
        /// and remembering which emitter object it is attached to.
        /// </summary>
        private class Cue3D
        {
            public Cue Cue;
            public IAudioEmitter Emitter;
        }
    }
}
