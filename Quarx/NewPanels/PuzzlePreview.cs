using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sxe.Engine.UI;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class PuzzlePreview : CompositePanel
    {
        int headerPaddingY = 28;

        int panelOffsetX = 15;
        int panelSizeX = 70;
        int panelSizeY = 45;
        int marginY = -10;


        Panel[] previewPanels;
        RenderTarget2D[] previewTargets;

        List<AtomClusterDescription> atomClusterList = new List<AtomClusterDescription>();

        public PuzzlePreview()
        {
            InitializeComponent();


            previewPanels = new Panel[8];
            for (int i = 0; i < 8; i++)
            {
                previewPanels[i] = new Panel();

                Point location = new Point(panelOffsetX, headerPaddingY + (panelSizeY + marginY) * i);
                Point size = new Point(panelSizeX, panelSizeY);

                this.previewPanels[i].Image = new UIImage();
                this.previewPanels[i].Location = location;
                this.previewPanels[i].Size = size;
                this.Panels.Add(this.previewPanels[i]);
            }
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            IGraphicsDeviceService graphics = content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;

            this.previewTargets = new RenderTarget2D[8];
            for (int i = 0; i < 8; i++)
                this.previewTargets[i] = new RenderTarget2D(graphics.GraphicsDevice, 64, 64, 1, SurfaceFormat.Color);


        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            for (int i = 0; i < 8; i++)
            {
                this.previewTargets[i].Dispose();
                this.previewTargets[i] = null;
            }
        }


        public void SetPreviews(QuarxBoardViewer3D viewer,Queue<AtomClusterDescription> atomClusters)
        {
            //We'll cache the atom clusters in a list, so we dont have to do a foreach every frame
            if (atomClusters.Count != atomClusterList.Count)
            {
                atomClusterList.Clear();

                foreach (AtomClusterDescription description in atomClusters)
                    atomClusterList.Insert(atomClusterList.Count, description);
            }

            //Set all preview panels to blank

          
            for (int i = 1; i < 8; i++)
            {
                Panels[i].Image.Color = Color.TransparentWhite;
            }


            this.Panels[0].Image.Value = viewer.PreviewTexture;
            this.Panels[0].Image.Color = Color.White;
            //Now, go through the list, and draw the preview
            for (int i = 0; i < atomClusterList.Count; i++)
            {
                if (i <= 6)
                {
                    Texture2D tex = viewer.GetPreviewTexture(atomClusterList[i].Color1, atomClusterList[i].Color2, this.previewTargets[i]);
                    this.Panels[i + 1].Image.Value = tex;
                    this.Panels[i + 1].Image.Color = Color.White;
                }
            }

        }


        void InitializeComponent()
        {
            // 
            // PuzzlePreview
            // 
            this.BackgroundPath = "Puzzle\\puzzlepreview";
            this.Size = new Microsoft.Xna.Framework.Point(100, 300);

        }
    }
}
