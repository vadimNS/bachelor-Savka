using System;
using System.Collections.Generic;

[Serializable]
public class SlotSaveData
{
    public string blockTypeName;   // "Dirt", "Stone", ... або null для порожнього
    public int count;
}

[Serializable]
public class SaveData
{
    public List<SlotSaveData> inventorySlots;
    public int coins;
    public string pickaxeType;     // зберігаємо як рядок, бо JsonUtility не любить enum
}