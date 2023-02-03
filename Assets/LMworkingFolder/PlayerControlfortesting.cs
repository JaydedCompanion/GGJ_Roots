using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlfortesting : MonoBehaviour
{
    public float Speed = 3.0f;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Vector2 lastEatenBoneMeal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rb2d.velocity = moveInput* Speed;
    }

    public void EatBoneMeal(Vector2 location)
    {
        lastEatenBoneMeal = location;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lastEatenBoneMeal, 2);
        
    }
}
