using SharedLibrary.DataTransferObjects;
using System.Reflection;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace RazorLibrary.Helpers
{
	public sealed  class MultipartFormDataContentBuilder
	{
		private MultipartFormDataContent content = new();

		public MultipartFormDataContentBuilder AddObjectPublicProperties(object obj)
		{
			foreach (var property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var propertyValue = property.GetValue(obj)?.ToString();
				if (propertyValue == null) continue;

				content.Add(new StringContent(propertyValue), property.Name);
			}

			return this;
		}

		public MultipartFormDataContentBuilder AddFile(IBrowserFile file, int maxAllowedFileSizeInMb = 5, string nameInContent = "file")
		{
			var stream = file.OpenReadStream(ConvertMbToBytes(maxAllowedFileSizeInMb));
			return AddStream(stream, file.Name, maxAllowedFileSizeInMb, nameInContent);
		}

		public MultipartFormDataContentBuilder AddStream(
			Stream stream, 
			string fileName,
			int maxAllowedFileSizeInMb = 5,
			string nameInContent = "file")
		{
			if (stream.Length > ConvertMbToBytes(maxAllowedFileSizeInMb))
			{
				throw new ArgumentException("File is too big");
			}

			var fileContent = new StreamContent(stream);
			fileContent.Headers.Add("Content-Type", "application/octet-stream");
			content.Add(fileContent, nameInContent, fileName);

			return this;
		}

		public MultipartFormDataContent Build()
		{
			var result = content;

			Reset();

			return result;
		}

		public void Reset()
		{
			content = new MultipartFormDataContent();
		}

		private static int ConvertMbToBytes(int maxAllowedFileSizeInMb)
		{
			return maxAllowedFileSizeInMb * 1024 * 1024;
		}
	}
}
