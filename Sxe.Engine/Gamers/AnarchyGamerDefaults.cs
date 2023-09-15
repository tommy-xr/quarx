using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Sxe.Engine
{
    /// <summary>
    /// This is an adapter over the XNA class GamerDefaults
    /// This class is necessary because the constructor of GamerDefaults is private
    /// and thus we cannot create "dummy" GamerDefaults for AI or temporary gamers
    /// This adapter will allow us to utilize the gamerdefaults but also do other things also
    /// </summary>
    public class AnarchyGamerDefaults
    {
        private GameDefaults gameDefaults = null;

        //Set the defaults here
        bool accelerateWithButtons = true;
        bool autoAim = false;
        bool autoCenter = false;
        bool brakeWithButtons = false;
        ControllerSensitivity controllerSensitivity = Microsoft.Xna.Framework.GamerServices.ControllerSensitivity.Medium;
        GameDifficulty gameDifficulty = GameDifficulty.Easy;
        bool invertYAxis = false;
        bool manualTransmission = false;
        bool moveWithRightThumbstick = false;
        Color? primaryColor = new Nullable<Color>(Color.CornflowerBlue);
        Color? secondaryColor = new Nullable<Color>(Color.Crimson);
        RacingCameraAngle racingCameraAngle = RacingCameraAngle.Back;

        public void SetFromGameDefaults(GameDefaults defaults)
        {
            if (defaults == null)
                return;

            this.gameDefaults = defaults;
            this.accelerateWithButtons = defaults.AccelerateWithButtons;
            this.autoAim = defaults.AutoAim;
            this.autoCenter = defaults.AutoCenter;
            this.brakeWithButtons = defaults.BrakeWithButtons;
            this.controllerSensitivity = defaults.ControllerSensitivity;
            this.gameDifficulty = defaults.GameDifficulty;
            this.invertYAxis = defaults.InvertYAxis;
            this.manualTransmission = defaults.InvertYAxis;
            this.moveWithRightThumbstick = defaults.MoveWithRightThumbStick;
            this.primaryColor = defaults.PrimaryColor;
            this.secondaryColor = defaults.SecondaryColor;
            this.racingCameraAngle = defaults.RacingCameraAngle;
        }

        public bool AccelerateWithButtons
        {
            get { return this.accelerateWithButtons; }
        }

        public bool AutoAim
        {
            get { return this.autoAim; }
        }

        public bool AutoCenter
        {
            get { return this.autoCenter; }
        }

        public bool BrakeWithButtons
        {
            get { return this.brakeWithButtons; }
        }

        public float ControllerSensitivity
        {
            get { return (float)this.controllerSensitivity / 2f; }
        }

        public GameDifficulty GameDifficulty
        {
            get { return this.gameDifficulty; }
        }

        public bool InvertYAxis
        {
            get { return this.invertYAxis; }
        }

        public bool ManualTransmission
        {
            get { return this.manualTransmission; }
        }

        public bool MoveWithRightThumbstick
        {
            get { return this.moveWithRightThumbstick; }
        }

        public Color? PrimaryColor
        {
            get { return this.primaryColor; }
        }

        public Color? SecondaryColor
        {
            get { return this.secondaryColor; }
        }

        public RacingCameraAngle RacingCameraAngle
        {
            get { return this.racingCameraAngle; }
        }
    }
}
