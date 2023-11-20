using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BGMScript : MonoBehaviour
{


		private AudioSource audioSource;

		private void Start()
		{
			// "AudioSource"�R���|�[�l���g���擾
			audioSource = gameObject.GetComponent<AudioSource>();

		}

		
		/// �X���C�h�o�[�l�̕ύX�C�x���g
		
		public void SoundSliderOnValueChange(float newSliderValue)
		{
			// ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
			audioSource.volume = newSliderValue;
		}	
}
