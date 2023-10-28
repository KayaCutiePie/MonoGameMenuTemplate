using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MenuTool
{
    public abstract class GameObject
    {
        public abstract bool isActive { get; set; }
        public abstract Texture2D[] textures { get; set; }
        public abstract Vector2 position { get; set; }
        public abstract int currentTexture { get; set; }
        public abstract SpriteFont font { get; set; }
        public abstract string text { get; set; }
        public abstract float layerDepth { get; set; }


        //Draws the GameObject, called by Game1.Draw()
        public void DrawGameObject(SpriteBatch _spriteBatch, float rotation = 0)
        {
            if (this.GetType() == typeof(TextObj))
            {
                _spriteBatch.DrawString(Game1.infinium, text, position, Color.Red, rotation, new Vector2(0, 0), 1, SpriteEffects.None, layerDepth);
            }
            else if (this.GetType() == typeof(Primitive))
            {

            }
            else
            {
                _spriteBatch.Draw(
                    textures[this.currentTexture],
                    position,
                    null,
                    Color.White,
                    rotation,
                    new Vector2(0, 0),
                    1,
                    SpriteEffects.None,
                    layerDepth);
            }


        }

        public virtual void Click()
        {

        }

        public virtual void Update()
        {

        }
    }


}
