using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventClass : MonoBehaviour
{
    public void hitTime(){
        PlayerHealth.instance.HP--;
    }
}
