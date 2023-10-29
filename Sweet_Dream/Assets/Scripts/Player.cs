using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour
{
     
    public TextMeshProUGUI plantText;
    public GameObject textPanel; 
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected Vector2 direction;

    [SerializeField]
    [Header("基本属性")]
    protected float curHealth;
    public float  maxHealth;
    public float speed;//速度
    public float curSpeed;//当前速度
    public float attack;//攻击力
    public float defence;//防御力

    public float curintellect;//理智值

    public float maxIntellect;//最大理智值

    //
    [SerializeField]
    [Header("状态")]
    public bool isGod;//无敌状态   
    public bool isStun;//眩晕状态
    // Start is called before the first frame update
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
    protected virtual void  Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        SwitchAnimation();
        ReleaseSkill();
    }

    private void Init(){
        curSpeed = speed;
        curHealth = maxHealth;
        curintellect = maxIntellect;
    }

    public void Move()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(curSpeed * direction.x * Time.deltaTime,curSpeed * direction.y *Time.deltaTime,0f);
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

    public void ReleaseSkill(){
        ReleaseSkillFir();
    }

    public void ReleaseSkillFir(){
        if(Input.GetKeyDown(KeyCode.Space)){
            float time = 0;
            DOTween.To(()=>time,X=>time=X,1,5f).OnStart(()=>{
                BuffDistributer.instance.GiveBuffToPlayer(gameObject,BuffType.ChangeSpeed,num:speed*0.2f);
            }).OnComplete(()=>{
                BuffDistributer.instance.GiveBuffToPlayer(gameObject,BuffType.ChangeSpeed,num:-speed*0.2f);
            });
        }
    }
}
