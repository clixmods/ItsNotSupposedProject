using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestructBullet : MonoBehaviour
{
    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private BoxCollider InvisibleWallCollider;
    private void Awake()
    {
        Physics.IgnoreCollision(myCollider, InvisibleWallCollider, true);
    }
        
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
