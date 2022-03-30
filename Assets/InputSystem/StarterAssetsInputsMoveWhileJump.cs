using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class StarterAssetsInputsMoveWhileJump : StarterAssetsInputs
{

    public static void huhughjhjgjh()
    {
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        StarterAssetsInputs compo = PlayerObject.GetComponentInChildren<StarterAssetsInputs>();

        FirstPersonController plrController = PlayerObject.GetComponentInChildren<FirstPersonController>();
        compo.enabled = false;
        //compo.transform.gameObject.Comp
        GameObject gotoadd = compo.transform.gameObject;
        Destroy(compo);
        StarterAssetsInputsMoveWhileJump oof =  gotoadd.AddComponent<StarterAssetsInputsMoveWhileJump>();
         plrController.Input = oof;
    }

    public override void OnMove(InputValue value)
    {
        if(jump)
            base.OnMove(value);
        
    }
}