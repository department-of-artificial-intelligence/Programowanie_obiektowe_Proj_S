using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class PaymentTests
    {
        [Fact]
        public void PostPaiment_ZmieniaStatusNaOplacone_GdyNieoplacone()
        {
            // Arrange
            var payment = new Payment();
            // Upewniamy się, że stan początkowy jest poprawny
            Assert.Equal(PaymentStatuses.Unpaid, payment.Status);

            // Act
            bool czyUdaloSieoplacic = payment.PostPayment();

            // Assert
            Assert.True(czyUdaloSieoplacic);
            Assert.Equal(PaymentStatuses.Paid, payment.Status);
        }

        [Fact]
        public void PostPaiment_ZwracaFalse_GdyJuzOplacone()
        {
            // Arrange
            var payment = new Payment();
            payment.PostPayment(); // Pierwsze opłacenie

            // Act
            bool probaDrugiegoOplacenia = payment.PostPayment();

            // Assert
            Assert.False(probaDrugiegoOplacenia);
            Assert.Equal(PaymentStatuses.Paid, payment.Status);
        }
    }
}