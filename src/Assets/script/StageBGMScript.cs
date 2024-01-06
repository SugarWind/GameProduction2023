using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class StageBGMScript : MonoBehaviour
{
    private AudioSource audioSource;
    private static bool DontDestroyEnabled = true;
    private float _volume;
    public float VolumeProperty
    {
        get { return _volume; }
        set { _volume = value; }
    }
    private void Start()
	{
        gameObject.name = "NewStageBGM";
        // �ق���"BGM"������ꍇ���̃I�u�W�F�N�g���폜
        GameObject otherBGM = GameObject.Find("BGM");
        GameObject otherStageBGM = GameObject.Find("StageBGM");
        if (otherBGM)
		{
            _volume = otherBGM.GetComponent<BGMScript>().VolumeProperty;
            Destroy(otherBGM.gameObject);
		}
        else if (otherStageBGM)
        {
            _volume = otherStageBGM.GetComponent<StageBGMScript>().VolumeProperty;
            Destroy(otherStageBGM.gameObject);
        }
        // "AudioSource"�R���|�[�l���g���擾
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = _volume;
        gameObject.name = "StageBGM";
        if (DontDestroyEnabled)
        {
            // Scene��J�ڂ��Ă�BGM�������Ȃ�
            DontDestroyOnLoad(this);
        }
    }
}
