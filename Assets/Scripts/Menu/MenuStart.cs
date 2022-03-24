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

    // [Header("LVL1")]
    // public Button buttonLevel1;
     public Button buttonNextLVL1;
     public GameObject emptyLVL1;  
     public Image mapPreview;
     public Text textDescription;
    // [SerializeField] string SceneNameLevel1;
    // [Header("LVL2")]
    // public Button buttonLevel2;
    // public Button buttonNextLVL2;
    // public GameObject emptyLVL2;
    // [SerializeField] string SceneNameLevel2;
    // [Header("LVL3")]
    // public Button buttonLevel3;
    // public Button buttonNextLVL3;
    // public GameObject emptyLVL3;
    // [SerializeField] string SceneNameLevel3;
    // [Header("LVL4")]
    // public Button buttonLevel4;
    // public Button buttonNextLVL4;
    // public GameObject emptyLVL4;
    // [SerializeField] string SceneNameLevel4;
    // Start is called before the first frame update
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
            //button.transform.position = new Vector3(button.transform.position.x , button.transform.position.y , 0);
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


        // emptyLVL1.gameObject.SetActive(false);

        // emptyLVL2.gameObject.SetActive(true);
        // emptyLVL3.gameObject.SetActive(true);
        // emptyLVL4.gameObject.SetActive(true);

        // buttonNextLVL1.gameObject.SetActive(true);
        // buttonNextLVL2.gameObject.SetActive(true);
        // buttonNextLVL3.gameObject.SetActive(true);
        // buttonNextLVL4.gameObject.SetActive(true);

        
        emptySelectLevel.gameObject.SetActive(false);
       // buttonLevel1.gameObject.SetActive(false);

    }
    void OnNomminationButton()
    {
        buttonQuit.onClick.AddListener(OnQuit);
        buttonStart.onClick.AddListener(OnStart);
        buttonCredit.onClick.AddListener(OnCredit);

        backToMenu.onClick.AddListener(OnBeginMenu);

        // buttonLevel1.onClick.AddListener(OnSelectLevel1);
        // //buttonLevel2.onClick.AddListener(OnSelectLevel2);
        // //buttonLevel3.onClick.AddListener(OnSelectLevel3);
        // //buttonLevel4.onClick.AddListener(OnSelectLevel4);

        // buttonNextLVL1.onClick.AddListener(OnPlayLVL1);
        // buttonNextLVL2.onClick.AddListener(OnPlayLVL2);
        // buttonNextLVL3.onClick.AddListener(OnPlayLVL3);
        // buttonNextLVL4.onClick.AddListener(OnPlayLVL4);

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

        // emptyLVL1.gameObject.SetActive(false);
        // emptyLVL2.gameObject.SetActive(false);
        // emptyLVL3.gameObject.SetActive(false);
        // emptyLVL4.gameObject.SetActive(false);


        // buttonNextLVL1.gameObject.SetActive(false);

        // buttonNextLVL2.gameObject.SetActive(true);
        // buttonNextLVL3.gameObject.SetActive(true);
        // buttonNextLVL4.gameObject.SetActive(true);

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
        // emptyLVL2.gameObject.SetActive(false);
        // emptyLVL3.gameObject.SetActive(false);
        // emptyLVL4.gameObject.SetActive(false);
    }
 
     void OnPlayLVL1(string sceneName)
     {
         SceneManager.LoadScene(sceneName);
     }
    // void OnPlayLVL2()
    // {
    //     SceneManager.LoadScene(SceneNameLevel2);
    // }
    // void OnPlayLVL3()
    // {
    //     SceneManager.LoadScene(SceneNameLevel3);
    // }
    // void OnPlayLVL4()
    // {
    //     SceneManager.LoadScene(SceneNameLevel4);
    // }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
