using System;

using Sxe.Engine;

namespace Quarx
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
#if !XBOX
        [STAThread]
#endif
        static void Main(string[] args)
        {

#if DEBUG
            SxeGame.DebugMode = true;
#else
            SxeGame.DebugMode = false;
#endif

            using (QuarxGame game = new QuarxGame())
            {
                game.ProcessArguments(args);
                game.Run();
            }
        }
    }
}

