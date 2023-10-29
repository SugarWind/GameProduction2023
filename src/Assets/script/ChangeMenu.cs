using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    public void LoadingNewScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
