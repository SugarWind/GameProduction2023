using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    //問題あり
    //ステージ制限.
    //[SerializeField] private Button[] _stageButton;
    //void Start()
    //{
    //    int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
    //    for (int i = 0; i < _stageButton.Length; i++)
    //    {
    //        if (i < stageUnlock)
    //            _stageButton[i].interactable = true;
    //        else
    //            _stageButton[i].interactable = false;
    //    }
    //}

    ////ステージセレクト
    //public void StageSelect(int stage)
    //{
    //    SceneManager.LoadScene(stage);
    //}
}
