using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public float deathHeight = -14f;
    public GameObject resultUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deathHeight)
        {
            Debug.Log("owari");
            resultUI.SetActive(true);
           // SceneManager.LoadScene("GameOver");
        }
    }
}
