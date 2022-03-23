using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UiProperty property;

    [SerializeField] static GameObject MessageBox;
    [SerializeField] static GameObject InputBox;
    [SerializeField] static GameObject ClientBox;
    

    [Header("HINTSTRING")]
    [SerializeField] Vector3 offset = new Vector3(0, 15, 0);
    [SerializeField] static GameObject HintstringList;

    void Awake()
    {
        MessageBox = property.MessageBox;
        InputBox = property.InputBox;
        ClientBox = property.ClientBox;
        HintstringList = new GameObject("HintstringList");
        HintstringList.transform.parent = transform;
        HintstringList.transform.SetSiblingIndex(0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHintstring();
    }

    void UpdateHintstring()
    {
        for (int i = 0; i < HintstringList.transform.childCount; i++)
        {
            Transform hintstring = HintstringList.transform.GetChild(i);
            HintstringProperty hintPro = hintstring.GetComponent<HintstringProperty>();
            if (hintPro.relatedObject == null)
                continue;

            if(hintPro.offset == null)
                hintstring.transform.position = Camera.main.WorldToScreenPoint(hintPro.relatedObject.transform.position) + offset;
            else
                hintstring.transform.position = Camera.main.WorldToScreenPoint(hintPro.relatedObject.transform.position) + hintPro.offset;

        }

    }

    
    /*
        This function will create a hintstring at the top of your desired gameobject, if the gameobject is deleted
        the hintstring will be deleted too (in HintstringProperty.cs)
    */
    public static HintstringProperty CreateHintString(GameObject aGameObject, string message = "Use [F] to interact.", float minDistance = 50f , Sprite icon = null)
    {
        if(aGameObject == null)
        {
            Debug.Log("Attempt to create a hintstring on a non existant object (message : "+message);
            return null;
        }
        GameObject hintString = Instantiate(MessageBox, aGameObject.transform.position, Quaternion.identity, HintstringList.transform);
        HintstringProperty component = hintString.GetComponent<HintstringProperty>();
        component.relatedObject = aGameObject;
        component.MinDistance = minDistance;
        component.textComponent.text = message;
        if(icon == null)
        {
            component.icon.color = new Color(0,0,0,0);
        }
        else
            component.icon.sprite = icon;

        return component;
    }
    public static HintstringProperty CreateHintInput(GameObject aGameObject, string message = "[F]", float minDistance = 50f)
    {
        if (aGameObject == null)
        {
            Debug.Log("Attempt to create a hintInput on a non existant object (message : " + message);
            return null;
        }
        GameObject hintString = Instantiate(InputBox, aGameObject.transform.position, Quaternion.identity, HintstringList.transform);
        HintstringProperty component = hintString.GetComponent<HintstringProperty>();
        component.relatedObject = aGameObject;
        component.MinDistance = minDistance;
        component.textComponent.text = message;
        
        return component;
    }

    public static HintstringProperty CreateHintClient(GameObject aGameObject, string message = "[F]", float minDistance = 50f, Sprite icon = null)
    {
        if (aGameObject == null)
        {
            Debug.Log("Attempt to create a hintInput on a non existant object (message : " + message);
            return null;
        }
        GameObject hintString = Instantiate(ClientBox, aGameObject.transform.position, Quaternion.identity, HintstringList.transform);
        HintstringProperty component = hintString.GetComponent<HintstringProperty>();
        component.relatedObject = aGameObject;
        component.MinDistance = minDistance;
        component.textComponent.text = message;
        component.offset = new Vector3(-50, 150, 0);

        if (icon == null)
        {
            component.icon.color = new Color(0, 0, 0, 0);
        }
        else
            component.icon.sprite = icon;

        return component;
    }
}
