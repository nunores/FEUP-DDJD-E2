using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Buttons : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject DifficultySelectPanel;
    // Start is called before the first frame update
    void Start()
    {
        //MenuPanel.SetActive(true);
        //DifficultySelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDifficultyPanel()
        {
            MenuPanel.SetActive(false);
            DifficultySelectPanel.SetActive(true);
        }

    public void ShowMenuPanel()
        {
            MenuPanel.SetActive(true);
            DifficultySelectPanel.SetActive(false);
        }

}
