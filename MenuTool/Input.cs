using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTool
{
    static public class Input
    {
        static public bool PointIsInRectangle(GameObject obj, Vector2 point)
        {
            var rectangle = new Vector4(obj.position.X, obj.position.Y, obj.position.X + obj.textures[0].Width, obj.position.Y + obj.textures[0].Height);

            if (point.X >= rectangle.X && point.X <= rectangle.Z && point.Y >= rectangle.Y && point.Y <= rectangle.W)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
