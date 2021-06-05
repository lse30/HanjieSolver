using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hanjie;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace HanjieTest
{
    [TestClass]
    public class HanjieSolverTests
    {
        [TestMethod]
        public void Solve7in10LineTest()
        {

            int[] blocks = { 7 };
            char[] currentLine = new char[10];
            for (int i = 0; i < currentLine.Length; i++)
            {
                currentLine[i] = '_';
            }
            char[] expected = new char[10];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = '_';
            }
            expected[3] = 'S';
            expected[4] = 'S';
            expected[5] = 'S';
            expected[6] = 'S';
            char[] actual = HanjieSolver.LineSolver(blocks, currentLine);


            CollectionAssert.AreEqual(expected, actual, "Line solver doesn't work.");

        }

        [TestMethod]
        public void Solve131in10Test()
        {

            int[] blocks = { 1, 3, 1 };
            char[] currentLine = new char[10];

            for (int i = 0; i < currentLine.Length; i++)
            {
                currentLine[i] = '_';
            }
            currentLine[4] = 'S';
            currentLine[5] = 'S';
            currentLine[6] = 'S';
            currentLine[9] = 'S';
            char[] expected = new char[10];
            for (int i = 0; i < expected.Length; i++)
            {
                expected[i] = '_';
            }
            expected[4] = 'S';
            expected[5] = 'S';
            expected[6] = 'S';
            expected[9] = 'S';

            expected[3] = 'x';
            expected[7] = 'x';
            expected[8] = 'x';
            char[] actual = HanjieSolver.LineSolver(blocks, currentLine);

            CollectionAssert.AreEqual(expected, actual, "Line solver doesn't work.");

        }

        [TestMethod]
        public void SolveExpertPuzzleTest()
        {
            var horizontalBlocks = new List<int[]>
            {
                new int[] { 7 },
                new int[] { 2, 3 },
                new int[] { 3 },
                new int[] { 1 },
                new int[] { 2, 2 },
                new int[] { 3, 3, 1, 5 },
                new int[] { 1, 2, 2, 4, 2, 1 },
                new int[] { 2, 4, 4, 1, 1, 1 },
                new int[] { 4, 2, 1, 1, 2 },
                new int[] { 5, 3, 7 },
                new int[] { 6, 4 },
                new int[] { 11 },
                new int[] { 12 },
                new int[] { 4, 4 },
                new int[] { 3, 4 },
                new int[] { 4, 5 },
                new int[] { 3, 6 },
                new int[] { 3, 7 },
                new int[] { 3, 6 },
                new int[] { 2 }
            };

            var verticalBlocks = new List<int[]>
            {
                new int[] { 1, 1, 3, 1 },
                new int[] { 2, 2, 3, 2 },
                new int[] { 1, 1, 3, 2 },
                new int[] { 1, 2, 6, 4 },
                new int[] { 1, 2, 9 },
                new int[] { 1, 1, 8 },
                new int[] { 1, 1, 6, 1 },
                new int[] { 2, 2, 4, 2 },
                new int[] { 1, 2, 2, 2, 2 },
                new int[] { 2, 1, 3, 2, 3 },
                new int[] { 1, 1, 1, 2, 3 },
                new int[] { 3, 2, 4, 4 },
                new int[] { 5, 8 },
                new int[] { 1, 9 },
                new int[] { 3, 2, 5 },
                new int[] { 1, 2, 2 },
                new int[] { 2, 1 },
                new int[] { 3, 1 },
                new int[] { 1, 2 },
                new int[] { 4 }
            };

            var lines = HanjieSolver.InitMap(horizontalBlocks.Count);
            int attempts = 0;
            int maxAttempts = 20;
            while (!HanjieSolver.CheckComplete(lines) && attempts < maxAttempts)
            {
                lines = HanjieSolver.CheckMap(lines, horizontalBlocks, verticalBlocks);
                attempts++;
            }


            Assert.IsTrue(attempts < maxAttempts, "Solver solved puzzled unsuccessfully");
        }
    }
}
