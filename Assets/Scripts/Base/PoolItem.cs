using UnityEngine;
using UnityEngine.Events;

public class PoolItem : MonoBehaviour
{
    [HideInInspector] public Enum_PoolObject _PoolEnum;
    public UnityEvent OnDeath;
    public UnityEvent OnSpawn;

    void OnEnable()
    {
        OnSpawn?.Invoke();
        EventManager.OnBeforeLoadedLevel += Kill;
    }
    private void OnDisable()
    {
        OnDeath?.Invoke();
        EventManager.OnBeforeLoadedLevel -= Kill;
        PoolManager.Instance.BackToList(this);
    }
    private void Kill()
    {
        gameObject.SetActive(false);
    }

    #region Defualts

    public PoolItem SetEnum(Enum_PoolObject en)
    {
        _PoolEnum = en;
        return this;
    }

    public PoolItem SetPosition(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public PoolItem SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
        return this;
    }

    public PoolItem SetRotation(Vector3 rot)
    {
        transform.eulerAngles = rot;
        return this;
    }

    public PoolItem SetLocalRotation(Vector3 rot)
    {
        transform.localEulerAngles = rot;
        return this;
    }

    public PoolItem SetScale(Vector3 scale)
    {
        transform.localScale = scale;
        return this;
    }

    public PoolItem SetActive(bool active)
    {
        gameObject.SetActive(active);
        return this;
    }

    public PoolItem SetParent(Transform parent)
    {
        transform.SetParent(parent);
        return this;
    }

    public PoolItem SetLayer(int layer)
    {
        gameObject.layer = layer;
        return this;
    }

    public PoolItem SetTag(string tag)
    {
        gameObject.tag = tag;
        return this;
    }

    public PoolItem SetName(string name)
    {
        gameObject.name = name;
        return this;
    }

    public PoolItem DeActive()
    {
        gameObject.SetActive(false);
        return this;
    }

    public PoolItem AddPower(Vector3 pow)
    {
        GetComponent<Rigidbody>().velocity = pow;
        return this;
    }

    #endregion
}