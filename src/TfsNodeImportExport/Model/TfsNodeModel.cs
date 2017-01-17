using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TfsNodeImportExport.Model.Tfs
{
	public class TfsNode
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("path")]
		public string Path { get; set; }

		public string Uri { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		//public AreaPath Parent { get; set; }
		[JsonProperty("children")]
		public IList<TfsNode> Children { get; set; } = new List<TfsNode>();

		public static void SaveToFile(TfsNode areaPath, string fileName)
		{
			var str = JsonConvert.SerializeObject(areaPath,
													Formatting.Indented,
													new JsonSerializerSettings
													{
														NullValueHandling = NullValueHandling.Ignore
													});

			if (System.IO.File.Exists(fileName))
			{
				System.IO.File.Delete(fileName);
			}

			System.IO.File.WriteAllText(fileName, str);
		}

		public static TfsNode LoadFromFile(string fileName)
		{
			if (!System.IO.File.Exists(fileName)) return null;

			var str = System.IO.File.ReadAllText(fileName);

			var result = JsonConvert.DeserializeObject<TfsNode>(str,
																new JsonSerializerSettings
																{
																	NullValueHandling = NullValueHandling.Ignore
																});

			return result;
		}

		/// <summary>
		/// https://msdn.microsoft.com/en-us/library/ms181692(v=vs.120).aspx
		/// </summary>
		public void CleanName()
		{
			if (string.IsNullOrEmpty(Name)) return;

			Name = Name.Replace("\\", "-")
						.Replace("/", "-")
						.Replace("$", "")
						.Replace("?", "")
						.Replace("*", "")
						.Replace(":", "-")
						.Replace("\"", "'")
						.Replace("&", " and ")
						.Replace(">", "-")
						.Replace("<", "-")
						.Replace("#", "-")
						.Replace("%", "")
						.Replace("|", "-")
						.Replace("+", " and ");

			if (Name.Length >= 255) Name = Name.Substring(0, 254);
		}
	}

	public enum TfsNodeTypes
	{
		AreaPath,
		Iteration
	}
}
