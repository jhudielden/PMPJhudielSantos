using UnityEngine;
using EditorAttributes;

public class PlayerCharacter : MonoBehaviour
{
    private Vector3 mouse_downposition;
    private Vector3 mouse_upposition;
    private Vector2 force;
    public float slingForce;
    //private float force;
    public Vector3 minPower;
    public Vector3 maxPower;
    [SerializeField] private bool stroke;
    [SerializeField] private bool isDragging;
    public LineRenderer lr;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       sr = GetComponent<SpriteRenderer>();
       lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse_downposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_downposition.z = 0;
            if (Vector3.Distance(mouse_downposition, transform.position) < 0.5f)
            {
                isDragging = true;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            mouse_upposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_upposition.z = 0;
            if (isDragging)
            {
                Movement();
            }
            isDragging = false;
        }

        if (isDragging)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                sr.flipX = true;
            }
            else
            { sr.flipX = false; }
        }

        else
        { lr.enabled = false; }
    }
       void Movement ()
    {
        if (stroke == true)
        {
            Vector3 direction = mouse_upposition - mouse_downposition;
            force = new Vector2 (Mathf.Clamp(mouse_upposition.x - mouse_downposition.x, minPower.x, maxPower.x), Mathf.Clamp(mouse_upposition.y - mouse_downposition.y, minPower.y, maxPower.y));
            rb.AddForce(- force * slingForce, ForceMode2D.Impulse);
            //rb.AddForce(- direction * slingForce, ForceMode2D.Impulse);
        }
    }

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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector3.zero;
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = true;
        }
    }
}
