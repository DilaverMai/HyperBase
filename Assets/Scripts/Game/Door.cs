using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Door : MonoBehaviour
{
    public UnityEvent<GameObject> ExtraFunctions = new UnityEvent<GameObject>();
    public int Value;
    public Maths _Maths = Maths.Subtract;
    private Collider _collider;
    private TextMeshPro _textOnDoor;
    private RunnerDoor _doorManager;
    public LayerMask DetectMask;
    private bool _isOpen;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _collider = GetComponent<Collider>();
        _textOnDoor = GetComponentInChildren<TextMeshPro>();
        _doorManager = GetComponentInParent<RunnerDoor>();

        if(!_collider.isTrigger) {
            _collider.isTrigger = true;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SetDoorOnText(_Maths);
    }

    private void SetDoorOnText(Maths math)
    {
        string doorText = "";

        switch (math)
        {
            case Maths.Add:
                doorText = "+";
                break;
            case Maths.Subtract:
                doorText = "-";
                break;
            case Maths.Multiply:
                doorText = "X";
                break;
            case Maths.Divide:
                doorText = "%";
                break;
            default:
                break;
        }

        
        doorText += Value.ToString();
        _textOnDoor.text = doorText;
    }

    public int DoorAdd(int getVal)
    {
        if(getVal <= 0) getVal = 1;

        switch (_Maths)
        {
            case Maths.Add:
                return Value + getVal;
            case Maths.Subtract:
                return Value - getVal;
            case Maths.Multiply:
                return Value * getVal;
            case Maths.Divide:
                return Value / getVal;
            default:
            Debug.LogError("No return 0");
                return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((DetectMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if(_isOpen) return;
            _isOpen = true;
            var player = other.GetComponent<Player>();
            player.AddPower(DoorAdd(player.Power));
            ExtraFunctions.Invoke(other.gameObject);
            //_doorManager.CloseTheDoors();
        }
    }
}
