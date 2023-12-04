using CleanArchAttendanceApp.Core.Entities;
using CleanArchAttendanceApp.Core.Models;

namespace CleanArchAttendanceApp.Core.Interfaces;

public interface IAttendanceRepository
{
  // User Methods
  public Task<IEnumerable<User>> GetAllUsersAsync();
  public Task<User> GetUserByIdAsync(Guid id, bool includeAttendance = false);
  public Task<User> FindUserByIdAsync(Guid id);
  public Task<bool> AddUser(string fullname, string username, string passwordhash, string role);
  public Task<User> GetUserByUserNameAsync(string userName);
  public Task<bool> UserExistsWithIdAsync(Guid id);
  public Task<bool> UserExistsWithUserNameAsync(string username);
  public Task<bool> DeleteUserAsync(User user);


  // Attendace Record Methods
  public Task<IEnumerable<Attendance>> GetAllAttendanceRecordsAsync();
  public Task<IEnumerable<Attendance>> GetAllAttendanceRecordsByUserIdAsync(Guid id);
  public Task<Attendance> GetAttendanceRecordByIdAsync(Guid id);
  public Task<bool> AttendanceRecordExists(Guid id);
  public Task<bool> DeleteAttendanceRecordAsync(Guid id);
  public Task<Attendance> AddAttendanceRecordForClockIn(Guid userId);
  public Task<Attendance> AddAttendanceRecordForUser(AttendanceDto attendanceDto, Guid userId);
  public Task<Attendance> GetAttendanceRecordForTodayByUserIDAsync(Guid id);


  // SaveChanegs
  public Task<bool> SaveChangesAsync();
}
