## Description:
Write a method that takes a field for well-known board game "Battleship" as an argument and returns true if it has a valid disposition of ships, false otherwise. Argument is guaranteed to be 10*10 two-dimension array. Elements in the array are numbers, 0 if the cell is free and 1 if occupied by ship.

**Battleship** (also Battleships or Sea Battle) is a guessing game for two players. Each player has a 10x10 grid containing several "ships" and objective is to destroy enemy's forces by targetting individual cells on his field. The ship occupies one or more cells in the grid. Size and number of ships may differ from version to version. In this kata we will use Soviet/Russian version of the game.

![Field example](https://github.com/IvanovArtyom/Battleship-field-validator/blob/master/Field%20example.jpg)

Before the game begins, players set up the board and place the ships accordingly to the following rules:
- There must be single battleship (size of 4 cells), 2 cruisers (size 3), 3 destroyers (size 2) and 4 submarines (size 1). Any additional ships are not allowed, as well as missing ships.
- Each ship must be a straight line, except for submarines, which are just single cell.

![Сorrect placement example](https://github.com/IvanovArtyom/Battleship-field-validator/blob/master/%D0%A1orrect%20placement%20example.jpg)

- The ship cannot overlap or be in contact with any other ship, neither by edge nor by corner.

![Incorrect placement example](https://github.com/IvanovArtyom/Battleship-field-validator/blob/master/Incorrect%20placement%20example.jpg)

This is all you need to solve this kata. If you're interested in more information about the game, visit [this link](https://en.wikipedia.org/wiki/Battleship_(game)).
### My solution
```C#
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace Solution
{
    public class BattleshipField
    {
        public static bool ValidateBattlefield(int[,] field)
        {
            List<List<Point>> ships = new();
            int length = field.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (field[i, j] != 1)
                        continue;

                    bool IsShipAlreadyExists = false;

                    for (int k = 0; k < ships.Count; k++)
                    {
                        Point point = ships[k].FirstOrDefault(p => Math.Abs(p.X - i) < 2
                            && Math.Abs(p.Y - j) < 2, new Point(-1, -1));

                        if (point.X != -1)
                        {
                            ships[k].Add(new Point(i, j));
                            IsShipAlreadyExists = true;
                        }
                    }

                    if (!IsShipAlreadyExists)
                        ships.Add(new List<Point>() { new Point(i, j) });
                }
            }

            if (ships.Count != 10)
                return false;

            var groups = ships.GroupBy(ship => ship.Count).OrderByDescending(x => x.Count()).ToList();

            if (groups.Count != 4 || groups.Last().Key != 4)
                return false;

            for (int i = 0; i < ships.Count; i++)
            {
                if (!ships[i].All(p => p.X == ships[i].First().X)
                    && !ships[i].All(p => p.Y == ships[i].First().Y))
                    return false;
            }

            return true;
        }
    }
}
```
