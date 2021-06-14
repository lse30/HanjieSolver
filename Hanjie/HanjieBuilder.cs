using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanjie
{
    public class HanjieBuilder
    {
        public static HanjieMap BuildHanjieMap()
        {
            var length = 10;
            var map = MakeRandomMap(length);
            var blocks = CreateBlocks(map);
            return new HanjieMap(blocks[0], blocks[1]);
        }

        private static List<bool[]> MakeRandomMap(int length)
        {
            var map = new List<bool[]>();
            int random;

            for (var i = 0; i < length; i++)
            {
                var toAdd = new bool[length];
                for (var j = 0; j < length; j++)
                {
                    random = new Random().Next(2);
                    toAdd[j] = Convert.ToBoolean(random); 
                }
                map.Add(toAdd);
            }
            return map;
        }
        private static void PrintRow(bool[] row)
        {
            Console.WriteLine();
            Console.Write("    ");
            foreach (var item in row)
            {
                if (item)
                {
                    Console.Write("S ");
                } else
                {
                    Console.Write("_ ");
                }
            }
        }

        private static string RowToBlocks(bool[] row)
        {
            string output = "";
            int block = 0;
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i])
                {
                    block += 1;
                }
                else if (block > 0)
                {
                    output += block.ToString() + " ";
                    block = 0;
                }
            }
            if (block > 0)
            {
                output += block.ToString() + " ";
            }
            return output;
        }

        private static string[] CreateBlocks(List<bool[]> map)
        {
            var horizontalString = "";
            var verticalString = "";
            
            foreach (var row in map)
            {
                PrintRow(row);
                horizontalString += RowToBlocks(row).Trim() + ",";
            }
            bool[] vertRow;
            for (var i = 0; i < map.Count; i++)
            {
                vertRow = new bool[map.Count];
                for (var j = 0; j < map.Count; j++)
                {
                    vertRow[j] = map[j][i];
                }
                verticalString += RowToBlocks(vertRow).Trim() + ",";
            }
            Console.WriteLine();
            return new string[] { horizontalString[0..^1], verticalString[0..^1]};
        }
    }
}
