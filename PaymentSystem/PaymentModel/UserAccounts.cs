using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.PaymentModel
{
    public partial class UserAccounts
    {
        [Key]
        public int UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
