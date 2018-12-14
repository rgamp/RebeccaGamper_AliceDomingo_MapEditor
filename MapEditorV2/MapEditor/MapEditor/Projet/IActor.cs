using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public interface IActor
    {
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; }
        bool ToRemove { get; set; }

        void Update(GameTime pGameTime);
        void Draw(SpriteBatch pSpriteBatch);
        void TouchedBy(IActor pBy);
    }
}
