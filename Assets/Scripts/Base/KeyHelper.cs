using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyHelper : Singleton<KeyHelper>
{
    private List<FunctionKey> functionKeys = new List<FunctionKey>();
    
    public void AddFunctionKey(FunctionKey functionKey)
    {
        functionKeys.Add(functionKey);
    }
    
    public void RemoveFunctionKey(FunctionKey functionKey)
    {
        functionKeys.Remove(functionKey);
    }
    
    public void OnGUI()
    {
        var e = Event.current;
        
        if (e.isKey)
        {
            foreach (var functionKey in functionKeys)
            {
                if (functionKey.key == e.keyCode)
                    functionKey.action();
            }
        }
    }
}

public static class KeyHelperExtension
{
    public static void AddNewFunctionKey(Action action,KeyCode key)
    {
        var functionKey = new FunctionKey(action,key);
        KeyHelper.Instance.AddFunctionKey(functionKey);
    }
    
    public static void AddFunctionKey(this Action action,KeyCode key)
    {
        var functionKey = new FunctionKey(action,key);
        KeyHelper.Instance.AddFunctionKey(functionKey);
    }


}

[Serializable]
public class FunctionKey
{
    public Action action{ get; private set; }
    public KeyCode key { get; private set; }
    
    public FunctionKey(Action action, KeyCode key)
    {
        this.action = action;
        this.key = key;
    }
}
