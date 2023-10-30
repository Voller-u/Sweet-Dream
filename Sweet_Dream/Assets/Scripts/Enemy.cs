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
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x + speed*Time.deltaTime,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Init(){
        curSpeed = speed;
        curHealth = maxHealth;
        curAttack = attackInit;
        curDefence = defenceInit;
    }
}
