using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    //メニューへ移動
    public void LoadingNewScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
