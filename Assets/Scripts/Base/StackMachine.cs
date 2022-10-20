using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMachine : Singleton<StackMachine>
{
    private Queue<IEnumerator> enumaQueue = new Queue<IEnumerator>();
    private Coroutine baseCoroutine;
    private Coroutine currentCoroutine;
    
    public void AddToQueue(IEnumerator coroutine)
    {
        enumaQueue.Enqueue(coroutine);
        
        if (baseCoroutine == null)
            baseCoroutine = StartCoroutine(BaseCoroutine());
    }
    
    public void SetNull()
    {
        currentCoroutine = null;
    }
    
    private IEnumerator BaseCoroutine()
    {
        while (enumaQueue.Count > 0)
        {
            currentCoroutine = StartCoroutine(enumaQueue.Dequeue());
            yield return new WaitUntil(()=> currentCoroutine == null);
        }
        
        baseCoroutine = null;
    }
    
    public IEnumerator DelayCall(Action action,float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}


public static class StackMachineExtension
{
    public static void AddToQueue(this IEnumerator coroutine)
    {
        StackMachine.Instance.AddToQueue(coroutine);
    }
    
    public static void SetNull()
    {
        StackMachine.Instance.SetNull();
    }
    
    public static IEnumerator DelayCall(this Action action,float time)
    {
        return StackMachine.Instance.DelayCall(action,time);
    }
}