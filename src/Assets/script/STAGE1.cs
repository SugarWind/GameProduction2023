using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class STAGE1 : MonoBehaviour
{
    public void LoadingNewScene()
    {
        SceneManager.LoadScene("StageScene");
    }
}
