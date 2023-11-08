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
    public GameObject[] EnemySub;
    public RoomTemplates roomTemplates;
    public SpriteRenderer  InterFace;
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

            if(Death !=null) Instantiate(Death, transform.position, Death.transform.rotation);
            HealthBar.transform.localScale = new Vector3(0, 0, 0);

            int rand = Random.Range(1,4);
            if(!NotBoss) rand = 50;

            for(int i=0; i<rand; i++){
                GameObject Tmp = Instantiate(ManaOrb, gameObject.transform.position, Quaternion.identity);
                Tmp.transform.position+= new Vector3(Random.Range(-2,3), Random.Range(-2,3), 0);
            }
           
            if(!isSummonObject) roomTemplates.countEnemy --;
            Destroy(gameObject);
            Destroy(Main);
        }

    }
    

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("bullet") && (NotBoss || !Immune)) {
            
            TakeDmg(1);
        }
    }

    public void TakeDmg(int Dmg){

        if(EnemyCurShield <=0) {
            EnemyCurHp -= Dmg;
        } else {
            EnemyCurShield -= Dmg;
        }

        GameObject instance = Instantiate(DmgPopup, gameObject.transform, worldPositionStays: false);
        instance.GetComponent<TextMesh>().text = Dmg.ToString();
        instance.transform.position += new Vector3(Random.Range(-3, 3), 3, 0);

        InterFace.GetComponent<SpriteRenderer>().material.color = Color.red;
        Invoke(nameof(Nomal), 0.1f);

        if( EnemyCurHp <=0){
            
            isDead=true;
           
        } 
        
    }

    void Nomal(){
        InterFace.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    
}
