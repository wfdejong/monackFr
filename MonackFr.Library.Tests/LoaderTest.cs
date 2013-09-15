using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using MonackFr;
using System.Linq;
using MonackFr.Interfaces;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class LoaderTest
	{
		[TestMethod]
		[ExpectedException(typeof(System.IO.FileNotFoundException))]
		public void file_not_found_exeption()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
		}

		[TestMethod]
		public void compose_parts_is_hit()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
			compositionMock.Verify(c => c.ComposeParts(It.IsAny<Object>()), Times.Exactly(1));
		}

		[TestMethod]
		public void file_exists_hit()
		{
			Mock<IFile> fileMock = new Mock<IFile>();
			Mock<ICompositionContainer> compositionMock = new Mock<ICompositionContainer>();
			fileMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			Loader<object> loader = new Loader<object>("testfile", fileMock.Object, compositionMock.Object);
			fileMock.Verify(f => f.Exists("testfile"), Times.Exactly(1));
		}

	}
}
