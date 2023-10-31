using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMove : MonoBehaviour
{
    [SerializeField] private GameObject _moveObject;
    private ObjectAutoMove _objectAutoMove;
    // Start is called before the first frame update
    void Start()
    {
        _objectAutoMove = _moveObject.GetComponent<ObjectAutoMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _objectAutoMove.SetCanMove(true, true);
        }
    }
}
