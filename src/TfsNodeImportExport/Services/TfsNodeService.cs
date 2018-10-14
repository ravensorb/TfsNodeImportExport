// ***********************************************************************
// Assembly         : TfsNodeImportExport
// Author           : ravensorb
// Created          : 10-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="TfsNodeService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

using TfsNodeImportExport.Model;
using TfsNodeImportExport.Model.Tfs;

namespace TfsNodeImportExport.Services
{
	/// <summary>
	/// Class TfsNodeService.
	/// </summary>
	public class TfsNodeService
	{
		/// <summary>
		/// The connection
		/// </summary>
		private TfsImportExportConnection _conn;

		#region Events
		/// <summary>
		/// Occurs when [on progress].
		/// </summary>
		public event EventHandler<ProgressEventArgs> OnProgress;

		#endregion Events

		#region Public Methods
		/// <summary>
		/// Connects the asynchronous.
		/// </summary>
		/// <param name="conn">The connection.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> ConnectAsync(TfsImportExportConnection conn)
		{
			_conn = conn;

			var task = Task.Factory.StartNew(() =>
			{
				RaiseOnProgress("Connecting to Tfs");

				return true;
			}, TaskCreationOptions.AttachedToParent);

			return task;
		}

		/// <summary>
		/// Closes the asynchronous.
		/// </summary>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> CloseAsync()
		{
			var task = Task.Factory.StartNew(() =>
			{
				RaiseOnProgress("Closing Connection to Tfs");

				return true;
			}, TaskCreationOptions.AttachedToParent);

			return task;
		}

		/// <summary>
		/// Imports the file asynchronous.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="nodeType">Type of the node.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> ImportFileAsync(string fileName, TfsNodeTypes nodeType)
		{
			var task = Task.Factory.StartNew(() =>
			{
				try
				{
					RaiseOnProgress("Starting Import Process");

					RaiseOnProgress("Loading Area Path from file");
					var areaPath = TfsNode.LoadFromFile(fileName);

					var css = (ICommonStructureService)_conn.TfsProjectCollection.GetService(typeof(ICommonStructureService));

					if (!CreateOrRenameNode(css, areaPath, null, nodeType))
					{
						RaiseOnProgress("Failed to load area paths");

						return false;
					}

					return true;
				}
				catch (Exception ex)
				{
					RaiseOnProgress(ex.Message);
				}

				return false;
			}, TaskCreationOptions.AttachedToParent);

			return task;
		}

		/// <summary>
		/// Exports the file asynchronous.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="nodeType">Type of the node.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> ExportFileAsync(string fileName, TfsNodeTypes nodeType)
		{
			var task = Task.Factory.StartNew(() =>
			{
				try
				{
					RaiseOnProgress("Starting Export Process");

					var css = (ICommonStructureService)_conn.TfsProjectCollection.GetService(typeof(ICommonStructureService));

					var wiService = _conn.TfsProjectCollection.GetService<WorkItemStore>();

					Project selectedProject = wiService.Projects.GetByUri(new Uri(_conn.TfsProject.Uri));

					NodeCollection nc = null;
					switch (nodeType)
					{
						case TfsNodeTypes.Iteration:
							nc = selectedProject.IterationRootNodes;
							break;
						default:
						//case TfsNodeTypes.AreaPath:
							nc = selectedProject.AreaRootNodes;
							break;
					}

					RaiseOnProgress("Processing all Nodes in Tfs");
					var tfsNodeRoot = nc.ToTfsNode(css, _conn.TfsProject);

					RaiseOnProgress("Exporting to file");

					TfsNode.SaveToFile(tfsNodeRoot, fileName);

					return true;
				}
				catch (Exception ex)
				{
					RaiseOnProgress(ex.Message);
				}

				return false;
			}, TaskCreationOptions.AttachedToParent);

			return task;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Creates the or rename node.
		/// </summary>
		/// <param name="css">The CSS.</param>
		/// <param name="item">The item.</param>
		/// <param name="parent">The parent.</param>
		/// <param name="nodeType">Type of the node.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool CreateOrRenameNode(ICommonStructureService css, TfsNode item, TfsNode parent, TfsNodeTypes nodeType)
		{
			try
			{
				if (parent != null)
				{
					var rootNodeInfo = nodeType == TfsNodeTypes.AreaPath ? css.GetAreaPathRootNodeInfo(_conn.TfsProject) : css.GetIterationRootNodeInfo(_conn.TfsProject);
					var nodePath = item.ToValidPath(rootNodeInfo);
					var parentPath = parent.ToValidPath(rootNodeInfo);

					NodeInfo node = null;
					NodeInfo parentNode = null;

#pragma warning disable RCS1023 // Format empty block.
					try { node = css.GetNodeFromPath(nodePath); } catch { }
					try { parentNode = css.GetNodeFromPath(parentPath); } catch { }
#pragma warning restore RCS1023 // Format empty block.

					if (parentNode == null)
					{
						//This should never happen
						throw new NullReferenceException($"Unable to find Parent Node: {parentPath}");
					}

					if (node == null)
					{
						RaiseOnProgress($"Creating '{item.Name}' under '{parent.Path}'");
						css.CreateNode(item.Name, parentNode.Uri);
					}
					else
					{
						var s = item.Path.Split('\\').LastOrDefault();
						if (s != null && string.Compare(item.Name, s, true) != 0)
						{
							RaiseOnProgress($"Renaming '{node.Name}' to '{item.Name}'");
							css.RenameNode(node.Uri, item.Name);
						}
					}
				}

				foreach (var ap in item.Children)
				{
					if (!CreateOrRenameNode(css, ap, item, nodeType))
					{
						return false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				RaiseOnProgress(ex.Message);
			}

			return false;
		}

		/// <summary>
		/// Raises the on progress.
		/// </summary>
		/// <param name="message">The message.</param>
		private void RaiseOnProgress(string message)
		{
			Serilog.Log.Information(message);

			OnProgress?.BeginInvoke(this, new ProgressEventArgs { Message = message }, null, null);
		}
		#endregion Private Methods
	}

	/// <summary>
	/// Class ProgressEventArgs.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class ProgressEventArgs : EventArgs
	{
		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }
	}
}
