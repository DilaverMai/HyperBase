using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    private List<Transform> baseTypes = new List<Transform>();
    protected float speed;
    public float FarSpeed, CloseSpeed;
    //protected Rigidbody rb;
    protected Transform target;
    protected Collider _collider;
    protected Animator _animator;
    protected float distance;
    public float FarDistance;
    private Renderer _renderer;
    private bool isDead = false;
    private Vector3 lastPosition;
    private float animSpeed;
    private float randomExtraSpeed;
    protected NavMeshAgent _agent;
    private Color matColor, emissionColor;
    internal async void TakeDamage()
    {
        isDead = true;

        foreach (var mat in _renderer.materials)
        {
            mat.DOColor(Color.black, 1f);
            mat.SetColor("_EmissionColor", Color.black);
        }
        MenuManager.Instance.CoinEffect(transform.position);

        DataManager.AddCoin.Invoke(1);
        BeforeActiveFalse();

        // if (EnemyManager.Instance != null)
        //     EnemyManager.Instance.EnemyCounter();

        await Task.Delay(1500);
        if (Application.isPlaying)
            gameObject.SetActive(false);
    }

    protected virtual void BeforeActiveFalse()
    {

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();

        matColor = _renderer.materials[1].color;
        emissionColor = _renderer.materials[1].GetColor("_EmissionColor");

        Setup();
    }

    protected virtual void Setup()
    {
        _renderer.materials[1].color = matColor;
        _renderer.materials[1].SetColor("_EmissionColor", emissionColor);

        randomExtraSpeed = Random.Range(0.25f, 2f);
        _agent.enabled = true;
        _animator.enabled = true;
        _collider.enabled = true;

        speed = FarSpeed;
        CreateType();
    }

    protected virtual void CreateType() //Random instantiate body
    {
        foreach (Transform item in transform)
        {
            baseTypes.Add(item);
            item.gameObject.SetActive(false);
        }

        if (baseTypes.Count == 0)
        {
            Debug.LogError("No found bodys");
            return;
        }

        var selectedBody = baseTypes[Random.Range(0, baseTypes.Count)];
        selectedBody.gameObject.SetActive(true);

        // var baseType = Instantiate(type.gameObject, transform);
        // baseType.transform.localPosition = Vector3.zero;
        // baseType.transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isDead) return;
        if (!Base.IsPlaying()) return;


        // animSpeed = (new Vector3(transform.position.x,0,transform.position.z)- lastPosition).magnitude;
        // lastPosition = new Vector3(transform.position.x,0,transform.position.z);

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        var movePos = target.position;

        distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
         new Vector3(movePos.x, 0, movePos.z));

        //movePos.y = transform.position.y;

        if (distance > FarDistance)
        {
            if (speed != FarSpeed) speed = FarSpeed;

            movePos.x = transform.position.x;
        }
        else
        {
            if (speed != CloseSpeed)
                speed = CloseSpeed;

            // transform.rotation =
            // Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), (speed / 5) * Time.deltaTime);
        }

        if (distance < 2f)
        {
            if (Base.IsPlaying())
            {
                if (Player.Instance != null)
                    Player.Instance.HealthSystem(1);
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, movePos, (speed + randomExtraSpeed) * Time.deltaTime);
        if (!_agent.enabled) return;
        _agent.speed = speed;
        _agent.SetDestination(movePos);
    }


    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (isDead) return;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        isDead = false;
        EventManager.BeforeFinishGame += WhenWin;
        target = FindObjectOfType<Player>().transform;
        Setup();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventManager.BeforeFinishGame -= WhenWin;
    }


    private void WhenWin(Enums.GameStat stat)
    {
        if (stat == Enums.GameStat.Win)
        {
            TakeDamage();
        }
        else
        {
            _animator.SetInteger("Victory", Random.Range(1, 3));

            if (!_agent.enabled) return;
            _agent.SetDestination(transform.position);
            _agent.enabled = false;
        }
    }

}
