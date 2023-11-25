using System;

namespace BreannaGame
{
    class Program
    {
        //the constant and global variables are here

        static Int32 screenhight = 30;
        static Int32 screenwidth = 100;
        const Char userchar = '■';
        const Char wallchar = '|';
        static Int32 userx = 15;
        static Int32 usery = 10;
        static Int32 olduserx = 1;
        static Int32 oldusery = 1;
        static Int32 speed = 1;
        static Int32 wallspeed = 500;
        static Int32 worth = 1; //value for each point scored, will increase as they progress

        //main with the very begginings of the game

        static void Main(string[] args)
        {
            Console.WindowHeight = screenhight;
            Console.WindowWidth = screenwidth;

            Int32 wallxposition = 0; // this is here so that the wall colours change only when a new wall appears
            Int32 usercolour = 1;
            Int32 points = -1, isittimetospeedthingsup = 3, wallspassedthrough = 0;

            ConsoleKey k;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Int32[] walcol0 = new Int32[screenhight]; //array to carry each wall character's colour

            bool gameover = stoplaying(wallxposition, usercolour, walcol0);

            screen("Colour Walls", "Invalid entry", ref gameover, "Play game");

            while (!gameover)
            {
                if (Console.KeyAvailable)
                {
                    k = Console.ReadKey(true).Key;
                    moveplayer(k, ref usercolour);
                }
                checkingdetails(wallspassedthrough, ref isittimetospeedthingsup);
                speed++;
                draw(walcol0, ref wallxposition, usercolour, ref points, ref wallspassedthrough);
                gameover = stoplaying(wallxposition, usercolour, walcol0);
            }
            if (points > -1)
            {
                overscreen(points);
            }
        }

        /*
         * this fucntion checks to see if it is time to up the speed of the walls and the value of a point
         */

        static void checkingdetails(Int32 d1, ref Int32 d2)
        {
            if (d1 == d2)
            {
                worth++;
                wallspeed = wallspeed - 50;
                d2 = d2 + 5;
            }
        }

        /*
         * starting screen
         */

        static void screen(String l1, String l2, ref bool exitgame, String l3)
        {
            Int32 userinput = 0;
            bool valid = false, startgame = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = true;
            do
            {
                Console.SetCursorPosition(45, 10);
                Console.Write(l1 + "\n");
                Console.SetCursorPosition(25, 12);
                Console.Write("1. " + l3 + " \n");
                Console.SetCursorPosition(25, 13);
                Console.Write("2. Exit \n");
                Console.SetCursorPosition(25, 15);
                Console.Write("Enter the number of what you would like to do: ");
                checking(l2, ref userinput, ref valid);
                if (valid)
                {
                    startgame = true;
                }
            } while (!startgame);
            Console.CursorVisible = false;

            switch (userinput)
            {
                case 1:
                    Console.Clear();
                    instructions();
                    break;
                case 2:
                    exitgame = true;
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        /* 
         * tryparse function
         */

        static void checking(String error, ref Int32 playinput, ref bool okay)
        {
            okay = Int32.TryParse(Console.ReadLine(), out playinput);
            if (!okay)
            {
                Console.WriteLine(error);
            }
            else
            {
                if ((playinput < 1) || (playinput > 2))
                {
                    okay = false;
                    Console.Write(error);
                }
            }
        }

        /*
         * instructions for how to play the game
         */

        static void instructions()
        {
            Console.Clear();
            Console.WriteLine("Here is how you play: \n");
            Console.WriteLine("You move your character up and down by pressing the up and down arrow. \nThere is no moving side to side. \n");
            Console.WriteLine("There is a wall that will come towards you from the right. You have to pass through the \npart of the wall that is the same colour as your character. \nYou get a point for every wall you pass through and the game ends when you hit the wall. \n");
            Console.Write("You can change the colour of your character by pressing the spacebar. \nIt changes the colour in the following order: \n");
            Console.WriteLine("     Green \n     Red\n     Cyan\n     Yellow/Beige\n     Magenta\n     White\n     Dark Blue\n     Dark Yellow/Gold\n ");
            Console.WriteLine("If you go above the screen height, you will be placed at the bottom of the screen and \nvice versa.\n");
            Console.WriteLine("After every 5 walls, the speed and the point value increases.\n");

            Console.WriteLine("Press the enter key to begin");
            Console.ReadLine();
        }

        /*
         * moving the player, this part includes assigning oldx+oldy coordinates, reading and interpreting the keys.
         * it accesses functions that are responsible for changing the colour of the player, setting the limits of the screen.
        */

        static void moveplayer(ConsoleKey let1, ref Int32 num1)
        {
            olduserx = userx;
            oldusery = usery;
            if (let1 == ConsoleKey.UpArrow)
            {
                usery--;
            }
            if (let1 == ConsoleKey.DownArrow)
            {
                usery++;
            }
            if (let1 == ConsoleKey.Spacebar)
            {
                playercolour(ref num1);
            }
            limits();
        }

        /*
         * changes the colour of the user's bonhomme
         */

        static void playercolour(ref Int32 usercol)
        {
            if (usercol == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 2;
                return;
            }
            if (usercol == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 3;
                return;
            }
            if (usercol == 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 4;
                return;
            }
            if (usercol == 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 5;
                return;
            }
            if (usercol == 5)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 6;
                return;
            }
            if (usercol == 6)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 7;
                return;
            }
            if (usercol == 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 8;
                return;
            }
            if (usercol == 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
                usercol = 1;
                return;
            }
        }

