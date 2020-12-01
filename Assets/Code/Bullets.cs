using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 OriginPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        OriginPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(OriginPoint, transform.position);
        if (distance > 10)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Score.UpdateScore();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else
            Destroy(gameObject);
    }
}

