using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public GameObject MenuPanel;
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
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Time.timeScale = 0;
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ShowMenuPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(true);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    public void ShowDeadPanel()
    {
        Time.timeScale = 0;
        MenuPanel.SetActive(false);
        DiedPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        if(MenuPanel.activeSelf==false && DiedPanel.activeSelf==false){
            Time.timeScale = 0;
            MenuPanel.SetActive(false);
            DiedPanel.SetActive(false);
            PausePanel.SetActive(true);
        }
    }

    public void resumeGame(){
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
        
    public void startGame()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        DiedPanel.SetActive(false);
        PausePanel.SetActive(false);
        showInitial = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
