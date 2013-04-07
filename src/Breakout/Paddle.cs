using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle : Sprite
    {
        private const float HorizontalVelocity = 70f;

        private readonly FixedPrismaticJoint _joint;

        public Paddle(Texture2D texture, Rectangle screenBounds, World world)
            : base(texture, screenBounds)
        {
            var data = new uint[texture.Width * texture.Height];
            texture.GetData(data);
            var verts = PolygonTools.CreatePolygon(data, texture.Width, true);
            var scale = ConvertUnits.ToSimUnits(new Vector2(1, 1));
            verts.Scale(ref scale);
            var list = BayazitDecomposer.ConvexPartition(verts);
            var compound = BodyFactory.CreateCompoundPolygon(world, list, 1);
            compound.BodyType = BodyType.Dynamic;
            Body = compound;
            Body.Restitution = 1;
            SetStartPosition();

            _joint = new FixedPrismaticJoint(Body, Body.Position, new Vector2(1, 0));
            _joint.LimitEnabled = true;
            _joint.LowerLimit = -ConvertUnits.ToSimUnits((screenBounds.Width - Width) / 2f);
            _joint.UpperLimit = ConvertUnits.ToSimUnits((screenBounds.Width - Width) / 2f);
            _joint.Enabled = true;
            _joint.MaxMotorForce = HorizontalVelocity;
            world.AddJoint(_joint);
        }

        public void SetStartPosition()
        {
            Body.Position = new Vector2(
                ConvertUnits.ToSimUnits((ScreenBounds.Width - Width) / 2f),
                ConvertUnits.ToSimUnits(ScreenBounds.Height - 50));
        }

        public override void Update(GameTime gameTime)
        {
            Body.LinearVelocity = Vector2.Zero;

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Body.ApplyLinearImpulse(ConvertUnits.ToSimUnits(new Vector2(-HorizontalVelocity, 0)));
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                Body.ApplyLinearImpulse(ConvertUnits.ToSimUnits(new Vector2(HorizontalVelocity, 0)));
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}