using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousPage : MonoBehaviour
{
    public GameObject previousPage;
    public GameObject currentPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToPage()
    {
        if (previousPage != null)
        {
            previousPage.SetActive(true);
            currentPage.SetActive(false);
        }
    }

}
