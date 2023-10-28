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

        static public bool RectangleIsInRectangle(Primitive obj1, Primitive obj2)
        {
            var rectangle1 = new Vector4(obj1.position.X, obj1.position.Y, obj1.position.X + obj1.width, obj1.position.Y + obj1.height);
            var rectangle2 = new Vector4(obj2.position.X, obj2.position.Y, obj2.position.X + obj2.width, obj2.position.Y + obj2.height);

            if (rectangle2.Y >= rectangle1.W && rectangle2.W <= rectangle1.Y && rectangle2.Z >= rectangle1.X && rectangle2.X <= rectangle1.Z)       
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
