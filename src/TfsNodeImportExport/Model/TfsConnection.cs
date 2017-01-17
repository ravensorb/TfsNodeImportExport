namespace TfsNodeImportExport.Model
{
    public class TfsImportExportConnection
    {
       public  Microsoft.TeamFoundation.Client.TfsTeamProjectCollection TfsProjectCollection { get; set; }
       public  Microsoft.TeamFoundation.Server.ProjectInfo TfsProject { get; set; }
    }
}
