using System;
using System.Collections.Generic;

[Serializable]
public class SlotSaveData
{
    public string blockTypeName;   
    public int count;
}

[Serializable]
public class SaveData
{
    public List<SlotSaveData> inventorySlots;
    public int coins;
    public string pickaxeType;     
}