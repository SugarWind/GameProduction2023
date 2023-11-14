using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    //public int stage_num; // スコア変数
    //public GameObject ni;
    //public GameObject san;
    //public GameObject yon;
    //public GameObject go;
    //public GameObject roku;
    //public GameObject nana;
    //public GameObject hati;
    //public GameObject kyuu;
    //public GameObject zyuu;

    //// Use this for initialization
    //void Start()
    //{
    //    //現在のstage_numを呼び出す
    //    stage_num = PlayerPrefs.GetInt("SCORE", 0);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //stage_numが２以上のとき、ステージ２を解放する。以下同様
    //    if (stage_num >= 2)
    //    {
    //        ni.SetActive(true);
    //    }

    //    if (stage_num >= 3)
    //    {
    //        san.SetActive(true);
    //    }

    //    if (stage_num >= 4)
    //    {
    //        yon.SetActive(true);
    //    }

    //    if (stage_num >= 5)
    //    {
    //        go.SetActive(true);
    //    }

    //    if (stage_num >= 6)
    //    {
    //        go.SetActive(true);
    //    }

    //    if (stage_num >= 7)
    //    {
    //        go.SetActive(true);
    //    }

    //    if (stage_num >= 8)
    //    {
    //        go.SetActive(true);
    //    }

    //    if (stage_num >= 9)
    //    {
    //        go.SetActive(true);
    //    }

    //    if (stage_num >= 10)
    //    {
    //        go.SetActive(true);
    //    }

    //}

    public int Save_num; // スコア変数
    public GameObject[] stageSelect = default;

    void Start()
    {
        //現在のstage_numを呼び出す
        Save_num = PlayerPrefs.GetInt("SCORE", 0);

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
    public void StageClear()

    {
        { 
            //PlayerPrefsのSCOREに3という値を入れる
            PlayerPrefs.SetInt("SCORE", 2);
            //PlayerPrefsをセーブする         
            PlayerPrefs.Save();
        }
    }
}

