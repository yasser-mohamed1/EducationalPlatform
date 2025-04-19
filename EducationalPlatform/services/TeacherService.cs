using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class TeacherService : ITeacherService
{
    private readonly Func<HttpContext, UserManager<ApplicationUser>> _userManagerFactory;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IWebHostEnvironment _env;

    public TeacherService(Func<HttpContext,UserManager<ApplicationUser>> userManagerFactory, 
        ITeacherRepository teacherRepository, IWebHostEnvironment env)
    {
        _userManagerFactory = userManagerFactory;
        _teacherRepository = teacherRepository;
        _env = env;
    }

    public Task<List<TeacherDto>> GetTeachersAsync()
    {
        return _teacherRepository.GetTeachersAsync();
    }

    public Task<TeacherDto> GetTeacherByIdAsync(int id)
    {
        return _teacherRepository.GetTeacherByIdAsync(id);
    }

    public async Task<bool> UpdateTeacherAsync(int id, UpdateTeacherDto dto, HttpContext httpContext)
    {
        var existingTeacher = await _teacherRepository.GetTeacherUserByIdAsync(id);
        if (existingTeacher == null)
            return false;

        if (!string.IsNullOrEmpty(dto.FirstName))
        {
            existingTeacher.FirstName = dto.FirstName;
        }
        if (!string.IsNullOrEmpty(dto.LastName))
        {
            existingTeacher.LastName = dto.LastName;
        }
        if (!string.IsNullOrEmpty(dto.Phone))
        {
            existingTeacher.User.PhoneNumber = dto.Phone;
        }
        if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
        {
            await UploadProfileImageAsync(id, dto.ProfileImage);
        }
        if (!string.IsNullOrEmpty(dto.Address))
        {
            existingTeacher.Address = dto.Address;
        }

        if (!string.IsNullOrEmpty(dto.Governorate))
        {
            existingTeacher.Governorate = dto.Governorate;
        }

        if (!string.IsNullOrEmpty(dto.Password))
        {
            var userManager = _userManagerFactory.Invoke(httpContext);
            var newPasswordHash = userManager.PasswordHasher.HashPassword(existingTeacher.User, dto.Password);
            existingTeacher.User.PasswordHash = newPasswordHash;
        }

        try
        {
            await _teacherRepository.UpdateTeacherAsync(existingTeacher);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public Task<bool> DeleteTeacherAsync(int id)
    {
        return _teacherRepository.DeleteTeacherAsync(id);
    }

    public async Task<string> UploadProfileImageAsync(int id, IFormFile imageFile)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
        if (teacher == null)
            return "";

        var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp" };
        if (imageFile == null || imageFile.Length == 0 || !permittedMimeTypes.Contains(imageFile.ContentType))
        {
            throw new Exception("Provide a valid image format");
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        if (!permittedExtensions.Contains(extension))
        {
            return "";
        }

        if (!string.IsNullOrEmpty(teacher.ProfileImageUrl))
        {
            var oldImagePath = Path.Combine(_env.WebRootPath, teacher.ProfileImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return "http://edu1.runasp.net/uploads/" + fileName;
    }

}
