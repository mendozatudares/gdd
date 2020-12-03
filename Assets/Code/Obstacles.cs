using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    private AudioSource _as;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (collision.transform.name.Contains("Bullet"))
        // {
        //     // _as.PlayOneShot(ExplosionAudioClip, 0.7f);
        //     Destroy(gameObject);
        // }
    }
}
