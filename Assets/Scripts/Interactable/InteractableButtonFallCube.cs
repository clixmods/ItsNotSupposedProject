using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableButtonFallCube : InteractableObject 
{
    [SerializeField] GameObject Cube;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] int stock = 5;

    public override void PickupBehavior()
    {
        if(stock > 0)
        {
            Instantiate(Cube,_spawnPoint.position, _spawnPoint.rotation);
            stock--;
        }
    }
}
