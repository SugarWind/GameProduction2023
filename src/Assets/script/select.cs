using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    public int Save_num; // スコア変数
    public GameObject[] stageSelect = default;

    void Start()
    {
        //現在のstage_numを呼び出す
        Save_num = PlayerPrefs.GetInt("SCORE", 9);

        for (int loop = 0; loop < stageSelect.Length; loop++)
        {
            if (loop < Save_num)
            {
                stageSelect[loop].SetActive(true);
            }
            else
            {
                stageSelect[loop].SetActive(false);
            }
        }
    }

    //問題ありだったためtestscpirtを採用
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
