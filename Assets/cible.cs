using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cible : MonoBehaviour
{
    public int nbTouch = 4;
    public GameObject door;
    public Material colorRed;
    public Material colorGreen;

    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        door.gameObject.SetActive(true);
        Object.GetComponent<MeshRenderer>().material = colorRed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /* void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.layer == 9)
         {
             Object.GetComponent<MeshRenderer>().material = colorGreen;
         }
     }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            nbTouch -= 1;
            if (nbTouch <= 0)
            {
                Object.GetComponent<MeshRenderer>().material = colorGreen;
                door.gameObject.SetActive(false);
            }
            //Object.GetComponent<MeshRenderer>().material = colorGreen;
        }
    }
}
