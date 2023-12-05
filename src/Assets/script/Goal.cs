using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //�S�[��������ɃX�e�[�W�Z���N�g�ֈړ�


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();

            playerScript.DeleteGoal();
            Debug.Log("�S�[��");
            SceneManager.LoadScene("StageSlect");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PlayerPrefs��SCORE��3�Ƃ����l������
            PlayerPrefs.SetInt("StageUnlock", 3);
            //PlayerPrefs���Z�[�u����         
            PlayerPrefs.Save();
        }
    }
}