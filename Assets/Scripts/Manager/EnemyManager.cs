using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IStageEngageObserver, IPauseObserver
{
    public void SetOnEnemyDeadCallback(OnEnemyDeadDelegate _onEnemyDeadCallback)
    {
        onEnemyDeadCallback = _onEnemyDeadCallback;
    }

    public void CheckStageEngage()
    {
        StartCoroutine("SpawnEnemy");
        enemyMemoryPool.CheckIsEnemyClear();
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;
    }

    public void ResetSpawnPos(GameObject _minSpawnPoint, GameObject _maxSpawnPoint)
    {
        minSpawnPosition = _minSpawnPoint;
        maxSpawnPosition = _maxSpawnPoint;
    }

    public void InitEnemyClearCallback(OnEnemyClearDelegate _enemyClearCallback)
    {
        enemyMemoryPool.OnEnemyClearCallback = _enemyClearCallback;
    }

    private IEnumerator SpawnEnemy()
    {
        int ttlEnemyCnt = enemyMaxSpawnCnt;

        for (int i = 0; i < ttlEnemyCnt; ++i)
        {
            GameObject enemyGo = enemyMemoryPool.SpawnInit(
                (EEnemyType)Random.Range(0, (int)EEnemyType.None),
                GetRandomSpawnPosition(),
                transform
                );
            EnemyController enemyCTRL = enemyGo.GetComponent<EnemyController>();
            enemyCTRL.Setup(player, onEnemyDeadCallback);

            if (isPaused)
                enemyCTRL.CheckPaused(isPaused);

            yield return null;
        }

        
    }

    private IEnumerator WaitSeconds(float _delayTime)
    {
        float curTime = Time.time;
        while (Time.time - curTime < _delayTime)
        {
            if (isPaused)
                curTime += Time.deltaTime;

            yield return null;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(minSpawnPosition.transform.position.x, maxSpawnPosition.transform.position.x);
        float z = Random.Range(minSpawnPosition.transform.position.z, maxSpawnPosition.transform.position.z);

        return new Vector3(x, 0.1f, z);
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        enemyMemoryPool = GetComponent<EnemyMemoryPool>();
    }

    private void Start()
    {
        gameManager.RegisterStageobserver(GetComponent<IStageEngageObserver>());
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
        enemyMemoryPool.SetupEnemyMemoryPool(transform);
    }


    [SerializeField]
    private GameObject player;

    private int enemyMaxSpawnCnt = 5;
    private int curStage = 0;

    private bool isPaused = false;

    private GameObject minSpawnPosition = null;
    private GameObject maxSpawnPosition = null;

    private GameManager         gameManager = null;
    private EnemyMemoryPool     enemyMemoryPool = null;
    private OnEnemyDeadDelegate onEnemyDeadCallback = null;
}
