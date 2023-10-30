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
         Debug.Log((MousePosition()-transform.position).normalized);
         GameObject bul = Instantiate(bullet);
         bul.transform.parent = transform;
         bul.transform.localPosition =new Vector3(0,0,100);
         bul.transform.parent = transform.parent;
         bul.GetComponent<MagicAttacks_Projectile>().Setup((MousePosition()-transform.position).normalized);
      }
   }

   private Vector3 MousePosition(){
      var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousePosition.z = 0;
      return mousePosition;
   }
}
