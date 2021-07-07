using System;
using System.Linq;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int optionSelected = 0;
            int generation = 0;
            int seedRow = 0;
            int seedColumn = 0;

            Life lifeGame = new Life(25, 25);

            do
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("1.Entering Generation");
                Console.WriteLine("2.Entering Seed details");
                Console.WriteLine("3.Start Process");
                Console.WriteLine("4.Clear");
                Console.WriteLine("5.Exit");

                optionSelected = Convert.ToInt32(Console.ReadLine());

                switch(optionSelected)
                {
                    case 1:
                        Console.Write("Enter Generation:");
                        generation = Convert.ToInt32(Console.ReadLine());
                        lifeGame.MaxGenerations = generation;
                        break;
                    case 2:
                        Console.Write("Enter Row:");
                        seedRow = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Column:");
                        seedColumn = Convert.ToInt32(Console.ReadLine());
                        lifeGame.ToggleGridCell(seedRow, seedColumn);
                        break;
                    case 3:
                        Console.Clear();
                        lifeGame.Start();
                        var aliveRowList = lifeGame.InputGrid.Rows.Where(x => x.Cells.Any(y => y.IsAlive)).ToList();
                        foreach(var row in aliveRowList)
                        {
                            var rowIndex = lifeGame.InputGrid.Rows.IndexOf(row);
                            var aliveCells = row.Cells.Where(x => x.IsAlive).ToList();
                            foreach (var cell in aliveCells)
                            {
                                Console.WriteLine($"({rowIndex} {row.Cells.IndexOf(cell)})");
                            }
                        }
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                }

            } while (optionSelected != 5);
            
            Console.ReadKey();
        }
    }
}
