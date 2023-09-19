using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerADScript : MonoBehaviour
{
    public GameObject PropellerArea;
    public AreaEffector2D Paf;
    // Start is called before the first frame update
    void Start()
    {
        
         Paf = PropellerArea.GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("検知プロペラ");

            Paf.forceMagnitude += 2f;

            if(Paf.forceMagnitude > 20)
            {
                Paf.forceMagnitude = 20;
            }
            
        }

        if(collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("検知減速プロペラ");

            Paf.forceMagnitude -= 2f;

            if(Paf.forceMagnitude < 1)
            {
                Paf.forceMagnitude = 1f;
            }
        }
    }
}
