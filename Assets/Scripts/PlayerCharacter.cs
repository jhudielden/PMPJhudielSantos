using UnityEngine;
using EditorAttributes;
using CameraShake;

public class PlayerCharacter : MonoBehaviour
{
    private Vector3 mouse_downposition;
    private Vector3 mouse_upposition;
    private Vector2 direction;
    public float slingForce;
    public float speed;
    public float maxPower;
    [SerializeField] private bool stroke;
    [SerializeField] private bool isDragging;
    public LineRenderer lr;
    public LineRenderer TrajLR;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private Vector2 _storedVelocity;
    [SerializeField] private bool _isGrounded;

    [Header("Camera Shake Value")]
    [SerializeField] private Vector2 minMaxShakeReq = new Vector2(2f, 5f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       sr = GetComponent<SpriteRenderer>();
       lr.enabled = false;
       TrajLR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetMousePos();
        }
        if (Input.GetMouseButtonUp(0))
        {
            FiringPlayer();
        }
        if (!isDragging)
        {
            lr.enabled = false;
            TrajLR.enabled = false;
        }
        else
        {
            SpriteManagement();
        }
    }

    public void GetMousePos()
    {
        // getting the initial point of dragging and only is calcultated when mouse is on the player character
        mouse_downposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_downposition.z = 0;
        if (Vector3.Distance(mouse_downposition, transform.position) < 0.5f)
        {
            isDragging = true;
        }
    }

    public void FiringPlayer()
    {
        // getting the last point of dragging to trigger Movement
        mouse_upposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_upposition.z = 0;
        if (isDragging)
        {
            Movement();
        }
        isDragging = false;
        lr.enabled = false;
        TrajLR.enabled = false;
    }

    public void SpriteManagement()
    {
        //  triggers the line renderer and allows the mirror flip of player character 
        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        TrajLR.enabled = true;
        TrajLR.SetPosition(0, transform.position);
        TrajLR.SetPosition(1, Camera.main.ScreenToViewportPoint(- Input.mousePosition));
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            sr.flipX = true;
        }
        else
        { 
            sr.flipX = false; 
        }
    }

    void Movement ()
    {
        if (stroke)
        {
            direction = mouse_upposition - mouse_downposition;
            direction = Vector2.ClampMagnitude(direction, maxPower);
            
            rb.AddForce(- direction * slingForce, ForceMode2D.Impulse);
        }

        if (_isGrounded) _storedVelocity = rb.linearVelocity;
    }

    // using the calculated clamped distance to allow the player character have physics acted on it and goes in the opposite way
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = false;

            _isGrounded = false;
        }
    }

    // makes sure the player character can only make movement if on a platform avoiding double inputs
    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector3.zero;
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = true;

            _isGrounded = true;

            // Landed, do cam shake
            CalculateCamShake();
        }
    } 

    // Calculates the strength of the camera shake upon landing
    private void CalculateCamShake()
    {
        float value = _storedVelocity.magnitude;

        // If between min and max shake requirement: Do small shake!
        if (value < minMaxShakeReq.y && value > minMaxShakeReq.x)
        {
            CameraShaker.Presets.ShortShake2D();
            print("Did Shake Explosion " + value);
        }
        // If greater than the make shake requirement: Do heavy shake!
        else if (value > minMaxShakeReq.y)
        {
            CameraShaker.Presets.Explosion2D();
            print("Did Shake Explosion " + value);
        }

        print("Landed " + value);
    }
}
