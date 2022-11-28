#if UNITY_EDITOR
using Assets.HeadStart.Features.Database;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class HeadStartBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Building on path " + report.summary.outputPath + " reseting player.json");
        var dataService = new DataService(Application.streamingAssetsPath + "/Database.db");
        dataService.CleanDB();

        //__json.Database.Recreate(Application.streamingAssetsPath + "/player.json");
    }
}
#endif