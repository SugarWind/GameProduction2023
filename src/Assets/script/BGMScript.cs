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
        // ほかに"BGM"がある場合このオブジェクトを削除
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
			// Sceneを遷移してもBGMが消えない
			DontDestroyOnLoad(this);
		}
	}
	// スライドバー値の変更イベント
	public void ChangeVolume()
	{
        // 音楽の音量をスライドバーの値に変更
        if (_audioSource)
        {
            _audioSource.volume = _volume;
        }
    }

    public void OptionStart()
    {
        Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
		// スライドバーの値を音楽の音量に変更
		volumeSlider.value = _volume;
    }
}
