namespace EducationalPlatform.DTO
{
	public class CreateSubjectDTO
	{
		public string subjName { get; set; }
		public string Level { get; set; }
		public string Describtion { get; set; }
		public int pricePerHour { get; set; }
		public int TeacherId { get; set; }
		public int Term { get; set; }
        public int totalPrice { get; set; }
        public bool isOnilne { get; set; }
        public bool isActive { get; set; }
    }
}
