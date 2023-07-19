using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsoleToText : MonoBehaviour
{
    public Text debugText;
    private string output = "";
    private string stack = "";

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        Debug.Log("Log Enabled!");
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
        ClearLog();
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString + "\n" + output;
        stack = stackTrace;
    }

    private void OnGUI()
    {
        debugText.text = output;
    }

    public void ClearLog()
    {
        output = "";
    }

    public void PrintPrefabPositions(List<Vector3> positions)
    {
        ClearLog();
        output += "Prefab Positions:\n";
        foreach (Vector3 position in positions)
        {
            output += position.ToString() + "\n";
        }
    }
}
