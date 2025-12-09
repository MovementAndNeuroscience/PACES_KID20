using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableKeyboardPanel : MonoBehaviour
{
    public GameObject virtualKeyboard;
    // Start is called before the first frame update
    void Start()
    {
        virtualKeyboard.GetComponent<TNVirtualKeyboard>().HideVirtualKeyboardPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
