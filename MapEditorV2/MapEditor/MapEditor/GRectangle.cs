using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class GCRectangle : IDisposable
    {
        public enum Type { fill, outline };
        public Rectangle Rect;
        public Rectangle InsideRect;
        public Color FillColor;
        public Color LineColor;
        private Type _type;
        private Texture2D _textureFill;
        private Texture2D _textureLine;
        private Game _game;
        public GCRectangle(Game pGame, Type pType, int pfX, int pfY, int pWidth, int pfHauteur, Color pFillColor, Color pLineColor)
        {
            _game = pGame;
            Rect = new Rectangle(pfX, pfY, pWidth, pfHauteur);
            InsideRect = new Rectangle(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            InsideRect.Inflate(-1, -1);
            _type = pType;
            FillColor = pFillColor;
            LineColor = pLineColor;

            _textureFill = new Texture2D(_game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _textureFill.SetData(new[] { FillColor });
            _textureLine = new Texture2D(_game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _textureLine.SetData(new[] { LineColor });
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            if (_type == Type.fill)
            {
                pSpriteBatch.Draw(_textureFill, Rect, FillColor);
            }
            else if (_type == Type.outline)
            {
                pSpriteBatch.Draw(_textureLine, Rect, LineColor);
                pSpriteBatch.Draw(_textureFill, InsideRect, FillColor);
            }
            else
            {
                Debug.Fail("Not implemented");
            }
        }

        public void Dispose()
        {
            Debug.WriteLine("GCRectangle:Dispose");
            _textureFill.Dispose();
        }
    }

}
