using System;
using Xunit;
using IIG.FileWorker;
using System.IO;

namespace XUnitTest {
    public class BaseFileWorkerTests {
        private string testsDirFullPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\test";

        [Theory]
        [InlineData("Test.txt")]
        [InlineData("FileDoesNotExist.txt")]
        public void TestGetFileName(string filename) {
            string path = testsDirFullPath + "\\" + filename;
            string realName = BaseFileWorker.GetFileName(path);

            Assert.Equal(filename, realName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestGetFileNameException(string filename) {
            try {
                BaseFileWorker.GetFileName(filename);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Theory]
        [InlineData("Test.txt")]
        [InlineData("FileDoesNotExist.txt")]
        public void TestGetFullPath(string filename) {
            string fullPathExpected = testsDirFullPath + "\\" + filename;
            string localPath = new Uri(fullPathExpected).LocalPath;
            Assert.Equal(fullPathExpected, BaseFileWorker.GetFullPath(localPath));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestGetFullPathException(string filename)
        {
            try {
                BaseFileWorker.GetFullPath(filename);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Theory]
        [InlineData("Test.txt")]
        [InlineData("FileDoesNotExist.txt")]
        public void TestGetPath(string filename) {
            string pathExpected = testsDirFullPath + "\\" + filename;
            string filePathExpected = testsDirFullPath;
            Assert.Equal(filePathExpected, BaseFileWorker.GetPath(pathExpected));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestGetPathException(string filename) {
            try {
                BaseFileWorker.GetPath(filename);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void TestMkDir() {
            string fullPathExpected = Environment.CurrentDirectory + "\\dir";
            string fullPathReal = BaseFileWorker.MkDir("dir");
            Assert.Equal(fullPathExpected, fullPathReal);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestMkDirException(string dirname) {
            try {
                BaseFileWorker.MkDir(dirname);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void TestReadAll() {
            string fullPathExpected = testsDirFullPath + "\\Test.txt";
            string contentReal = BaseFileWorker.ReadAll(fullPathExpected);
            Assert.Equal("oh shit i'm sorry\r\nsorry for what", contentReal);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestReadAllException(string dirname) {
            try {
                BaseFileWorker.ReadAll(dirname);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Fact]
        public void TestReadLines() {
            string fileFullPath = testsDirFullPath + "\\Test.txt";
            string[] linesExpected = { "oh shit i'm sorry", "sorry for what" };
            string[] lines = BaseFileWorker.ReadLines(fileFullPath);
            Assert.Equal(linesExpected, lines);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestReadLinesException(string dirname) {
            try {
                BaseFileWorker.ReadLines(dirname);
            } catch (Exception e) {
                Assert.NotNull(e);
            }
        }

        [Theory]
        [InlineData("Test.txt", "Copied.txt", true)]
        [InlineData("Test.txt", "Copied.txt", false)]
        public void TestTryCopy(string from, string to, bool force) {
            string fileFromFullPath = testsDirFullPath + "\\" + from;
            string fileToFullPath = testsDirFullPath + "\\" + to;
            bool copied = BaseFileWorker.TryCopy(fileFromFullPath, fileToFullPath, force);
            Assert.True(copied);
        }

        [Fact]
        public void TestTryWrite() {
            string fileWrittenFullPath = testsDirFullPath + "\\TryWrite.txt";
            bool written = BaseFileWorker.TryWrite("should write it in file", fileWrittenFullPath);
            Assert.True(written);
        }

        [Theory]
        [InlineData("Write.txt", "should write it in file")]
        [InlineData("", "wrong filename")]
        public void TestWrite(string filename, string text) {
            string fileWrittenFullPath = testsDirFullPath + "\\" + filename;
            bool written = BaseFileWorker.Write(text, fileWrittenFullPath);
            Assert.True(written);
        }
    }
}
