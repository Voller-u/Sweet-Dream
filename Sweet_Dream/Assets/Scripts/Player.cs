using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float speed; 
    public TextMeshProUGUI plantText;
    public GameObject textPanel; 
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected Vector2 direction;

    [SerializeField]
    protected float curHealth;
    public float  maxHealth;

    //
    [SerializeField]
    [Header("Status")]
    public bool isGod;//无敌状态   
    public bool isStun;//眩晕状态
    // Start is called before the first frame update
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
    protected void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    protected void Update()
    {
        Move();
        SwitchAnimation();
    }



    public void Move()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(speed * direction.x * Time.deltaTime,speed * direction.y *Time.deltaTime,0f);
    }

    protected void SwitchAnimation()
    {
        if (animator != null)
        {
            //使得停止移动时，以上一次移动方向作为站立方向
            if(direction != Vector2.zero){
                animator.SetFloat("horizontal",direction.x);
                animator.SetFloat("vertical",direction.y);
            }
            animator.SetFloat("speed",direction.magnitude);
        }
    }

    public void GotAttacked(float damage){

    }

}
