using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActivateExperiment : MonoBehaviour
{

    public GameObject stimuliController;
    public GameObject VirtualKeyboard;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        VirtualKeyboard.GetComponent<TNVirtualKeyboard>().EnableEnterMode();
        if (Input.GetKeyDown(KeyCode.Return) || VirtualKeyboard.GetComponent<TNVirtualKeyboard>().GetEnter())
        {
            StartCoroutine(DelayBeforePresentation(1.0f));
        }
        
    }
    IEnumerator DelayBeforePresentation(float delay)
    {
        VirtualKeyboard.GetComponent<TNVirtualKeyboard>().EnterReleased();
        yield return new WaitForSeconds(delay);
        if (stimuliController == null)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.OpenURL("https://erda.dk/vgrid/mopac_surveys/Paces_Kid20/SurveyEnd.html");
            Application.Unload();
#else
            Application.Quit();
#endif
        }
        else
        {
            stimuliController.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }


}
