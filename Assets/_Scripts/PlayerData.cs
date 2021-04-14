using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public List<int> itemIds;
    public int currentCheckpoint;
    public float experience;
    public float currentHealth;
    public float currentMana;
    public string characterName;
    public Skin characterSkin;
}

public enum Skin
{
    Human,
    Orc,
    Goblin,
    Hobbit,
    Elf,
    Dwarf
}
