using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private BGMScript _bgmScript;
    private bool _isSetUp;      // エラー回避用

    // Start is called before the first frame update
    void Start()
    {
        _bgmScript = GameObject.Find("BGM").GetComponent<BGMScript>();
        if (_bgmScript) _bgmScript.OptionStart();
        _isSetUp = true;
    }

    public void ValueChanged(float value)
    {
        if (_isSetUp)
        {
            _bgmScript.VolumeProperty = value;
            _bgmScript.ChangeVolume();
        }
    }
}
