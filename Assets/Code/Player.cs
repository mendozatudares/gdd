using UnityEngine;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace

public class Player : MonoBehaviour
{
    public float JumpForce = 5.0f;
    private Vector3 _jump;
    private bool _isGrounded;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jump = new Vector3(0, JumpForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleControls();
    }

    private void HandleControls()
    {

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
    }

    void OnCollisionStay()
    {
        _isGrounded = true;
    }
}
