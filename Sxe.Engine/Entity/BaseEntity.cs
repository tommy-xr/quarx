using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Entity
{
    public class BaseEntity
    {
        IServiceProvider services;
        protected IServiceProvider Services
        {
            get { return services; }
        }

        public BaseEntity(IServiceProvider inServices)
        {
            services = inServices;
        }


        public virtual void Create()
        {
        }

        public virtual void Spawn()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void Touch(Entity touchEntity)
        {
        }

        public virtual void Trigger(Entity triggerEntty)
        {
        }

        public virtual void Use(Entity useEntity)
        {
        }

        public virtual void PreDraw(GameTime gameTime, Vector3 position,
            Matrix view, Matrix projection)
        {
        }

        public virtual void Draw(GameTime gameTime, Vector3 position,
            Matrix view, Matrix projection)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void TakeDamage(Entity inAttacker, float dmgAmount, object dmgType)
        {
        }

        public virtual void Die()
        {
        }
    }
}
