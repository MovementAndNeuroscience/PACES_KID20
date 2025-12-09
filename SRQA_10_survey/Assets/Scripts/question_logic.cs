using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class question_logic : MonoBehaviour
{
    public string redcap_varname;
    public bool revers_order; 

    private string answer_name; 

    private ToggleGroupPublicToggles toggleGr;

    // Start is called before the first frame update
    void Start()
    {
        toggleGr = GetComponent<ToggleGroupPublicToggles>(); 
    }

    // Update is called once per frame
    void Update()
    {
        var toggles = toggleGr.GetToggles(); 
        foreach (Toggle t in toggles)
        {   
            if (t.isOn == true)
            {
                t.image.color = t.colors.selectedColor;
                answer_name = t.name;
            }
            else
            {
                t.image.color = Color.white;
            }

        }
    }
    public string GetREDCapVarName()
    {
        return redcap_varname; 
    }

    public int getAnswerValue()
    {
        if(revers_order)
        {
            switch (answer_name)
            {
                case "A1":
                    return 5;
                case "A2":
                    return 4;
                case "A3":
                    return 3;
                case "A4":
                    return 2;
                case "A5":
                    return 1;
                default:
                    return 0;
            }
        }
        else
        {
            switch (answer_name)
            {
                case "A1":
                    return 1;
                case "A2":
                    return 2;
                case "A3":
                    return 3;
                case "A4":
                    return 4;
                case "A5":
                    return 5;
                default:
                    return 0;
            }
        }
    }

}
