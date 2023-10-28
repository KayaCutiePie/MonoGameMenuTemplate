using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace MenuTool
{
    public class Primitive : GameObject
    {
        public override bool isActive { get; set; }
        public override Texture2D[] textures { get; set; }
        public override Vector2 position { get; set; }
        public override int currentTexture { get; set; }
        public override SpriteFont font { get; set; }
        public override string text { get; set; }
        public override float layerDepth { get; set; }

        public float width;
        public float height;
        public Color color;

        

        private int[] allocatedVertices;

        public Primitive(Color color, float x, float y, float width = 100f, float height = 100f)
        {
            Game1.gameObjects.Add(this);

            isActive = true;
            position = new Vector2(x,y);
            this.width = width;
            this.height = height; 
            this.color = color;

            const int shapeVertexCount = 4;

            Game1.primitiveIndices[Game1.indexCount++] = 0 + Game1.vertexCount;
            Game1.primitiveIndices[Game1.indexCount++] = 1 + Game1.vertexCount;
            Game1.primitiveIndices[Game1.indexCount++] = 2 + Game1.vertexCount;
            Game1.primitiveIndices[Game1.indexCount++] = 0 + Game1.vertexCount;
            Game1.primitiveIndices[Game1.indexCount++] = 2 + Game1.vertexCount;
            Game1.primitiveIndices[Game1.indexCount++] = 3 + Game1.vertexCount;

            allocatedVertices = new int[shapeVertexCount]
            {
                Game1.vertexCount++,
                Game1.vertexCount++,
                Game1.vertexCount++,
                Game1.vertexCount++
            };

            Game1.primitiveVertices[allocatedVertices[0]] = new VertexPositionColor(new Vector3(position.X, position.Y + height, 0f), color);
            Game1.primitiveVertices[allocatedVertices[1]] = new VertexPositionColor(new Vector3(position.X + width, position.Y + height, 0f), color);
            Game1.primitiveVertices[allocatedVertices[2]] = new VertexPositionColor(new Vector3(position.X + width, position.Y, 0f), color);
            Game1.primitiveVertices[allocatedVertices[3]] = new VertexPositionColor(new Vector3(position.X, position.Y, 0f), color);
        }

        public override void Update()
        {
            Game1.primitiveVertices[allocatedVertices[0]] = new VertexPositionColor(new Vector3(position.X, position.Y + height,0f), color);
            Game1.primitiveVertices[allocatedVertices[1]] = new VertexPositionColor(new Vector3(position.X + width, position.Y + height, 0f), color);
            Game1.primitiveVertices[allocatedVertices[2]] = new VertexPositionColor(new Vector3(position.X + width, position.Y, 0f), color);
            Game1.primitiveVertices[allocatedVertices[3]] = new VertexPositionColor(new Vector3(position.X, position.Y, 0f), color);
          
        }

        public static Vector2 PongPrimitive(GameObject obj, float x, float y)
        {

            Vector2 newPosition = obj.position + new Vector2(x,y);

            foreach (GameObject obj2 in Game1.gameObjects)
            {
                if (Input.RectangleIsInRectangle(obj as Primitive, obj2 as Primitive))
                {
                    Game1.x *= -1;
                    Game1.y *= -1;
                }
            }

            return newPosition;
        }

        
    }
}
