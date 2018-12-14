using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projet
{
    public abstract class Sprite : IActor
    {
        // IActor
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; private set; }
        public bool ToRemove { get; set; }

        // Sprite
        public Texture2D Texture { get; }
        public float vx;
        public float vy;

        public Sprite(Texture2D pTexture)
        {
            Texture = pTexture;
        }

        public void Move(float pX, float pY)
        {
            Position = new Vector2(Position.X + pX, Position.Y + pY);
        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update(GameTime pGameTime)
        {
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            Move(vx, vy);
        }

        public virtual void TouchedBy(IActor pBy)
        {

        }
    }
}
