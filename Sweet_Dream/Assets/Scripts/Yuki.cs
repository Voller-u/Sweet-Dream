using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Yuki : Player
{
   [Header("技能")]
   public GameObject bullet;
   public GameObject bullet2;
   protected override void Start(){
        base.Start();
   }

   protected override void Update(){
      base.Update();
      //attackDirection = (MousePosition()-transform.position).normalized;
      attackDirection = GetClosestEnemyDirection();
      if(attackDirection == Vector3.zero) attackDirection = faceDirection.normalized; 
      if(Input.GetKeyDown(KeyCode.F)){
         GameObject bul = Instantiate(bullet);
         bul.transform.parent = transform;
         bul.transform.localPosition =new Vector3(0,0,100);
         bul.transform.parent = transform.parent;
         bul.GetComponent<MagicAttacks_Projectile>().Setup(attackDirection);
      }
      if(Input.GetKeyDown(KeyCode.Q)){
         GameObject bul = Instantiate(bullet2);
         bul.transform.parent = transform;
         bul.transform.localPosition =new Vector3(0,0,100);
         if(attackDirection.x ==0) bul.transform.localEulerAngles = new Vector3(235-attackDirection.y*80,-90,-90);
         else bul.transform.localEulerAngles = new Vector3((float)(Math.Acos(attackDirection.x/Math.Sqrt(attackDirection.x*attackDirection.x + attackDirection.y*attackDirection.y))/Math.PI*2*80+75),-90,-90);
         Debug.Log(attackDirection);
         Debug.Log((float)(Math.Acos(attackDirection.x/Math.Sqrt(attackDirection.x*attackDirection.x + attackDirection.y*attackDirection.y))/Math.PI*2*80+75));
         Debug.Log(bul.transform.localEulerAngles);
         //bul.GetComponent<MagicAttacks_Projectile>().Setup(attackDirection);
      }
   }

   /// <summary>
   /// 自动瞄准功能（自动瞄准最近的怪物）
   /// </summary>
   /// <returns>角色与敌人之间的单位方向向量</returns>
   private Vector3 GetClosestEnemyDirection(){
      int radius = 1;//探测半径
      while(radius < 100){
         Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position,radius);
         //如果探测到了碰撞体
         if(cols.Length > 0){
            for(int i=0;i<cols.Length;i++){
               if(cols[i].tag.Equals("Enemy")){
                  return (cols[i].gameObject.transform.position - transform.position).normalized;
               }
            }
         }
         radius += 2;
      }
      return Vector3.zero;
   }
}
