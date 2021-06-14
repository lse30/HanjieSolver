using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Solves the game of Hanjie, a Japanese puzzle 
/// in which you have to fill rows to match the given blocks
/// EXAMPLE
///        1
///        1   2
///      3 1 4 2 3
///      _ _ _ _ _
///  3  |_|X|X|X|_|
///1 3  |X|_|X|X|X|
///3 1  |X|X|X|_|X|
///1 3  |X|_|X|X|X|
///1 1  |_|X|_|X|_|
/// </summary>
namespace Hanjie
{
    public class HanjieMain
    {
        public static HanjieMap SolveHanjieMap(int maxAttempts, HanjieMap hanjieMap)
        {
            var attempts = 0;
            while (!CheckComplete(hanjieMap.map) && attempts < maxAttempts)
            {
                hanjieMap.map = CheckMap(hanjieMap.map, hanjieMap.horizontalBlock, hanjieMap.verticalBlock);
                attempts++;
            }

            if (attempts >= maxAttempts)
            {
                Console.WriteLine("Unable to Complete the Puzzle.");
            }
            else
            {
                Console.WriteLine($"Map completed in {attempts} attempts.");
            }

            return hanjieMap;
        }

        /// <summary>
        /// Generates all possible combinations of blocks on the line and
        /// compares them with each other and the current line to see what
        /// deductions can be made.
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="currentLine"></param>
        /// <returns></returns>
        public static char[] LineSolver(int[] blocks, char[] currentLine)
        {
            var numberOfBlocks = blocks.Length;
            var offsets = new int[numberOfBlocks];
            var maxOffset = currentLine.Length - blocks.Sum() - blocks.Length + 1;
            bool[] nextLine;
            var confirmedSquares = new bool[currentLine.Length];
            var confirmedCrosses = new bool[currentLine.Length];
            for (var x = 0; x < currentLine.Length; x++)
            {
                confirmedSquares[x] = true;
                confirmedCrosses[x] = true;
            }
            int i;
            var mostRecent = 0;
            bool validLine;
            bool impossibleLine;

            while (offsets[0] <= maxOffset)
            {
                validLine = true;
                impossibleLine = false;
                i = 0;
                nextLine = new bool[currentLine.Length];
                for (var blockIndex = 0; blockIndex < blocks.Length; blockIndex++)
                {
                    i += offsets[blockIndex];
                    if (i + blocks[blockIndex] <= currentLine.Length)
                    {
                        for (var j = i; j < i + blocks[blockIndex]; j++)
                        {
                            nextLine[j] = true;
                        }
                        i += blocks[blockIndex] + 1;
                    }
                    else
                    {
                        impossibleLine = true;
                        break;
                    }
                }
                if (impossibleLine)
                {
                    mostRecent--;
                    offsets[mostRecent]++;
                    for (var k = mostRecent + 1; k < offsets.Length; k++)
                    {
                        offsets[k] = 0;
                    }
                }
                else
                {
                    for (var z = 0; z < currentLine.Length; z++)
                    {
                        if ((currentLine[z] == 'S' && !nextLine[z]) || (currentLine[z] == 'x' && nextLine[z]))
                        {
                            validLine = false;
                            break;
                        }
                    }
                    if (validLine)
                    {
                        for (var squareIndex = 0; squareIndex < nextLine.Length; squareIndex++)
                        {
                            confirmedSquares[squareIndex] = confirmedSquares[squareIndex] && nextLine[squareIndex];
                            confirmedCrosses[squareIndex] = confirmedCrosses[squareIndex] && !nextLine[squareIndex];
                        }

                    }
                    offsets[^1]++;
                    mostRecent = offsets.Length - 1;
                }
            }
            for (i = 0; i < currentLine.Length; i++)
            {
                if (currentLine[i] == '_')
                {
                    if (confirmedSquares[i])
                    {
                        currentLine[i] = 'S';
                    }
                    else if (confirmedCrosses[i])
                    {
                        currentLine[i] = 'x';
                    }
                }
            }
            return currentLine;
        }

