using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //ゴールした後にステージセレクトへ移動
    public GameObject resultUI;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();

            playerScript.DeleteGoal();
            Debug.Log("ゴール");
            resultUI.SetActive(true);
            Result();
            //SceneManager.LoadScene("StageSlect");
        }
    }

    public void Result()
    {
        Time.timeScale = 0f;
        //Debug.Log("owari");
        resultUI.SetActive(true);
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        //PlayerPrefsのSCOREに3という値を入れる
    //        PlayerPrefs.SetInt("StageUnlock", 3);
    //        //PlayerPrefsをセーブする         
    //        PlayerPrefs.Save();
    //    }
    //}
}