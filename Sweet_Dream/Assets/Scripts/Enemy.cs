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

    public GameObject player;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime,transform.position.y,transform.position.z);
        player = GameObject.FindObjectOfType<Player>().gameObject;
        Init();
        TestSkill();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log(rb.velocity);
        var offset = (player.transform.position - transform.position).normalized;
        // transform.position = new Vector3(transform.position.x + speed * offset.x * Time.deltaTime,
        // transform.position.y + speed * offset.y * Time.deltaTime,0);
        if(curHealth <= 0){
            Destroy(gameObject);
        }
    }

    protected void Init(){
        rb = GetComponent<Rigidbody2D>();
        curSpeed = speed;
        curHealth = maxHealth;
        curAttack = attackInit;
        curDefence = defenceInit;
    }

    public void TestSkill(){
        float timer = 0;
        DOTween.To(()=>timer,X=>timer=X,1,0.8f).OnUpdate(()=>{
            rb.AddForce(new Vector2(10,0));
        });
    }
}
