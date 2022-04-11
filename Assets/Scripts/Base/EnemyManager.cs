using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
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
    public static EnemyManager Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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

    // public void EnemyCounter()
    // {
    //     if (enemyCount <= 0)
    //     {
    //         enemyCount = 0;
    //         return;
    //     }
    //     enemyCount--;
    // }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
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

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
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
