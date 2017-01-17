using TfsNodeImportExport.Model.Tfs;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Xml;

namespace TfsNodeImportExport
{
	public static class TfsExtensions
	{
		#region Project Collections
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
		public static string ToValidPath(this TfsNode tfsNod, NodeInfo nodeInfo)
		{
			tfsNod.CleanName();

			var isPathValid = !string.IsNullOrEmpty(tfsNod.Path) && tfsNod.Path != "\\";

			var str = string.Format("{0}{1}{2}", nodeInfo.Path, isPathValid ? "\\" : "", isPathValid ? tfsNod.Path : "");

			return str;
		}
		#endregion TfsNode

		#region Common Structure Service
		public static NodeInfo GetAreaPathRootNodeInfo(this ICommonStructureService css, ProjectInfo project)
		{
			return css.GetNodeInfoByStructureType(project, "ProjectModelHierarchy");
		}

		public static NodeInfo GetIterationRootNodeInfo(this ICommonStructureService css, ProjectInfo project)
		{
			return css.GetNodeInfoByStructureType(project, "ProjectLifecycle");
		}

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
		private static TfsNode ToTfsNode(this Node node, ICommonStructureService css, ProjectInfo project)
		{
			Serilog.Log.Information("Processing {0} [{1}]", node.Name, node.Path);

			var tfsNode = new TfsNode { Name = node.Name, Path = node.Path };

			if (tfsNode.Path.StartsWith($"\\{project.Name}\\"))
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
