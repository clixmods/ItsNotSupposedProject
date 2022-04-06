using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestructBullet : MonoBehaviour
{
    float pvTime = 1;
    void Start()
    {
        this.gameObject.SetActive(true);       
    }
    void Update()
    {
        AutoDestruct();
    }
    void AutoDestruct()
    {
        pvTime -= Time.deltaTime;
        if(pvTime<=0)
        {
            Destroy(gameObject);           
        }
    }
   
    private void OnCollisionExit(Collision collision)
    {
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
}
