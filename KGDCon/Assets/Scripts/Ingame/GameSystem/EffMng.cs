using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class EffMng : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<EEffect, EffScripts> eff = new Dictionary<EEffect, EffScripts>();

    private Dictionary<EEffect, Queue<EffScripts>> effQueue = new Dictionary<EEffect, Queue<EffScripts>>();
    public List<EffScripts> effList { private set; get; } = new List<EffScripts>();

    public static EffMng Instance => _instance;
    private static EffMng _instance;

    protected void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public EffScripts CreateItem(EEffect pEff)
    {
        if (effQueue.ContainsKey(pEff) == false)
            effQueue[pEff] = new Queue<EffScripts>();

        if (effQueue[pEff].Count > 0)
        {
            EffScripts effScript = effQueue[pEff].Dequeue();
            effScript.gameObject.SetActive(true);
            effScript.transform.name = pEff.ToString();
            return effScript;
        }

        EffScripts newEffect = Instantiate(eff[pEff]);
        newEffect.gameObject.SetActive(true);
        newEffect.transform.name = pEff.ToString();
        effList.Add(newEffect);
        return newEffect;
    }


    public void RemoveAllItem()
    {
        for (int i = 0; i < effList.Count; i++)
        {
            EEffect eEffect = effList[i].effect;
            effList[i].gameObject.SetActive(false);
            if (effQueue.ContainsKey(eEffect) == false)
                effQueue[eEffect] = new Queue<EffScripts>();
            effQueue[eEffect].Enqueue(effList[i]);
        }
    }
}
