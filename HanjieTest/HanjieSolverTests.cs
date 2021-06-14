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
            var actual = HanjieMain.LineSolver(blocks, currentLine);


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
            var actual = HanjieMain.LineSolver(blocks, currentLine);

            CollectionAssert.AreEqual(expected, actual, "Line solver doesn't work.");

        }

        [TestMethod]
        public void SolveGenericPuzzleTest()
        {
            var horizontalString = "2 2, 3 1 3, 6 5, 1 4, 4 3, 1 5 2, 9 2, 1 4, 1 4, 9 4, 13, 2 1, 4 8, 8, 15";
            var verticalString = "2 1 1 1, 3 1 1 1, 3 1 2 1 1,1 1 2 1 1, 2 2 2 1, 1 1 2 1,3 2 1,3 6,11,4 1 3, 1 2 1 3, 2 4 3, 4 8, 11 3,11 3";
            var hanjieMap = new HanjieMap(horizontalString, verticalString);
            var maxAttempts = 20;
            hanjieMap = HanjieMain.SolveHanjieMap(maxAttempts, hanjieMap);
            Assert.IsTrue(HanjieMain.CheckComplete(hanjieMap.map));
        }

        [TestMethod]
        public void SolveExpertPuzzleTest()
        {
            var horizontalBlocks = "7,2 3,3,1,2 2,3 3 1 5,1 2 2 4 2 1,2 4 4 1 1 1,4 2 1 1 2,5 3 7,6 4,11,12,4 4,3 4,4 5,3 6,3 7,3 6,2";
            var verticalBlocks = "1 1 3 1,2 2 3 2,1 1 3 2,1 2 6 4,1 2 9,1 1 8,1 1 6 1,2 2 4 2,1 2 2 2 2,2 1 3 2 3,1 1 1 2 3,3 2 4 4,5 8,1 9,3 2 5,1 2 2,2 1,3 1,1 2,4";
            var hanjieMap = new HanjieMap(horizontalBlocks, verticalBlocks);
            var attempts = 0;
            var maxAttempts = 20;
            while (!HanjieMain.CheckComplete(hanjieMap.map) && attempts < maxAttempts)
            {
                hanjieMap.map = HanjieMain.CheckMap(hanjieMap.map, hanjieMap.horizontalBlock, hanjieMap.verticalBlock);
                attempts++;
            }


            Assert.IsTrue(attempts < maxAttempts, "Solver solved puzzled unsuccessfully");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRowsMapTest()
        {
            var horizontalBlocks = "4 3 7,3 6,2";
            var verticalBlocks = "1 1 3 1,2 2 3 2,1 1 3 2,2 2,2 1,3 1,1 2,4";
            var hanjieMap = new HanjieMap(horizontalBlocks, verticalBlocks);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidSumMapTest()
        {
            var horizontalBlocks = "1, 1, 1, 1, 1";
            var verticalBlocks = "9, 9, 9, 9, 9";
            var hanjieMap = new HanjieMap(horizontalBlocks, verticalBlocks);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OversizedHorizontalRowTest()
        {
            var horizontalBlocks = "1, 1 1 2, 1, 1, 1";
            var verticalBlocks = "1, 1 1, 1 2, 1, 1";
            var hanjieMap = new HanjieMap(horizontalBlocks, verticalBlocks);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OversizedVerticalRowTest()
        {
            var horizontalBlocks = "1, 1 1, 1, 1, 1";
            var verticalBlocks = "1, 7, 1 2, 1, 1";
            var hanjieMap = new HanjieMap(horizontalBlocks, verticalBlocks);
        }
    }
}

