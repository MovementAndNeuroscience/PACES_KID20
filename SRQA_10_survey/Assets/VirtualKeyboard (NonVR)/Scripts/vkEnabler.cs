using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class vkEnabler : MonoBehaviour
{
    private TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        //ShowVirtualKeyboard();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputField.isActiveAndEnabled) 
        {
            ShowVirtualKeyboard();
        }


    }
	
	public void ShowVirtualKeyboard(){
		TNVirtualKeyboard.instance.ShowVirtualKeyboard();
		TNVirtualKeyboard.instance.targetText = gameObject.GetComponent<TMP_InputField>();
	}
}
