using System;
using Xunit;
using IIG.FileWorker;
using System.IO;

namespace XUnitTestProject1 {
    public class UnitTest1 {
        private string testsDirFullPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\test";
        [Theory]
        [InlineData("Test.txt")]
        [InlineData("gucci.png")]
        [InlineData("gucci2.png")]
        [InlineData(null)]
        public void TestGetFileName(string filename) {
            string path = testsDirFullPath + "\\" + filename;
            string realName = BaseFileWorker.GetFileName(path);

            Assert.Equal(filename, realName);
        }
        [Theory]
        [InlineData("Test.txt")]
        [InlineData(null)]
        public void TestGetFullPath(string filename) {
            string fullPathExpected = testsDirFullPath + "\\" + filename;
            string localPath = new Uri(fullPathExpected).LocalPath;
            string fullPathReal = BaseFileWorker.GetFullPath(localPath);
            Assert.Equal(fullPathExpected, fullPathReal);
        }
        [Theory]
        [InlineData("Test.txt")]
        [InlineData(null)]
        public void TestGetPath(string filename) {
            string pathExpected = testsDirFullPath + "\\" + filename;
            string filePathExpected = testsDirFullPath;
            string fullPathReal = BaseFileWorker.GetPath(pathExpected);
            Assert.Equal(filePathExpected, fullPathReal);
        }
        [Theory]
        [InlineData("dir")]
        [InlineData(null)]
        public void TestMkDir(string dirname) {
            string fullPathExpected = Environment.CurrentDirectory + "\\" + dirname;
            string fullPathReal = BaseFileWorker.MkDir(dirname);
            Assert.Equal(fullPathExpected, fullPathReal);
        }
        [Theory]
        [InlineData("Test.txt", "oh shit i'm sorry\r\nsorry for what")]
        [InlineData(null, "there is no such file")]
        public void TestReadAll(string filename, string text) {
            string fullPathExpected = testsDirFullPath + "\\" + filename;
            string contentReal = BaseFileWorker.ReadAll(fullPathExpected);
            Assert.Equal(text, contentReal);
        }
        [Fact]
        public void TestReadLines() {
            string fileFullPath = testsDirFullPath + "\\Test.txt";
            string[] linesExpected = { "oh shit i'm sorry", "sorry for what" };
            string[] lines = BaseFileWorker.ReadLines(fileFullPath);
            Assert.Equal(linesExpected, lines);
        }
        [Theory]
        [InlineData("Test.txt", "Copied.txt", false)]
        [InlineData("Test.txt", "Copied.txt", true)]
        public void TestTryCopy(string from, string to, bool force) {
            string fileFromFullPath = testsDirFullPath + "\\" + from;
            string fileToFullPath = testsDirFullPath + "\\" + to;
            bool copied = BaseFileWorker.TryCopy(fileFromFullPath, fileToFullPath, force);
            Assert.True(copied, "Not copied");
        }
        [Theory]
        [InlineData("TryWrite.txt", "should write it in file")]
        [InlineData(null, "wrong filename")]
        public void TestTryWrite(string filename, string text) {
            string fileWrittenFullPath = testsDirFullPath + "\\" + filename;
            bool written = BaseFileWorker.TryWrite(text, fileWrittenFullPath);
            Assert.True(written, "Not written");
        }
        [Theory]
        [InlineData("Write.txt", "should write it in file")]
        public void TestWrite(string filename, string text) {
            string fileWrittenFullPath = testsDirFullPath + "\\" + filename;
            bool written = BaseFileWorker.Write(text, fileWrittenFullPath);
            Assert.True(written, "Not written");
        }
    }
}
