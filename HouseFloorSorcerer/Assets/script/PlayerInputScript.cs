using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputScript : MonoBehaviour
{   
    public float movespeed = 0;
    public float collisionOffset = 0;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D  rB;
    List<RaycastHit2D>castCollision = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;
    public
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(movementInput != Vector2.zero)
        {
            bool success =TryMove(movementInput);
            if(!success )
            {
                success = TryMove(new Vector2(movementInput. x,0));
                
            }
            if(!success )
            {
                success = TryMove(new Vector2(0,movementInput.y));
            }
            animator.SetBool("isMoving", success);
            if(movementInput.y >0){
                animator.SetBool("isMovingUp",true);
            }else{
                animator.SetBool("isMovingUp",false);
            }   
            if(movementInput.y < 0){
                animator.SetBool("isMovingDown",true);
            }else{
                animator.SetBool("isMovingDown",false);
            }
            if(movementInput.x !=0){
                animator.SetBool("isMovingLeftRight",true);
            }else{
                animator.SetBool("isMovingLeftRight",false);
            }
        }else{
            animator.SetBool("isMoving",false);
        }
    }
    private bool TryMove(Vector2 direction){
        if(direction != Vector2.zero){
        int count = rB.Cast(
                direction,
                movementFilter,
                castCollision,
                movespeed * Time.fixedDeltaTime + collisionOffset);
            if(movementInput.x < 0)
            {
            spriteRenderer.flipX = true;
            }
            else if(movementInput.x >0)
            {
            spriteRenderer.flipX = false;
            }
            if(count == 0)
            {
            rB.MovePosition(rB.position + direction * movespeed * Time.fixedDeltaTime);
            return true;
            }
            else
            {
                return false;
            }
        }else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
