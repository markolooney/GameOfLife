using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameOfLife
{
    class Cell : GameEntity
    {

        Random  rand = new Random();
        
        public static int Row = 107; 
        public static int Col = 107;
        public int generation = 0;

        //creates the rows for bitarray
        public BitArray[] array = new BitArray[Row];
        public BitArray[] array2 = new BitArray[Row];
        static Color[] acolour = new Color[10] { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.DeepPink, Color.DarkViolet, Color.Orange, Color.MediumSlateBlue, Color.Navy };

        public Cell ()
        {

            for (int i = 0; i < Row; i++)
            {
                //creates the columns for bitarray
                array[i] = new BitArray(Col, false);
                array2[i] = new BitArray(Col, false);
            }
            
        }

        public override void LoadContent()
        {
            //loads text and spirte
            cell = Game1.Instance.Content.Load<Texture2D>("life");
            rules = Game1.Instance.Content.Load<SpriteFont>("Rules");
        }

        public override void Update(GameTime gameTime)
        {

            int r = rand.Next(2 , 105);
            int r2 = rand.Next(2 ,105);
            

            KeyboardState keyState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            //if up is pressed speeds up generations
            if (keyState.IsKeyDown(Keys.Up))
            {
                
                Game1.Instance.time = 100;
            }
            //if down is pressed slows down generations
            else if(keyState.IsKeyDown(Keys.Down))
            {
                
                Game1.Instance.time = 2;
                
            }
            //you can turn cells on with left click
            else if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (mouse.X > 2 && mouse.Y > 2 && mouse.X < 318 && mouse.Y < 318)
                {
                    if (array[mouse.X / 3][mouse.Y / 3] == false)
                    {
                        array[mouse.X / 3][mouse.Y / 3] = true;
                    }
                }
            }
            //can turn cells off with right click
            else if (mouse.RightButton == ButtonState.Pressed)
            {
                if (mouse.X > 2 && mouse.Y > 2 && mouse.X < 318 && mouse.Y < 318)
                {
                    
                    if (array[mouse.X / 3][mouse.Y / 3] == true)
                    {
                        array[mouse.X / 3][mouse.Y / 3] = false;
                    }
                }
            }
            //when space is clicked turns random cells on in the array
            else if (keyState.IsKeyDown(Keys.Space))
            {
                array[r][r2] = true;
            }
            //turns on cells in array off
            else if (keyState.IsKeyDown(Keys.Back))
            {
                for (int i = 1; i < Row - 1; i++)
                {
                    for (int j = 1; j < Col - 1; j++)
                    {
                        array[i][j] = false;
                    }
                }
            }
            //using random variables turns random cells on and off to create a random state
            else if (keyState.IsKeyDown(Keys.Enter))
            {
                for (int i = 1; i < Row - 1; i++)
                {
                    for (int j = 1; j < Col - 1; j++)
                    {
                        bool tf = rand.Next(1, 3) % 2 == 0;
                        array[i][j] = tf;
                    }
                }
            }
            //goes through the rules to update array and generations
            else
            {
                
                generation++;
                int survive;
                for (int i = 1; i < Row - 1; i++)
                {
                    for (int j = 1; j < Col - 1; j++)
                    {
                        survive = 0;
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (array[i + k][j + l] == true)
                                {
                                    survive++;
                                }
                            }
                        }
                        if (array[i][j])
                        {
                            if (survive - 1 < 2)
                            {
                                array2[i][j] = false;
                            }
                            else if (survive - 1 > 3)
                            {
                                array2[i][j] = false;
                            }
                            else if (survive - 1 == 2)
                            {
                                array2[i][j] = true;
                            }
                            else if (survive - 1 == 3)
                            {
                                array2[i][j] = true;
                            }
                        }
                        else
                        {

                            if (survive == 3)
                            {
                                array2[i][j] = true;
                            }
                            else
                            {
                                array2[i][j] = false;
                            }
                        }
                    }
                }
                BitArray[] array3 = array;
                array = array2;
                array2 = array3;
            }
        }
        
        public override void Draw(GameTime gameTime)
        {
            //draws controls
            Game1.Instance.spriteBatch.DrawString(rules,
            "Controls:\n\nPress Enter to create a random starting state\nTurn on cells with left mouse click\nTurn off cells with right mouse click\nHold down to Slow down\nHold up to Speed up\nHold Space to place random cells around board\nPress Backspace to clear the Screen"
            , new Vector2(1, 340), Color.Black);
            //draws generations different colour for every generation goes through 10 colours
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (array[i][j])
                    {
                       Game1.Instance.spriteBatch.Draw(cell, new Vector2(i * 3, j * 3), acolour[generation % 10]);
                    } 
                }
            }
            
        }
    }
}
