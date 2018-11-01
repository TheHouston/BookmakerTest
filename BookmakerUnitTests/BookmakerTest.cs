using Bookmaker.Extentions;
using Moq;
using PaymentSystem.Core;
using Xunit;

namespace BookmakerUnitTests
{
    public class BookmakerTest
    {     
        [Fact]
        public void CheckGetPaymentId()
        {
            var mockRepository = new Mock<IAccountRepository>();
            mockRepository.Setup(p => p.CheckFunds(1, 232))
                .Returns(true);           
            Assert.True(condition: mockRepository.Object.CheckFunds(1,232));
        }

        [Fact]
        public void CheckCrypted()
        {
            var password = "123456";
            var decryptedPassword = "TWda9coPZiWSqG4R4pmvZg==";
            Assert.Equal(SecurePasswordHasher.Encrypt(password), decryptedPassword);
        }
        [Fact]
        public void CheckDecrypted()
        {
            var password = "123456";
            var decryptedPassword = "TWda9coPZiWSqG4R4pmvZg==";
            Assert.Equal(SecurePasswordHasher.Decrypt(decryptedPassword), password);
        }
    }
}
