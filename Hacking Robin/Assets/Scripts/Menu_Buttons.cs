using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject DifficultySelectPanel;
    public GameObject DiedPanel;
    public GameObject PausePanel;
    // Start is called before the first frame update
    private static bool showInitial = true; 
    void Start()
    {
        
        if(showInitial){
            Time.timeScale = 0;
            MenuPanel.SetActive(true);
        } else{
            MenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        DifficultySelectPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDifficultyPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(false);
        DifficultySelectPanel.SetActive(true);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(true);
        DifficultySelectPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void ShowDeadPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(false);
        DifficultySelectPanel.SetActive(false);
        DiedPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        if(MenuPanel.activeSelf==false && DifficultySelectPanel.activeSelf==false && DiedPanel.activeSelf==false){
            Time.timeScale = 0;
            MenuPanel.SetActive(false);
            DifficultySelectPanel.SetActive(false);
            DiedPanel.SetActive(false);
            PausePanel.SetActive(true);
        }
    }

    public void resumeGame(){
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        DifficultySelectPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
        
    public void startGame()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        DifficultySelectPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
        showInitial = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
