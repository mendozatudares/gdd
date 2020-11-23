using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace

public class Player : MonoBehaviour
{
    public Object BulletPrefab;
    public float JumpForce = 5.0f;
    public float Speed = 1.0f;
    public float FireTime = 0.5f;
    public AudioClip FireAudioClip;
    public AudioClip DeathAudioClip;
    private Vector3 _jump;
    private bool _isGrounded;
    private float _lastFire;
    private Rigidbody _rb;
    private AudioSource _as;
    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jump = new Vector3(0, JumpForce, 0);
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();
    }

    private void HandleControls()
    {
        // Move player using WASD (currently multiplying by 0.1f to limit change per frame)
        var w = Input.GetKey(KeyCode.W) ? 1f : 0f;
        var a = Input.GetKey(KeyCode.A) ? 1f : 0f;
        var s = Input.GetKey(KeyCode.S) ? 1f : 0f;
        var d = Input.GetKey(KeyCode.D) ? 1f : 0f;
        var vertical = w - s;
        var horizontal = d - a;
        Vector3 temp = new Vector3(horizontal, 0, vertical).normalized * Speed * 0.1f;
        _rb.MovePosition(_rb.transform.position + temp);

        // Jump if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(_jump, ForceMode.Impulse);
            _isGrounded = false;
        }

        // Aim using mouse position relative to player position
        var mousePos = Input.mousePosition;
        var playerPos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = mousePos - playerPos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        // Fire if the left mouse button is clicked and enough time has passed
        if (!Input.GetKeyDown(KeyCode.Mouse0) || (Time.time - _lastFire) < FireTime) return;
        _lastFire = Time.time;
        Fire();
    }

    private void Fire()
    {
        Debug.Log("Fire!");
        // _as.PlayOneShot(FireAudioClip, 0.7f);
        // Make a new bullet in front of player
        // Instantiate(BulletPrefab,
        //     transform.TransformPoint(new Vector3(1f, 0, 0)),
        //     transform.rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Enemy"))
        {
            // _as.PlayOneShot(DeathAudioClip, 0.7f);
            Destroy(gameObject);
        }
    }

    void OnCollisionStay()
    {
        _isGrounded = true;
    }
}
