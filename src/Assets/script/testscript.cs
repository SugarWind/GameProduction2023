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
        //�X�e�[�W����
        int stageUnlock = PlayerPrefs.GetInt("StageUnlock", 1);
        for (int i = 0; i < _stageButton.Length; i++)
        {
            if (i < stageUnlock)
                _stageButton[i].interactable = true;
            else
                _stageButton[i].interactable = false;
        }
    }
    //�X�e�[�W�Z���N�g
    public void StageSelect(int stage)
    {
        SceneManager.LoadScene(stage);
    }
    
}
