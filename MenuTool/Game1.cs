using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Linq;

namespace MenuTool
{
    public class Game1 : Game
    {
        static public Game game;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static MouseState mState;
        public static Vector2 cursorPosition;
        public static SpriteFont infinium;

        public static int bottomOfScreen;
        public static int rightOfScreen;

        public static int drawFrame = 0;

        private Menu mainMenu;
        private bool initialized = false;

        public Color bgColor;

        public Game1()
        {
            game = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            rightOfScreen = _graphics.PreferredBackBufferWidth;
            bottomOfScreen = _graphics.PreferredBackBufferHeight;
            bgColor = new Color(40, 38, 44, 1);
            base.Initialize();
        }

        //Menu buttons require a texture for their Default, Hover and Pressed states
        //Assign menu button textures in Game1.LoadMenuContent()
        public static Texture2D[] testMenuTextures = new Texture2D[3];
        public static Texture2D[] startButtonTextures = new Texture2D[3];
        public static Texture2D[] loadButtonTextures = new Texture2D[3];
        public static Texture2D[] optionsButtonTextures = new Texture2D[3];
        public static Texture2D[] exitButtonTextures = new Texture2D[3];
        public static Texture2D[] fightButtonTextures = new Texture2D[3];
        public static Texture2D[] equipmentButtonTextures = new Texture2D[3];
        public static Texture2D[] shopButtonTextures = new Texture2D[3];
        public static Texture2D[] quitButtonTextures = new Texture2D[3];

        public static List<GameObject> gameObjects = new List<GameObject>();


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadMenuContent();

            infinium = Content.Load<SpriteFont>("Infinium");
        }

        protected override void Update(GameTime gameTime)
        {
            if (!initialized)
            {
                //Creates the first menu, aptly named Main Menu, in the center of the screen (Aproximate center. For a more accurate center,
                //subtract half the width of the texture being used from the xPos parameter: (rightOfScreen / 2) - startButtonTextures[1].Width / 2
                //ToDo: Make preset screen locations like 'Center' and 'Bottom Left' that can be chosen in the the constructor
                mainMenu = new Menu("MainMenu", new string[4] { "Start", "Load", "Options", "Exit" }, (rightOfScreen / 2), bottomOfScreen / 2);
                initialized = true;
            }

            mState = Mouse.GetState();
            cursorPosition = new Vector2(mState.X, mState.Y);

            //The menuButton class is derived from and each one is added to the list of GameObjects to be updated here. Inactive buttons do not get updated.  
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects.ElementAt(i).isActive)
                {
                    gameObjects.ElementAt(i).Update();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(bgColor);
            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            //draws every active object in the gameObjects list. 
            foreach (GameObject obj in gameObjects)
            {
                if (obj.isActive)
                {
                    obj.DrawGameObject(_spriteBatch);
                }
            }
            _spriteBatch.End();
            drawFrame++;
            base.Draw(gameTime);
        }

        private void LoadMenuContent()
        {
            //There are a bunch of buttons here from the game I was making before I decided to make all my menu code reusable. I've decided to keep them in 
            //as a demonstration of how I use this tool and so that other programmers can test it out before deciding to use it. The test menu texture is ugly
            //and entirely unlike the other textures. This wasn't on purpose initially, but I think it makes it more obvious that any button using it is
            //awaiting the creation of a custom texture.

            //0 = default, 1 = hover, 2 = pressed
            testMenuTextures[0] = Content.Load<Texture2D>("MenuTextures/Button");
            testMenuTextures[1] = Content.Load<Texture2D>("MenuTextures/ButtonOver");
            testMenuTextures[2] = Content.Load<Texture2D>("MenuTextures/ButtonPressed");
            startButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/CyberButtonDefault");
            startButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/CyberButtonHover");
            startButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/CyberButtonPressed");
            loadButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/LoadButtonDefault");
            loadButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/LoadButtonHover");
            loadButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/LoadButtonPressed");
            exitButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/ExitButtonDefault");
            exitButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/ExitButtonHover");
            exitButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/ExitButtonPressed");
            fightButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/FightButtonDefault");
            fightButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/FightButtonHover");
            fightButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/FightButtonPressed");
            equipmentButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/EquipmentButtonDefault");
            equipmentButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/EquipmentButtonHover");
            equipmentButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/EquipmentButtonPressed");
            shopButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/ShopButtonDefault");
            shopButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/ShopButtonHover");
            shopButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/ShopButtonPressed");
            optionsButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/OptionsButtonDefault");
            optionsButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/OptionsButtonHover");
            optionsButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/OptionsButtonPressed");
            quitButtonTextures[0] = Content.Load<Texture2D>("MenuTextures/QuitButtonDefault");
            quitButtonTextures[1] = Content.Load<Texture2D>("MenuTextures/QuitButtonHover");
            quitButtonTextures[2] = Content.Load<Texture2D>("MenuTextures/QuitButtonPressed");
        }

        //Even though it isn't functional without my player class and the PlayerDataToXML method, I'm leaving this SaveGame method in for my own sake. 
        //I'm thinking about adding a boolean property to the gameObject class called "savable" or something like that, and iterating over the
        //list of gameObjects and, if they're savable, running that object's method for generating XML data. Perhaps that's out of the scope of a menu tool. 
        public static void SaveGame()
        {
            //string playerData = player.PlayerDataToXML();
            //File.WriteAllText("SaveData.xml", playerData, Encoding.UTF8);
        }
    }
}