using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private AudioSource _as;
    private Rigidbody _rb;
    public float Speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _as = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Bullet"))
        {
            // _as.PlayOneShot(EnemyAudioClip, 0.7f);
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        if (GameObject.Find("Player") != null)
        {
            Vector3 movement = GameObject.Find("Player").transform.position - transform.position;
            movement.Normalize();
            Vector3 temp = movement * Speed * 0.1f;
            _rb.MovePosition(_rb.transform.position + temp);
        }
        else
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