        /// <summary>
        /// Finds a vertical line from the row map
        /// </summary>
        /// <param name="map"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static char[] GetVerticalLine(List<char[]> map, int index)
        {
            var length = map[0].Length;
            var output = new char[length];
            for (var i = 0; i < length; i++)
            {
                output[i] = map[i][index];
            }
            return output;
        }

        /// <summary>
        /// places a vertical line solution back into the matrix
        /// </summary>
        /// <param name="map"></param>
        /// <param name="line"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<char[]> AddVerticalLine(List<char[]> map, char[] line, int index)
        {
            for (var i = 0; i < line.Length; i++)
            {
                map[i][index] = line[i];
            }
            return map;
        }

        /// <summary>
        /// Tests to see if anything new squares can be solved
        /// </summary>
        /// <param name="map"></param>
        /// <param name="horizontalBlocks"></param>
        /// <param name="verticalBlocks"></param>
        /// <returns></returns>
        public static List<char[]> CheckMap(List<char[]> map, List<int[]> horizontalBlocks, List<int[]> verticalBlocks)
        {
            for (var i = 0; i < horizontalBlocks.Count; i++)
            {
                map[i] = LineSolver(horizontalBlocks[i], map[i]);
            }
            for (var i = 0; i < verticalBlocks.Count; i++)
            {
                var verticalLine = LineSolver(verticalBlocks[i], GetVerticalLine(map, i));
                map = AddVerticalLine(map, verticalLine, i);
            }
            return map;
        }

        /// <summary>
        /// Tests to see if a map is complete by searching for empty squares
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static bool CheckComplete(List<char[]> map)
        {
            foreach (var line in map)
            {
                if (line.Contains('_'))
                {
                    return false;
                }
            }
            return true;
        }
    }


    public class HanjieSolverRecursive
    {
        public static HanjieMap SolveHanjieMapRecursively(HanjieMap hanjieMap)
        {
            var size = hanjieMap.map.Count;
            hanjieMap = SolveHanjieMapRecursivelyLoop(hanjieMap, size);
            return hanjieMap;
        }

        private static HanjieMap SolveHanjieMapRecursivelyLoop(HanjieMap hanjieMap, int size)
        {
            if (size == 0)
            {
                return hanjieMap;
            }
            else
            {
                return null;
            }
        }
    }

    public class Hanjie
    {
        static void Main()
        {
            var horizontalString = "4, 2 2, 2 2, 2 2, 2 2, 2 2, 4,6, 8, 10";
            var verticalString = "1, 3 2, 5 3, 2 5, 1 4, 1 4, 2 5, 5 3, 3 2, 1";
            var hanjieMap = new HanjieMap(horizontalString, verticalString);
            var maxAttempts = 20;
            hanjieMap = HanjieMain.SolveHanjieMap(maxAttempts, hanjieMap);
            hanjieMap.PrintMap();

            Console.WriteLine();
            Console.WriteLine("Creating a random Hanjie Puzzle to solve.");
            var hanjieMapRand = HanjieBuilder.BuildHanjieMap();
            hanjieMapRand = HanjieMain.SolveHanjieMap(maxAttempts, hanjieMapRand);
            hanjieMapRand.PrintMap();



            //solve the puzzle "1,1" "1,1"

            //horizontalString = "2 2, 3 1 3, 6 5, 1 4, 4 3, 1 5 2, 9 2, 1 4, 1 4, 9 4, 13, 2 1, 4 8, 8, 15";
            //verticalString = "2 1 1 1, 3 1 1 1, 3 1 2 1 1,1 1 2 1 1, 2 2 2 1, 1 1 2 1,3 2 1,3 6,11,4 1 3, 1 2 1 3, 2 4 3, 4 8, 11 3,11 3";
            //hanjieMap = new HanjieMap(horizontalString, verticalString);         
            //hanjieMap = HanjieSolver.SolveHanjieMap(maxAttempts, hanjieMap);
            //hanjieMap.PrintMap();
        }
    }
}
