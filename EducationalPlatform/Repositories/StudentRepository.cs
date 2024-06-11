using EducationalPlatform.Controllers;
using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EduPlatformContext _context;

        public StudentRepository(EduPlatformContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDTO>> GetStudentsAsync()
        {
            return await _context.Students
                .Include(s => s.User)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    userName = s.User.UserName,
                    Email = s.User.Email,
                    Phone = s.User.PhoneNumber,
                    ProfileImageUrl = s.ProfileImageUrl,
                    Level = s.Level
                })
                .ToListAsync();
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    userName = s.User.UserName,
                    Email = s.User.Email,
                    Phone = s.User.PhoneNumber,
                    ProfileImageUrl = s.ProfileImageUrl,
                    Level = s.Level
                })
                .FirstOrDefaultAsync();

            return student;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
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

        public async Task<Student> GetStudentUserByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return student;
        }

        public async Task<IEnumerable<SearchSubjectDto>> SearchSubjects(string searchQuery, string governorate)
        {
            var query = _context.Subjects.Include(s => s.Teacher).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(s => (s.subjName.LevenshteinDistance(searchQuery) <= 3) && ContainsMajorityOfCharacters(s.subjName, searchQuery));
            }

            if (!string.IsNullOrEmpty(governorate))
            {
                query = query.Where(s => s.Teacher.Governorate == governorate);
            }

            return await query.Select(s => new SearchSubjectDto
            {
                subjectId = s.Id,
                subjName = s.subjName,
                Level = s.Level,
                Describtion = s.Describtion,
                pricePerHour = s.pricePerHour,
                Term = s.Term,
                AddingTime = s.AddingTime,
                TeacherId = s.TeacherId,
                teacherName = s.Teacher.FirstName + " " + s.Teacher.LastName,
                profileImageUrl = s.Teacher.ProfileImageUrl
            }).ToListAsync();
        }

        public async Task<IEnumerable<TeacherWithSubjectDTO>> SearchTeachers(string searchQuery, string governorate)
        {
            var query = _context.Teachers.Include(t => t.Subjects).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t => (t.FirstName + t.LastName).LevenshteinDistance(searchQuery) <= 3 ||
                                          t.FirstName.LevenshteinDistance(searchQuery) <= 1 ||
                                          t.LastName.LevenshteinDistance(searchQuery) <= 1);
            }

            if (!string.IsNullOrEmpty(governorate))
            {
                query = query.Where(t => t.Governorate == governorate);
            }

            return await query.Select(t => new TeacherWithSubjectDTO
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                ProfileImageUrl = t.ProfileImageUrl,
                Address = t.Address,
                Email = t.User.Email,
                Phone = t.User.PhoneNumber,
                Governorate = t.Governorate,
                Subjects = t.Subjects.Select(s => new SubjectDto
                {
                    subjectId = s.Id,
                    subjName = s.subjName,
                    Level = s.Level,
                    Describtion = s.Describtion,
                    pricePerHour = s.pricePerHour,
                    Term = s.Term,
                    AddingTime = s.AddingTime
                }).ToList()
            }).ToListAsync();
        }

        private static bool ContainsMajorityOfCharacters(string source, string target)
        {
            var sourceChars = source.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            var targetChars = target.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
            int matchingCharactersCount = 0;
            int requiredMajorityCount = (int)Math.Ceiling(targetChars.Sum(kv => kv.Value) / 2.0);

            foreach (var targetChar in targetChars)
            {
                if (sourceChars.ContainsKey(targetChar.Key))
                {
                    matchingCharactersCount += Math.Min(sourceChars[targetChar.Key], targetChar.Value);
                }
            }

            return matchingCharactersCount >= requiredMajorityCount;
        }
    }

    public static class StringExtensions
    {
        public static int LevenshteinDistance(this string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b.Length;
            if (string.IsNullOrEmpty(b)) return a.Length;

            int[,] costs = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                costs[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                costs[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    costs[i, j] = Math.Min(Math.Min(costs[i - 1, j] + 1, costs[i, j - 1] + 1), costs[i - 1, j - 1] + cost);
                }
            }

            return costs[a.Length, b.Length];
        }
    }
}
