using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public GameObject dataLayer; 
    public GameObject subjectIdTextField;
    public GameObject InsertSubIdText;
    public GameObject introtext;
    public GameObject kidscreen_1_5;
    public GameObject kidscreen_intro;
    public GameObject kidscreen_intro_2;
    public bool enableREDCap;
    public bool enableFilesaver;
    public GameObject config;
    public GameObject redcapCom;
    public GameObject VirtualKeyboard;
    public GameObject progressSlider;
    public bool enableVirtualKeyboard = false;
    public int numberOfPages = 5;


    public string subjectId;

    private bool enableIntroText = true;
    private bool enablePauseAfterNormTraining = true;
    private bool enablePauseAfterBlock1 = true;
    private REDCapCommunicator communicator;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        subjectIdTextField.SetActive(true);
        InsertSubIdText.SetActive(true);
        kidscreen_1_5.SetActive(false);
        kidscreen_intro.SetActive(false);
        kidscreen_intro_2.SetActive(false);
        introtext.SetActive(false);
        progressSlider.SetActive(false);
        if (enableVirtualKeyboard == false)
            VirtualKeyboard.SetActive(false); 

        communicator = redcapCom.GetComponent<REDCapCommunicator>();
        dataLayer.GetComponent<Database>().number_of_pages = numberOfPages;

        dataLayer.GetComponent<Database>().enable_REDCap = enableREDCap;
        dataLayer.GetComponent<Database>().enable_Filesaver = enableFilesaver;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && enableIntroText || VirtualKeyboard.GetComponent<TNVirtualKeyboard>().GetEnter() && enableIntroText)
        {
            VirtualKeyboard.GetComponent<TNVirtualKeyboard>().EnterReleased();
            var textField = subjectIdTextField.GetComponent<TMPro.TMP_InputField>();
            subjectId = textField.text;
            var isInt = int.TryParse(subjectId, out int value);
            var isSplitable = subjectId.Split(" ");
            if(enableREDCap && isInt == false && isSplitable.Length == 1)
            {
                StartCoroutine(UniloginCoroutineWrapper());
            }
            else if (enableREDCap && isSplitable.Length > 1)
            {
                StartCoroutine(FindChildCoroutineWrapper());
                
            }
            else if (!enableREDCap)
            {
                subjectIdTextField.SetActive(false);
                InsertSubIdText.SetActive(false);
                introtext.SetActive(true);
                enableIntroText = false;
            }
            var res = 1;
            if(int.TryParse(subjectId,out res) && enableREDCap)
            {
                StartCoroutine(FindCurrentInstance());
            }
            dataLayer.GetComponent<Database>().subjectId = subjectId;
            
        }
        if(GameObject.FindGameObjectsWithTag("page").Length == 0 )
        {
            progressSlider.SetActive(false);
        }
    }
    private IEnumerator UniloginCoroutineWrapper()
    {
        //make this an Ienumerator and yield the routine
        yield return StartCoroutine(communicator.GetRecordIdFromUniLogin(subjectId, config.GetComponent<REDCapN2BackKey>().GetApiKey()));
        var id = communicator.subject_ID; 
        if (id != "")
        {
            subjectId = id;
            subjectIdTextField.SetActive(false);
            InsertSubIdText.SetActive(false);
            introtext.SetActive(true);
            VirtualKeyboard.GetComponent<TNVirtualKeyboard>().EnableEnterMode();
            enableIntroText = false;
            dataLayer.GetComponent<Database>().subjectId = subjectId;
            var res = 1;
            if (int.TryParse(subjectId, out res))
            {
                StartCoroutine(FindCurrentInstance());
            }

        }
        else
        {
            InsertSubIdText.GetComponent<TMPro.TMP_Text>().text = "Indtast et validt Uni-Login eller dit <color=red> fornavn, efternavn og klasse</color> \nTryk dernæst på <color=green>Enter</color>";
            subjectIdTextField.GetComponent<ActiveFieldAtStartUp>().activateField();
        }
    }

private IEnumerator FindChildCoroutineWrapper()
    {
        yield return StartCoroutine(communicator.ValidateNameAndClass(subjectId, config.GetComponent<REDCapN2BackKey>().GetApiKey()));

        var id = communicator.subject_ID;
        if (id != "")
        {
            subjectId = id;
            subjectIdTextField.SetActive(false);
            InsertSubIdText.SetActive(false);
            introtext.SetActive(true);
            VirtualKeyboard.GetComponent<TNVirtualKeyboard>().EnableEnterMode();
            enableIntroText = false;
            dataLayer.GetComponent<Database>().subjectId = subjectId;
            var res = 1;
            if (int.TryParse(subjectId, out res))
            {
                StartCoroutine(FindCurrentInstance());
            }
        }
        else
        {
            InsertSubIdText.GetComponent<TMPro.TMP_Text>().text = "Indtast et validt Uni-Login eller dit <color=red> fornavn, efternavn og klasse</color> \nTryk dernæst på <color=green>Enter</color>";
            subjectIdTextField.GetComponent<ActiveFieldAtStartUp>().activateField();
        }
    }
    private IEnumerator FindCurrentInstance()
    {
        yield return StartCoroutine(communicator.FindNextRepetition(config.GetComponent<REDCapN2BackKey>().GetApiKey()));
    }
}
