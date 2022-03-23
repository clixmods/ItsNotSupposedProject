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
    [Header("LVL1")]
    public Button buttonLevel1;
    public Button buttonNextLVL1;
    public GameObject emptyLVL1;
    [Header("LVL2")]
    public Button buttonLevel2;
    public Button buttonNextLVL2;
    public GameObject emptyLVL2;
    [Header("LVL3")]
    public Button buttonLevel3;
    public Button buttonNextLVL3;
    public GameObject emptyLVL3;
    [Header("LVL4")]
    public Button buttonLevel4;
    public Button buttonNextLVL4;
    public GameObject emptyLVL4;
    // Start is called before the first frame update
    void Start()
    {
        OnBeginMenu();
        OnNomminationButton();
    }
    void OnBeginMenu()
    {
        
        buttonQuit.gameObject.SetActive(true);
        buttonStart.gameObject.SetActive(true);
        buttonCredit.gameObject.SetActive(true);
        emptyMenu.gameObject.SetActive(true);

        backToMenu.gameObject.SetActive(false);

        emptyLVL1.gameObject.SetActive(true);
        emptyLVL2.gameObject.SetActive(true);
        emptyLVL3.gameObject.SetActive(true);
        emptyLVL4.gameObject.SetActive(true);

        buttonNextLVL1.gameObject.SetActive(true);
        buttonNextLVL2.gameObject.SetActive(true);
        buttonNextLVL3.gameObject.SetActive(true);
        buttonNextLVL4.gameObject.SetActive(true);

        
        emptySelectLevel.gameObject.SetActive(false);

    }
    void OnNomminationButton()
    {
        buttonQuit.onClick.AddListener(OnQuit);
        buttonStart.onClick.AddListener(OnStart);
        buttonCredit.onClick.AddListener(OnCredit);

        backToMenu.onClick.AddListener(OnBeginMenu);

        buttonLevel1.onClick.AddListener(OnSelectLevel1);
        buttonLevel2.onClick.AddListener(OnSelectLevel2);
        buttonLevel3.onClick.AddListener(OnSelectLevel3);
        buttonLevel4.onClick.AddListener(OnSelectLevel4);

        buttonNextLVL1.onClick.AddListener(OnPlayLVL1);
        buttonNextLVL2.onClick.AddListener(OnPlayLVL2);
        buttonNextLVL3.onClick.AddListener(OnPlayLVL3);
        buttonNextLVL4.onClick.AddListener(OnPlayLVL4);

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

        emptyLVL1.gameObject.SetActive(false);
        emptyLVL2.gameObject.SetActive(false);
        emptyLVL3.gameObject.SetActive(false);
        emptyLVL4.gameObject.SetActive(false);

        buttonNextLVL1.gameObject.SetActive(true);
        buttonNextLVL2.gameObject.SetActive(true);
        buttonNextLVL3.gameObject.SetActive(true);
        buttonNextLVL4.gameObject.SetActive(true);

    }
    void OnCredit()
    {
        SceneManager.LoadScene("Credits");
    }
    void OnSelectLevel1()
    {
        emptyLVL1.gameObject.SetActive(true);
        emptyLVL2.gameObject.SetActive(false);
        emptyLVL3.gameObject.SetActive(false);
        emptyLVL4.gameObject.SetActive(false);
    }
    void OnSelectLevel2()
    {
        emptyLVL1.gameObject.SetActive(false);
        emptyLVL2.gameObject.SetActive(true);
        emptyLVL3.gameObject.SetActive(false);
        emptyLVL4.gameObject.SetActive(false);
    }
    void OnSelectLevel3()
    {
        emptyLVL1.gameObject.SetActive(false);
        emptyLVL2.gameObject.SetActive(false);
        emptyLVL3.gameObject.SetActive(true);
        emptyLVL4.gameObject.SetActive(false);
    }
    void OnSelectLevel4()
    {
        emptyLVL1.gameObject.SetActive(false);
        emptyLVL2.gameObject.SetActive(false);
        emptyLVL3.gameObject.SetActive(false);
        emptyLVL4.gameObject.SetActive(true);
    }
    void OnPlayLVL1()
    {
        SceneManager.LoadScene("Level1");
    }
    void OnPlayLVL2()
    {
        SceneManager.LoadScene("Level2");
    }
    void OnPlayLVL3()
    {
        SceneManager.LoadScene("Level3");
    }
    void OnPlayLVL4()
    {
        SceneManager.LoadScene("Level4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
