// ***********************************************************************
// Assembly         : TfsNodeImportExport
// Author           : ravensorb
// Created          : 10-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="TfsNodeModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace TfsNodeImportExport.Model.Tfs
{
	/// <summary>
	/// Class TfsNode.
	/// </summary>
	public class TfsNode
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty("name")]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
		[JsonProperty("path")]
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the URI.
		/// </summary>
		/// <value>The URI.</value>
		public string Uri { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>The start date.</value>
		public DateTime? StartDate { get; set; }
		/// <summary>
		/// Gets or sets the finish date.
		/// </summary>
		/// <value>The finish date.</value>
		public DateTime? FinishDate { get; set; }

		//public AreaPath Parent { get; set; }
		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>The children.</value>
		[JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
		public IList<TfsNode> Children { get; set; } = new List<TfsNode>();

		/// <summary>
		/// Saves to file.
		/// </summary>
		/// <param name="areaPath">The area path.</param>
		/// <param name="fileName">Name of the file.</param>
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

		/// <summary>
		/// Loads from file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>TfsNode.</returns>
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

	/// <summary>
	/// Enum TfsNodeTypes
	/// </summary>
	public enum TfsNodeTypes
	{
		/// <summary>
		/// The area path
		/// </summary>
		AreaPath,
		/// <summary>
		/// The iteration
		/// </summary>
		Iteration
	}
}
