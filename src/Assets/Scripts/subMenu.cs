using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenu : MonoBehaviour
{
    public bool menuDisplay = false;

    public GameObject menuUI;

    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2) && !menuDisplay)
        {
            menuUI.SetActive(true);
            menuDisplay = true;
        }
        else if (Input.GetMouseButtonDown(2) && menuDisplay)
        {
            menuUI.SetActive(false);
            menuDisplay = false;
        }
    }
}
