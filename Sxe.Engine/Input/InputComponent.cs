using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;

namespace Sxe.Engine.Input
{
    public class InputComponent : DrawableGameComponent, IInputService
    {
        IMouseInput mouse;
        IKeyboardInput keyboard;
        //IGameController controller;
        ControllerCollection controllers;

        bool drawCursor = true;

        public IMouseInput Mouse
        {
            get { return mouse; }
            set { mouse = value; }
        }

        public IKeyboardInput Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        public ControllerCollection Controllers
        {
            get { return controllers; }
            //set { controller = value; }
        }

        public IGameController Controller
        {
            get { return controllers[0]; }
        }

        public bool DrawCursor
        {
            get { return drawCursor; }
            set { drawCursor = value; }
        }

        public bool FreezeMouse
        {
            get
            {
#if !XBOX
                return mouse.FreezeMouse;
#else
                return true;
#endif
            }
            set
            {
#if !XBOX
                mouse.FreezeMouse = value; 
#endif
            }
        }


        public InputComponent(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IInputService), this);

            controllers = new ControllerCollection();

#if !XBOX
            mouse = new BasicMouseInput(game);
            keyboard = new BasicKeyboardInput();
#endif


#if !XBOX
                //controllers.Add(new KeyboardMouseGameController(Game.Services, Game.Content, 4));
                //controllers.Add(new KeyboardMouseGameController(Game.Services, Game.Content, 0));
            controllers.Add(new KeyboardMouseGameController(Game.Services, Game.Content, 0));
#else
            controllers.Add(new XboxGameController(Game.Services, Game.Content, 0));

#endif

            //#else
            for (int i = 1; i < 4; i++)
            {
                controllers.Add(new XboxGameController(Game.Services, Game.Content, i));
            }
//#endif



            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].AddAction("up");
                controllers[i].AddAction("down");
                controllers[i].AddAction("forward");
                controllers[i].AddAction("backward");
                controllers[i].AddAction("left");
                controllers[i].AddAction("right");


#if !XBOX
                //TODO:
                //Should this be taken care of in a virtual function? I'd say yes. This is stupid.
                if (controllers[i] is KeyboardMouseGameController)
                {
                    controllers[i].Bind("q", "up");
                    controllers[i].Bind("e", "down");
                    controllers[i].Bind("w", "forward");
                    controllers[i].Bind("s", "backward");
                    controllers[i].Bind("a", "left");
                    controllers[i].Bind("d", "right");
                }
                else if(controllers[i] is XboxGameController)
                {
#endif
                    controllers[i].Bind("left_stick_up", "forward");
                    controllers[i].Bind("left_stick_down", "backward");
                    controllers[i].Bind("right_stick_up", "up");
                    controllers[i].Bind("right_stick_down", "down");
#if !XBOX
                }
#endif
            }
//#else
            
//            //controllers[i].Bind("left_stick_left", "left");
//            //controllers[i].Bind("left_stick_right", "right");
//#endif
            
            //controller = new KeyboardMouseGameController(Game.Services, Game.Content, 0);


 
        }

        public override void Draw(GameTime gameTime)
        {

            //if (controller != null)
            //    controller.Draw(gameTime);
            for (int i = 0; i < controllers.Count; i++)
                controllers[i].Draw(gameTime);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
#if !XBOX
            mouse.Update(gameTime);
            keyboard.Update(gameTime);
#endif

            //if (controller != null)
            //    controller.Update(gameTime);
            for (int i = 0; i < controllers.Count; i++)
                controllers[i].Update(gameTime);

            base.Update(gameTime);

        }


    }
}
