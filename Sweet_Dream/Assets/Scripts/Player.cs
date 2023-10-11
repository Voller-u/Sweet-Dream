using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; 

    protected Animator animator;

    protected Vector3 direction;


    // Start is called before the first frame update
    protected void Awake()
    {
    }
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int position = new Vector3Int((int)transform.position.x,
                (int)transform.position.y, 0);

            if (GameManager.instance.tileManager.IsInteractable(position))
            {
                GameManager.instance.tileManager.SetInteracted(position);
            }
        }

        Move();
    }

    protected void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Move()
    {
        float v_x = Input.GetAxisRaw("Horizontal");
        float v_y = Input.GetAxisRaw("Vertical");
        direction = new Vector3(v_x, v_y);
        animation_change(direction);
    }

    protected void animation_change(Vector3 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
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
