using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator animator;

    public float speed = 4f;

    private Rigidbody2D rb;
    private Vector2 input;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;

        SetAnimation();
    }

    private void SetAnimation()
    {
        if(input != null && animator != null)
        {
            if(input.x != 0)
            {
                animator.SetBool("IsIdle", false);
                if (input.x < 0)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
                animator.Play("WalkingSideways");
            }
            else if(input.y != 0)
            {
                animator.SetBool("IsIdle", false);
                if (input.y > 0)
                {
                    animator.Play("WalkingUp");
                }
                else
                {
                    animator.Play("WalkingDown");
                }
            }
            else
            {
                animator.SetBool("IsIdle", true);
            }
        }
    }
}
