using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;
    public List<Stat> stats = new List<Stat>();
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

    void OnDisable()
    {
        Instance = null;
    }
#if UNITY_EDITOR

    [Button]
    private void CreateEnum()
    {
        string[] names = new string[stats.Count];

        for (int i = 0; i < stats.Count; i++)
        {
            names[i] = stats[i].Name;
        }

        EnumCreator.CreateEnum("Stats", names);
    }
#endif

    public void AddValue(Enum_Stats _stat, float _value)
    {
        foreach (var item in stats)
        {
            if (item.Name == _stat.ToString())
            {
                item.AddValue(_value);
            }
        }
    }


}

public static class ExtensionStatManager
{
    public static void AddStat(this Enum_Stats _stat, float v)
    {
        StatManager.Instance.AddValue(_stat, v);
    }

    public static Stat GetStat(this Enum_Stats _stat)
    {
        return StatManager.Instance.stats.Find(x => x.Name == _stat.ToString());
    }
}
[System.Serializable]
public class Stat
{
    public string Name;
    public float Value;
    public float MaxValue;

    public bool AddValue(float v = 1f)
    {
        if (Value < MaxValue)
        {
            Value += v;
            if (Value > MaxValue) Value = MaxValue;
            return true;
        }
        return false;
    }
}


