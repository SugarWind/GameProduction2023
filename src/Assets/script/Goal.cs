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
            Debug.Log("�S�[��");
            SceneManager.LoadScene("StageSlect");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //PlayerPrefs��SCORE��3�Ƃ����l������
            PlayerPrefs.SetInt("SCORE", 3);
            //PlayerPrefs���Z�[�u����         
            PlayerPrefs.Save();

        }
    }
}
