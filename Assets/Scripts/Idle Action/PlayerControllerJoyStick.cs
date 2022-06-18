using UnityEngine;
public class PlayerControllerJoyStick : PlayerController
{
    [SerializeField]
    private float maxDistance;
    private Rigidbody _rigidbody;
    public bool CheckEnvoriment = false;
    private EnvironmentalControl EnvorimentCheck;
    private Gravity _gravity;

    protected override void Awake()
    {
        base.Awake();
        EnvorimentCheck = GetComponent<EnvironmentalControl>();
        _rigidbody = GetComponent<Rigidbody>();
        _gravity = GetComponent<Gravity>();
    }

    private void Start()
    {
        targetPos = transform.position;
        FindObjectOfType<PlayTimeMenu>().JoyStick.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!Base.IsPlaying()) return;
        if(_gravity & !EnvorimentCheck.CheckGround(transform.position)) 
            _gravity.GravityForce();

        JoyStickMove();
    }

    private void JoyStickMove()
    {
        if (JoyStick.Instance == null)
        {
            Debug.LogError("JoyStick is null");
            return;
        }
        
        var posPower = Vector3.zero;
        posPower += JoyStick.Instance.GetVector();
        var checkPos = targetPos + (posPower * playerData.RotationSpeed) * Time.deltaTime;
        

        if(CheckEnvoriment)
            if (EnvorimentCheck.CheckCircleBool(checkPos) | CheckDistanceWithTargetPos())
                checkPos = targetPos;
        
        checkPos.y = transform.position.y;
        targetPos = checkPos;
        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position,
        targetPos, Time.deltaTime * playerData.MoveSpeed);
    }



    private bool CheckDistanceWithTargetPos()
    { 
        var distance = Vector3.Distance(transform.position, targetPos);
        if (distance > maxDistance)
        {
            return true;
        }
        return false;
    }
}

