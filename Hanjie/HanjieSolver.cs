using System;
using System.Collections.Generic;
using System.Linq;

namespace Hanjie
{
    public class HanjieSolver
    {
        public static int SanityCheck()
        {
            return 1;
        }

        public static char[] LineSolver(int[] blocks, char[] currentLine)
        {
            int numberOfBlocks = blocks.Length;
            int[] offsets = new int[numberOfBlocks];
            int maxOffset = currentLine.Length - blocks.Sum() - blocks.Length + 1;
            bool[] nextLine;
            bool[] confirmedSquares = new bool[currentLine.Length];
            bool[] confirmedCrosses = new bool[currentLine.Length];
            for (int x = 0; x < currentLine.Length; x++)
            {
                confirmedSquares[x] = true;
                confirmedCrosses[x] = true;
            }
            int i;
            int mostRecent = 0;
            bool validLine;
            bool impossibleLine;

            while (offsets[0] <= maxOffset)
            {
                validLine = true;
                impossibleLine = false;
                i = 0;
                nextLine = new bool[currentLine.Length];
                for (int blockIndex = 0; blockIndex < blocks.Length; blockIndex++)
                {
                    i += offsets[blockIndex];
                    if (i + blocks[blockIndex] <= currentLine.Length)
                    {
                        for (int j = i; j < i + blocks[blockIndex]; j++)
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
                    for (int k = mostRecent + 1; k < offsets.Length; k++)
                    {
                        offsets[k] = 0;
                    }
                }
                else
                {
                    for (int z = 0; z < currentLine.Length; z++)
                    {
                        if ((currentLine[z] == 'S' && !nextLine[z]) || (currentLine[z] == 'x' && nextLine[z]))
                        {
                            validLine = false;
                            break;
                        }
                    }
                    if (validLine)
                    {
                        for (int squareIndex = 0; squareIndex < nextLine.Length; squareIndex++)
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

        public static char[] GetVerticalLine(List<char[]> map, int index)
        {
            int length = map[0].Length;
            char[] output = new char[length];
            for (int i = 0; i < length; i++)
            {
                output[i] = map[i][index];
            }
            return output;
        }

        public static List<char[]> AddVerticalLine(List<char[]> map, char[] line, int index)
        {
            for (int i = 0; i < line.Length; i++)
            {
                map[i][index] = line[i];
            }
            return map;
        }

        public static List<char[]> InitMap(int length)
        {
            var lines = new List<char[]>();

            for (int i = 0; i < length; i++)
            {
                char[] toAdd = new char[length];
                for (int j = 0; j < length; j++)
                {
                    toAdd[j] = '_';
                }
                lines.Add(toAdd);
            }
            return lines;
        }

        public static List<char[]> CheckMap(List<char[]> map, List<int[]> horizontalBlocks, List<int[]> verticalBlocks)
        {
            for (int i = 0; i < horizontalBlocks.Count; i++)
            {
                map[i] = LineSolver(horizontalBlocks[i], map[i]);
            }
            for (int i = 0; i < verticalBlocks.Count; i++)
            {
                char[] verticalLine = LineSolver(verticalBlocks[i], GetVerticalLine(map, i));
                map = AddVerticalLine(map, verticalLine, i);
            }
            return map;
        }

        public static void PrintMap(List<char[]> map)
        {
            foreach (char[] line in map)
            {
                Console.WriteLine();
                char output;
                foreach (char c in line)
                {
                    output = c == 'S' ? 'S' : '_';
                    Console.Write($"{output} ");
                }
            }
        }

        public static bool CheckComplete(List<char[]> map)
        {
            foreach (char[] line in map)
            {
                if (line.Contains('_'))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<int[]> CreateBlocks(string blockString)
        {
            var output = new List<int[]>();
            string[] BlockStringList = blockString.Split(",");
            string[] rowDetails;
            int[] rowBlock;
            for (int i = 0; i < BlockStringList.Length; i++)
            {
                rowDetails = BlockStringList[i].Split(" ");
                rowBlock = new int[rowDetails.Length];
                for (int j = 0; j < rowDetails.Length; j++)
                {
                    rowBlock[j] = int.Parse(rowDetails[j]);
                }
                output.Add(rowBlock);
            }
            return output;
        }


        static void Main(string[] args)
        {
            string horizontalString = "3,1 3,3 1,1 3,1 1";
            string verticalString = "3,1 1 1,4,2 2,3";

            var horizontalBlocks = CreateBlocks(horizontalString);
            var verticalBlocks = CreateBlocks(verticalString);

            var lines = InitMap(horizontalBlocks.Count);
            int attempts = 0;
            int maxAttempts = 20;
            while (!CheckComplete(lines) && attempts < maxAttempts)
            {
                lines = CheckMap(lines, horizontalBlocks, verticalBlocks);
                attempts++;
            }


            if (attempts >= maxAttempts)
            {
                Console.WriteLine("Unable to Complete the Puzzle.");
            }
            else
            {
                PrintMap(lines);
                Console.WriteLine();
                Console.WriteLine($"Map completed in {attempts} attempts.");
            }





        }
    }
}
