using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Graphics;

namespace Quarx
{
    public class QuarxBoardViewer3D : IBoardViewer
    {

        private struct ShatterInfo
        {
            public Model mode;
            public Quaternion rotation;
            public float fadeAmount;
            public float scale;
            public Vector3 position;
            public Color color;
            public float shatterRotation;
            public float translation;
            public float time;
        }



        BloomSettings Settings = BloomSettings.PresetSettings[3];

        Model blockModel;
        Model isotopeModel;
        Model isotopeHiRes;
        Texture2D blockTexture;
        //Texture2D glossTexture;
        Texture2D isotopeTexture;
        Effect shatterEffect;
        EffectParameter shatterWorldParameter;
        EffectParameter shatterColorParameter;
        EffectParameter shatterFadeAmountParameter;

        
        Effect blockEffect;
        EffectParameter blockWorldParameter;
        EffectParameter blockColorParameter;
        EffectParameter blockFadeAmountParameter;
    

        Effect bloomExtract;
        Effect gaussianBlur;
        Effect bloomCombine;

        //QuarxGameBoard board;
        BaseGameModel model;

        SpriteBatch spriteBatch;
        RenderTarget2D target1;
        RenderTarget2D target2;
        RenderTarget2D boardTarget;
        RenderTarget2D previewTarget;


        Texture2D outTexture;
        Camera camera;

        GraphicsDevice device;

        Vector3 isotopeAxis = Vector3.Up;

        Vector3 currentRotation = Vector3.Up;
        Vector3 nextRotation = Vector3.Up;

        float rotationDelay = 1f;
        float rotPosition = 0.0f;
        double previewRotation = 0.0;
        float width;
        float height;
        float currentHeight = 1f;
        float loseTime = 0f;

        //The max things that could ever be in here are 8*18 + 2
        private List<ShatterInfo> shatterList = new List<ShatterInfo>(200);
        private List<ShatterInfo> blockList = new List<ShatterInfo>(200);

        public Texture2D Texture
        {
            get { return outTexture; }
        }

        public QuarxGameBoard Board
        {
            get { return model.Board; }
        }

        public BaseGameModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public Texture2D PreviewTexture
        {
            get { return this.GetPreviewTexture(); }
        }

        public QuarxBoardViewer3D(GraphicsDevice inDevice, ContentManager content, int boardWidth, int boardHeight)
        {
            device = inDevice;
            Load(content, boardWidth, boardHeight);

        }

        void Load(ContentManager content, int boardWidth, int boardHeight)
        {
            width = boardWidth;
            height = boardHeight;
            blockModel = content.Load<Model>("block_model");
            isotopeModel = content.Load<Model>("isotope_model");
            isotopeHiRes = content.Load<Model>("isotope_model_hires");
            blockTexture = content.Load<Texture2D>("blockstex");
            //glossTexture = content.Load<Texture2D>("blocksbloom2");
            //isotopeTexture = content.Load<Texture2D>("isotopebloom");
            isotopeTexture = blockTexture;

            blockEffect = content.Load<Effect>("blockEffect");
            shatterEffect = content.Load<Effect>("shatterEffect");

            blockWorldParameter = blockEffect.Parameters["World"];
            blockColorParameter = blockEffect.Parameters["BlockColor"];
            blockFadeAmountParameter = blockEffect.Parameters["FadeAmount"];

            shatterWorldParameter = shatterEffect.Parameters["World"];
            shatterColorParameter = shatterEffect.Parameters["BlockColor"];
            shatterFadeAmountParameter = shatterEffect.Parameters["FadeAmount"];

            bloomExtract = content.Load<Effect>("BloomExtract");
            bloomCombine = content.Load<Effect>("BloomCombine");
            gaussianBlur = content.Load<Effect>("GaussianBlur");

            spriteBatch = new SpriteBatch(device);

            camera = new Camera();
            camera.Position = new Vector3(0f, 10f, 0f);
            camera.Forward = Vector3.Forward;
            camera.AspectRatio = 0.5f;
            camera.Update(null);

            int targetWidth = 256;
            int targetHeight = 512;
            int factor = 1;

            previewTarget = new RenderTarget2D(device, 128, 128, 1, SurfaceFormat.Color);
            boardTarget = new RenderTarget2D(device, targetWidth, targetHeight, 1, SurfaceFormat.Color);
            target1 = new RenderTarget2D(device, targetWidth / factor, targetHeight/factor, 1, SurfaceFormat.Color);
            target2 = new RenderTarget2D(device, targetWidth / factor, targetHeight/factor, 1, SurfaceFormat.Color);

        }


