using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movemennt")]
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    private Rigidbody2D rigidbody;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

  
    void FixedUpdate()
    {
        Move();
        checkIsGrounded();
    }

	private void Move()
	{
		if (isGrounded)
		{

        var deltaTime = Time.deltaTime;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");
            // check for filip
            if(x!=0)
            x = FilipAnimation(x);
        Vector2 WorldTouch = new Vector2();
		foreach ( var touch in Input.touches)
		{
            WorldTouch = Camera.main.ScreenToWorldPoint(touch.position);
		}

            float horizontalMoveForce = x * horizontalForce; // * deltaTime;
            float jumpMoveForce = jump * verticalForce;// * deltaTime;
           rigidbody.AddForce(new Vector2( horizontalMoveForce,jumpMoveForce));
            //rigidbody.velocity *= 0.99f;
        }
    }

    private void checkIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);
        isGrounded = (hit) ? true : false;
    }

    private float FilipAnimation(float x)
	{
        x = (x > 0) ? 1 : -1;
        transform.localScale = new Vector3(x, 1.0f);
        return x;
	}
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
	}
}
