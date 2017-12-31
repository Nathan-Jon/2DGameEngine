﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using EngineV2.Buttons;
using EngineV2.BackGround;
using EngineV2.Managers;
using EngineV2.Interfaces;

namespace EngineV2.Scenes
{
    class GameOver : IScene
    {

        IButton ExitBut;
        IBackGrounds back;
        MouseState mouseinput;
        Point mousePosition;


        public GameOver()
        {

            back = new BackGrounds(900, 600);
            ExitBut = new ExitButton();
        }


        public void LoadContent(ContentManager Content)
        {
            SoundManager.getSoundInstance.Initialize(Content.Load<SoundEffect>("background"));
            SoundManager.getSoundInstance.CreateInstance();


            back.Initialize(Content.Load<Texture2D>("LoseGame"));
            ExitBut.Initialize(Content.Load<Texture2D>("Exit Button"), new Vector2(355, 300));

        }

        public void update(GameTime gameTime)
        {
            ExitBut.update();
            mouseinput = Mouse.GetState();
            mousePosition = new Point(mouseinput.X, mouseinput.Y);

            if (ExitBut.getHitbox().Contains(mousePosition) && mouseinput.LeftButton == ButtonState.Pressed)
            {
                ExitBut.click();
            }

            if (SceneManager.LoseScreen == true)
            {
                SoundManager.getSoundInstance.Playsnd(0, 0.5f);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            back.Draw(spriteBatch);
            ExitBut.Draw(spriteBatch);
        }
    }
}
