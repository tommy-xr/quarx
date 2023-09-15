using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sxe.Engine.Entity
{
    public delegate void ThinkFunction(GameTime gameTime);

    public class Entity : BaseEntity,IRecycleable
    {
        #region Private Members
        int recycleIndex;
        Vector3 position;
        int id;
        string name;
        IServiceProvider services;
        EntityModuleCollection modules = new EntityModuleCollection();
        EntityManager manager;
        ThinkFunction thinkFunction = null;
        double nextThink = 0.0;
        double thinkDelay = 0.1;
        #endregion

        #region Properties
        int IRecycleable.RecycleIndex
        {
            get { return recycleIndex; }
            set { recycleIndex = value; }
        }

        public virtual Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public EntityManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public double NextThink
        {
            get { return nextThink; }
        }

        public double ThinkDelay
        {
            get { return thinkDelay; }
            set { thinkDelay = value; }
        }

        public ThinkFunction ThinkFunction
        {
            get { return thinkFunction; }
            set { thinkFunction = value; nextThink = 0.0;  }
        }

        protected EntityModuleCollection Modules
        {
            get { return modules; }
        }


        #endregion



        #region Constructors
        public Entity(IServiceProvider inServices)
            : base(inServices)
        {

            //TODO: Populate with some popular services
        }
        #endregion

        /// <summary>
        /// Called when the entity is created
        /// </summary>
        public override void Create()
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Create();
        }

        /// <summary>
        /// Called when the entity is spawned into the world
        /// </summary>
        public override void Spawn()
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Spawn();
        }

        /// <summary>
        /// Called when the entity is de-allocated and removed from the world
        /// </summary>
        public override void Destroy()
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Destroy();

            if (manager != null)
                manager.DestroyEntity(this);
        }

        /// <summary>
        /// Called when the entity is touched by another entity
        /// </summary>
        /// <param name="entity"></param>
        public override void Touch(Entity entity)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Touch(entity);
        }

        public override void TakeDamage(Entity inflictor, float dmgAmount, object damageType)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].TakeDamage(inflictor, dmgAmount, damageType);
        }

        public override void Die()
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Die();
        }

        public override void Trigger(Entity triggerEntity)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Trigger(triggerEntity);
        }

        public override void Use(Entity useEntity)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Use(useEntity);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Update(gameTime);

            if (thinkFunction != null)
            {
                //See if its time to do another think?
                nextThink += gameTime.ElapsedGameTime.TotalSeconds / thinkDelay;

                if (nextThink >= 1.0)
                {
                    thinkFunction(gameTime);
                    nextThink = 0.0;
                }
            }
        }

        public override void Draw(GameTime gameTime, Vector3 position,
            Matrix view, Matrix projection)
        {
            for (int i = 0; i < modules.Count; i++)
                modules[i].Draw(gameTime, position, view, projection);
        }


    }
}
