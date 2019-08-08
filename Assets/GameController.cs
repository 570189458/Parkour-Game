using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController _instance;


    public bool isPause;
    public bool isPlay;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        isPause = true;
        isPlay = true;
    }

    public void Play()
    {
        isPause = false;
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        
        //GameAttribute._instance.ResetAll();
        //PlayerControl._instance.ResetAll();
        //PlayerControl._instance.Play();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
