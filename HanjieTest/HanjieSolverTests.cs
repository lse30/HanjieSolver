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

            var blocks = new int[]{ 7 };
            var currentLine = new char[10];
            for (var i = 0; i < currentLine.Length; i++)
            {
                currentLine[i] = '_';
            }
            var expected = new char[10];
            for (var i = 0; i < expected.Length; i++)
            {
                expected[i] = '_';
            }
            expected[3] = 'S';
            expected[4] = 'S';
            expected[5] = 'S';
            expected[6] = 'S';
            var actual = HanjieSolver.LineSolver(blocks, currentLine);


            CollectionAssert.AreEqual(expected, actual, "Line solver doesn't work.");

        }

        [TestMethod]
        public void Solve131in10Test()
        {

            var blocks = new[]{ 1, 3, 1 };
            var currentLine = new char[10];

            for (var i = 0; i < currentLine.Length; i++)
            {
                currentLine[i] = '_';
            }
            currentLine[4] = 'S';
            currentLine[5] = 'S';
            currentLine[6] = 'S';
            currentLine[9] = 'S';
            var expected = new char[10];
            for (var i = 0; i < expected.Length; i++)
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
            var actual = HanjieSolver.LineSolver(blocks, currentLine);

            CollectionAssert.AreEqual(expected, actual, "Line solver doesn't work.");

        }

        [TestMethod]
        public void SolveExpertPuzzleTest()
        {
            var horizontalBlocks = new List<int[]>
            {
                new [] { 7 },
                new [] { 2, 3 },
                new [] { 3 },
                new [] { 1 },
                new [] { 2, 2 },
                new [] { 3, 3, 1, 5 },
                new [] { 1, 2, 2, 4, 2, 1 },
                new [] { 2, 4, 4, 1, 1, 1 },
                new [] { 4, 2, 1, 1, 2 },
                new [] { 5, 3, 7 },
                new [] { 6, 4 },
                new [] { 11 },
                new [] { 12 },
                new [] { 4, 4 },
                new [] { 3, 4 },
                new [] { 4, 5 },
                new [] { 3, 6 },
                new [] { 3, 7 },
                new [] { 3, 6 },
                new [] { 2 }
            };

            var verticalBlocks = new List<int[]>
            {
                new [] { 1, 1, 3, 1 },
                new [] { 2, 2, 3, 2 },
                new [] { 1, 1, 3, 2 },
                new [] { 1, 2, 6, 4 },
                new [] { 1, 2, 9 },
                new [] { 1, 1, 8 },
                new [] { 1, 1, 6, 1 },
                new [] { 2, 2, 4, 2 },
                new [] { 1, 2, 2, 2, 2 },
                new [] { 2, 1, 3, 2, 3 },
                new [] { 1, 1, 1, 2, 3 },
                new [] { 3, 2, 4, 4 },
                new [] { 5, 8 },
                new [] { 1, 9 },
                new [] { 3, 2, 5 },
                new [] { 1, 2, 2 },
                new [] { 2, 1 },
                new [] { 3, 1 },
                new [] { 1, 2 },
                new [] { 4 }
            };

            var lines = HanjieSolver.InitMap(horizontalBlocks.Count);
            var attempts = 0;
            var maxAttempts = 20;
            while (!HanjieSolver.CheckComplete(lines) && attempts < maxAttempts)
            {
                lines = HanjieSolver.CheckMap(lines, horizontalBlocks, verticalBlocks);
                attempts++;
            }


            Assert.IsTrue(attempts < maxAttempts, "Solver solved puzzled unsuccessfully");
        }
    }
}
