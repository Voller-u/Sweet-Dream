using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //角色移动的速度
    
    protected Animator animator;
    
    protected Vector3 direction;

    public Inventory inventory;
    // Start is called before the first frame update
    protected void Awake()
    {
        inventory = new Inventory(28);
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
                //TODO 响应
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
    public void DropItem(Collectable item)
    {
        Vector2 spwan_location = transform.position;

        Vector2 spawn_offset = Random.insideUnitCircle * 2f;

        //修正位置，避免扔不出去
        if (spawn_offset.x < 0){spawn_offset.x -= 0.5f;}
        else {spawn_offset.x += 0.5f;}
        if (spawn_offset.y < 0) { spawn_offset.y -= 0.5f; }
        else { spawn_offset.y += 0.5f; }

        Collectable item_to_drop =
            Instantiate(item, spwan_location + spawn_offset, Quaternion.identity);

        item_to_drop.rb.AddForce(spawn_offset * 2f, ForceMode2D.Impulse);
    }

}
