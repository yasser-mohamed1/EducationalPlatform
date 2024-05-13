using EducationalPlatform.Data;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.services
{
	public class OptionServices : IOptionServices
	{
		private readonly EduPlatformContext _context;

		public OptionServices(EduPlatformContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task AddOption(int QuestionId, string OptionContent)
		{
			if (string.IsNullOrEmpty(OptionContent))
			{
				throw new ArgumentException("Option content cannot be null or empty.", nameof(OptionContent));
			}

			var option = new Option { Content = OptionContent, QuestionId = QuestionId };

			try
			{
				_context.Options.Add(option);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to add option.", ex);
			}
		}

		public async Task EditOption(int id, string OptionContent)
		{
			var option = await _context.Options.FindAsync(id);

			if (option == null)
			{
				throw new ArgumentException("Option not found.", nameof(id));
			}

			option.Content = OptionContent;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to edit option.", ex);
			}
		}

		public async Task<Option> GetOptionById(int id)
		{
			var option = await _context.Options.FindAsync(id);

			if (option == null)
			{
				throw new ArgumentException("Option not found.", nameof(id));
			}

			return option;
		}

		public async Task RemoveOption(int id)
		{
			var option = await _context.Options.FindAsync(id);

			if (option == null)
			{
				throw new ArgumentException("Option not found.", nameof(id));
			}

			_context.Options.Remove(option);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to remove option.", ex);
			}
		}
	}
}
