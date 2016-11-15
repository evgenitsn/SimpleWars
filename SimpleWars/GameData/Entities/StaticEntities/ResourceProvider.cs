﻿namespace SimpleWars.GameData.Entities.StaticEntities
{
    using System;
    using Microsoft.Xna.Framework;

    using SimpleWars.GameData.EconomyData.ConcreteResources;
    using SimpleWars.GameData.Entities.Interfaces;

    /// <summary>
    /// The resource provider.
    /// </summary>
    public abstract class ResourceProvider : Entity, IResourceProvider
    {
        #region Constructors
        protected ResourceProvider()
            : base()
        {
        }

        protected ResourceProvider(int quantity, string resourceType, Vector3 position, float scale = 1) 
            : this(quantity, resourceType, position, Vector3.Zero, scale)
        {
        }

        protected ResourceProvider(int quantity, string resourceType, Vector3 position, Vector3 rotation, float scale = 1) 
            : this(quantity, resourceType, position, rotation, 1f, scale)
        {
        }

        protected ResourceProvider(int quantity, string resourceType, Vector3 position, Vector3 rotation, float weight, float scale) 
            : base(position, rotation, weight, scale)
        {
            this.Quantity = quantity;
            this.ResourceType = resourceType;
        }
        #endregion

        #region Resource Provider Implementation
        private int quantity;

        public int Quantity
        {
            get
            {
                return this.quantity;
            }

            set
            {
                if (value <= 0)
                {
                    this.quantity = 0;
                    this.Disappear();
                    return;
                }

                this.quantity = value;
            }
        }

        public string ResourceType { get; protected set; }

        public void Gather(int amount)
        {
            int mined = this.Quantity - amount < 0 ? Math.Abs(this.Quantity - amount) : amount;

            if (this.ResourceType == typeof(Gold).Name)
            {
                this.Player.ResourceSet.Gold.Quantity += mined;
            }
            else if (this.ResourceType == typeof(Food).Name)
            {
                this.Player.ResourceSet.Food.Quantity += mined;
            }
            else if (this.ResourceType == typeof(Wood).Name)
            {
                this.Player.ResourceSet.Wood.Quantity += mined;
            }
            else if (this.ResourceType == typeof(Rock).Name)
            {
                this.Player.ResourceSet.Rock.Quantity += mined;
            }
            else if (this.ResourceType == typeof(Metal).Name)
            {
                this.Player.ResourceSet.Metal.Quantity += mined;
            }
            else if (this.ResourceType == typeof(Population).Name)
            {
                this.Player.ResourceSet.Population.Quantity += mined;
            }

            this.Quantity -= amount;
        }

        public void Disappear()
        {
            // No disappear logic yet
        }
        #endregion
    }
}