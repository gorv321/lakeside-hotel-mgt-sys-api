using LakeSideHotelApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LakeSideHotelApi.Data
{
    public class LakeSideHotelDbContext : DbContext
    {
        public LakeSideHotelDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<BookedRoom> BookedRooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.BookedRooms)
                .WithOne(br => br.Room)
                .HasForeignKey(br => br.RoomId);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18,2)");
        }

    }
    }

