using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rutrun : MonoBehaviour
{
    public void LodingNewSence()
    {
        SceneManager.LoadScene("Title");
        PlayerPrefs.DeleteAll();
    }
}