        /*
         * sets up the limits of the screen, currently used by user's bonhomme only, but could be modified to work with the walls later on if necessary
         */

        static void limits()
        {
            if (usery > screenhight - 1)
            {
                usery = 0;
            }
            if (usery < 0)
            {
                usery = screenhight - 1;
            }
        }

        /*
         * function that is called to change the foreground colour so that the wall is printed in the right colour
         */

        static void settingwallcolour(Int32 thecolour)
        {
            if (thecolour == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Green;
            }
            if (thecolour == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Red;
            }
            if (thecolour == 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            if (thecolour == 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            if (thecolour == 5)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            if (thecolour == 6)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.White;
            }
            if (thecolour == 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            if (thecolour == 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
        }

        /*
         * function that declares and prints the walls
         * if it is the first time the wall is appearing on the screen, it figures out what colour each wall character will be and then prints it
         * if it isn't the first time, it just prints it
         * this is also where the player's score is being calculated
         */

        static void wall(Int32[] p1, ref Int32 xposition, ref Int32 userscore, ref Int32 p2)
        {
            Int32 customizingwalls = auhazard(1, 6);
            if (xposition < (userx - 7)) //here so that the wall changes colours only after it has crossed the screen
            {
                userscore = userscore + worth;
                p2++;
                for (Int32 l = 0; l < screenhight; l++)
                {
                    Console.SetCursorPosition(xposition + 1, l);  //these line of code are what determines how thick the wall is
                    Console.Write(" ");
                    Console.SetCursorPosition(xposition + 2, l);
                    Console.Write(" ");
                    Console.SetCursorPosition(xposition + 3, l);
                    Console.Write(" ");
                    Console.SetCursorPosition(xposition + 4, l);
                    Console.Write(" ");
                    Console.SetCursorPosition(xposition + 5, l);
                    Console.Write(" ");
                }
                xposition = screenwidth - 2;
                if (customizingwalls == 1) //these if statements are to create different types of walls, which colours they can or can't be, etc.
                {
                    for (Int32 i = 0; i < screenhight; i++)
                    {
                        p1[i] = auhazard(1, 9); //assigning each wall character a colour
                    }
                }
                if (customizingwalls == 2)
                {
                    for (Int32 a = 0; a < screenhight; a++)
                    {
                        p1[a] = auhazard(1, 5);
                    }
                }
                if (customizingwalls == 3)
                {
                    for (Int32 b = 0; b < screenhight; b++)
                    {
                        p1[b] = auhazard(5, 9);
                    }
                }
                if (customizingwalls == 4)
                {
                    Int32 jumping = 5, holdingnumber = 0;
                    for (Int32 c = 0; c < screenhight; c++)
                    {
                        if (c % jumping == 0)
                        {
                            p1[c] = auhazard(1, 5);
                            holdingnumber = p1[c];
                        }
                        else
                        {
                            p1[c] = holdingnumber;
                        }
                    }
                }
                if (customizingwalls == 5)
                {
                    Int32 jumpings = 5, holdingnumbers = 0;
                    for (Int32 c = 0; c < screenhight; c++)
                    {
                        if (c % jumpings == 0)
                        {
                            p1[c] = auhazard(5, 9);
                            holdingnumbers = p1[c];
                        }
                        else
                        {
                            p1[c] = holdingnumbers;
                        }
                    }
                }
            }

            if (speed % wallspeed == 0) //controlling the speed of the walls
            {
                for (Int32 j = 0; j < screenhight; j++)
                {
                    Int32 oldwx = 0, oldwy = 0, colour;
                    if (xposition < screenwidth - 3) //doing this so that the screen doesn't shift everytime a new wall is created
                    {
                        oldwx = xposition + 5; // five because it's how thick the wall is
                        oldwy = j;
                    }
                    colour = p1[j];
                    Console.SetCursorPosition(xposition, j);
                    settingwallcolour(colour);
                    Console.Write(wallchar);
                    Console.SetCursorPosition(oldwx, oldwy);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
                xposition--; //this is what actually moves the wall across the screen
            }
        }

        /*
         * function to get a random number
         */

        static Int32 auhazard(Int32 lowest, Int32 highest)
        {
            Int32 thenum;
            Random r = new Random();
            thenum = r.Next(lowest, highest);
            return thenum;
        }

        /*
         * function that prints the player in the correct colour
         */

        static void printingplayer(Int32 pc)
        {
            if (pc == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 5)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 6)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            if (pc == 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.Write(userchar);
        }

        /*
         * function that actually prints everything on the screen or calls to the functions that will print things on the screen
         */

        static void draw(Int32[] p1, ref Int32 p3, Int32 p4, ref Int32 score, ref Int32 p5)
        {
            Console.SetCursorPosition(userx, usery);
            printingplayer(p4); //using a function so that it prints with the proper colour
            Console.SetCursorPosition(olduserx, oldusery);
            if (oldusery != usery)
            {
                Console.Write(" ");
                olduserx = 0;
                oldusery = 0;
            }
            wall(p1, ref p3, ref score, ref p5); //function that prints the wall
            printingscore(score); //function that prints the player's score
        }

        /*
         * function to print the right score on the side of the screen
         */

        static void printingscore(Int32 p)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 10);
            Console.Write("SCORE");
            Console.SetCursorPosition(1, 12);
            Console.Write(p);
        }

        /*
         * function to tell if the game is over or not
         */

        static bool stoplaying(Int32 xwall, Int32 playercol, Int32[] wallcol)
        {
            bool valid = false;
            if ((xwall > userx - 1) && (xwall < userx + 1))
            {
                if (playercol != wallcol[usery])
                {
                    valid = true;
                }
            }
            return valid;
        }

        /*
         * game over screen
         */

        static void overscreen(Int32 userscore)
        {
            Console.SetCursorPosition(45, 10);
            Console.Write("Game over \n");
            Console.SetCursorPosition(35, 12);
            Console.Write("You had a final score of " + userscore + " \n");
            Console.SetCursorPosition(35, 14);
            Console.Write("Press the enter button to exit");
            Console.ReadLine();
        }
    }
}

