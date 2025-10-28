using UnityEngine;
using EditorAttributes;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 mouse_downposition;
    private Vector3 mouse_upposition;
    public float slingForce;
    private bool stroke;
    private float distance;
    [SerializeField] private bool isDragging;
    public LineRenderer lr;
    private SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
