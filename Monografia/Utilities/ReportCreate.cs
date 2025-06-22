using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace Monografia.Utilities
{
    public static class ReportCreate
    {
        public static ReportViewer GetReport(string reportPath, object datos)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = reportPath;
            localReport.DataSources.Add(new ReportDataSource("DataSet1", datos));

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = reportPath;
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datos));
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1200);
            reportViewer.Height = Unit.Pixel(800);
            reportViewer.AsyncRendering = true;
            reportViewer.ShowExportControls = true;
            reportViewer.ShowToolBar = true;
            reportViewer.ShowPrintButton = true;
            reportViewer.LocalReport.Refresh();
            return reportViewer;

        }

    }
}