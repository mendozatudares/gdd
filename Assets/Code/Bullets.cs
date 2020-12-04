using UnityEngine;

public class Bullets : MonoBehaviour
{
    public AudioClip ExplodeAudioClip;
    private Rigidbody _rb;
    private Vector3 _origin;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
        _rb = GetComponent<Rigidbody>();
        _origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(_origin, transform.position);
        if (distance > 10)
            Destroy(gameObject);
        else
            _rb.MovePosition(transform.TransformPoint(1f, 0, 0));
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Enemy"))
            Score.UpdateScore();
        else
            AudioSource.PlayClipAtPoint(ExplodeAudioClip, transform.position);
        Destroy(gameObject);
    }
}

