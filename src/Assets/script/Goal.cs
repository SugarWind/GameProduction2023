using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("ゴール");
            SceneManager.LoadScene("StageSlect");
        }
    }
    public void StageClear()
    {
        //PlayerPrefsのSCOREに3という値を入れる
        PlayerPrefs.SetInt("SCORE", 2);
        //PlayerPrefsをセーブする         
        PlayerPrefs.Save();
    }
}
