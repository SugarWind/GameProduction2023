using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerADScript : MonoBehaviour
{
    public GameObject PropellerArea;
    public AreaEffector2D Paf;
    [SerializeField] private float PropellerMax = 23;
    [SerializeField] private float PropellerMin = 1;

    private float defaultPower;

    Animator propellerAnimator_normal;

    public AnimationClip propeller_default;
    public AnimationClip propeller_a;
    public AnimationClip propeller_d;

    // Start is called before the first frame update
    void Start()
    {

        propellerAnimator_normal = GetComponent<Animator>();
        Paf = PropellerArea.GetComponent<AreaEffector2D>();
        defaultPower = Paf.forceMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (Paf.forceMagnitude == defaultPower)
        {
            propellerAnimator_normal.Play(propeller_default.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acammo")
        {
            Debug.Log("検知プロペラ");

            //propellerAnimator_normal.Play(propeller_a.name);
            Paf.forceMagnitude += 2f;

            if (Paf.forceMagnitude > defaultPower)
            {
                propellerAnimator_normal.Play(propeller_a.name);
            }

            /*if(Paf.forceMagnitude > defaultPower)
            {
                propellerAnimator_normal.Play(propeller_a.name);
            }*/

            if (Paf.forceMagnitude > PropellerMax)
            {
                Paf.forceMagnitude = PropellerMax;
            }

        }

        if (collision.gameObject.tag == "Dcammo")
        {
            Debug.Log("検知減速プロペラ");

            //propellerAnimator_normal.Play(propeller_d.name);
            Paf.forceMagnitude -= 2f;


            if (Paf.forceMagnitude < defaultPower)
            {
                propellerAnimator_normal.Play(propeller_d.name);
            }

            /*if(Paf.forceMagnitude < defaultPower)
            {
                propellerAnimator_normal.Play(propeller_d.name);
            }*/

            if (Paf.forceMagnitude < PropellerMin)
            {
                Paf.forceMagnitude = PropellerMin;
            }
        }
    }
}
