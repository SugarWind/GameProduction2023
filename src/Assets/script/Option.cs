using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Option : MonoBehaviour
{
    //オプションへ移動
    public void LodingNewSence()
    {
        SceneManager.LoadScene("Option");
    }
}
