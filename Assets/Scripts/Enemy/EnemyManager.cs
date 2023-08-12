using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IStageObserver, IPauseObserver
{
    public void SetOnEnemyDeadCallback(OnEnemyDeadDelegate _onEnemyDeadCallback)
    {
        onEnemyDeadCallback = _onEnemyDeadCallback;
    }

    public void CheckStage(int _curStage)
    {
        curStage = _curStage;
        StartCoroutine("SpawnEnemy");
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;
    }

    private IEnumerator SpawnEnemy()
    {
        enemySpawnCnt += curStage * 2;

        for (int i = 0; i < enemySpawnCnt; ++i)
        {
            GameObject enemyGo = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], GetRandomSpawnPosition(), Quaternion.identity, transform);
            enemyGo.GetComponent<EnemyController>().Setup(playerTr, onEnemyDeadCallback);

            yield return StartCoroutine("WaitSeconds", 1f);
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

        return new Vector3(x, 0f, z);
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        gameManager.RegisterStageobserver(GetComponent<IStageObserver>());
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
    }



    [SerializeField]
    private GameObject[] enemyPrefabs = null;
    [SerializeField]
    private GameObject minSpawnPosition = null;
    [SerializeField]
    private GameObject maxSpawnPosition = null;
    [SerializeField]
    private Transform playerTr;

    private int curStage = 0;
    private int enemySpawnCnt = 5;

    private bool isPaused = false;

    private GameManager gameManager = null;

    private OnEnemyDeadDelegate onEnemyDeadCallback = null;
}
