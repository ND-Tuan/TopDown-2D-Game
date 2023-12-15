using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemyControll : MonoBehaviour
{
    public int EnemyMaxHp;
    public int EnemyCurHp;
    public int EnemyMaxShield;
    public int EnemyCurShield;
    public bool Immune = false;
    public bool NotBoss ;
    public bool isSummonObject = false;
    public GameObject HealthBar;
    public GameObject Health;
    public GameObject Shield;
    public GameObject DmgPopup;
    public GameObject Death;
    public GameObject Main;
    public GameObject ManaOrb;
    public GameObject Coin;
    public GameObject[] EnemySub;
    public GameObject IceFreeze;
    public GameObject Burning;
    private float BurnTime;
    public RoomTemplates roomTemplates;
    public SpriteRenderer  InterFace;
    public bool InmmuFreeze = false;
    private bool isDead =false;
    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        roomTemplates.countEnemy++;
        EnemyCurHp = EnemyMaxHp;
        
    }

    void Update(){
        double ShieldAmount;
        if(EnemyMaxShield !=0) {
            ShieldAmount = EnemyCurShield *(1.2/EnemyMaxShield);
            Shield.transform.localScale = new Vector3((float)ShieldAmount, (float)1.6, 0);
        }

        double HealAmount = EnemyCurHp *(1.2/EnemyMaxHp);    
        Health.transform.localScale = new Vector3((float)HealAmount, (float)1.6, 0);

        if( isDead) {

            if(Death !=null){
                Instantiate(Death, transform.position, Death.transform.rotation);
                
            } 
            HealthBar.transform.localScale = new Vector3(0, 0, 0);

            int rand = Random.Range(2,6);
            if(!NotBoss) rand = 50;

            //Rơi hạt mana khi chết
            for(int i=0; i<rand; i++){
                GameObject Tmp = Instantiate(ManaOrb, gameObject.transform.position, Quaternion.identity);
                Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
            }

            //Rơi vàng khi chết
            if(!isSummonObject){
                if(!NotBoss) rand = Random.Range(2,6);
                for(int i=0; i<rand; i++){
                    GameObject Tmp = Instantiate(Coin, gameObject.transform.position, Quaternion.identity);
                    Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
                }
            }

            // Nghệ thuật là sự bùng nổ :))
            BoomAttack boomAttack = gameObject.GetComponentInChildren<BoomAttack>();
            if(boomAttack!=null){
                roomTemplates.countEnemy ++;
                boomAttack.KaBoooommm();
            }
            
            if(!isSummonObject) roomTemplates.countEnemy --;
            Destroy(gameObject);
        }

        BurnTime -=Time.deltaTime;

    }

    public void TakeDmg(int Dmg, bool IsCrit, float freeze){
        if(!Immune)
            if(EnemyCurShield <=0) {
                EnemyCurHp -= Dmg;
            } else {
                EnemyCurShield -= Dmg;
            }

            GameObject instance = Instantiate(DmgPopup, gameObject.transform, worldPositionStays: false);
            instance.GetComponent<TextMesh>().text = Dmg.ToString();
            if(IsCrit){
                instance.GetComponent<TextMesh>().color = new Color(1, 0.8719501f, 0, 1);
            }

            InterFace.GetComponent<SpriteRenderer>().material.color = Color.red;
            Invoke(nameof(Nomal), 0.1f);

            if(NotBoss && !InmmuFreeze){
                if(gameObject.GetComponent<EnemyAI>().freeze<= freeze)
                    gameObject.GetComponent<EnemyAI>().freeze =  freeze;
            }

            if( EnemyCurHp <=0){
                
                isDead=true;
            
            } 
        
    }

    public void Freeze(float duration){
        if(NotBoss){
            GameObject Ice = Instantiate(IceFreeze, gameObject.transform, worldPositionStays:false);
            Ice.transform.position += new Vector3(0, 2,0);
            gameObject.GetComponent<EnemyAI>().freeze = duration;
            InterFace.GetComponent<SpriteRenderer>().material.color = Color.blue;
            Ice.GetComponent<EffectController>().Ending();
            Destroy(Ice, duration);
            Invoke(nameof(Nomal), duration);
        }
    }

     public void Burn(float duration){
        if(NotBoss && BurnTime<=0){
            BurnTime = duration;
            GameObject fire = Instantiate(Burning, gameObject.transform, worldPositionStays:false);
            fire.transform.position += new Vector3(0, 5,0);
            fire.GetComponent<EffectController>().Burning(this);
            fire.GetComponent<EffectController>().Ending();
            InterFace.GetComponent<SpriteRenderer>().material.color = new Color(1, 0.3f,0);
            Destroy(fire, duration);
            Invoke(nameof(Nomal), duration);
        }
    }


    void Nomal(){
        InterFace.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    
}
