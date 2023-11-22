using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameKameHaControll : MonoBehaviour
{
    private GameObject Player;
    private Player PlayerScripts;

    // Start is called before the first frame update

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScripts = Player.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScripts.Energy -= Time.deltaTime/PlayerScripts.SkillDuration;

        if(PlayerScripts.Energy <=0){

            PlayerScripts.InUltiTime = false;
            PlayerScripts.Energy=0;
            PlayerScripts.animator.SetBool("Blash", false);

            Destroy(gameObject);
        }

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) {
            transform.position = Player.transform .position+ new Vector3(-1.33f, -1.42f, 0);
            PlayerScripts.CharacterSR.transform.localScale = new Vector3(-1, 1, 0);
           
         }
            
        else {
            transform.position =  Player.transform.position+ new Vector3(1.33f, -1.42f, 0);
            PlayerScripts.CharacterSR.transform.localScale = new Vector3(1, 1, 0);
            
        }
    }

}
