using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class HelpTickerLabel
    {
        private float transitionAmount = -1f;
        private string text;
        private const float holdTime = 5f;
        private float hold = 0f;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public float TransitionAmount
        {
            get { return transitionAmount; }
        }

        public void Start()
        {
            this.transitionAmount = -1f;
            this.hold = 0f;
        }

        public void End()
        {
            if (this.transitionAmount < 0.1f)
            {
                this.transitionAmount = 1.1f;
                this.hold = 1f;
            }
        }

        public void Update(float amount)
        {
            transitionAmount += amount;

            if (transitionAmount >= 0f && hold < 1f)
            {
                transitionAmount = 0f;
                hold += amount / holdTime;
            }
        }

        public HelpTickerLabel(string helpString)
        {
            this.text = helpString;
        }
    }

    public class HelpTicker : CompositePanel
    {
        private UIImage gradient;
        private SpriteFont font;
        private List<HelpTickerLabel> labels = new List<HelpTickerLabel>();
        private List<HelpTickerLabel> labelsToRemove = new List<HelpTickerLabel>();
        private int verticalHeight = -50;
        private Color tickerColor = new Color(0, 255, 0, 255);

        public Color TickerColor
        {
            get { return tickerColor; }
            set { tickerColor = value; }
        }


        public HelpTicker()
        {
            this.InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts\\HelpTicker");
            gradient = LoadImage(content, "horizontal_gradient");
            base.LoadContent(content);
        }

        public void Add(HelpTickerLabel label)
        {
            for (int i = 0; i < labels.Count; i++)
                labels[i].End();

            if(!this.labels.Contains(label))
            this.labels.Add(label);
            label.Start();
        }

        public override void Update(GameTime gameTime)
        {
            labelsToRemove.Clear();
            for (int i = 0; i < labels.Count; i++)
            {
                labels[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (labels[i].TransitionAmount > 1f)
                    this.labelsToRemove.Add(labels[i]);
            }

            for (int i = 0; i < labelsToRemove.Count; i++)
                labels.Remove(labelsToRemove[i]);
            

            base.Update(gameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            
            for (int i = 0; i < labels.Count; i++)
            {
                if (labels[i].TransitionAmount > 1f)
                    continue;

                Rectangle destRectangle = this.GetDestinationRectangle(positionOffset, scale);
                destRectangle.Y += (int)(labels[i].TransitionAmount * verticalHeight);
                byte alpha = (byte)(255 * (1f - Math.Abs(labels[i].TransitionAmount)));
                Color color = new Color(tickerColor.R, tickerColor.G, tickerColor.B, alpha);
                Color fontColor = new Color(255, 255, 255, alpha);

                Vector2 textPosition = Vector2.Zero;
                float textScale = 1f;
                string actualText = labels[i].Text;


                //Now, we figure out where to draw the text
                Utilities.GetTextParams(destRectangle, labels[i].Text, this.font, HorizontalAlignment.Center,
                    VerticalAlignment.Middle, out textPosition, out actualText, out textScale);

                //Draw the shiz
                this.gradient.Draw(sb, destRectangle, color);
                sb.DrawString(font, actualText, textPosition, fontColor, 0f, Vector2.One, textScale, SpriteEffects.None, 1f);




            }

            base.Paint(sb, positionOffset, scale);
        }


        private void InitializeComponent()
        {
            // 
            // HelpTicker
            // 
            this.Size = new Microsoft.Xna.Framework.Point(300, 25);

        }
    }
}
