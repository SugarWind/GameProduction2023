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
        if (!_moveObject)
        {
            Destroy(this.gameObject);
        }
        _objectAutoMove = _moveObject.GetComponent<ObjectAutoMove>();
    }

    // Update is called once per frame
    void Update()
    {
        _objectAutoMove.SetCanMove(false, false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _objectAutoMove.SetCanMove(true, true);
            Destroy(this.gameObject);
        }
    }
}
