using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    [Header("基本属性")]
    public float curHealth;
    public float  maxHealth;
    public float speed;//速度
    public float curSpeed;//当前速度
    public float attackInit;//攻击力
    public float curAttack;//当前攻击力
    public float defenceInit;//防御力
    public float curDefence;//当前防御力
    public float attackRadius;//攻击范围

    [Header("技能")]
    public List<Skill> skills = new List<Skill>();

    public GameObject player;
    private Rigidbody2D rb;
    private Vector3 offsetFromPlayer;

    private bool isRealsing;//正在释放技能
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime,transform.position.y,transform.position.z);
        player = GameObject.FindObjectOfType<Player>().gameObject;
        Init();
        InitSkill();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        offsetFromPlayer = (player.transform.position - transform.position);
        // transform.position = new Vector3(transform.position.x + speed * offset.x * Time.deltaTime,
        // transform.position.y + speed * offset.y * Time.deltaTime,0);
        if(curHealth <= 0){
            Destroy(gameObject);
        }
        if(!isRealsing){
            ProbableAction(0.3f,TestSkill,offsetFromPlayer.magnitude < attackRadius);
            Debug.Log(this.name + "技能释放了" + "技能冷却状态：" + isRealsing);
        } 
    }

    protected void Init(){
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        curHealth = maxHealth;
        curAttack = attackInit;
        curDefence = defenceInit;
    }

    protected void InitSkill(){
        Skill skill0 = new Skill("冲撞",5f,"niha");
        skills.Add(skill0);
        Debug.Log(skills[0].skillName);
    }

    public void TestSkill(){
        Vector2 attackDirection = offsetFromPlayer.normalized;
        isRealsing = true;
        float timer = 0;
        DOTween.To(()=>timer,X=>timer=X,1,0.8f).OnUpdate(()=>{
            rb.AddForce(new Vector2(attackDirection.x * 10,attackDirection.y * 10));
        });
        DOTween.To(()=>timer,X=>timer=X,1,skills[0].skillCoolTime).OnComplete(()=>{
            isRealsing = false;
        });
    }

    public delegate void  ProAction();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="probability">发生的概率</param>
    /// <param name="proAction">发生的事件</param>
    /// <param name="otherRestriction">其他条件是否满足</param>

    public void ProbableAction(float probability,ProAction proAction,bool otherRestriction = true){
        if(Random.Range(0,100) <= probability*100){
            proAction();
        }
    }
    

}
