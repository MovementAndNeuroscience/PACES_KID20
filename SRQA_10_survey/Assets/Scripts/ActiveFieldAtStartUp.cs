
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveFieldAtStartUp : MonoBehaviour
{
    private TMP_InputField inputField;
    public bool enableVirtualKeyboard = false; 
    public GameObject VirtualKeyboard;
    // Start is called before the first frame update
    void Start()
    {
        if(enableVirtualKeyboard == false)
        {
            inputField = GetComponent<TMP_InputField>();
            inputField.ActivateInputField();
        }
        
        //VirtualKeyboard.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0))
        {
            if (enableVirtualKeyboard == false)
            {
                inputField.ActivateInputField();
            }
            
            //VirtualKeyboard.SetActive(true);
        }
    }
    public void activateField()
    {
        inputField.ActivateInputField();
    }
}
