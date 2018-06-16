using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldPlayerController : MonoBehaviour {

	
	public float speed = .01f;
	public enum PlayerState
    {
        Walking,
        Talking
    };
    public PlayerState playerState;

    Animator animator;

    bool walking;
    


    private void Start()
    {
        playerState = PlayerState.Walking;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
	{
        if (playerState == PlayerState.Walking)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                transform.Translate(Vector3.right * speed * Input.GetAxisRaw("Horizontal"));
            }
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                transform.Translate(Vector3.up * speed * Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetBool("walkingRight", true);
            }
            else
            {
                animator.SetBool("walkingRight", false);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                animator.SetBool("walkingLeft", true);
            }
            else
            {
                animator.SetBool("walkingLeft", false);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetBool("walkingUp", true);
            }
            else
            {
                animator.SetBool("walkingUp", false);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                animator.SetBool("walkingDown", true);
            }
            else
            {
                animator.SetBool("walkingDown", false);
            }
        }
	}
}
