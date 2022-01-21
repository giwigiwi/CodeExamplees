using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;


public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    // public static void InvokeAction(this MonoBehaviour parent, Action action, float time)
    // {
    //     parent.StartCoroutine(ExecuteAfterTime(action, time));
    // }

    public static void InvokeAction(this MonoBehaviour parent, UnityAction action, float time)
    {
        parent.StartCoroutine(ExecuteAfterTime(action, time));
    }

    // private static IEnumerator ExecuteAfterTime(Action action, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     action();
    // }

    private static IEnumerator ExecuteAfterTime(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}