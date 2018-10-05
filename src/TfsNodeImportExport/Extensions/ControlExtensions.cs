// ***********************************************************************
// Assembly         : TfsNodeImportExport
// Author           : ravensorb
// Created          : 10-04-2018
//
// Last Modified By : ravensorb
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="ControlExtensions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows.Forms;

namespace TfsNodeImportExport
{
	/// <summary>
	/// Class ControlExtensions.
	/// </summary>
	public static class ControlExtensions
	{
		/// <summary>
		/// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
		/// </summary>
		/// <param name="this">The this.</param>
		/// <param name="code">The code.</param>
		public static void UIThread(this Control @this, Action code)
		{
			if (@this.InvokeRequired)
			{
				@this.BeginInvoke(code);
			}
			else
			{
				code.Invoke();
			}
		}
	}
}
