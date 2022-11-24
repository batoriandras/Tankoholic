﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TankoholicClient
{
    internal class Button : Component
    {
        public string Text { get; set; }
        public Action OnClick
        {
            get; private set;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Texture2D ImageTexture { get; private set; }


        public Button() { }

        public Button(Vector2 position, Action onClick, string text = "", Texture2D image = null,
            int width = 100, int height = 50, Object id = null)
        {
            this.Text = text;
            this.OnClick = onClick;
            this.Position = position;
            this.ImageTexture = image;
            this.Width = width;
            this.Height = height;
        }
    }
}
