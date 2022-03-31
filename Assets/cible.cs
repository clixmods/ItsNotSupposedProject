using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cible : MonoBehaviour
{
    public int nbTouch = 0;
    public GameObject door;
    public Material colorRed;
    public Material colorGreen;

    public GameObject thisCible;
    public GameObject lum1;
    public GameObject lum2;
    public GameObject lum3;
    public GameObject lum4;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        door.gameObject.SetActive(true);
        thisCible.GetComponent<MeshRenderer>().material = colorRed;
        lum1.GetComponent<MeshRenderer>().material = colorRed;
        lum2.GetComponent<MeshRenderer>().material = colorRed;
        lum3.GetComponent<MeshRenderer>().material = colorRed;
        lum4.GetComponent<MeshRenderer>().material = colorRed;


        rb.useGravity = false;
        rb = GetComponent<Rigidbody>();
    }
    public void EnableGravity()
    {
        rb.useGravity = true;
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
        if (collision.gameObject.layer == 10)
        {
            nbTouch += 1;
            if(nbTouch==1)
            {
                lum1.GetComponent<MeshRenderer>().material = colorGreen;
                EnableGravity();
            }
            if (nbTouch == 2)
            {
                lum2.GetComponent<MeshRenderer>().material = colorGreen;
            }
            if (nbTouch == 3)
            {
                lum3.GetComponent<MeshRenderer>().material = colorGreen;
            }
            if (nbTouch == 4)
            {
                lum4.GetComponent<MeshRenderer>().material = colorGreen;
            }
            if (nbTouch >=4)
            {
                thisCible.GetComponent<MeshRenderer>().material = colorGreen;
                door.gameObject.SetActive(false);
            }
            //Object.GetComponent<MeshRenderer>().material = colorGreen;
        }
    }
}
