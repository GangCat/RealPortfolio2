using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IPauseSubject
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
        foreach (var observer in listPauseObservers)
            observer.CheckPaused(isPaused);
    }

    private void Awake()
    {
        instance = this;
    }



    private GameManager() { }

    private bool isPaused = false;
    private List<IPauseObserver> listPauseObservers = new List<IPauseObserver>();
    private static GameManager instance = null;
}
