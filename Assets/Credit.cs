using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public Button backMenu;
    // Start is called before the first frame update
    void Start()
    {
        OnBeginCredit();
    }
    void OnBeginCredit()
    {
        backMenu.gameObject.SetActive(true);
        backMenu.onClick.AddListener(OnBackToMenu);
    }
     void OnBackToMenu()
    {
        SceneManager.LoadScene("MenuStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
