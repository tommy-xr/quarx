using System;
using System.Collections.Generic;
using System.Text;

using Engine.Source.Shared.Entities;
using Engine.Source.Client.BSP;

namespace SXERadMapper
{
    class RadiosityEntityManager : EntityManager
    {
        public RadiosityEntityManager(IServiceProvider services, RenderBsp bsp)
            : base(1024, services, bsp)
        {
        }

        public override void Initialize()
        {
            LinkEntity<LightEntity>("light", 1024);
        }
    }
}
