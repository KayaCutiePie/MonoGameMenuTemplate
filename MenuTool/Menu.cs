using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MenuTool
{
    internal class Menu
    {
        public string menuID;
        public List<MenuButton> menuButtons = new List<MenuButton>();
        public bool drawLabels = false;

        /// <summary>
        /// Menus aren't GameObjects, so much as a list of MenuButtons, which ARE GameObjects. The buttons are generated automatically by
        /// the Menu object. 
        /// </summary>
        /// <param name="menuID">The name of the menu</param>
        /// <param name="buttonLabels"></param>
        /// <param name="xPos">The position of the menu in the xAxis</param>
        /// <param name="yPos">The position of the menu in the yAxis</param>
        /// <param name="padding">The size of the space between buttons</param>
        public Menu(string menuID, string[] buttonLabels, float xPos = 500f, float yPos = 500f, float padding = 10f)
        {
            this.menuID = menuID;

            Vector2 buttonPosition = new Vector2(xPos, yPos);

            //Create a button object for each string passed to the menu constructor and add it to the gameObjects list.
            for (int i = 0; i < buttonLabels.Length; i++)
            {
                Texture2D[] textures = ChooseTextures(buttonLabels[i]);
                float yOffset = textures[0].Height;
                buttonPosition.Y = buttonPosition.Y + yOffset + padding;
                MenuButton newButton = new MenuButton(this, buttonPosition, buttonLabels[i], textures, drawLabels);
                Game1.gameObjects.Add(newButton);
                menuButtons.Add(newButton);
            }
        }

        //If you want to use custom textures for a button, you have to return the ones you want here.
        private Texture2D[] ChooseTextures(string buttonID)
        {
            switch (buttonID)
            {
                case "Start":
                    return Game1.startButtonTextures;
                case "Load":
                    return Game1.loadButtonTextures;
                case "Exit":
                    return Game1.exitButtonTextures;
                case "Fight":
                    return Game1.fightButtonTextures;
                case "Equipment":
                    return Game1.equipmentButtonTextures;
                case "Shop":
                    return Game1.shopButtonTextures;
                default:
                    drawLabels = true;
                    return Game1.testMenuTextures;
            }
        }

        public void UnloadMenu()
        {
            foreach (MenuButton menuButton in menuButtons)
            {
                menuButton.isActive = false;
                if (menuButton.label != null) menuButton.label.isActive = false;
            }
        }


    }

    internal class MenuButton : GameObject
    {
        public override bool isActive { get; set; }
        public override Texture2D[] textures { get; set; }
        public override Vector2 position { get; set; }
        public override int currentTexture { get; set; }
        public override SpriteFont font { get; set; }
        public override string text { get; set; }
        public override float layerDepth { get; set; }

        private bool isHovering = false;
        private string buttonID { get; }
        public TextObj label;
        private Vector2 labelPosition;

        private Menu parentMenu;
        private bool clicked = false;

        /// <summary>
        /// Creates a menu button, only intended to be used by the Menu constructor. 
        /// </summary>
        /// <param name="parentMenu">The menu that created this button</param>
        /// <param name="position">The position of the button</param>
        /// <param name="buttonLabel">The name of the button</param>
        /// <param name="textures">The optional textures to be used for the button. If left null, the test menu textures will be used</param>
        /// <param name="drawLabels">Determines whether or not the labels need to be drawn. Should only be true if textures is null</param>
        public MenuButton(Menu parentMenu, Vector2 position, string buttonLabel, Texture2D[] textures = null, bool drawLabels = false)
        {
            layerDepth = 0.5f;
            isActive = true;
            this.parentMenu = parentMenu;
            this.buttonID = buttonLabel;

            if (textures == null)
            {
                this.textures = Game1.testMenuTextures;
            }
            else
            {
                this.textures = new Texture2D[3];
                this.textures[0] = textures[0];
                this.textures[1] = textures[1];
                this.textures[2] = textures[2];
            }

            this.position = new Vector2(position.X, position.Y);

            //creates the textObj for the button. textObj is a GameObject and is actually rendered
            if (drawLabels)
            {
                int textSizeOffset = Game1.infinium.Glyphs[25].BoundsInTexture.Height / 2;
                labelPosition = new Vector2(position.X + 25, this.position.Y + this.textures[0].Height / 2 - textSizeOffset);
                label = new TextObj(buttonLabel, labelPosition, layerDepth - 0.1f);
                Game1.gameObjects.Add(label);
            }


        }

        public override void Update()
        {
            if (isActive)
            {
                //checks if the cursor is over the button
                isHovering = Input.PointIsInRectangle(this, Game1.cursorPosition);

                //Determines which of the three textures to use based on the player's interaction. Also calls a method when clicked
                if (isHovering)
                {
                    currentTexture = 1;
                    if (Game1.mState.LeftButton == ButtonState.Pressed)
                    {
                        currentTexture = 2;
                        clicked = true;
                    }
                    else if (clicked && Game1.mState.LeftButton == ButtonState.Released)
                    {
                        clicked = false;
                        this.MenuButtonClick();
                    }
                }
                else currentTexture = 0;
            }
        }

        
        public void MenuButtonClick()
        {
            if (isActive)
            {
                //Invokes a method with the same name as the id of the menu and sends the buttonID as a parameter. Throws an exception if that method doesn't exist.
                MethodInfo method = typeof(ButtonLogicMethods).GetMethod(String.Format(parentMenu.menuID));
                if (method != null)
                {
                    method.Invoke(method, new object[] { buttonID });
                }
                else
                {
                    throw new NotImplementedException("The \"" + parentMenu.menuID + "\" menu has not been implemented. Create a \"" + parentMenu.menuID + "\" method in the " +
                        "ButtonLogicMethods class with a switch containing the logic for the \"" + buttonID + "\" button");
                }
                parentMenu.UnloadMenu();

            }
        }
    }

    public static class ButtonLogicMethods
    {
        #pragma warning disable IDE0059 //"unneccesary assignment of a value to 'menu'" The menu buttons litterally won't exist if a value isn't assigned to the menu. 

        //Different menus have their own methods for organization. These methods are invoked in MenuButton.MenuButtonClick() 
        public static void MainMenu(string buttonID)
        {
            //The logic for what happens when a particular button is pressed. 
            switch (buttonID)
            {
                case "Start":
                    {
                        Menu menu = new Menu("TownMenu", new string[4] { "Fight", "Equipment", "Shop", "Quit" });
                        break;
                    }
                case "Load":
                    {
                        Menu menu = new Menu("TownMenu", new string[4] { "Fight", "Equipment", "Shop", "Quit" });
                        break;
                    }
                case "Exit":
                    {
                        Game1.game.Exit();
                        break;
                    }
                default:
                    throw new NotImplementedException("The MainMenu function doesn't have logic for a \"" + buttonID + "\" button.");
            }
        }

        public static void TownMenu(string buttonID)
        {
            switch (buttonID)
            {
                case "Fight":
                    Menu menu = new Menu("FightMenu", new string[4] { "Attack", "Magic", "Use", "Flee" });
                    break;
                case "Quit":
                    {
                        Game1.SaveGame();
                        Game1.game.Exit();
                        break;
                    }
                default:
                    throw new NotImplementedException("The TownMenu function doesn't have logic for a \"" + buttonID + "\" button.");
            }
        }

        #pragma warning restore IDE0059
    }
}
