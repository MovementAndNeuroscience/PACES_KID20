using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    public GameObject slider;
    public GameObject datalayer;
    private bool enableupdateSlider = true; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.SetActive(true);
        if (enableupdateSlider)
        {
            datalayer.GetComponent<Database>().number_of_activated_pages += 1; 
            slider.GetComponent<Slider>().value = (datalayer.GetComponent<Database>().number_of_activated_pages/datalayer.GetComponent<Database>().number_of_pages);
            enableupdateSlider = false;
        }
        
    }
}
