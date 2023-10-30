using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本属性")]
    protected float curHealth;
    public float  maxHealth;
    public float speed;//速度
    public float curSpeed;//当前速度
    public float attackInit;//攻击力
    public float curAttack;//当前攻击力
    public float defenceInit;//防御力
    public float curDefence;//当前防御力

    public GameObject player;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime,transform.position.y,transform.position.z);
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        var offset = (player.transform.position - transform.position).normalized;
        transform.position = new Vector3(transform.position.x + speed * offset.x * Time.deltaTime,
        transform.position.y + speed * offset.y * Time.deltaTime,0);
    }

    protected void Init(){
        curSpeed = speed;
        curHealth = maxHealth;
        curAttack = attackInit;
        curDefence = defenceInit;
    }
}
