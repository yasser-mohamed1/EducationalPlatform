using EducationalPlatform.Entities;

namespace EducationalPlatform.services
{
	public interface IOptionServices
	{
		public Task<Option> GetOptionById(int id);
		public Task AddOption(int QuestionId,string OptionContent);
		public Task RemoveOption(int id);
		public Task EditOption(int id, string OptionContent);
	}
}
