using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDistributer : MonoBehaviour
{
    public static BuffDistributer instance; 

    private void Awake() {

        if(BuffDistributer.instance != null){
            Destroy(BuffDistributer.instance);
        }
        BuffDistributer.instance = this;
    }

    /// <summary>
    /// 给玩家发配buff
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="buffType">buff的类型</param>
    /// <param name="rate">倍率（可选，默认为1）</param>
    /// <param name="num">具体数额（可选，默认为0）</param>
    public void GiveBuffToPlayer(GameObject player,BuffType buffType,float rate = 1f,float num = 0){
        Player target = player.GetComponent<Player>();
        if(target == null) return;
        switch(buffType){
            case BuffType.ChangeMaxHealth:{
                break;
            }

            case BuffType.ChangeHealth:{
                break;
            }

            case BuffType.ChangeSpeed:{
                if(num != 0){
                    target.curSpeed += num;
                }
                else target.curSpeed *= rate;
                break;
            }

            case BuffType.ChangeAttack:{
                break;
            }

            case BuffType.ChangeDefence:{
                break;
            }

            case BuffType.ChangeIntellect:{
                break;
            }
        }
    }

    //给敌人发配buff
    public void GiveBuffToEnemy(GameObject enemy,BuffType buffType,float rate = 1f,float num = 0){
        Enemy target = enemy.GetComponent<Enemy>();
        if(target == null) return;
        switch(buffType){
            case BuffType.ChangeMaxHealth:{
                break;
            }

            case BuffType.ChangeHealth:{
                break;
            }

            case BuffType.ChangeSpeed:{
                if(num != 0){
                    target.curSpeed += num;
                }
                else target.curSpeed *= rate;
                break;
            }

            case BuffType.ChangeAttack:{
                break;
            }

            case BuffType.ChangeDefence:{
                break;
            }

            case BuffType.ChangeIntellect:{
                break;
            }
        }
    }
}
