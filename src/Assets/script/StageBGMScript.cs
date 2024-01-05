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
        // ほかに"BGM"がある場合このオブジェクトを削除
        GameObject otherBGM = GameObject.Find("BGM");
        GameObject otherStageBGM = GameObject.Find("StageBGM");
        if (otherBGM)
		{
            _volume = otherBGM.GetComponent<BGMScript>().VolumeProperty;
            Destroy(otherBGM.gameObject);
		}
        else if (otherStageBGM && otherStageBGM != otherStageBGM.gameObject)
        {
            _volume = otherStageBGM.GetComponent<StageBGMScript>().VolumeProperty;
            Destroy(otherStageBGM.gameObject);
        }
        // "AudioSource"コンポーネントを取得
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = _volume;
        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもBGMが消えない
            DontDestroyOnLoad(this);
        }
    }
}
