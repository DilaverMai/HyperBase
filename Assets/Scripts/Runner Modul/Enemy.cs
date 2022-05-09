using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Title("Stats")]
    public float FarSpeed;
    public float CloseSpeed;
    public int MaxHealth;
    public int Damage;
    [Title("Generaly")]
    public bool AttackAfterDead;
    public bool AttackAfterDelay;
    [Title("Attack After")]
    [ShowIf("AttackAfterDelay")]
    public float Delay;
    [Title("Range")]
    public float FarDistance;
    protected int health;
    //Generally
    protected float speed;
    protected Transform target;
    protected Collider _collider;
    protected Animator _animator;
    protected bool isDead;
    protected float animSpeed;
    protected float randomExtraSpeed;
    protected NavMeshAgent _agent;

    #region Delays

    private IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private IEnumerator DelayAfterAttack()
    {
        yield return new WaitForSeconds(Delay);
        if (AttackAfterDead)
            TakeDamage();
        else isDead = false;
    }

    #endregion

    #region Unity Methods
    void OnEnable()
    {
        EventManager.BeforeFinishGame += WhenFinish;
        Setup();
    }

    void OnDisable()
    {
        EventManager.BeforeFinishGame -= WhenFinish;
        EnemySpawnerManager.Instance.RemoveEnemy(transform);
    }

    void Update()
    {
        if (isDead | !Base.IsPlaying()) return;

        if (SpeedChanger())
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
            if (!_agent.enabled) return;
            _agent.speed = speed;
            _agent.SetDestination(target.position);
        }
        else
        {
            Attack();
        }
    }

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
    }
    #endregion

    #region Calculations

    private bool SpeedChanger()
    {
        if (DistanceWithPlayer() > FarDistance)
        {
            speed = FarSpeed;
            return true;
        }
        else if (DistanceWithPlayer() > 2)
        {
            speed = CloseSpeed;
            return true;
        }
        else return false;
    }

    private float DistanceWithPlayer()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position);
    }

    #endregion

    #region Methods
    protected virtual void Setup()
    {
        if (!target)
            target = FindObjectOfType<Player>().transform;
        health = MaxHealth;
        randomExtraSpeed = Random.Range(0.25f, 2f);
        _agent.enabled = true;
        _animator.enabled = true;
        _collider.enabled = true;
        isDead = false;
    }

    protected virtual void Attack()
    {
        isDead = true;
        target.GetComponent<Player>().HealthSystem(Damage);
        _animator.SetTrigger("Attack");
        StartCoroutine("DelayAfterAttack");
    }

    internal virtual void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health > 0 | isDead) return;
        isDead = true;
        CameraParticle.Coin.PlayCoinEffect(transform.position);
        Datas.Coin.CoinAdd(1);
        StartCoroutine("DelayDie");
    }

    private void WhenFinish(GameStat stat)
    {
        if (stat == GameStat.Win)
        {
            TakeDamage();
        }
        else
        {
            _animator.SetInteger("Victory", Random.Range(1, 3));

            if (_agent.enabled)
            {
                _agent.isStopped = true;
                _agent.enabled = false;
            }
        }
    }

    #endregion
}
