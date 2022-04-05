using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Door : MonoBehaviour
{
    public UnityEvent ExtraFunctions = new UnityEvent();
    public int Value;
    public Enums.Maths _Maths = Enums.Maths.Subtract;
    private Collider _collider;
    private TextMeshPro _textOnDoor;
    private RunnerDoor _doorManager;
    private bool _isOpen;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _collider = GetComponent<Collider>();
        _textOnDoor = transform.parent.GetComponentInChildren<TextMeshPro>();
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

    private void SetDoorOnText(Enums.Maths math)
    {
        string doorText = "";

        switch (math)
        {
            case Enums.Maths.Add:
                doorText = "+";
                break;
            case Enums.Maths.Subtract:
                doorText = "-";
                break;
            case Enums.Maths.Multiply:
                doorText = "X";
                break;
            case Enums.Maths.Divide:
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
            case Enums.Maths.Add:
                return Value + getVal;
            case Enums.Maths.Subtract:
                return Value - getVal;
            case Enums.Maths.Multiply:
                return Value * getVal;
            case Enums.Maths.Divide:
                return Value / getVal;
            default:
            Debug.LogError("No return 0");
                return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if(_isOpen) return;
            _isOpen = true;
            player.AddPower(DoorAdd(player.Power));
            ExtraFunctions.Invoke();
            //_doorManager.CloseTheDoors();
        }
    }
}
