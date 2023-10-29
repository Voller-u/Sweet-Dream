using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Yuki : Player
{
   [Header("技能")]
   public GameObject bullet;
   protected override void Start(){
        base.Start();
   }

   protected override void Update(){
      base.Update();
      if(Input.GetKeyDown(KeyCode.F)){
         GameObject bul = Instantiate(bullet);
         bul.transform.parent = transform;
         bul.transform.localPosition =new Vector3(0,0,100);
         bul.transform.parent = transform.parent;
         bul.GetComponent<MagicAttacks_Projectile>().Setup(new Vector3(1,0,0).normalized);
      }
   }
}
