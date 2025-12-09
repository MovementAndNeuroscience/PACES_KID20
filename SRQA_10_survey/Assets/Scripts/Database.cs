using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionAnswerInfo
{
    public string question;
    public int answer;
    public string instrumentName;
    public string eventName;
    public string redcap_time_varname; 
}
public class Database : MonoBehaviour
{
    public string subjectId { get; set; }
    public string instrumentName { get; set; }
    public string event_name { get; set; }
    public string redcap_time_varname { get; set; }
    public float number_of_pages { get; set; }
    public float number_of_activated_pages { get; set; }
    public bool enable_REDCap { get; set; }
    public bool enable_Filesaver { get; set; }

    public List<QuestionAnswerInfo> questAnswInfoTable = new List<QuestionAnswerInfo>(); 

    public void addQAPair(string question, int answer)
    {
        foreach (var qaInfoTableRow in questAnswInfoTable)
        {
            if (qaInfoTableRow.question == question)
            {
                questAnswInfoTable.Remove(qaInfoTableRow);
                break;
            }
        }

        var tempRow = new QuestionAnswerInfo();
        tempRow.question = question;
        tempRow.answer = answer;
        tempRow.instrumentName = instrumentName;
        tempRow.eventName = event_name;
        tempRow.redcap_time_varname = redcap_time_varname;

        if (!questAnswInfoTable.Contains(tempRow))
            questAnswInfoTable.Add(tempRow);
    }
    public List<QuestionAnswerInfo> GetQATable()
    {
        return questAnswInfoTable;
    }
}
