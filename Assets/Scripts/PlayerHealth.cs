using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [SerializeField]
    int hp = 3;
    public Image damageUI;
    float blinkTime = 0.1f;

    private void Awake() {
        instance = this;
    }

    public int HP{
        get{
            return hp;
        }
        set{
            hp = value;
            damageUI.enabled = true;
        }
    }

    void Start()
    {   
            damageUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(damageUI.enabled){
            blinkTime -= Time.deltaTime;
            if(blinkTime <= 0 ){
                damageUI.enabled = false;
                blinkTime = 0.1f;
            }
        }
        
    }
}
