using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    public GameObject PlayUI;
    public GameObject ResumeUI;
    public GameObject RestartUI;
    public GameObject PauseUI;

    public Canvas canvas;

    private void Awake()
    {
        _instance = this;
    }

    public void HidePlayUI()
    {

        iTween.MoveTo(PlayUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1);
    }

    public void ShowPauseUI()
    {
        iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(-Screen.width / 2+30, -Screen.height / 2+25, 0), 1);
    }

    public void PlayHandler()
    {
        HidePlayUI();
        ShowPauseUI();
        AudioManager._instance.PlayButtonAudio();
        GameController._instance.Play();
    }

    public void HidePauseUI()
    {
        iTween.MoveTo(PauseUI, canvas.transform.position + new Vector3(-Screen.width / 2 -500, -Screen.height / 2 + 25, 0), 1);
    }

    public void ShowResumeUI()
    {
        iTween.MoveTo(ResumeUI, canvas.transform.position+Vector3.zero, 1);
    }

    public void HideResumeUI()
    {
        iTween.MoveTo(ResumeUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1);
    }

    public void ShowRestartUI()
    {
        iTween.MoveTo(RestartUI, canvas.transform.position + Vector3.zero, 1);
    }

    public void HideRestartUI()
    {
        iTween.MoveTo(RestartUI, canvas.transform.position + new Vector3(-Screen.width / 2 - 500, 0, 0), 1);
    }

    public void PauseHandler()
    {
        ShowResumeUI();
        HidePauseUI();
        AudioManager._instance.PlayButtonAudio();
        GameController._instance.Pause();
    }

    public void ResumeHandler()
    {
        HideResumeUI();
        ShowPauseUI();
        AudioManager._instance.PlayButtonAudio();
        GameController._instance.Resume();
    }

    public void RestartHandler()
    {

        HideRestartUI();
        ShowPauseUI();
        AudioManager._instance.PlayButtonAudio();
        GameController._instance.Restart();
    }

    public void ExitHandler()
    {
        AudioManager._instance.PlayButtonAudio();
        GameController._instance.Exit();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
