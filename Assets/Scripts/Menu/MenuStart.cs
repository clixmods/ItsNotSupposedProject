using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{

    [Header("Menu")]
    public GameObject emptyMenu;
    public Button buttonQuit;
    public Button buttonStart;
    public Button buttonCredit;

    public Button backToMenu;
    public GameObject emptySelectLevel;

    [Header("Prefabs")]
    public RoomList mapList;
    public GameObject buttonMapSelect;
    public GameObject ListButtonMapContainer;

     public Button buttonNextLVL1;
     public GameObject emptyLVL1;  
     public Image mapPreview;
     public Text textDescription;

    async void Start()
    {
        OnBeginMenu();
        OnNomminationButton();

        for(int i = 0 ; i < mapList.ListRooms.Length; i++)
        {
            GameObject button = Instantiate(buttonMapSelect);
            button.transform.SetParent(ListButtonMapContainer.transform);
            button.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(button.GetComponent<RectTransform>().anchoredPosition3D.x , button.GetComponent<RectTransform>().anchoredPosition3D.y , 0);
            button.GetComponent<RectTransform>().localScale = Vector3.one;
            button.GetComponentInChildren<Text>().text = mapList.ListRooms[i].Name;
            int index = i;
           button.GetComponent<Button>().onClick.AddListener(delegate{OnSelectLevel1(index);});

        }
        
    }
    void OnBeginMenu()
    {
        
        buttonQuit.gameObject.SetActive(true);
        buttonStart.gameObject.SetActive(true);
        buttonCredit.gameObject.SetActive(true);
        emptyMenu.gameObject.SetActive(true);

        backToMenu.gameObject.SetActive(false);

        emptySelectLevel.gameObject.SetActive(false);

    }
    void OnNomminationButton()
    {
        buttonQuit.onClick.AddListener(OnQuit);
        buttonStart.onClick.AddListener(OnStart);
        buttonCredit.onClick.AddListener(OnCredit);

        backToMenu.onClick.AddListener(OnBeginMenu);
    }
    void OnQuit()
    {
        Application.Quit();
    }
    void OnStart()
    {
        emptyMenu.gameObject.SetActive(false);
        emptySelectLevel.gameObject.SetActive(true);
        backToMenu.gameObject.SetActive(true);
    }
    void OnCredit()
    {
        SceneManager.LoadScene("Credits");
    }
    public void OnSelectLevel1(int index)
    {
        Debug.Log("Trigger OnSelectLevel1 "+mapList.ListRooms[index].Name);

        
        emptyLVL1.SetActive(true);
        textDescription.text = mapList.ListRooms[index].Description;
        mapPreview.sprite = mapList.ListRooms[index].PreviewImage;

        buttonNextLVL1.onClick.AddListener(delegate{OnPlayLVL1(mapList.ListRooms[index].sceneName);});

    }
 
     void OnPlayLVL1(string sceneName)
     {
         SceneManager.LoadScene(sceneName);
     }


    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
