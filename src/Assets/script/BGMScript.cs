using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BGMScript : MonoBehaviour
{
	private AudioSource audioSource;
	public bool DontDestroyEnabled = true;


	private void Start()
	{
		// "AudioSource"�R���|�[�l���g���擾
		audioSource = gameObject.GetComponent<AudioSource>();

		if (DontDestroyEnabled)
		{
			// Scene��J�ڂ��Ă��I�u�W�F�N�g�������Ȃ�
			DontDestroyOnLoad(this);
		}

	}


	/// �X���C�h�o�[�l�̕ύX�C�x���g

	public void SoundSliderOnValueChange(float newSliderValue)
	{
		// ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
		audioSource.volume = newSliderValue;
	}
}
