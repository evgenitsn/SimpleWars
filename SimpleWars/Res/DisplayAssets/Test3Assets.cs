﻿namespace SimpleWars.Res.DisplayAssets
{
    using Microsoft.Xna.Framework.Graphics;

    using SimpleWars.Res.Interfaces;

    /// <summary>
    /// The test 3 assets.
    /// </summary>
    public class Test3Assets : IAssetLoader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test3Assets"/> class.
        /// </summary>
        public Test3Assets()
        {
            this.LoadAssets();
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public Model Model { get; private set; }

        /// <summary>
        /// Gets the terrain texture.
        /// </summary>
        public Texture2D TerrainTexture { get; private set; }

        public Model Terra { get; private set; }

        /// <summary>
        /// The load assets.
        /// </summary>
        public void LoadAssets()
        {
            Assets3Manager.Instance.Load3DAsset("Models3D", "tree");
            Assets3Manager.Instance.Load3DAsset("TerrainTextures", "test");
            Assets2Manager.Instance.Load2DAsset("TerrainTextures", "grass");

            this.Model = Assets3Manager.Instance.Get3DAsset("tree").Model;
            this.TerrainTexture = Assets2Manager.Instance.Get2DAsset("grass").Texture;
            this.Terra = Assets3Manager.Instance.Get3DAsset("test").Model;
        }

        /// <summary>
        /// The unload assets.
        /// </summary>
        public void UnloadAssets()
        {
            Assets3Manager.Instance.Dispose3DAsset("Models3D", "tree");
            Assets2Manager.Instance.Dispose2DAsset("TerrainTextures", "grass");
            Assets3Manager.Instance.Dispose3DAsset("TerrainTextures", "test");
        }
    }
}