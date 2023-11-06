using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Dead),0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Enemy")){
            Enemy enmey = other.GetComponent<Enemy>();
            if(enmey != null){
                enmey.curHealth = Mathf.Clamp(enmey.curHealth - damage,0,enmey.maxHealth);
            }
            
        }
    }

    void Dead(){
        PolygonCollider2D pgc = GetComponent<PolygonCollider2D>();
        if(pgc != null) pgc.enabled = false;
    }
}
