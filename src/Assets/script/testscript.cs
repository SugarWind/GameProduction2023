using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour
{
    [SerializeField] private Button[] _stageButton;
    void Start()
    {
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        for (int i = 0; i < _stageButton.Length; i++)
        {
            if (i < stageUnlock)
                _stageButton[i].interactable = true;
            else
                _stageButton[i].interactable = false;
        }
    }
    public void StageSelect(int stage)
    {
        SceneManager.LoadScene(stage);
    }
    //public int stage_num; // �X�R�A�ϐ�
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
    //    //���݂�stage_num���Ăяo��
    //    stage_num = PlayerPrefs.GetInt("SCORE", 0);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //stage_num���Q�ȏ�̂Ƃ��A�X�e�[�W�Q���������B�ȉ����l
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

    //public int Save_num; // �X�R�A�ϐ�
    //public GameObject[] stageSelect = default;

    //void Start()
    //{
    //    //���݂�stage_num���Ăяo��
    //    Save_num = PlayerPrefs.GetInt("SCORE", 0);

    //    for (int loop = 0; loop < stageSelect.Length; loop++)
    //    {
    //        if (loop < Save_num)
    //        {
    //            stageSelect[loop].SetActive(true);
    //        }
    //        else
    //        {
    //            stageSelect[loop].SetActive(false);
    //        }
    //    }
    //}
}

