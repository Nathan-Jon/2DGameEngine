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
using EngineV2.Input;
using EngineV2.Input_Managment;

namespace EngineV2.Entities
{
    class PressurePlate : GameEntity
    {
        public string tag = "PressurePlate";
        public static Texture2D Texture;
        public Vector2 Position;
        public Rectangle HitBox;
        private float moveDirec = 3;
        private bool moveObject = false;
        private bool canMove = true;
        private bool crateContact = false;

        //Physics
        public bool gravity = true;

        //Input Management
        private KeyboardState keyState;

        //Collision Management
        private IEntity collisionObj;
        private ICollidable colliders;
        IPhysicsObj physics;

        //Lists
        private List<IEntity> physicsObj;
        private List<IEntity> environementObjs;
        private List<IEntity> interactiveObj;
        private IEntity triggerWall;



        public override void Initialize(Texture2D Tex, Vector2 Posn, ICollidable _collider, IPhysicsObj phys, IBehaviourManager behaviours)
        {
            Position = Posn;
            Texture = Tex;
            colliders = _collider;
            physics = phys;
            InputManager.GetInputInstance.AddListener(OnNewInput);

            CollisionManager.GetColliderInstance.subscribe(onCollision);
            CollidableObjs();
            _collider.isInteractiveCollidable(this);
        }

        public virtual void OnNewInput(object source, EventData data)
        {
            keyState = data.newKey;
        }

        //List of Collidable Objects
        public override void CollidableObjs()
        {
            //physicsObj = physics.getPhysicsList();
            environementObjs = colliders.getEnvironment();
            interactiveObj = colliders.getInteractiveObj();
        }

        public virtual void onCollision(object source, CollisionEventData data)
        {
            collisionObj = data.objectCollider;


            #region Player Collision
            for (int i = 0; i < interactiveObj.Count; i++)
            {
                if (HitBox.Intersects(interactiveObj[i].getHitbox()) && interactiveObj[i].getTag() == "Crate")
                {
                    activate();
                }
                
            }
            
            #endregion
        }

        #region behaviours

        public void activate()
        {
            for (int i = 0; i < environementObjs.Count; i++)
            {
                if (environementObjs[i].getTag() == "triggerWall")
                {
                    triggerWall = environementObjs[i];
                }
            }
            triggerWall.setYPos(1000);
        }

        public void reset()
        { triggerWall.setYPos(351); }

        #endregion


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.AntiqueWhite);
        }

        public override void update(GameTime game)
        {
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }


        #region  get/Sets


        public override Vector2 getPos()
        {
            return Position;
        }

        public override Rectangle getHitbox()
        {
            return HitBox;
        }
        public override bool getGrav()
        {
            return gravity;
        }
        public override string getTag()
        {
            return tag;
        }

        public override void setYPos(float Ypos)
        {
            Position.Y = Ypos;
        }
        public override void setXPos(float Xpos)
        {
            Position.X = Xpos;
        }
        public override void setGrav(bool active)
        {
            gravity = active;
        }

        #endregion
    }
}