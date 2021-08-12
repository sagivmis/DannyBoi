using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] public float cooldownTime = 1f;
    public bool onCooldown = false;
    float timer = 0.0f;
    public Transform firePoint;
    public GameObject arrowPrefab;
    
    void Update()
    {
        if(timer<=0 ){
            onCooldown = false;
            timer = cooldownTime;
            // Shoot();
        }
        else{
            timer-=Time.deltaTime;
        }
    }

    public void Shoot(){
        if(!onCooldown){
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            onCooldown = true;
        }
    }
}
