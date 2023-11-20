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
			// "AudioSource"コンポーネントを取得
			audioSource = gameObject.GetComponent<AudioSource>();

		}

		
		/// スライドバー値の変更イベント
		
		public void SoundSliderOnValueChange(float newSliderValue)
		{
			// 音楽の音量をスライドバーの値に変更
			audioSource.volume = newSliderValue;
		}	
}
