// ***********************************************************************
// Assembly         : TfsNodeImportExport
// Author           : ravensorb
// Created          : 10-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="TfsConnection.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace TfsNodeImportExport.Model
{
	/// <summary>
	/// Class TfsImportExportConnection.
	/// </summary>
	public class TfsImportExportConnection
	{
		/// <summary>
		/// Gets or sets the TFS project collection.
		/// </summary>
		/// <value>The TFS project collection.</value>
		public Microsoft.TeamFoundation.Client.TfsTeamProjectCollection TfsProjectCollection { get; set; }
		/// <summary>
		/// Gets or sets the TFS project.
		/// </summary>
		/// <value>The TFS project.</value>
		public Microsoft.TeamFoundation.Server.ProjectInfo TfsProject { get; set; }
	}
}
