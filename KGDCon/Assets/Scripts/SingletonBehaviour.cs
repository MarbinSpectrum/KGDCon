using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance => _instance;
    private static T _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this as T;
    }
}