using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace Solution
{
    public class BattleshipField
    {
        public static void Main()
        {
            int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

            // Test
            var t = ValidateBattlefield(field);
            // ...should return true
        }

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
                        Point point = ships[k].FirstOrDefault(p => Math.Abs(p.X - i) < 2 && Math.Abs(p.Y - j) < 2, new Point(-1, -1));

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
                if (!ships[i].All(p => p.X == ships[i].First().X) && !ships[i].All(p => p.Y == ships[i].First().Y))
                    return false;
            }

            return true;
        }
    }
}