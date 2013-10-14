using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using MonackFr;
using System.Linq;
using MonackFr.Wrappers;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class LoaderTests
	{
		[TestMethod]
		[ExpectedException(typeof(System.IO.FileNotFoundException))]
		public void Constructor_WithNonExistingFile_ThrowsFileNotFoundException()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);
			
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
		}

		[TestMethod]
		public void Constructor_WithExistingFile_ComposesFile()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
			compositionMock.Verify(c => c.ComposeParts(It.IsAny<Object>()), Times.Exactly(1));
		}

		[TestMethod]
		public void Constructor_WithAnyFile_TestsIfFileExists()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
			fileMock.Verify(f => f.Exists("testfile"), Times.Exactly(1));
		}

	}
}
