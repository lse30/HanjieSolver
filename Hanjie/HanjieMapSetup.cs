using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanjie
{
    public class HanjieMap
    {
        public List<int[]> horizontalBlock;
        public List<int[]> verticalBlock;
        public List<char[]> map = new List<char[]>();

        public HanjieMap(string horizontalBlocksString, string verticalBlocksString)
        {
            horizontalBlock = CreateBlock(horizontalBlocksString);
            verticalBlock = CreateBlock(verticalBlocksString);
            map = InitMap(horizontalBlock.Count);
            VerifyMapForCorrectness();
        }

        /// <summary>
        /// Creates Blocks from a string of values
        /// Numbers are seperated by spaces and rows/columns by commas.
        /// </summary>
        /// <param name="blockString"></param>
        /// <returns></returns>
        public static List<int[]> CreateBlock(string blockString)
        {
            var output = new List<int[]>();
            var BlockStringList = blockString.Split(",");
            string[] rowDetails;
            int[] rowBlock;
            for (var i = 0; i < BlockStringList.Length; i++)
            {
                rowDetails = BlockStringList[i].Trim().Split(" ");
                rowBlock = new int[rowDetails.Length];
                for (var j = 0; j < rowDetails.Length; j++)
                {
                    rowBlock[j] = int.Parse(rowDetails[j]);
                }
                output.Add(rowBlock);
            }
            return output;
        }

        /// <summary>
        /// Sets up the grid with values
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<char[]> InitMap(int length)
        {
            var lines = new List<char[]>();

            for (var i = 0; i < length; i++)
            {
                var toAdd = new char[length];
                for (var j = 0; j < length; j++)
                {
                    toAdd[j] = '_';
                }
                lines.Add(toAdd);
            }
            return lines;
        }

        /// <summary>
        /// Checks the Map given is valid to solve
        /// </summary>
        public void VerifyMapForCorrectness()
        {
            // Check if the map is square
            if (this.horizontalBlock.Count != this.verticalBlock.Count)
                throw new ArgumentException("The number of horizontal and vertical rows does not match");

            // Check if both edges add to the same value and each block could actually fit in the space
            var horizontalSum = 0;
            var verticalSum = 0;
            var edgeLength = this.horizontalBlock.Count;
            for (var i = 0; i < this.horizontalBlock.Count; i++)
            {
                if (this.horizontalBlock[i].Sum() + this.horizontalBlock[i].Length - 1 > edgeLength)
                    throw new ArgumentException("Horizontal row too large for given map size");
                if (this.verticalBlock[i].Sum() + this.verticalBlock[i].Length - 1 > edgeLength)
                    throw new ArgumentException("Vertical row too large for given map size");

                horizontalSum += this.horizontalBlock[i].Sum();
                verticalSum += this.verticalBlock[i].Sum();
            }
            if (horizontalSum != verticalSum)
                throw new ArgumentException("The sum of the horizontal and vertical rows does not match");
        }

        /// <summary>
        /// displays the map to the console.
        /// </summary>
        public void PrintMap()
        {
            Console.WriteLine();
            foreach (var line in this.map)
            {

                char output;
                Console.Write("    ");
                foreach (var c in line)
                {
                    output = c == 'S' ? 'S' : '_';
                    Console.Write($"{output} ");
                }
                Console.WriteLine();
            }
        }
    }
}
