using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;

using Microsoft.Xna.Framework;

namespace Quarx
{
    public class PuzzleGameModel : PlayerGameModel
    {
        private PuzzleDescription puzzleDescription;
        private Queue<AtomClusterDescription> atomClusters = new Queue<AtomClusterDescription>();


        public Queue<AtomClusterDescription> AtomClusters
        {
            get { return atomClusters; }
        }


        public PuzzleGameModel(int width, int height, IAnarchyGamer gamer, PuzzleDescription description)
            : base(width, height, gamer)
        {
            
            

            puzzleDescription = description;
        }

        public override bool CheckLossCondition()
        {
            if (atomClusters.Count == 0 && CurrentAtomCluster == null && NextAtomCluster == null && !IsFalling )
                return true;

            return base.CheckLossCondition();
        }

        public override AtomCluster GetNewAtomCluster(Point position)
        {
            if(atomClusters.Count > 0)
            {
                AtomClusterDescription description = atomClusters.Dequeue();
                AtomCluster ac = new AtomCluster(this, description.Color1, description.Color2, position);
                return ac;
            }
            else
            {
                return null;
            }
        }

        public override bool CheckVictoryCondition()
        {
            int totalCount = 0;
            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < Board.Height; y++)
                {
                    if (Board[x, y] != null)
                        totalCount++;
                }
            }

            if (totalCount > 0)
                return false;

            if (atomClusters.Count > 0 || NextAtomCluster != null || CurrentAtomCluster != null)
                return false;

            return true;


            //return base.CheckVictoryCondition();
        }

        public override void Start(int maxHeight, int numberOfIsotopes, int numberOfAtoms, int seed)
        {
            for (int y = 0; y < 18; y++)
            {
                for (int x = 0; x < Board.Width; x++)
                {
                    Tile tile = puzzleDescription.GetTile(x, y);

                    if(tile != null)
                    {
                        if (tile.IsBlock)
                        {
                            if (tile.Type == BlockType.Isotope)
                            {
                                AddIsotope(tile.Color, new Microsoft.Xna.Framework.Point(x, y));
                            }
                            else if (tile.Type == BlockType.Atom)
                            {
                                AddBlock(tile.Color, new Microsoft.Xna.Framework.Point(x, y));
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < puzzleDescription.AtomClusters.Count; i++)
            {
                atomClusters.Enqueue(puzzleDescription.AtomClusters[i]);
            }

            //base.Start(maxHeight, numberOfIsotopes, numberOfAtoms, seed);
        }
    }
}
