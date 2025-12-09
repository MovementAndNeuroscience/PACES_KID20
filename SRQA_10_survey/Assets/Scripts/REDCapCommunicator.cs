using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text.Json.Nodes;
using UnityEngine.Networking;

[System.Serializable]
public class logininfo
{
    public int record_id;
    public string redcap_event_name;
    public string stud_unlogin;
    public string stud_name;
    public string stud_class;
}
[System.Serializable]
public class infos
{
    public List<logininfo> logininfos;
}
[System.Serializable]
public class Instance
{
    public int record_id;
    public string redcap_event_name;
    public string recap_repeated_instrument;
    public int redcap_repeat_instance;
    public string paces_time;
}
[System.Serializable]
public class Instances
{
    public List<Instance> instances;
}

public class REDCapCommunicator : MonoBehaviour
{
    public GameObject dataGameObject;
    public REDCapSubjectData redcapData;
    public RestResponse config;
    public string restResponse;
    public string idRestRequest;
    public string subject_ID;
    public int no_instance = 0;

    public void Start()
    {
        redcapData = dataGameObject.GetComponent<REDCapSubjectData>();
    }

    public IEnumerator GetConfig(string apiToken)
    {

        string url = "https://redcap.nexs.ku.dk/api/";

        // Prepare form data
        WWWForm form = new WWWForm();
        form.AddField("token", apiToken);
        form.AddField("content", "report");
        form.AddField("format", "json");
        form.AddField("report_id", "787");
        form.AddField("csvDelimiter", "");
        form.AddField("rawOrLabel", "raw");
        form.AddField("rawOrLabelHeaders", "raw");
        form.AddField("exportCheckboxLabel", "false");
        form.AddField("returnFormat", "json");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.disposeDownloadHandlerOnDispose = true;
        request.disposeCertificateHandlerOnDispose = true;
        request.disposeUploadHandlerOnDispose = true;
        yield return request.SendWebRequest();
        restResponse = request.downloadHandler.text;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("REDCap Response: " + request.responseCode);
        }
        else
        {
            Debug.LogError("REDCap API Error: " + request.error);
        }
        request.Dispose();
        if (request.uploadHandler != null)
        {
            request.uploadHandler.Dispose();
        }
        if (request.downloadHandler != null)
        {
            request.downloadHandler.Dispose();
        }
    }
    public void SendEntryTimeToREDCap(int id, string event_name, string apikey, string instrumentName, string redcap_time_fieldName)
    {
        StartCoroutine(PostDataRequest(id, $"{DateTime.Now:yyyyMMdd_HHmmssfff}", redcap_time_fieldName, event_name, instrumentName, apikey));
    }
    public void SendQuestionDataToREDCap(int id, string event_name, string apikey, int answer, string redcap_varname, string instrumentName)
    {
        StartCoroutine(PostDataRequest(id, answer.ToString(), redcap_varname, event_name,instrumentName, apikey));
    }

    public IEnumerator PostDataRequest(int record, string data, string field_name, string event_name,string instrumentName ,string apiToken)
    {
        string url = "https://redcap.nexs.ku.dk/api/";

        // Prepare form data
        var byteArr = new byte[1]; 

            WWWForm form = new WWWForm();
            form.AddField("token", apiToken);
            form.AddField("content", "version");
            form.AddField("content", "record");
            form.AddField("action", "import");
            form.AddField("format", "xml");
            form.AddField("type", "eav");
            form.AddField("overwriteBehavior", "overwrite");
            form.AddField("forceAutoNumber", "false");
            form.AddField("data", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><records> <item><record>" + record + "</record><redcap_event_name>" + event_name + "</redcap_event_name><redcap_repeat_instrument>" + instrumentName + "</redcap_repeat_instrument><redcap_repeat_instance>" + no_instance + "</redcap_repeat_instance><field_name>" + field_name + "</field_name><value>" + data + "</value></item> </records>");
            form.AddField("returnContent", "count");
            form.AddField("returnFormat", "json");

        using (UnityWebRequest redcapRequest = UnityWebRequest.Post(url, form))
        {
            redcapRequest.disposeDownloadHandlerOnDispose = true;
            redcapRequest.disposeCertificateHandlerOnDispose = true;
            redcapRequest.disposeUploadHandlerOnDispose = true;

            yield return redcapRequest.SendWebRequest();
            if (redcapRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("REDCap Response: " + redcapRequest.downloadHandler.text);
            }
            else
            {

                Debug.LogError("REDCap API Error: " + redcapRequest.error + "<records> <item><record>" + record + "</record><redcap_event_name>" + event_name + "</redcap_event_name><redcap_repeat_instrument>" + instrumentName + "</redcap_repeat_instrument><redcap_repeat_instance>" + no_instance + "</redcap_repeat_instance><field_name>" + field_name + "</field_name><value>" + data + "</value></item> </records>");
            }
            redcapRequest.uploadHandler.Dispose();
            redcapRequest.downloadHandler.Dispose();
            redcapRequest.Dispose();
        }

    }

    public IEnumerator GetRecordIdFromUniLogin(string id, string apiToken)
    {
        string url = "https://redcap.nexs.ku.dk/api/";

        // Prepare form data
        WWWForm form = new WWWForm();
        form.AddField("token", apiToken);
        form.AddField("content", "report");
        form.AddField("format", "json");
        form.AddField("report_id", "790");
        form.AddField("csvDelimiter", "");
        form.AddField("rawOrLabel", "raw");
        form.AddField("rawOrLabelHeaders", "raw");
        form.AddField("exportCheckboxLabel", "false");
        form.AddField("returnFormat", "json");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.disposeDownloadHandlerOnDispose = true;
        request.disposeCertificateHandlerOnDispose = true;
        request.disposeUploadHandlerOnDispose = true;
        yield return request.SendWebRequest();
        idRestRequest = request.downloadHandler.text;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("REDCap Response: " + request.responseCode);
        }
        else
        {
            Debug.LogError("REDCap API Error: " + request.error);
            subject_ID = null;
        }
        request.Dispose();
        if (request.uploadHandler != null)
        {
            request.uploadHandler.Dispose();
        }
        if (request.downloadHandler != null)
        {
            request.downloadHandler.Dispose();
        }


        if (idRestRequest != null)
        {
            var obj = JsonUtility.FromJson<infos>("{\"logininfos\":" + idRestRequest + "}");
            foreach (var unilogin in obj.logininfos)
            {
                if (id == unilogin.stud_unlogin)
                {
                    subject_ID = unilogin.record_id.ToString();
                }
            }
        }
        else
            subject_ID = null; 
    }
    public IEnumerator ValidateNameAndClass(string id, string apiToken)
    {
        string url = "https://redcap.nexs.ku.dk/api/";

        // Prepare form data
        WWWForm form = new WWWForm();
        form.AddField("token", apiToken);
        form.AddField("content", "report");
        form.AddField("format", "json");
        form.AddField("report_id", "790");
        form.AddField("csvDelimiter", "");
        form.AddField("rawOrLabel", "raw");
        form.AddField("rawOrLabelHeaders", "raw");
        form.AddField("exportCheckboxLabel", "false");
        form.AddField("returnFormat", "json");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.disposeDownloadHandlerOnDispose = true;
        request.disposeCertificateHandlerOnDispose = true;
        request.disposeUploadHandlerOnDispose = true;
        yield return request.SendWebRequest();
        idRestRequest = request.downloadHandler.text;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("REDCap Response: " + request.responseCode);
        }
        else
        {
            Debug.LogError("REDCap API Error: " + request.error);
            subject_ID = null;
        }
        request.Dispose();
        if (request.uploadHandler != null)
        {
            request.uploadHandler.Dispose();
        }
        if (request.downloadHandler != null)
        {
            request.downloadHandler.Dispose();
        }


        if (idRestRequest != null)
        {
            var lastname1 = "";
            var lastname2 = "";
            var lastname3 = "";
            var firstname = "";
            var klasse = "";
            var obj = JsonUtility.FromJson<infos>("{\"logininfos\":" + idRestRequest + "}");
            var splitId = id.Split(" ");
            firstname = splitId[0];
            klasse = splitId[splitId.Length-1];
            if (klasse.Length == 1)
            {
                klasse = splitId[splitId.Length - 2] + splitId[splitId.Length - 1];
                if(splitId.Length == 4)
                    lastname1 = splitId[1];

                if (splitId.Length == 5)
                {
                    lastname1 = splitId[1];
                    lastname2 = splitId[2];
                }
                if (splitId.Length >= 6)
                {
                    lastname1 = splitId[1];
                    lastname2 = splitId[2];
                    lastname2 = splitId[3];
                }
            }
            else
            {
                if (splitId.Length == 3)
                    lastname1 = splitId[1];

                if (splitId.Length == 4)
                {
                    lastname1 = splitId[1];
                    lastname2 = splitId[2];
                }
                if (splitId.Length >= 5)
                {
                    lastname1 = splitId[1];
                    lastname2 = splitId[2];
                    lastname3 = splitId[3];
                }
            }
            string fractionOf1Lastname = lastname1.Substring(0, 3);

            foreach (var unilogin in obj.logininfos)
            {
                if (unilogin.stud_name.Contains(firstname, StringComparison.OrdinalIgnoreCase) )
                {
                    if (unilogin.stud_class.Contains(klasse.Substring(0, 1), StringComparison.OrdinalIgnoreCase) && unilogin.stud_class.Contains(klasse.Substring(klasse.Length - 1, 1), StringComparison.OrdinalIgnoreCase))
                    {
                        if (unilogin.stud_name.Contains(fractionOf1Lastname, StringComparison.OrdinalIgnoreCase))
                            subject_ID = unilogin.record_id.ToString();
                    }
                }
            }
        }
        else
            subject_ID = null;

    }
    public IEnumerator FindNextRepetition(string apiToken)
    {
        string url = "https://redcap.nexs.ku.dk/api/";

        // Prepare form data
        WWWForm form = new WWWForm();
        form.AddField("token", apiToken);
        form.AddField("content", "report");
        form.AddField("format", "json");
        form.AddField("report_id", "915");
        form.AddField("csvDelimiter", "");
        form.AddField("rawOrLabel", "raw");
        form.AddField("rawOrLabelHeaders", "raw");
        form.AddField("exportCheckboxLabel", "false");
        form.AddField("returnFormat", "json");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.disposeDownloadHandlerOnDispose = true;
        request.disposeCertificateHandlerOnDispose = true;
        request.disposeUploadHandlerOnDispose = true;
        yield return request.SendWebRequest();
        idRestRequest = request.downloadHandler.text;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("REDCap Response: " + request.responseCode);
        }
        else
        {
            Debug.LogError("REDCap API Error: " + request.error);
            subject_ID = null;
        }
        request.Dispose();
        if (request.uploadHandler != null)
        {
            request.uploadHandler.Dispose();
        }
        if (request.downloadHandler != null)
        {
            request.downloadHandler.Dispose();
        }


        if (idRestRequest != null)
        {

            var obj = JsonUtility.FromJson<Instances>("{\"instances\":" + idRestRequest + "}");

            foreach (var instance in obj.instances)
            {
                if (instance.record_id.ToString() == subject_ID)
                {
                    if (instance.redcap_repeat_instance >= no_instance)
                    {
                        no_instance = instance.redcap_repeat_instance + 1;
                    }
                }
            }
            if (no_instance == 0)
                no_instance = 1;
        }
    }

}
