using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Movement related fields
    private Rigidbody2D body;
    private UnityEvent onLand;
    private bool grounded;
    private float runSpeed = 50f;
    private float horizontal = 0f;
    private bool jumping = false;
    private const float groundRadius = 0.2f;
    private Vector3 velocity = Vector3.zero;
    public bool right = true;
    private bool freeze = false;
    private bool interact = false;
    private FileHolder i;

    // Player information fields
    private float coinsCollected = 0f;
    private float health = 1f;
    
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform isGround;
    [SerializeField] private LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true; 
        }
        
        if(freeze)
        {
            horizontal = 0;
            jumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && interact)
        {
            GameManager gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            gm.Dialogue(i.getFile());
            interact = false;
        }
        
    }

    private void FixedUpdate()
    {
        bool groundedBefore = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(isGround.position, groundRadius, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!groundedBefore)
                    onLand.Invoke();
            }
        }
        
        Move(horizontal * Time.fixedDeltaTime, jumping);
        jumping = false;
    }

    void Awake()
    {
        if (onLand == null)
        {
            onLand = new UnityEvent(); 
        }
    }

    void Move(float dist, bool jump)
    {
        if (grounded)
        {
            Vector3 vel = new Vector2(dist * 5f, body.velocity.y);
            body.velocity = Vector3.SmoothDamp(body.velocity, vel, ref velocity, 0.1f);
        }

        if (grounded && jump)
        {
            grounded = false;
            body.AddForce(new Vector2(0f, jumpForce));
        }
        
        if (dist > 0 && !right)
        {
            Flip();
        }
        else if (dist < 0 && right)
        {
            Flip();
        }
    }

    public void Flip()
    {
        right = !right;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void FlipRight()
    {
        right = true;
        Vector3 theScale = transform.localScale;
        if (theScale.x > 0)
        {
            theScale.x *= -1;
        }
        transform.localScale = theScale;
    }

    public void FlipLeft()
    {
        right = false;
        Vector3 theScale = transform.localScale;
        if (theScale.x < 0)
        {
            theScale.x *= -1;
        }
        transform.localScale = theScale;
    }
    
    public void addCoin()
    {
        coinsCollected++;
        health += 1f;
    }

    public float getHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dialogue"))
        {
            GameManager gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            gm.Dialogue(other.gameObject.GetComponent<FileHolder>().getFile());
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Interactable"))
        {
            i = other.gameObject.GetComponent<FileHolder>();
            interact = true;
            Destroy(other.gameObject.GetComponent<FileHolder>());
        }

        if (other.gameObject.CompareTag("Demo"))
        {
            GameManager gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
            gm.DemoFinished();
        }
    }

    public void Freeze()
    {
        freeze = true;
    }

    public void Unfreeze()
    {
        freeze = false;
    }
}
