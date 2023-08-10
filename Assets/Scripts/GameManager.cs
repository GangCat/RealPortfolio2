using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IPauseSubject, IBossEngageSubject
{
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManager = new GameObject("GameManager");
                    instance = gameManager.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public void RegisterPauseObserver(IPauseObserver _observer)
    {
        listPauseObservers.Add(_observer);
    }

    public void RemovePauseObserver(IPauseObserver _observer)
    {
        listPauseObservers.Remove(_observer);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        foreach (IPauseObserver observer in listPauseObservers)
            observer.CheckPaused(isPaused);

        if (isPaused)
            ShowPauseMenuTime();
        else
        {
            StopCoroutine("ShowElapsedTime");
            pauseMenu.ClosePauseMenu();
        }
    }


    public void ToggleBossEngage()
    {
        isBossEngage = !isBossEngage;
        foreach (IBossEngageObserver observer in listBossEngageObservers)
            observer.CheckBossEngage(isBossEngage);
    }

    public void RegisterBossEngageObserver(IBossEngageObserver _observer)
    {
        listBossEngageObservers.Add(_observer);
    }

    public void RemoveBossEngageObserver(IBossEngageObserver _observer)
    {
        listBossEngageObservers.Remove(_observer);
    }

    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        startTime = Time.time;
    }

    private void ShowPauseMenuTime()
    {
        startTime = Time.time;
        pauseMenu.ShowPauseMenu();
        StartCoroutine("ShowElapsedTime");
    }

    private IEnumerator ShowElapsedTime()
    {
        while (true)
        {
            textTime.text = "Time: " + (Time.time - startTime).ToString("F2");
            yield return null;
        }
    }


    private GameManager() { }

    [SerializeField]
    private TextMeshProUGUI textTime = null;
    [SerializeField]
    private PanelPauseMenu pauseMenu = null;

    private bool isPaused = false;
    private bool isBossEngage = false;

    private float startTime = 0f;

    private List<IPauseObserver> listPauseObservers = new List<IPauseObserver>();
    private List<IBossEngageObserver> listBossEngageObservers = new List<IBossEngageObserver>();
    private static GameManager instance = null;


}
