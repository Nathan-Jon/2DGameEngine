﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EngineV2.Interfaces;
using EngineV2.Managers;
using EngineV2.Collision_Management;
using EngineV2.Entities;
using EngineV2.Input;
using EngineV2.Input_Managment;

namespace EngineV2.Entities
{
    class Lever : GameEntity
    {
        #region Instance Variables

        //BEHAVIOURS
        //LEVER RESPONSIBILITY CLASS
        IEntity target;

        private bool canTrigger = false;
        //Input Management
        private KeyboardState keyState;

        //Collision Management
        private IEntity collisionObj;
        private ICollidable colliders;
        private List<IEntity> playerObj;
        private List<IEntity> targetObjs;

        #endregion

        //OnKeyboard Event Change scene

        public override void Initialize(Texture2D Tex, Vector2 Posn, ICollidable _collider, IPhysicsObj phys, IBehaviourManager behaviours)
        {
            Position = Posn;
            Texture = Tex;
            colliders = _collider;

            //SUBSCRIBERS
            InputManager.GetInputInstance.AddListener(OnNewInput);
            CollisionManager.GetColliderInstance.subscribe(onCollision);
      
            //CALL COLLIDABLEOBJS()
            CollidableObjs();
        }

        #region EVENTS

        //INITIALISE EVENT HANDLERS

        //INPUT EVENTS
        public virtual void OnNewInput(object source, EventData data)
        {
            keyState = data.newKey;
            if (canTrigger && keyState.IsKeyDown(Keys.H) || canTrigger && keyState.IsKeyDown(Keys.E))
            {
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    if (targetObjs[i].getTag() == "leverObj")
                    {
                            targetObjs[i].setYPos(105);
   
                    }
                }
            }
        }

        //INITIALISE INTERACTIVEOBJS LIST
        public override void CollidableObjs()
        {
            playerObj = colliders.getPlayableObj();
            targetObjs = colliders.getEnvironment();
        }

        //COLLISION EVENTS
        public virtual void onCollision(object source, CollisionEventData data)
        {
            collisionObj = data.objectCollider;

            for (int i = 0; i < playerObj.Count; i++)
            {
                //checks to see if player is in contact with the lever 
                if (HitBox.Intersects((playerObj[0].getHitbox())))
                {
                    //CAN ACTIVATE LEVER
                    canTrigger = true;
                }
                else canTrigger = false;
            }
        }

        //Draw Method
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, Color.AntiqueWhite);

        }
        //Update Method
        public override void update(GameTime game)
        {

            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
        #endregion


        #region GET/SETS
                public override void setXPos(float Xpos)
        {
            Position.X = Xpos;
        }
        public override void setYPos(float Ypos)
        {
            Position.Y = Ypos;
        }
        public override void setRow(int rows)
        {
            row = rows;
        }
        #endregion
    }
}