        public void Draw(GameTime gameTime)
        {
            //Adjust rotation
            previewRotation += gameTime.ElapsedGameTime.TotalSeconds;
            rotPosition += (float)gameTime.ElapsedGameTime.TotalSeconds / rotationDelay;
            if (rotPosition >= 1.0f)
            {
                rotPosition = 0.0f;
                currentRotation = nextRotation;
                Random r = new Random();

                float adjAmount = 0.5f;

                nextRotation.X += adjAmount * (float)(r.NextDouble() - 0.5);
                nextRotation.Y += adjAmount * (float)(r.NextDouble() - 0.5);
                nextRotation.Z += adjAmount * (float)(r.NextDouble() - 0.5);
                nextRotation.Normalize();
            }


            this.SetupLists(gameTime);



            device.RenderState.DepthBufferEnable = false;
            device.SetRenderTarget(0, boardTarget);
            //device.RenderState.DepthBufferEnable = true;
            device.Clear(Color.TransparentWhite);
            //device.RenderState.AlphaBlendEnable = false;

            shatterEffect.Parameters["View"].SetValue(Matrix.Identity);
            shatterEffect.Parameters["Projection"].SetValue(camera.Projection);
            shatterEffect.Parameters["modelTexture"].SetValue(blockTexture);

            shatterEffect.Begin();

            shatterEffect.CurrentTechnique.Passes[0].Begin();

            for (int i = 0; i < shatterList.Count; i++)
            {
                ShatterInfo si = shatterList[i];
                this.DrawShatterModel(si.mode, si.rotation, si.fadeAmount, si.scale, si.position, si.color, si.shatterRotation, si.translation, si.time);
            }

            shatterEffect.CurrentTechnique.Passes[0].End();
            shatterEffect.End();

            //Do block models here
            blockEffect.Parameters["View"].SetValue(Matrix.Identity);
            blockEffect.Parameters["Projection"].SetValue(camera.Projection);
            blockEffect.Parameters["modelTexture"].SetValue(blockTexture);

            blockEffect.Begin();
            blockEffect.CurrentTechnique.Passes[0].Begin();
            //Draw the atom cluster
            //But only if we are playing
            if (Model.CurrentAtomCluster != null && model.State == QuarxGameState.Playing)
            {
                Quaternion quat = Quaternion.CreateFromAxisAngle(Vector3.Up, rotPosition * 3.14f);
                DrawBlockModel(blockModel, quat, 0.0f, 0.02f,
                    GetPosition(Model.CurrentAtomCluster.Position1), GetColorFromType(Model.CurrentAtomCluster.AtomColor1));
                DrawBlockModel(blockModel, quat, 0.0f, 0.02f,
                    GetPosition(Model.CurrentAtomCluster.Position2), GetColorFromType(Model.CurrentAtomCluster.AtomColor2));
            }

            //Loop through the block effect
            for (int i = 0; i < blockList.Count; i++)
            {
                this.DrawBlockModel(blockList[i]);
            }

            blockEffect.CurrentTechnique.Passes[0].End();
            blockEffect.End();




            device.SetRenderTarget(0, null);

            Texture2D boardTexture = boardTarget.GetTexture();

            //outTexture = boardTexture;
            //return;


            bloomExtract.Parameters["BloomThreshold"].SetValue(Settings.BloomThreshold);

            DrawFullscreenQuad(boardTexture, target2,
                               bloomExtract);

            //outTexture = target2.GetTexture();
            //return;

     

            // Pass 2: draw from rendertarget 1 into rendertarget 2,
            // using a shader to apply a horizontal gaussian blur filter.
            SetBlurEffectParameters(1.0f / (float)target2.Width, 0);

            DrawFullscreenQuad(target2.GetTexture(), target1,
                               gaussianBlur);

            //outTexture = target1.GetTexture();
            //return;

            SetBlurEffectParameters(0, 1.0f / (float)target1.Height);

            DrawFullscreenQuad(target1.GetTexture(), target2,
                               gaussianBlur);

    

            //outTexture = target2.GetTexture();
            //return;


            EffectParameterCollection parameters = bloomCombine.Parameters;

            parameters["BloomIntensity"].SetValue(Settings.BloomIntensity);
            parameters["BaseIntensity"].SetValue(Settings.BaseIntensity);
            parameters["BloomSaturation"].SetValue(Settings.BloomSaturation);
            parameters["BaseSaturation"].SetValue(Settings.BaseSaturation);

            device.Textures[1] = boardTexture;

            DrawFullscreenQuad(target2.GetTexture(),
                               target1, bloomCombine);
            outTexture = target1.GetTexture();
            //outTexture = boardTexture;
                     


        }


