using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;

namespace Quarx
{


    public class NewCreditScreen : MessageScreen
    {
        List<ICreditEntry> creditEntries = new List<ICreditEntry>();

        private Panel panel1;
        private float fadeToBlack = 0.0f;
        private float fadeTime = 2.0f;
        private float spacing = 20f;
        private float speed = 20f;
        private float yPosition = 500f;
        private float borderSize = 100f;

        private float centerPosition = 415f;

        private SpriteFont neuroPol;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private SpriteFont calibri;

        bool stillDrawing = true;
        bool hasDrawn = false;

        public NewCreditScreen()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            neuroPol = content.Load<SpriteFont>("Fonts\\CreditHeading");
            calibri = content.Load<SpriteFont>("Fonts\\CreditEntry");

            SetupCreditEntries();
        }

        void SetupCreditEntries()
        {
            //creditEntries.Add(new CreditEntry("Questions? Comments? Hit us up at our forums!", calibri));
            creditEntries.Add(new CreditEntry("Thanks for playing!!!", calibri));
            creditEntries.Add(new SpacerCreditEntry());
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Input.Controllers.IsKeyJustPressed("menu_back") || !stillDrawing && hasDrawn)
                this.ExitScreen();


            if (this.TransitionPosition == 0.0f && fadeToBlack < 1.0f)
            {
                fadeToBlack += (float)gameTime.ElapsedGameTime.TotalSeconds / fadeTime;
            }

            if (fadeToBlack >= 1.0f)
                fadeToBlack = 1.0f;

            yPosition -= (float)gameTime.ElapsedGameTime.TotalSeconds * speed;

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            byte color = (byte)(255f * (1f - fadeToBlack));
            panel1.BackColor = new Color(color, color, color, 255);

            Color otherColor = new Color(255, 255, 255, (byte)(255 * fadeToBlack));
            this.panel2.BackColor = otherColor;
            this.panel4.BackColor = otherColor;

            stillDrawing = false;
            for (int i = 0; i < creditEntries.Count; i++)
            {
                float yPos = spacing * i + yPosition;

                Rectangle rect = this.panel1.GetDestinationRectangle(positionOffset, scale);

                //Find center
                float xPos = centerPosition;// -creditEntries[i].Font.MeasureString(creditEntries[i].Text).X / 2f;


                float alpha = 1.0f;

                float bottom = (this.panel1.AbsoluteLocation.Y  + this.panel1.Size.Y);
                float bottomBorder = this.panel1.AbsoluteLocation.Y + this.panel1.Size.Y - borderSize;
                float topBorder = this.panel1.AbsoluteLocation.Y + borderSize;
 

                if (yPos > bottom)
                {
                    alpha = 0f;
                }
                else if (yPos > bottomBorder)
                {
                    alpha =1.0f - (yPos - bottomBorder) / borderSize;
                }
                else if (yPos < this.panel1.AbsoluteLocation.Y )
                {
                    alpha = 0f;
                }
                else if (yPos < topBorder)
                {
                    alpha = (yPos - this.panel1.AbsoluteLocation.Y) / borderSize;
                }

                alpha = (float)Math.Pow((double)alpha, 3.0);

                if (this.TransitionPosition < 0f)
                    xPos += -this.TransitionPosition * this.TransitionOffOffset.X;
                else if (this.TransitionPosition > 0f)
                    xPos += this.TransitionPosition * this.TransitionOffOffset.X;


                if (alpha > 0.0f)
                {
                    stillDrawing = true;
                    hasDrawn = true;
                }

                creditEntries[i].Draw(sb, new Vector2(xPos * scale.X, yPos * scale.Y),
                    new Color(255, 255, 255, (byte)(alpha * 255f)));

                

            }

           
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "genericback";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(101, 90);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(576, 391);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "AnarchyX";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(123, 327);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(212, 109);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "credits";
            this.panel3.CanDrag = false;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(150, 97);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(190, 50);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "quarx";
            this.panel4.CanDrag = false;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(113, 206);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(219, 136);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // NewCreditScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.WatchingCredits;
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }
    }

    interface ICreditEntry
    {
        void Draw(SpriteBatch batch, Vector2 position, Color color);
    }

    class SpacerCreditEntry : ICreditEntry
    {
        public void Draw(SpriteBatch batch, Vector2 position, Color color)
        {
        }
    }

    class CreditEntry : ICreditEntry
    {
        public CreditEntry(string inText, SpriteFont inFont)
        {
            text = inText;
            font = inFont;
        }

        private string text;
        private SpriteFont font;

        public string Text
        {
            get { return text; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        public void Draw(SpriteBatch batch, Vector2 position, Color color)
        {

            position.X -= this.font.MeasureString(text).Y / 2f;
            batch.DrawString(font, text, position, color);
        }
    }


}
