using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

public class SkillManager : MonoBehaviour
{
    public static Action AUsingButton;
    public List<Skill> skills = new List<Skill>();
    public static SkillManager Instance;
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
        foreach (var item in skills)
        {
            if (item.UpgradeButton == null) Debug.LogError("SKILL BUTTON NULL");
            item.UpgradeButton.onClick.AddListener(() =>
            {
                item.AddedLevel(Datas.Coin.GetData());
                AUsingButton?.Invoke();
            });
        }
    }

    void OnDisable()
    {
        Instance = null;
    }

    private void LocaLevelAdd(Enum_Skills _skill)
    {
        foreach (var item in skills)
        {
            if (item.Name == _skill.ToString())
            {
                item.AddedLevel(Datas.Coin.GetData());
                break;
            }
        }
    }
    internal int MinCost()
    {
        var costLists = new List<int>();
        foreach (var item in skills)
        {
            costLists.Add(item.Cost);
        }

        costLists.Sort();

        return costLists[0];
    }

    internal void LevelAdd(Enum_Skills _skill)
    {
        foreach (var item in skills)
        {
            if (item.Name == _skill.ToString())
            {
                item.AddedLevel(Datas.Coin.GetData());
            }
        }
    }

    internal void AddFunction(Enum_Skills _skill, UnityAction _function)
    {
        foreach (var item in skills)
        {
            if (item.Name == _skill.ToString())
            {
                item.UpgradeButton.onClick.AddListener(_function);
            }
        }
    }

    internal void AddSkillAction(Enum_Skills _skill, UnityAction _function)
    {
        foreach (var item in skills)
        {
            if (item.Name == _skill.ToString())
            {
                item.SkillAction.AddListener(_function);
            }
        }
    }

#if UNITY_EDITOR
    [Button]
    private void CreateSkills()
    {
        string[] skillsNames = new string[skills.Count];
        for (int i = 0; i < skills.Count; i++)
        {
            skillsNames[i] = skills[i].Name;
        }

        EnumCreator.CreateEnum("Skills", skillsNames);
    }
#endif

    internal void RemoveFunction(Enum_Skills _skill, UnityAction _function)
    {
        foreach (var item in skills)
        {
            if (item.Name == _skill.ToString())
            {
                item.UpgradeButton.onClick.RemoveListener(_function);
            }
        }
    }
}

public static class ExtensionUpgradeMenu
{
    public static Skill GetData(this Enum_Skills _skill)
    {
        return SkillManager.Instance.skills.Find(x => x.Name == _skill.ToString());
    }
    public static void AddLevel(this Enum_Skills skill)
    {
        SkillManager.Instance.LevelAdd(skill);
    }

    public static void AddFunction(this Enum_Skills skill, UnityAction function)
    {
        SkillManager.Instance.AddFunction(skill, function);
    }

    public static void RemoveFunction(this Enum_Skills skill, UnityAction function)
    {
        SkillManager.Instance.RemoveFunction(skill, function);
    }

    public static void AddSkillAction(this Enum_Skills skill, UnityAction function)
    {
        SkillManager.Instance.AddSkillAction(skill, function);
    }

    public static Button GetButton(this Enum_Skills skill)
    {
        foreach (var item in SkillManager.Instance.skills)
        {
            if (item.Name == skill.ToString())
            {
                return item.UpgradeButton;
            }
        }
        return null;
    }
}
[System.Serializable]
public class Skill
{
    public int Level = 1;
    public int Cost;
    public string Name;
    //public Enum_Skills skill;
    [HideInInspector]
    public UnityEvent SkillAction;
    public Button UpgradeButton;
    public AnimationCurve CostMultipier;

    public bool AddedLevel(int money = 0)
    {
        if (Cost > money)
        {
            Debug.Log("not enough money");
            return false;
        }

        Level++;
        Datas.Coin.SetData(Datas.Coin.GetData() - Cost);
        if (Level > 10)
        {
            Cost = 10;
        }
        //Cost = (int)CostMultipier.Evaluate(Level);
        return true;
    }
}
