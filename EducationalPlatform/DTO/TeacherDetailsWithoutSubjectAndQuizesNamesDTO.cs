namespace EducationalPlatform.DTO
{
    public class TeacherDetailsWithoutSubjectAndQuizesNamesDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string>Phones { get; set; }
        public char gender { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
