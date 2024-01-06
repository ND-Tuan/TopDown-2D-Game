using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public int EffectTriggerRate;
    public int EffectType; //0:Ice, 1:Fire
    public float Duration = 2.5f;
    public float EndDuration;
    private bool end = false;
    void End(){
        end = true;
        GetComponent<Animator>().SetBool("End", true);
    }

    public void Ending(){
        float i = Duration - EndDuration;
        Invoke(nameof(End), i);
    }

    //gây sát thương cháy cho kẻ thù
    public async void Burning(EnemyControll enemyControll){
        while(!end){
            if(enemyControll!= null)
                enemyControll.TakeDmg(1, false, 0);
                await Task.Delay(600);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
       
        if( other.CompareTag("Enemy") && Random.Range(1,101)<=  EffectTriggerRate){
            if(EffectType == 0)
                other.GetComponent<EnemyControll>().Freeze(Duration);

            if(EffectType == 1){
                other.GetComponent<EnemyControll>().Burn(Duration);
            }

        }
    }
   
}

