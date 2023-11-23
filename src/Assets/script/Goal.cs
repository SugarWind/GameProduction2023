using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //ゴールした後にステージセレクトへ移動
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          PlayerScript playerScript =  collision.gameObject.GetComponent<PlayerScript>();

            playerScript.DeleteGoal();
            Debug.Log("ゴール");
            SceneManager.LoadScene("StageSlect");
        }
    }
    //public void StageClear()
    //{
    //    PlayerPrefsのSCOREに3という値を入れる
    //    PlayerPrefs.SetInt("SCORE", 2);
    //    PlayerPrefsをセーブする
    //    PlayerPrefs.Save();
    //}
}
