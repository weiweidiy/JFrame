using System.Collections.Generic;
using static JFrame.CombatReporter;

namespace JFrame
{
    public interface ICombatReporter
    {
        string AddReportData(ReportType reportType, ReportData reportData);

        ICombatReportData GetReportData(string uid);

        List<ICombatReportData> GetAllReportData();
    }
}