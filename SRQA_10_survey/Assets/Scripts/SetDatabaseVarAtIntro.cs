using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDatabaseVarAtIntro : MonoBehaviour
{
    public GameObject datalayer;
    public GameObject redcapCommunicator;
    public GameObject config;
    public string instrumentName;
    public string eventName;
    public string timeFieldName;
    private bool enableSendTimeStamp = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (datalayer.GetComponent<Database>().enable_REDCap)
        { 
            if (datalayer != null && instrumentName != "")
                datalayer.GetComponent<Database>().instrumentName = instrumentName;
            if (datalayer != null && eventName != "")
                datalayer.GetComponent<Database>().event_name = eventName;
            if (datalayer != null && timeFieldName != "")
                datalayer.GetComponent<Database>().redcap_time_varname = timeFieldName;

            if (enableSendTimeStamp && redcapCommunicator != null && config != null)
            {
                var id = datalayer.GetComponent<Database>().subjectId;
                var apikey = config.GetComponent<REDCapN2BackKey>().GetApiKey();

                StartCoroutine(postTimeStamp(id, apikey));

                enableSendTimeStamp = false;
            }
        }
    }
    public IEnumerator postTimeStamp(string id, string apiKey)
    {
        yield return new WaitForSeconds(0.5f);
        yield return redcapCommunicator.GetComponent<REDCapCommunicator>().PostDataRequest(int.Parse(id), $"{DateTime.Now:yyyyMMdd_HHmmssfff}", timeFieldName, eventName, instrumentName, apiKey);
    }
}

