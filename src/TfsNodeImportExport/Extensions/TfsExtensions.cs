// ***********************************************************************
// Assembly         : TfsNodeImportExport
// Author           : ravensorb
// Created          : 10-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="TfsExtensions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Xml;

using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

using TfsNodeImportExport.Model.Tfs;

namespace TfsNodeImportExport
{
	/// <summary>
	/// Class TfsExtensions.
	/// </summary>
	public static class TfsExtensions
	{
		#region Project Collections
		/// <summary>
		/// Returns the correct Project in the collection associated with the specified Url
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="uri">The URI.</param>
		/// <returns>Project.</returns>
		public static Project GetByUri(this ProjectCollection collection, Uri uri)
		{
			foreach (Project p in collection)
			{
				if (string.Compare(p.Uri.ToString(), uri.ToString(), true) == 0)
					return p;
			}

			return null;
		}
		#endregion Project Collections

		#region TfsNode
		/// <summary>
		/// Checked to see if the node is value (not null and not a root path) and returns a correctly formatted string (structured like a directory path).
		/// </summary>
		/// <param name="tfsNod">The TFS nod.</param>
		/// <param name="nodeInfo">The node information.</param>
		/// <returns>System.String.</returns>
		public static string ToValidPath(this TfsNode tfsNod, NodeInfo nodeInfo)
		{
			tfsNod.CleanName();

			var isPathValid = !string.IsNullOrEmpty(tfsNod.Path) && tfsNod.Path != "\\";

			var str = string.Format("{0}{1}{2}", nodeInfo.Path, isPathValid ? "\\" : "", isPathValid ? tfsNod.Path : "");

			return str;
		}
		#endregion TfsNode

		#region Common Structure Service
		/// <summary>
		/// Gets the area path root node information.
		/// </summary>
		/// <param name="css">A Common Structure service Instance</param>
		/// <param name="project">The project.</param>
		/// <returns>NodeInfo.</returns>
		public static NodeInfo GetAreaPathRootNodeInfo(this ICommonStructureService css, ProjectInfo project)
		{
			return css.GetNodeInfoByStructureType(project, "ProjectModelHierarchy");
		}

		/// <summary>
		/// Gets the iteration root node information.
		/// </summary>
		/// <param name="css">A Common Structure service Instance</param>
		/// <param name="project">The project.</param>
		/// <returns>NodeInfo.</returns>
		public static NodeInfo GetIterationRootNodeInfo(this ICommonStructureService css, ProjectInfo project)
		{
			return css.GetNodeInfoByStructureType(project, "ProjectLifecycle");
		}

		/// <summary>
		/// Gets the node info for from the structure service for the specified project and structure type.
		/// </summary>
		/// <param name="css">A Common Structure service Instance</param>
		/// <param name="project">The project.</param>
		/// <param name="structureTypeName">Name of the structure type.</param>
		/// <returns>NodeInfo.</returns>
		public static NodeInfo GetNodeInfoByStructureType(this ICommonStructureService css, ProjectInfo project, string structureTypeName)
		{
			foreach (NodeInfo info in css.ListStructures(project.Uri))
			{
				if (info.StructureType == structureTypeName)
				{
					return info;
				}
			}

			return null;
		}
		#endregion Common Structure Service

		#region Node Collection
		/// <summary>
		/// To the TFS node.
		/// </summary>
		/// <param name="nodeCollection">The node collection.</param>
		/// <param name="css">A Common Structure service Instance</param>
		/// <param name="project">The project.</param>
		/// <returns>TfsNode.</returns>
		public static TfsNode ToTfsNode(this NodeCollection nodeCollection, ICommonStructureService css, ProjectInfo project)
		{
			var tfsRootNode = new TfsNode { Name = "", Path = "\\" };

			foreach (Node node in nodeCollection)
			{
				tfsRootNode.Children.Add(node.ToTfsNode(css, project));
			}

			return tfsRootNode;
		}
		#endregion Node Collection

		#region Node
		/// <summary>
		/// To the TFS node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="css">A Common Structure service Instance</param>
		/// <param name="project">The project.</param>
		/// <returns>TfsNode.</returns>
		private static TfsNode ToTfsNode(this Node node, ICommonStructureService css, ProjectInfo project)
		{
			Serilog.Log.Information("Processing {0} [{1}]", node.Name, node.Path);

			var tfsNode = new TfsNode { Name = node.Name, Path = node.Path };

			if (tfsNode.Path.StartsWith($"\\{project.Name}\\", StringComparison.Ordinal))
			{
				tfsNode.Path = tfsNode.Path.Replace($"\\{project.Name}", "");
			}

			// We really only need to do this for Iteration Paths right now
			if (node.IsIterationNode)
			{
				XmlNode xmlNode = css.GetNodesXml(new[] { node.Uri.ToString() }, false);

				if (xmlNode == null || xmlNode.FirstChild == null) return null;
				xmlNode = xmlNode.FirstChild;

				var path = xmlNode.Attributes["Path"]?.Value;
				if (!string.IsNullOrEmpty(path))
				{
					var strStartDate = xmlNode.Attributes["StartDate"]?.Value;
					var strEndDate = xmlNode.Attributes["FinishDate"]?.Value;

					DateTime startDate = DateTime.MinValue;
					DateTime endDate = DateTime.MinValue;

					if (!string.IsNullOrEmpty(strStartDate) && !string.IsNullOrEmpty(strEndDate))
					{
						bool datesValid = true;

						// Both dates should be valid.
						datesValid &= DateTime.TryParse(strStartDate, out startDate);
						datesValid &= DateTime.TryParse(strEndDate, out endDate);

						// Clear the dates unless both are valid.
						if (!datesValid)
						{
							startDate = DateTime.MinValue;
							endDate = DateTime.MinValue;
						}
					}

					tfsNode.StartDate = (startDate != DateTime.MinValue) ? (DateTime?)startDate : null;
					tfsNode.FinishDate = (endDate != DateTime.MinValue) ? (DateTime?)endDate : null;
				}
			}

			if (node.HasChildNodes)
			{
				foreach (Node nc in node.ChildNodes)
				{
					tfsNode.Children.Add(nc.ToTfsNode(css, project));
				}
			}

			return tfsNode;
		}
		#endregion Node
	}
}
