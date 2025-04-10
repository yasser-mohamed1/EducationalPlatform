﻿using EducationalPlatform.Data;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Repositories
{
    public class StudentAccountRepository : IStudentAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EduPlatformContext _context;

        public StudentAccountRepository(UserManager<ApplicationUser> userManager, EduPlatformContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
