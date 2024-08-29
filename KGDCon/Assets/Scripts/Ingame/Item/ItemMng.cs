using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemMng : SerializedMonoBehaviour
{
    private Dictionary<EItem, Queue<GameItem>> itemQueue = new Dictionary<EItem, Queue<GameItem>>();
    [SerializeField] private Dictionary<EItem, GameItem> item = new Dictionary<EItem, GameItem>();
    public List<GameItem> gameItem { private set; get; } = new List<GameItem>();

    public static ItemMng Instance => _instance;
    private static ItemMng _instance;

    protected void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public GameItem CreateItem(EItem pItem)
    {
        if (itemQueue.ContainsKey(pItem) == false)
            itemQueue[pItem] = new Queue<GameItem>();

        if(itemQueue[pItem].Count > 0)
        {
            return itemQueue[pItem].Dequeue();
        }

        GameItem newItem = Instantiate(item[pItem]);
        gameItem.Add(newItem);

        return newItem;
    }

    public void RemoveItem(EItem pItem, GameItem gameItem)
    {
        if (itemQueue.ContainsKey(pItem) == false)
            itemQueue[pItem] = new Queue<GameItem>();
        itemQueue[pItem].Enqueue(gameItem);
        gameItem.gameObject.SetActive(false);
    }
}
