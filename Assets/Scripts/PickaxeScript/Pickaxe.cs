using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickaxeType
{
    Wooden,
    Copper,
    Stone,
    Iron,
    Diamond,
    Crystal,
    Emerald,
    Frost,
    Magma,
    Obisidian,
    Ruby,
    Sand,
    Shadow,
    Sun,
    Void
}
public class Pickaxe
{
    public PickaxeType Type { get; }
    public float Power { get; }
    public float Interval { get; }
    public int Price { get; }

    public Pickaxe(PickaxeData data)
    {
        Type = data.type;
        Power = data.power;
        Interval = data.interval;
        Price = data.price;
    }
}

