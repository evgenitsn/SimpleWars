﻿namespace SimpleWars.GUI.Layouts
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using SimpleWars.Assets;
    using SimpleWars.Data.Contexts;
    using SimpleWars.GUI.Interfaces;
    using SimpleWars.GUI.PrimitiveComponents;
    using SimpleWars.Input;
    using SimpleWars.Users;
    using SimpleWars.Users.Enums;

    public class LoginLayout : ILayout
    {
        public LoginLayout(GraphicsDevice device, GameContext context)
        {
            this.LoginState = LoginState.None;

            this.Buttons = new HashSet<IButton>();
            this.TextBoxes = new HashSet<ITextBox>();
            this.TextNodes = new HashSet<ITextNode>();

            this.Background = new Texture2D(device, 1, 1);
            this.Background.SetData<Color>(new Color[] { Color.Transparent });

            // Just placeholder values for now. Will be properly calculated.
            this.Dimensions = new Vector2(240, 140);
            this.Position = new Vector2(500, 300);

            this.InitializeComponents(context);
        }

        public LoginState LoginState { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Dimensions { get; set; }

        public ICollection<IButton> Buttons { get; }

        public ICollection<ITextBox> TextBoxes { get; }

        public ICollection<ITextNode> TextNodes { get; }

        public Texture2D Background { get; set; }

        public void Update(GameTime gameTime, GameContext context)
        {
            if (Input.LeftMouseClick())
            {
                float mouseX = Input.MousePos.X;
                float mouseY = Input.MousePos.Y;

                foreach (var button in this.Buttons)
                {
                    button.DetectClick(mouseX, mouseY);
                }

                foreach (var textBox in this.TextBoxes)
                {
                    textBox.DetectClick(mouseX, mouseY);
                }
            }
            else
            {
                foreach (var textBox in this.TextBoxes)
                {
                    textBox.ReadInput(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Background, this.Position, null, Color.White, 0f, Vector2.Zero, this.Dimensions, SpriteEffects.None, 0f);

            foreach (var button in this.Buttons)
            {
                button.Draw(spriteBatch);
            }

            foreach (var textBox in this.TextBoxes)
            {
                textBox.Draw(spriteBatch);
            }

            foreach (var textNode in this.TextNodes)
            {
                textNode.Draw(spriteBatch);
            }

            if (this.LoginState == LoginState.Invalid)
            {
                spriteBatch.DrawString(SpriteFontManager.Instance.GetFont("Arial_18"), "Invalid credentials", this.Position + new Vector2(20, -20), Color.Red);
            }
        }


        private void InitializeComponents(GameContext context)
        {
            var usernameTb = new TextBox(
                this.Position + new Vector2(20, 20),
                new Vector2(200, 30),
                Color.Black,
                Color.White);

            var passwordTb = new TextBox(
                this.Position + new Vector2(20, 60),
                new Vector2(200, 30),
                Color.Black,
                Color.White);

            var usernameTbDefaultTextNode = new TextNode(
                usernameTb,
                new Vector2(30, 0),
                Vector2.One,
                "Username",
                SpriteFontManager.Instance.GetFont("Arial_Italic_22"),
                Color.Gray);

            var passwordDefaultTextNode = new TextNode(
                passwordTb,
                new Vector2(30, 0),
                Vector2.One,
                "Password",
                SpriteFontManager.Instance.GetFont("Arial_Italic_22"),
                Color.Gray);

            var usernameTbPartialTextNode = new PartialTextNode(
                usernameTb,
                new Vector2(8, 0),
                Vector2.One,
                SpriteFontManager.Instance.GetFont("Arial_22"),
                Color.Black,
                12,
                12);

            var passwordTbPartialTextNode = new PasswordTextNode(
                passwordTb,
                new Vector2(15, 3),
                Vector2.One, 
                SpriteFontManager.Instance.GetFont("Arial_26"),
                Color.Black,
                12,
                '*',
                12);


            usernameTb.DefaultTextNode = usernameTbDefaultTextNode;
            passwordTb.DefaultTextNode = passwordDefaultTextNode;

            usernameTb.TextNode = usernameTbPartialTextNode;
            passwordTb.TextNode = passwordTbPartialTextNode;
            

            this.TextBoxes.Add(usernameTb);
            this.TextBoxes.Add(passwordTb);

            var loginButton = new Button(
                this.Position + new Vector2(20, 100),
                this.Background,
                new Vector2(200, 30),
                new Vector2(70, 0),
                () =>
                    {
                        this.LoginState = UsersManager.LoginUser(usernameTb.TextNode.TextContent, passwordTb.TextNode.TextContent, context);
                    });

            var loginButtonTextNode = new TextNode(loginButton, new Vector2(70, 0), Vector2.One, "Log In", SpriteFontManager.Instance.GetFont("Arial_22"), Color.Black);

            loginButton.TextNode = loginButtonTextNode;

            this.Buttons.Add(loginButton);
        }
    }
}