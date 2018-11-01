using Microsoft.EntityFrameworkCore;

namespace Bookmaker.DbModel
{
    public partial class BookmakerContext : DbContext
    {
        public BookmakerContext()
        {
        }

        public BookmakerContext(DbContextOptions<BookmakerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bets> Bets { get; set; }
        public virtual DbSet<Championships> Championships { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Sports> Sports { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bets>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity.Property(e => e.Stake).HasColumnType("money");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Bets)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bets_Events");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Bets)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bets_Players");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Bets)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Bets_Teams");
            });

            modelBuilder.Entity<Championships>(entity =>
            {
                entity.HasKey(e => e.ChampId);

                entity.Property(e => e.ChampName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Championships)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Champs_Sports");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventDate).HasColumnType("date");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.EventsAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_AwayTeams");

                entity.HasOne(d => d.Champ)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ChampId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Champs");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.EventsHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_HomeTeams");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Sports>(entity =>
            {
                entity.HasKey(e => e.SportId);

                entity.Property(e => e.SportName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_Sports");
            });
        }
    }
}
