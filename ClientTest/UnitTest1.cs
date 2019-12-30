using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Proba;

namespace ClientTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mockedTcpClient = new Mock<Client>();
            mockedTcpClient.Setup(x => Client.SendMessage(It.IsAny<string>())).Returns(true);
            mockedTcpClient.Object.SendMessage("test");
            mockedTcpClient.Verify(x => x.SendMessage("test"));
        }
    }
}
