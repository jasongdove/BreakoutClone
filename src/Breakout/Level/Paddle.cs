using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Paddle : GameplayObject
    {
        private const float HorizontalVelocity = 70f;

        private readonly World _world;
        private readonly Rectangle _screenBounds;

        public Paddle(World world, Rectangle screenBounds)
        {
            _world = world;
            _screenBounds = screenBounds;
        }

        public override Vector2 Origin
        {
            get { return Vector2.Zero; }
        }

        public void MoveLeft()
        {
            Body.ApplyLinearImpulse(ConvertUnits.ToSimUnits(new Vector2(-HorizontalVelocity, 0)));
        }

        public void MoveRight()
        {
            Body.ApplyLinearImpulse(ConvertUnits.ToSimUnits(new Vector2(HorizontalVelocity, 0)));
        }

        public void StopMoving()
        {
            Body.LinearVelocity = Vector2.Zero;
        }

        public void SetStartPosition()
        {
            Body.Position = new Vector2(
                ConvertUnits.ToSimUnits((_screenBounds.Width - Texture.Width) / 2f),
                ConvertUnits.ToSimUnits(_screenBounds.Height - 70));
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("paddleBlu");

            base.LoadContent(content);
        }

        public override void Initialize()
        {
            if (_world != null && Texture != null)
            {
                var data = new uint[Texture.Width * Texture.Height];
                Texture.GetData(data);
                var verts = PolygonTools.CreatePolygon(data, Texture.Width, true);
                var scale = ConvertUnits.ToSimUnits(new Vector2(1, 1));
                verts.Scale(ref scale);
                var list = BayazitDecomposer.ConvexPartition(verts);
                var compound = BodyFactory.CreateCompoundPolygon(_world, list, 1);
                compound.BodyType = BodyType.Dynamic;
                Body = compound;
                Body.Restitution = 1;
                
                SetStartPosition();

                var joint = new FixedPrismaticJoint(Body, Body.Position, new Vector2(1, 0));
                joint.LimitEnabled = true;
                joint.LowerLimit = -ConvertUnits.ToSimUnits((_screenBounds.Width - Texture.Width) / 2f);
                joint.UpperLimit = ConvertUnits.ToSimUnits((_screenBounds.Width - Texture.Width) / 2f);
                joint.Enabled = true;
                joint.MaxMotorForce = 70f;
                _world.AddJoint(joint);
            }

            base.Initialize();
        }
    }
}