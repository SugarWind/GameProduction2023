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
		// "AudioSource"コンポーネントを取得
		audioSource = gameObject.GetComponent<AudioSource>();

		if (DontDestroyEnabled)
		{
			// Sceneを遷移してもオブジェクトが消えない
			DontDestroyOnLoad(this);
		}

	}


	/// スライドバー値の変更イベント

	public void SoundSliderOnValueChange(float newSliderValue)
	{
		// 音楽の音量をスライドバーの値に変更
		audioSource.volume = newSliderValue;
	}
}
