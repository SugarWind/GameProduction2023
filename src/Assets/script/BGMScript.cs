using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BGMScript : MonoBehaviour
{
	private AudioSource _audioSource;
	private static bool DontDestroyEnabled = true;
    private float _volume;
    public float VolumeProperty
    {
        get { return _volume; }
        set { _volume = value; }
    }

    private void Start()
	{
        GameObject otherBGM = GameObject.Find("BGM");
        GameObject StageBGM = GameObject.Find("StageBGM");
        // �ق���"BGM"������ꍇ���̃I�u�W�F�N�g���폜
        if (otherBGM && otherBGM != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else if (StageBGM)
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _volume = StageBGM.GetComponent<StageBGMScript>().VolumeProperty;
            Destroy(StageBGM.gameObject);
        }
        else
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _volume = _audioSource.volume;
        }
        ChangeVolume();
        if (DontDestroyEnabled)
		{
			// Scene��J�ڂ��Ă�BGM�������Ȃ�
			DontDestroyOnLoad(this);
		}
	}
	// �X���C�h�o�[�l�̕ύX�C�x���g
	public void ChangeVolume()
	{
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        if (_audioSource)
        {
            _audioSource.volume = _volume;
        }
    }

    public void OptionStart()
    {
        Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
		// �X���C�h�o�[�̒l�����y�̉��ʂɕύX
		volumeSlider.value = _volume;
    }
}
