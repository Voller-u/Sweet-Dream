using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed; //角色移动的速度
    
    protected Animator animator;
    
    protected Vector3 direction;

    public Inventory inventory;
    // Start is called before the first frame update
    protected void Awake()
    {
        inventory = new Inventory(25);
    }
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        float v_x = Input.GetAxisRaw("Horizontal");
        float v_y = Input.GetAxisRaw("Vertical");
        direction = new Vector3(v_x, v_y);
        animation_change(direction);
        
    }

    protected void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected void animation_change(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
