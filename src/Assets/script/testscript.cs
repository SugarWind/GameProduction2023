using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    public int Save_num; // �X�R�A�ϐ�
    public GameObject[] stageSelect = default;

    void Start()
    {
        //���݂�stage_num���Ăяo��
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
}
