using Microsoft.EntityFrameworkCore;

namespace PaymentSystem.PaymentModel
{
    public partial class AccountContext : DbContext
    {
        public AccountContext()
        {
        }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAccounts> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccounts>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasDefaultValue(0);
            });
        }
    }
}
