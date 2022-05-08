using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    private Enum_PoolObject enemyObject;
    [SerializeField]
    private Vector2 maxCordiX = Vector2.zero;
    [SerializeField]
    private int maxEnemy;
    public int MaxEnemy;
    private Transform targetPlayer;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int sameTime;
    public static EnemySpawnerManager Instance;

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

    void Start()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            enemyObject.GetObject()
            .SetPosition(new Vector3(Random.Range(maxCordiX.x, maxCordiX.y), 0, Random.Range(1, 5f)));
        }
    }

    IEnumerator PlusPlus()
    {
        while (MaxEnemy > maxEnemy)
        {
            maxEnemy++;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Spawn()
    {
        while (Base.IsPlaying())
        {
            var enemys = FindObjectsOfType<Enemy>();
            var enemyCount = enemys.Length;

            if (enemyCount < maxEnemy)
            {
                var diff = maxEnemy - enemyCount;
                if (diff > sameTime) diff = sameTime;
                else if (diff <= 0) yield return null;


                for (int i = 0; i < diff; i++)
                {
                    if (enemyCount >= maxEnemy) break;

                    var posZ = targetPlayer.position.z - 50;
                    if (posZ < 0) posZ = 0;

                    enemyObject.GetObject().SetPosition(new Vector3(Random.Range(maxCordiX.x, maxCordiX.y), 0, posZ));
                    enemyCount++;
                }

                yield return new WaitForSeconds(spawnTime);

            }

            yield return new WaitForEndOfFrame();
        }
    }

    void OnEnable()
    {
        EventManager.OnAfterLoadedLevel += FindThePlayer;
        EventManager.FirstTouch += WhenStartSpawn;
    }

    private void WhenStartSpawn()
    {
        StartCoroutine("Spawn");
        StartCoroutine("PlusPlus");
    }

    void OnDisable()
    {
        Instance = null;
        EventManager.OnAfterLoadedLevel -= FindThePlayer;
        EventManager.FirstTouch -= WhenStartSpawn;
    }

    private void FindThePlayer()
    {
        targetPlayer = FindObjectOfType<Player>().transform;
    }
}
public class EnemySpawn
{
    public Enum_PoolObject EnemyObject;
    public int MaxSameSpawn; //Ekranda maksimum olabilme sayısı
}