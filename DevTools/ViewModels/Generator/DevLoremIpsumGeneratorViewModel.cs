using DevTools.Core.BaseClasses;
using System;
using System.Text;

namespace DevTools.ViewModels.Generator
{
	public class DevLoremIpsumGeneratorViewModel : ViewModelBase
	{
		public DevLoremIpsumGeneratorViewModel()
		{
			Words = 1;
			Sentences = 1;
			Paragraphs = 1;
		}

		public string GeneratedText
		{
			get => Get<string>();
			set => Set(value);
		}

		public int Words
		{
			get => Get<int>();
			set => Set(value);
		}

		public int Sentences
		{
			get => Get<int>();
			set => Set(value);
		}

		public int Paragraphs
		{
			get => Get<int>();
			set => Set(value);
		}

		public void Execute_Generate()
		{
			try
			{
				__LoremIpsum(Words, Sentences, Paragraphs);
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		private void __LoremIpsum(int words,
		int sentences, int paragraphs)
		{

			var allWords = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
				"adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
				"tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

			StringBuilder result = new StringBuilder();

			for (int p = 0; p < paragraphs; p++)
			{
				for (int s = 0; s < sentences; s++)
				{
					var random = new Random();
					for (int w = 0; w < words; w++)
					{
						if (w > 0) { result.Append(" "); }
						result.Append(allWords[random.Next(allWords.Length)]);
					}
					result.Append(". ");
				}
				result.Append("\n\n");
			}

			GeneratedText = result.ToString();
		}

	}
}
