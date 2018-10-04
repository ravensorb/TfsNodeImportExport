using Microsoft.TeamFoundation.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TfsNodeImportExport.Model;
using TfsNodeImportExport.Model.Tfs;
using TfsNodeImportExport.Services;

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
namespace TfsNodeImportExport
{
	public partial class MainForm : Form
	{
		private TfsImportExportConnection _connection = new TfsImportExportConnection();

		public MainForm()
		{
			InitializeComponent();
		}

		private void btnSelectTfsProject_Click(object sender, EventArgs e)
		{
			TeamProjectPicker picker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);

			var dialogResult = picker.ShowDialog();

			ClearTfsConnection();

			if (dialogResult == DialogResult.OK)
			{
				_connection.TfsProjectCollection = picker.SelectedTeamProjectCollection;
				_connection.TfsProject = picker.SelectedProjects.First();

				txtTfsCollectionUrl.Text = _connection.TfsProjectCollection.Uri.ToString();
				txtbxTfsSelectedProject.Text = _connection.TfsProject.Name;

				EnableDisableUI(true);
			}
			else { EnableDisableUI(false); }
		}

		private void ClearTfsConnection()
		{
			txtTfsCollectionUrl.Text = string.Empty;
			txtbxTfsSelectedProject.Text = string.Empty;
		}

		private void EnableDisableUI(bool enable)
		{
			btnExport.UIThread(() => btnExport.Enabled = enable);
			btnImport.UIThread(() => btnImport.Enabled = enable);
			rbtnTypeAreaPaths.UIThread(() => rbtnTypeAreaPaths.Enabled = enable);
			rbtnTypeIterations.UIThread(() => rbtnTypeIterations.Enabled = enable);
		}

		private async void btnImport_Click(object sender, EventArgs e)
		{
			var service = new TfsNodeService();
			service.OnProgress += (s, args) =>
			{
				txtbxProgress.UIThread(() => txtbxProgress.Text += args.Message + System.Environment.NewLine);
				txtbxProgress.UIThread(() => txtbxProgress.SelectionStart = txtbxProgress.Text.Length);
			};

			ofdlgImport.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
			var result = ofdlgImport.ShowDialog();

			if (result == DialogResult.OK)
			{
				EnableDisableUI(false);

				txtbxProgress.UIThread(() => txtbxProgress.Text = "");
				btnSelectTfsProject.UIThread(() => btnSelectTfsProject.Enabled = false);

				service.ConnectAsync(_connection).Wait();

				var nodeType = rbtnTypeAreaPaths.Checked ? TfsNodeTypes.AreaPath : TfsNodeTypes.Iteration;

				var task = service.ImportFileAsync(ofdlgImport.FileName, nodeType).ContinueWith((t) =>
						{
							service.CloseAsync();

							btnSelectTfsProject.UIThread(() => btnSelectTfsProject.Enabled = true);

							EnableDisableUI(true);
						});

				await Task.WhenAll(task).ConfigureAwait(true);
			}
		}

		private async void btnExport_Click(object sender, EventArgs e)
		{
			var service = new TfsNodeService();
			service.OnProgress += (s, args) =>
			{
				txtbxProgress.UIThread(() => txtbxProgress.Text += args.Message + System.Environment.NewLine);
				txtbxProgress.UIThread(() => txtbxProgress.SelectionStart = txtbxProgress.Text.Length);
			};

			sfdlgExport.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
			var result = sfdlgExport.ShowDialog();

			if (result == DialogResult.OK)
			{
				EnableDisableUI(false);

				txtbxProgress.UIThread(() => txtbxProgress.Text = "");
				btnSelectTfsProject.UIThread(() => btnSelectTfsProject.Enabled = false);

				service.ConnectAsync(_connection).Wait();

				var nodeType = rbtnTypeAreaPaths.Checked ? TfsNodeTypes.AreaPath : TfsNodeTypes.Iteration;

				var task = service.ExportFileAsync(sfdlgExport.FileName, nodeType).ContinueWith((t) =>
				{
					service.CloseAsync();

					btnSelectTfsProject.UIThread(() => btnSelectTfsProject.Enabled = true);

					EnableDisableUI(true);
				});

				await Task.WhenAll(task).ConfigureAwait(true);
			}
		}

		private void tlstrpProgress_Click(object sender, EventArgs e)
		{

		}

		private void tlstrpLabel_Click(object sender, EventArgs e)
		{

		}

		private void grpbxConnectionDetails_Enter(object sender, EventArgs e)
		{

		}

		private void txtbxTfsSelectedProject_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void txtTfsCollectionUrl_TextChanged(object sender, EventArgs e)
		{

		}

		private void lblTfsCollectionUrl_Click(object sender, EventArgs e)
		{

		}

		private void grpbxStatus_Enter(object sender, EventArgs e)
		{

		}

		private void txtbxProgress_TextChanged(object sender, EventArgs e)
		{

		}

		private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void rbtnTypeIterations_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void rbtnTypeAreaPaths_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void grpbxActions_Enter(object sender, EventArgs e)
		{

		}

		private void ofdlgImport_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void sfdlgExport_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}
	}
}
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
