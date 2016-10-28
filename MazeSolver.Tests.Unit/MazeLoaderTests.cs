using System;
using System.IO;
using MazeSolver.MazeComponents;
using MazeSolver.MazeComponents.Interfaces;
using Moq;
using NUnit.Framework;

namespace MazeSolver.Tests.Unit
{
    [TestFixture]
    public class MazeLoaderTests
    {
        [Test]
        public void IoExceptionThrownOnMazeValidationRaisesIoException()
        {
            var loader = new MazeLoader();

            var validatorMock = new Mock<IMazeValidator>();
            validatorMock.Setup(x => x.ValidateMazeFile(It.IsAny<string>()))
                .Throws<IOException>()
                .Verifiable("Must be called");

            var readerMock = new Mock<IMazeReader>();
            readerMock.Setup(x => x.ReadMaze(It.IsAny<string>())).Throws<Exception>().Verifiable("Must be called");

            loader.MazeValidator = validatorMock.Object;
            loader.MazeReader = readerMock.Object;

            Assert.Throws(typeof (IOException), () => loader.LoadCoordinatesFromFile("file"));

            validatorMock.Verify(x => x.ValidateMazeFile(It.IsAny<string>()), Times.Once);
            readerMock.Verify(x => x.ReadMaze(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ExceptionThrownOnMazeReadingRaisesException()
        {
            var loader = new MazeLoader();

            var validatorMock = new Mock<IMazeValidator>();
            validatorMock.Setup(x => x.ValidateMazeFile(It.IsAny<string>())).Verifiable("Must be called");

            var readerMock = new Mock<IMazeReader>();
            readerMock.Setup(x => x.ReadMaze(It.IsAny<string>())).Throws<Exception>().Verifiable("Must be called");

            loader.MazeValidator = validatorMock.Object;
            loader.MazeReader = readerMock.Object;

            Assert.Throws(typeof(Exception), () => loader.LoadCoordinatesFromFile("file"));

            validatorMock.Verify(x => x.ValidateMazeFile(It.IsAny<string>()), Times.Once);
            readerMock.Verify(x => x.ReadMaze(It.IsAny<string>()), Times.Once);
        }
    }
}
