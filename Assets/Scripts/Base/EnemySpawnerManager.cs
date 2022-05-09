using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Title("Generally")]
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float maxCordiX;
    public int MaxEnemy;
    public int StartEnemyCount;
    private List<Transform> enemys = new List<Transform>();

    //No show
    public static EnemySpawnerManager Instance;
    private Transform player;

    [Title("Enemys")]
    [SerializeField]
    private List<EnemySpawn> enemySpawns = new List<EnemySpawn>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FirstSpawn()
    {
        for (int i = 0; i < StartEnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private IEnumerator SpawnManager()
    {
        while (Base.IsPlaying())
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnTime);
        }
        yield return new WaitForEndOfFrame();
    }

    private void SpawnEnemy()
    {
        var enemy = enemySpawns[Random.Range(0, enemySpawns.Count)];

        if (enemy.IsSpawned() & enemys.Count < MaxEnemy)
        {
            if (enemy.IsLucky())
            {
                for (int i = 0; i < enemy.MaxSameSpawn; i++)
                {
                    enemys.Add(enemy.Spawn(CheckFarDistance(enemy.FarByPlayer)));
                    if (!enemy.IsSpawned()) break;
                }
            }
        }
    }

    void OnEnable()
    {
        EventManager.OnAfterLoadedLevel += FindThePlayer;
        EventManager.OnAfterLoadedLevel += FirstSpawn;
        EventManager.FirstTouch += WhenStartSpawn;
    }

    private void WhenStartSpawn()
    {
        if (enemySpawns.Count == 0) return;
        StartCoroutine("SpawnManager");
    }

    void OnDisable()
    {
        Instance = null;
        EventManager.OnAfterLoadedLevel -= FirstSpawn;
        EventManager.OnAfterLoadedLevel -= FindThePlayer;
        EventManager.FirstTouch -= WhenStartSpawn;
    }

    private void FindThePlayer()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private Vector3 CheckFarDistance(float z)
    {
        var pos = Vector3.zero;
        pos.y = 0;

        if (player.position.z - z > 0)
        {
            pos.z = player.position.z - z;
        }

        pos.x = Random.Range(-maxCordiX, maxCordiX);

        return pos;
    }

    public void RemoveEnemy(Transform _enemy)
    {
        enemys.Remove(_enemy);
    }

}

[System.Serializable]
public class EnemySpawn
{
    public Enum_PoolObject EnemyObject;
    public int MaxSameSpawn; //Ekranda maksimum olabilme sayısı
    public int MaxSpawn; //Max Spawn edilen sayı
    public float FarByPlayer; //Player ile enemy arasındaki mesafe
    private int spawnCount; //Spawn edilen sayı
    [PropertyRange(0, 10)]
    public int Lucky;
    public bool IsSpawned()
    {
        return spawnCount < MaxSpawn;
    }
    public bool IsLucky()
    {
        return Lucky > Random.Range(0, 10);
    }

    public Transform Spawn(Vector3 pos)
    {
        var spawedEnemy = EnemyObject.GetObject();
        spawedEnemy.SetPosition(pos);
        spawnCount++;
        return spawedEnemy.transform;
    }
}