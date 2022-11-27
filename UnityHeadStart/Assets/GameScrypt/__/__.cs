using GameScrypt.Channel.Inventory;
using GameScrypt.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public static class __
{
    public static Dictionary<object, object> _dict = new Dictionary<object, object>();
    public static IInventoryChannel InventoryChannel { get { return _inventoryChannel; } }
    public static ICore Core
    {
        get
        {
            if (_core == null)
            {
                GameObject go = Object.Instantiate(Resources.Load("Core")) as GameObject;
                go.name = "Core [DDOL]";
                _core = go.GetComponent<ICore>();
            }

            return _core;
        }
    }
    public static ITimeout Timeout { get; set; }

    private static ICore _core;
    private static IInventoryChannel _inventoryChannel;

    public static void SetInventoryChannel(IInventoryChannel inventoryChannel)
    {
        _inventoryChannel = inventoryChannel;
    }
}
