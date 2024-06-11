using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TeacherRepository : ITeacherRepository
{
    private readonly EduPlatformContext _context;

    public TeacherRepository(EduPlatformContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherDto>> GetTeachersAsync()
    {
        var teachers = await _context.Teachers
            .Include(t => t.User)
            .Select(t => new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                userName = t.User.UserName,
                Governorate = t.Governorate,
                Address = t.Address,
                Email = t.User.Email,
                Phone = t.User.PhoneNumber,
                ProfileImageUrl = t.ProfileImageUrl
            })
            .ToListAsync();

        return teachers;
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(int id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.User)
            .Where(t => t.Id == id)
            .Select(t => new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                userName = t.User.UserName,
                Governorate = t.Governorate,
                Address = t.Address,
                Email = t.User.Email,
                Phone = t.User.PhoneNumber,
                ProfileImageUrl = t.ProfileImageUrl
            })
            .FirstOrDefaultAsync();

        return teacher;
    }

    public async Task UpdateTeacherAsync(Teacher teacher)
    {
        var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacher.Id);

        if (existingTeacher == null)
            throw new InvalidOperationException("Teacher not found");

        // Update properties
        existingTeacher.FirstName = teacher.FirstName;
        existingTeacher.LastName = teacher.LastName;
        // Update other properties similarly...

        // Save changes
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteTeacherAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Teacher> GetTeacherUserByIdAsync(int id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.User)
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        return teacher;
    }
}
