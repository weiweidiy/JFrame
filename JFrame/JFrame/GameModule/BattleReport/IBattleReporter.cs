using System.Collections.Generic;

namespace JFrame
{
    public interface IBattleReporter
    {
        string AddReportMainData(int frame, float escapeTime, string casterUID, int actionId, List<string> targetsUID);

        void AddReportResultData(string uid, string targetUID, int value, int attr, int buff);
    }
}