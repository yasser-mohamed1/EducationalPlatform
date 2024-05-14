using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.services
{
	public interface IOptionServices
	{
		public Task<IEnumerable<OptionDto>> GetOptions(int QuestionId);
        public Task<OptionDto> GetOptionById(int id);
		public Task AddOption(int QuestionId,string OptionContent);
		public Task RemoveOption(int id);
		public Task EditOption(int id, string OptionContent);
	}
}
