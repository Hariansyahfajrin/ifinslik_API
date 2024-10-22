using System.Reflection;
using DotNetEnv;

namespace Service.Helper
{
	public class BaseService
	{
		public string GenerateCode<T>(FormatCode formatCode, T? lastRow, string columnName)
		{
			string code = formatCode.Prefix;

			if (formatCode.Date != null)
			{
				code += formatCode.Delimiter + formatCode.Date.Value.ToString(formatCode.DateFormat);
			}

			code += formatCode.Delimiter;

			int lastSequence = 0;

			try
			{
				if (lastRow != null)
				{
					PropertyInfo? property = lastRow.GetType().GetProperty(columnName);
					if (property != null)
					{
						string value = property.GetValue(lastRow)?.ToString() ?? "";
						if (value.Contains(code))
						{
							// Check if Value Format Match
							lastSequence = int.Parse(value.Replace(code, ""));
						}
					}
				}

			}
			catch (Exception)
			{
				throw new Exception($"{columnName} doesn't exist in {lastRow?.GetType().Name}");
			}

			int currentNumber = lastSequence + 1;



			code += currentNumber.ToString($"D{formatCode.RunNumberLen}");

			return code;
		}
		protected string GUID()
		{
			return Guid.NewGuid().ToString("N").ToLower();
		}
		protected async Task<string> FileUpload(FileRequest file)
		{
			if (file.Name is null) throw new Exception("File name not found");
			if (file.Path is null) throw new Exception("File path not found");
			if (file.Bytes is null) throw new Exception("File not found");

			string name = Path.GetFileNameWithoutExtension(file.Name);
			string ext = Path.GetExtension(file.Name);
			file.Name = name.Replace(" ", "_") + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;

			string configPath = Env.GetString("FILE_STORAGE") ?? throw new Exception("FileStorage is not configured");

			var path = Path.Combine(configPath, file.Path, file.Name);

			await System.IO.File.WriteAllBytesAsync(path, file.Bytes);

			return file.Name;
		}

		protected async Task<string> GetBase64File(string path)
		{
			byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

			string base64 = Convert.ToBase64String(bytes);

			return base64;
		}

		protected async Task<byte[]> GetFile(string path)
		{
			byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

			return bytes;
		}
	}


}