        public void SetupLists(GameTime gameTime)
        {
            shatterList.Clear();
            blockList.Clear();


            if (this.model.State == QuarxGameState.Lost && loseTime > 2f)
            {
                return;
            }
            else if(this.model.State == QuarxGameState.Lost)
            {
                loseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (loseTime > 0f)
                loseTime = 0f;

            

            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < Board.Height; y++)
                {

                    Block block = Board[x, y];
                    if (block != null)
                    {
                        block.Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds *
                            (1f + 4f * block.NearbyBlocks * ((1f + 5f * (float)Math.Pow(block.FadeAmount, 2))));

                        Vector3 position = GetPosition(x, y);
                        int height = model.GetMaxHeight();
                        int cutoff = 11;
                        float scaleFactor = 1f;


                        float fadeAmount = Math.Max(block.FadeAmount, loseTime);
                        float translation = block.FadeAmount * 12f;
                        float rotation = block.FadeAmount * 5f;
                        float time = 0f;

                        float adjTime = loseTime * 100f;

                        time += adjTime;
                        translation += adjTime * adjTime * 0.01f;
                        rotation += adjTime * adjTime * 10f;

           

                        if (y >= 0)
                        {
                            if (height > cutoff)
                            {

                                currentHeight = height;

                                //TRIPPY  MODE - keep multiplier at 10f
                                //ONLY SET TRANSLATION

                                float add = (float)(currentHeight - cutoff) / (model.Board.Height - cutoff) * 0.25f;

                                if (block.FadeAmount < 0.025f)
                                {
                                    float val = add * ((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 5f + position.X * 5f)
                                        + (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 2.5f + position.Y * 5f));

                                    float multiplier = 2.5f;

                                    if (block.Type == BlockType.Isotope)
                                        multiplier = 10f;

                                    translation += val * multiplier;

                                    scaleFactor += val * 1.5f;
                                }
                            }
                        }

                        float scale = 0.02f * (1f - block.FadeAmount * block.FadeAmount) * scaleFactor;

                        if (block.PunishBlock)
                        {
                            //DrawShatterModel(blockModel, Quaternion.CreateFromAxisAngle(Vector3.Up, block.Rotation),
                            //    block.FadeAmount,

                            //    scale * (float)(1.0 + 0.3 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds))),
                            //    position, Color.Orange, rotation, translation, time);

                            float modulation = (float)(2.5 + 1 * Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds)));

                            ShatterInfo si = new ShatterInfo() { color = Color.Orange, scale=scale * 0.8f,
                                mode=isotopeHiRes, rotation=Quaternion.CreateFromAxisAngle(Vector3.Up, block.Rotation), 
                                fadeAmount = block.FadeAmount,
                                position=position,
                                translation=modulation, 
                                time = time, 
                                shatterRotation = 0.5f};

                                this.shatterList.Add(si);
                        }

                        if (block.Type == BlockType.Isotope)
                        {
                            //DrawShatterModel(isotopeModel, GetQuaternion(block.Rotation), block.FadeAmount, scale,
                            //    position, GetColorFromType(block.Color), rotation, translation, time);
                            //rot += 0.01f;

                            ShatterInfo si = new ShatterInfo()
                            {
                                mode = isotopeModel,
                                rotation = GetQuaternion(block.Rotation),
                                fadeAmount = block.FadeAmount,
                                scale = scale,
                                position = position,
                                color = GetColorFromType(block.Color),
                                translation = translation,
                                time = time,
                                shatterRotation = rotation
                            };

                            if (fadeAmount > 0.01f)
                            {
                                //Use high res if its a block fading out
                                if(block.FadeAmount > 0.01f)
                                si.mode = isotopeHiRes;
                                this.shatterList.Add(si);
                            }
                            else
                                this.blockList.Add(si);
                        }
                            //This is a block
                        else
                        {

                            ShatterInfo si = new ShatterInfo()
                            {
                                mode = blockModel,
                                color = GetColorFromType(block.Color),
                                rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, block.Rotation),
                                position = position,
                                fadeAmount = block.FadeAmount,
                                scale = scale,
                                translation = translation,
                                time = time,
                                shatterRotation = rotation
                            };

                            if (fadeAmount > 0.01f)
                            {
                                //Use high res if its just for blocks being removed
                                if (block.FadeAmount > 0.01f)
                                    si.mode = isotopeHiRes;
                                this.shatterList.Add(si);
                            }
                            else
                                this.blockList.Add(si);
                        }
                    }
                }
            }

        }

        public Texture2D GetPreviewTexture()
        {
            if (this.Model != null)
            {
                if(this.Model.NextAtomCluster != null)
                return this.GetPreviewTexture(this.Model.NextAtomCluster.AtomColor1, this.Model.NextAtomCluster.AtomColor2, this.previewTarget);
            }
          
                return  this.GetPreviewTexture(BlockColor.Null, BlockColor.Null, this.previewTarget);
        }

        public Texture2D GetPreviewTexture(BlockColor color1, BlockColor color2, RenderTarget2D target)
        {
            device.RenderState.DepthBufferEnable = true;
            device.SetRenderTarget(0, target);
            device.Clear(Color.TransparentWhite);

            shatterEffect.Parameters["View"].SetValue(Matrix.Identity);
            shatterEffect.Parameters["Projection"].SetValue(
                Matrix.CreatePerspectiveFieldOfView((float)MathHelper.PiOver2, 1.0f, 1.0f, 5000.0f));
            shatterEffect.Parameters["modelTexture"].SetValue(blockTexture);

            shatterEffect.Begin();

            shatterEffect.CurrentTechnique.Passes[0].Begin();

            Vector3 position1 = new Vector3(-0.7f, 0f, 0f);

            Vector3 position2 = new Vector3(0.7f, 0f, 0f);

            float rotAmount = (float)Math.Sin(previewRotation);
            Matrix tempRot = Matrix.CreateRotationY(rotAmount * rotAmount * rotAmount *0.5f);
            //Matrix tempRot = Matrix.CreateRotationY(rotPosition * 6.28f);
            position1 = Vector3.Transform(position1, tempRot);
            position2 = Vector3.Transform(position2, tempRot);

            position1.Z -= 2.75f;
            position2.Z -= 2.75f;

            //Draw the atom cluster
            if (Model != null)
            {
                if (color1 != BlockColor.Null && color2 != BlockColor.Null)
                {
                    Quaternion quat = Quaternion.CreateFromAxisAngle(Vector3.Up, rotPosition * 3.14f);
                    DrawShatterModel(blockModel, quat, 0.0f, 0.3f,
                        position1, GetColorFromType(color1), 0f, 0f, 0f);
                    DrawShatterModel(blockModel, quat, 0.0f, 0.3f,
                        position2, GetColorFromType(color2), 0f, 0f, 0f);
                }
            }

            shatterEffect.CurrentTechnique.Passes[0].End();

            shatterEffect.End();

            device.SetRenderTarget(0, null);


            return target.GetTexture();
        }

        Vector3 GetPosition(Point p)
        {
            return GetPosition(p.X, p.Y);
        }

        Vector3 GetPosition(int x, int y)
        {

            float xDelta = 0.5f / width;
            float yDelta = 1.0f / height;
            Vector3 position = new Vector3(-0.45f + 2f * (xDelta * x), -1f + 2f * (yDelta * y), -2.75f);
            return position;
        }

        /// <summary>
        /// Helper for drawing a texture into a rendertarget, using
        /// a custom shader to apply postprocessing effects.
        /// </summary>
        void DrawFullscreenQuad(Texture2D texture, RenderTarget2D renderTarget,
                                Effect effect)
        {
            device.SetRenderTarget(0, renderTarget);

            DrawFullscreenQuad(texture,
                               renderTarget.Width, renderTarget.Height,
                               effect);

            device.SetRenderTarget(0, null);
        }


        /// <summary>
        /// Helper for drawing a texture into the current rendertarget,
        /// using a custom shader to apply postprocessing effects.
        /// </summary>
        void DrawFullscreenQuad(Texture2D texture, int width, int height,
                                Effect effect)
        {
            spriteBatch.Begin(SpriteBlendMode.None,
                              SpriteSortMode.Immediate,
                              SaveStateMode.None);


            effect.Begin();
            effect.CurrentTechnique.Passes[0].Begin();
            

            // Draw the quad.
            spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            effect.CurrentTechnique.Passes[0].End();
            effect.End();
            
        }

        /// <summary>
        /// Computes sample weightings and texture coordinate offsets
        /// for one pass of a separable gaussian blur filter.
        /// </summary>
        void SetBlurEffectParameters(float dx, float dy)
        {
            // Look up the sample weight and offset effect parameters.
            EffectParameter weightsParameter, offsetsParameter;

            weightsParameter = gaussianBlur.Parameters["SampleWeights"];
            offsetsParameter = gaussianBlur.Parameters["SampleOffsets"];

            // Look up how many samples our gaussian blur effect supports.
            int sampleCount = weightsParameter.Elements.Count;

            // Create temporary arrays for computing our filter settings.
            float[] sampleWeights = new float[sampleCount];
            Vector2[] sampleOffsets = new Vector2[sampleCount];

            // The first sample always has a zero offset.
            sampleWeights[0] = ComputeGaussian(0);
            sampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = sampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                sampleWeights[i * 2 + 1] = weight;
                sampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // To get the maximum amount of blurring from a limited number of
                // pixel shader samples, we take advantage of the bilinear filtering
                // hardware inside the texture fetch unit. If we position our texture
                // coordinates exactly halfway between two texels, the filtering unit
                // will average them for us, giving two samples for the price of one.
                // This allows us to step in units of two texels per sample, rather
                // than just one at a time. The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                sampleOffsets[i * 2 + 1] = delta;
                sampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < sampleWeights.Length; i++)
            {
                sampleWeights[i] /= totalWeights;
            }

            // Tell the effect about our new filter settings.
            weightsParameter.SetValue(sampleWeights);
            offsetsParameter.SetValue(sampleOffsets);
        }


        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        float ComputeGaussian(float n)
        {
            float theta = Settings.BlurAmount;

            return (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) *
                           Math.Exp(-(n * n) / (2 * theta * theta)));
        }


        Quaternion GetQuaternion(float rotation)
        {
      
            Quaternion quat1 = Quaternion.CreateFromAxisAngle(currentRotation, rotation);
            Quaternion quat2 = Quaternion.CreateFromAxisAngle(nextRotation, rotation);

            Vector3 newVec = Vector3.Lerp(currentRotation, nextRotation, rotPosition);
           // Quaternion outQuat = Quaternion.CreateFromAxisAngle(newVec, rotation);

            //outQuat.Normalize();
            //return outQuat;
            Quaternion outQuat = Quaternion.Lerp(quat1, quat2, rotPosition);
            //Quaternion outQuat=  Quaternion.Multiply(quat1, 1f - rotPosition) + Quaternion.Multiply(quat2, rotPosition) ;
            outQuat.Normalize();
            return outQuat;
        }



        void DrawShatterModel(Model model, Quaternion rotationQuat, float fadeAmount, float scale, Vector3 position, Color color, float rotation, float translation, float time)
        {

            this.SetupEffectParameters(this.shatterWorldParameter, this.shatterColorParameter, this.shatterFadeAmountParameter, rotationQuat, fadeAmount, scale, position, color);
            shatterEffect.Parameters["TranslationAmount"].SetValue(translation);
            shatterEffect.Parameters["RotationAmount"].SetValue(rotation);
            shatterEffect.Parameters["time"].SetValue(time);

            shatterEffect.CommitChanges();

            this.DrawModel(model);

        }

        void DrawBlockModel(Model model, Quaternion rotationQuat, float fadeAmount, float scale, Vector3 position, Color color)
        {
            this.SetupEffectParameters(this.blockWorldParameter, this.blockColorParameter, this.blockFadeAmountParameter, rotationQuat, fadeAmount, scale, position, color);
            this.blockEffect.CommitChanges();
            this.DrawModel(model);
        }


        void DrawBlockModel(ShatterInfo blockInfo)
        {
            this.SetupEffectParameters(this.blockWorldParameter, this.blockColorParameter, this.blockFadeAmountParameter,blockInfo.rotation, blockInfo.fadeAmount, blockInfo.scale, blockInfo.position, blockInfo.color);
            this.blockEffect.CommitChanges();

            this.DrawModel(blockInfo.mode);
        }


        void SetupEffectParameters(EffectParameter worldParam, EffectParameter colorParam, EffectParameter fadeParam, Quaternion rotationQuat, float fadeAmount, float scale, Vector3 position, Color color)
        {
            worldParam.SetValue(
                Matrix.CreateFromQuaternion(rotationQuat) *
                Matrix.CreateScale(scale) * Matrix.CreateTranslation(position));

            colorParam.SetValue(color.ToVector4());
            fadeParam.SetValue(fadeAmount);
        }
           
        void DrawModel(Model model)
        {
            for (int j = 0; j < model.Meshes.Count; j++)
            {
                ModelMesh mesh = model.Meshes[j];
                device.Indices = mesh.IndexBuffer;
                for (int k = 0; k < mesh.MeshParts.Count; k++)
                {
                    ModelMeshPart part = mesh.MeshParts[k];
                    device.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                    device.VertexDeclaration = part.VertexDeclaration;
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);

                }
            }
        }


        Color GetColorFromType(BlockColor color)
        {
            switch (color)
            {
                default:
                    return Color.White;
                case BlockColor.Blue:
                    return Color.Blue;
                case BlockColor.Red:
                    return Color.Red;
                case BlockColor.Yellow:
                    return Color.Yellow;
                case BlockColor.Null:
                    return Color.Orange;
            }
        }


    }
}
