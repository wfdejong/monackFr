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
		public void Load_WithNonExistingFile_ThrowsFileNotFoundException()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);

			ILoader<object> loader = new Loader<object>(fileMock.Object, compositionMock.Object);
			loader.Load("");
		}

		[TestMethod]
		public void Load_WithExistingFile_ComposesFile()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			ILoader<object> loader = new Loader<object>(fileMock.Object, compositionMock.Object);
			loader.Load("");
			
			compositionMock.Verify(c => c.ComposeParts(It.IsAny<object>()), Times.Exactly(1));
		}

		[TestMethod]
		public void Load_WithAnyFile_TestsIfFileExists()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			ILoader<object> loader = new Loader<object>(fileMock.Object, compositionMock.Object);
			loader.Load("");

			fileMock.Verify(f => f.Exists(It.IsAny<string>()), Times.Exactly(1));
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void Load_WithoutFileSet_ThrowsNullReferenceException()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();			
			ILoader<object> loader = new Loader<object>(fileMock.Object, compositionMock.Object);
			loader.Load("");			
		}

		[TestMethod]
		public void Load_WithPath_SetsCompositionContainerPath()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			ILoader<object> loader = new Loader<object>(fileMock.Object, compositionMock.Object);
			loader.Load("");
			compositionMock.VerifySet(c => c.Path = It.IsAny<string>()) ;
		}
	}
}
