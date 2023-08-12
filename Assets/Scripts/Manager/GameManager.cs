using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPauseSubject, IBossEngageSubject, IStageSubject
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
    #region PauseObserver
    public void RegisterPauseObserver(IPauseObserver _observer)
    {
        pauseObserverList.Add(_observer);
    }

    public void RemovePauseObserver(IPauseObserver _observer)
    {
        pauseObserverList.Remove(_observer);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        foreach (IPauseObserver observer in pauseObserverList)
            observer.CheckPaused(isPaused);

        if (isPaused)
        {
            pauseMenu.ShowPauseMenu();
        }

        else
        {
            StopCoroutine("ShowElapsedTime");
            pauseMenu.ClosePauseMenu();
        }
    }
    #endregion

    #region BossEngageRegion
    public void RegisterBossEngageObserver(IBossEngageObserver _observer)
    {
        bossEngageObserverList.Add(_observer);
    }

    public void RemoveBossEngageObserver(IBossEngageObserver _observer)
    {
        bossEngageObserverList.Remove(_observer);
    }

    public void ToggleBossEngage()
    {
        isBossEngage = !isBossEngage;
        foreach (IBossEngageObserver observer in bossEngageObserverList)
            observer.CheckBossEngage(isBossEngage);
    }
    #endregion

    #region StageObserver
    public void RegisterStageobserver(IStageObserver _observer)
    {
        stageObserverList.Add(_observer);
    }

    public void RemoveStageObserver(IStageObserver _observer)
    {
        stageObserverList.Remove(_observer);
    }

    public void StageStart()
    {
        ++curStage;
        foreach (IStageObserver observer in stageObserverList)
            observer.CheckStage(curStage);
    }
    #endregion

    private void Awake()
    {
        pauseMenu = FindAnyObjectByType<PanelPauseMenu>();
        playerManager = FindAnyObjectByType<PlayerInputManager>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        instance = this;
    }

    private void Start()
    {
        if(mainMenuManager != null)
            mainMenuManager.OnButtonMainCallback = ChangeScene;

        if(playerManager != null)
        {
            playerManager.SetOnUseAmmoCallback(UpdateUsedAmmo);
            playerManager.SetOnGoldChangeCallback(UpdateGold);
            playerManager.SetOnPlayerDamagedCallback(UpdateDamagedCount);
            playerManager.SetOnEnemyDamagedCallback(UpdateEnemyDamaged);
        }

        if(enemyManager != null)
            enemyManager.SetOnEnemyDeadCallback(CalcDeadEnemy);

        if(pauseMenu != null)
        {
            pauseMenu.OnClickResumeCallback = TogglePause;
            pauseMenu.OnClickRestartCallback = ChangeScene;
            pauseMenu.OnClickMainCallback = ChangeScene;
            pauseMenu.UpdateTime();
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameStart();
            StageStart();
        }
    }

    public void GameStart()
    {
        
    }

    private void UpdateUsedAmmo()
    {
        pauseMenu.UpdateUsedAmmo();
    }

    private void CalcDeadEnemy()
    {
        pauseMenu.UpdateDeadEnemy();
    }

    private void UpdateGold(int _increasedGold)
    {
        pauseMenu.UpdateGold(_increasedGold);
    }

    private void UpdateDamagedCount()
    {
        pauseMenu.UpdateDamagedCount();
    }

    private void UpdateEnemyDamaged(int _dmg)
    {
        pauseMenu.UpdateTotalAttackDamage(_dmg);
    }

    private void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }


    private GameManager() { }

    [SerializeField]
    private PanelPauseMenu pauseMenu = null;
    [SerializeField]
    private PlayerInputManager playerManager = null;
    [SerializeField]
    private EnemyManager enemyManager = null;
    [SerializeField]
    private MainMenuUIManager mainMenuManager = null;
    

    private bool isPaused = false;
    private bool isBossEngage = false;

    private int curStage = 0;

    private static GameManager instance = null;

    private List<IPauseObserver> pauseObserverList = new List<IPauseObserver>();
    private List<IBossEngageObserver> bossEngageObserverList = new List<IBossEngageObserver>();
    private List<IStageObserver> stageObserverList = new List<IStageObserver>();


}
