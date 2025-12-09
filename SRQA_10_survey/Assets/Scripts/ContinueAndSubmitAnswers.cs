using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ContinueAndSubmitAnswers : MonoBehaviour
{
    public GameObject datalayer;
    public GameObject[] questions;
    public GameObject nextPage;
    public GameObject redcapCommunicator;
    public GameObject config;
    public GameObject currentPage;
    public bool lastSurveyPage; 
    private bool enableTimeStamp = true;
    private bool using_redcap;
    private bool using_filesaver; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubmitAndContinue()
    {
        using_redcap = datalayer.GetComponent<Database>().enable_REDCap;
        using_filesaver = datalayer.GetComponent<Database>().enable_Filesaver;

        if (questions.Length == 0 )
            questions = GameObject.FindGameObjectsWithTag("Question");

        foreach (GameObject question in questions)
        {
            datalayer.GetComponent<Database>().addQAPair(question.GetComponent<question_logic>().GetREDCapVarName(), question.GetComponent<question_logic>().getAnswerValue());
        }
        var id = datalayer.GetComponent<Database>().subjectId;
        var apikey = config.GetComponent<REDCapN2BackKey>().GetApiKey(); 
        
        if(using_redcap)
        {
            foreach (var questionAnswerRow in datalayer.GetComponent<Database>().GetQATable() )
            {
                if (enableTimeStamp)
                {
                    redcapCommunicator.GetComponent<REDCapCommunicator>().SendEntryTimeToREDCap(int.Parse(id), questionAnswerRow.eventName,
                    apikey, questionAnswerRow.instrumentName, questionAnswerRow.redcap_time_varname);

                    enableTimeStamp = false;
                }

                redcapCommunicator.GetComponent<REDCapCommunicator>().SendQuestionDataToREDCap(int.Parse(id), questionAnswerRow.eventName,
                    apikey, questionAnswerRow.answer, questionAnswerRow.question, questionAnswerRow.instrumentName);
            }
        }
        if(using_filesaver && lastSurveyPage)
        {
            datalayer.GetComponent<FileSaver>().CreateFile(id, datalayer.GetComponent<Database>().GetQATable()); 
        }

        if (nextPage == null)
        {
            StartCoroutine(DelayBeforeQuitting(2.0f));
        }
        else
        {
            nextPage.SetActive(true);
            currentPage.SetActive(false); 
        }
        IEnumerator DelayBeforeQuitting(float delay)
        {
            yield return new WaitForSeconds(delay);
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
                Application.OpenURL("https://erda.dk/vgrid/mopac_surveys/Paces_Kid20/SurveyEnd.html");
                Application.Unload();
#else
            Application.Quit();
#endif
        }

    }
}
