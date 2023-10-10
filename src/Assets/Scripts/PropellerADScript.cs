using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerADScript : MonoBehaviour
{
    public GameObject PropellerArea;
    public AreaEffector2D Paf;
    [SerializeField] private float PropellerMax =20;
    [SerializeField] private float PropellerMin =1;
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

            if(Paf.forceMagnitude > PropellerMax)
            {
                Paf.forceMagnitude = PropellerMax;
            }
            
        }

        if(collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("検知減速プロペラ");

            Paf.forceMagnitude -= 2f;

            if(Paf.forceMagnitude < PropellerMin)
            {
                Paf.forceMagnitude = PropellerMin;
            }
        }
    }
}
