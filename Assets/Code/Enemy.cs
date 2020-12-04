using Assets.Code;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 0.5f;
    private Rigidbody _rb;
    private int _groan;
    private float _lastGroan;
    public AudioClip DeathAudioClip;
    public AudioClip GroanAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _rb = GetComponent<Rigidbody>();
        _groan = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        if (Time.time - _lastGroan < _groan || Random.value > 0.1) return;
        _lastGroan = Time.time;
        AudioSource.PlayClipAtPoint(GroanAudioClip, transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Bullet"))
        {
            AudioSource.PlayClipAtPoint(DeathAudioClip, transform.position);
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        if (GameObject.Find("Player") != null && !LevelManager.Ctx.paused)
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
