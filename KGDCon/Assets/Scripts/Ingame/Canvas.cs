using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : SingletonBehaviour<Canvas>
{
    private Dictionary<Type, UI> _uiDict = new();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < transform.childCount; ++i)
        {
            var ui = transform.GetChild(i).GetComponent<UI>();
            _uiDict.Add(ui.GetType(), ui);
        }
    }

    public T Get<T>() where T : UI
    {
        var type = typeof(T);
        return _uiDict[type] as T;
    }
}
