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
            GameItem item = itemQueue[pItem].Dequeue();
            item.gameObject.SetActive(true);
            return item;
        }

        GameItem newItem = Instantiate(item[pItem]);
        newItem.gameObject.SetActive(true);
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

    public void RemoveAllItem(int pos)
    {
        for(int i = 0; i < gameItem.Count; i++)
        {
            if(gameItem[i].eItem == EItem.Holl_2 && (gameItem[i].pos == pos || gameItem[i].pos == pos-1))
                gameItem[i].DestroyObj();
            else if (gameItem[i].pos == pos)
                gameItem[i].DestroyObj();
        }
    }

    public void RemoveAllItem()
    {
        for (int i = 0; i < gameItem.Count; i++)
            gameItem[i].DestroyObj();
    }
}
