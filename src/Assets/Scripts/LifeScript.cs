using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    public GameObject uiLife1;
    public GameObject uiLife2;
    public GameObject uiLife3;

    public bool death;

    private GameObject Player;
    private PlayerScript PlayerLife;

    // Start is called before the first frame update
    void Start()
    {
        uiLife1.SetActive(true);
        uiLife2.SetActive(true);
        uiLife3.SetActive(true);

        death = false;

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerLife = Player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerLife.playerLife >= 3)
        {
            uiLife1.SetActive(true);
            uiLife2.SetActive(true);
            uiLife3.SetActive(true);
        }
        else if (PlayerLife.playerLife == 2)
        {
            uiLife1.SetActive(true);
            uiLife2.SetActive(true);
            uiLife3.SetActive(false);
        }
        else if (PlayerLife.playerLife == 1)
        {
            uiLife1.SetActive(true);
            uiLife2.SetActive(false);
            uiLife3.SetActive(false);
        }
        else if (PlayerLife.playerLife == 0)
        {
            uiLife1.SetActive(false);
            uiLife2.SetActive(false);
            uiLife3.SetActive(false);

            death = true;
        }
    }
}
