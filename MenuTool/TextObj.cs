using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTool
{
    internal class TextObj : GameObject
    {
        public override bool isActive { get; set; }
        public override Texture2D[] textures { get; set; }
        public override Vector2 position { get; set; }
        public override int currentTexture { get; set; }
        public override string text { get; set; }
        public override float layerDepth { get; set; }
        public override SpriteFont font { get; set; }

        /// <summary>
        /// This is only used by buttons without custom textures. It is generated on top of the ugly testMenuTextures. It can, however, be used to draw text
        /// anywhere, just make sure you add them to Game1.gameObjects. 
        /// </summary>
        /// <param name="text">Text to be drawn</param>
        /// <param name="position">Coordinates from which to draw the text</param>
        /// <param name="layerDepth"></param>
        /// <param name="font"></param>
        public TextObj(string text, Vector2 position, float layerDepth = 0f, SpriteFont font = null)
        {
            this.layerDepth = layerDepth;
            isActive = true;
            this.text = text;
            this.position = position;

            if (font != null)
            {
                this.font = font;
            }
            else this.font = Game1.infinium;
        }



    }
}
