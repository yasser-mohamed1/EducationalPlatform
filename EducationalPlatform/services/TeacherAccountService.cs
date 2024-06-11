﻿using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class TeacherAccountService : ITeacherAccountService
    {
        private readonly ITeacherAccountRepository _teacherRepository;

        public TeacherAccountService(ITeacherAccountRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<string> RegisterTeacherAsync(RegisterTeacherDto teacherDto)
        {
            var user = new ApplicationUser
            {
                UserName = teacherDto.Username,
                Email = teacherDto.Email,
                PhoneNumber = teacherDto.Phone,
                Teacher = new Teacher
                {
                    FirstName = teacherDto.FirstName,
                    LastName = teacherDto.LastName,
                    Address = teacherDto.Address,
                    Governorate = teacherDto.Governorate,
                }
            };

            var result = await _teacherRepository.CreateUserAsync(user, teacherDto.Password);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            await _teacherRepository.AddUserToRoleAsync(user, "Teacher");
            //await _teacherRepository.AddTeacherAsync(user.Teacher);
            await _teacherRepository.SaveChangesAsync();

            return "Teacher Registration Success";
        }
    }
}
