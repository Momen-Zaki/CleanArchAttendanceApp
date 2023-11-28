using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchAttendanceApp.Infrastructure.DbContexts;

public class AttendanceContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Attendance> AttenanceRecords { get; set; }

  public AttendanceContext(DbContextOptions<AttendanceContext> options)
  : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

    modelBuilder.Entity<User>()
                .HasMany(u => u.AttendanceRecords)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

    modelBuilder.Entity<Attendance>().HasKey(u => u.Id);

    modelBuilder.Entity<User>()
        .HasData(
            new User()
            {
              FullName = "User 1 Full Name",
              UserName = "UserName1",
              PasswrodHash = "user 1 password hashed",
              Role = UserRole.Employee
            },
            new User()
            {
              FullName = "User 2 Full Name",
              UserName = "UserName2",
              PasswrodHash = "user 2 password hashed",
              Role = UserRole.Employee
            },
            new User()
            {
              FullName = "User 3 Full Name",
              UserName = "UserName3",
              PasswrodHash = "user 3 password hashed",
              Role = UserRole.Employee
            }
        );

    //modelBuilder.Entity<Attendance>()
    //    .HasData(
    //        new Attendance()
    //        {
    //            AttendanceDay = DateTime.Now,
    //            ClockedIn = false,
    //            ClockedInAt = DateTime.Now,
    //            ClockedOut = false,
    //            ClockedOutAt = DateTime.Now,
    //        },
    //        new Attendance()
    //        {
    //            AttendanceDay = DateTime.Now,
    //            ClockedIn = false,
    //            ClockedInAt = DateTime.Now,
    //            ClockedOut = false,
    //            ClockedOutAt = DateTime.Now,
    //        },
    //        new Attendance()
    //        {
    //            AttendanceDay = DateTime.Now,
    //            ClockedIn = false,
    //            ClockedInAt = DateTime.Now,
    //            ClockedOut = false,
    //            ClockedOutAt = DateTime.Now,
    //        },
    //        new Attendance()
    //        {
    //            AttendanceDay = DateTime.Now,
    //            ClockedIn = false,
    //            ClockedInAt = DateTime.Now,
    //            ClockedOut = false,
    //            ClockedOutAt = DateTime.Now,
    //        }
    //    );

    base.OnModelCreating(modelBuilder);
  }

}
