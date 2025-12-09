using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class FileSaver : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    private string fileName = "Paces_Kid20_Data.csv";

    public void CreateFile(string SubjectId, List<QuestionAnswerInfo> qaTable)
    {
        fileName = SubjectId + "_Paces_Kid20_Data.csv";
        string filePath = getPath();
        string[] rowDataTemp = new string[2];
        rowDataTemp[0] = "Question";
        rowDataTemp[1] = "Answer";
        rowData.Add(rowDataTemp);

        foreach (var questionAnswerRow in qaTable)
        {
            rowDataTemp = new string[2];
            rowDataTemp[0] = questionAnswerRow.question;
            rowDataTemp[1] = questionAnswerRow.answer.ToString();
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + fileName;
#elif UNITY_ANDROID
        return Application.persistentDataPath+fileName;
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+fileName;
#else
        return Application.dataPath +"/"+fileName;
#endif
    }
}
