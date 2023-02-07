using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class ScriptableCharacter : ScriptableObject
{
    public string characterName;
    public string characterDescription = "Just a usual character";

    public int rarity;

    public float dmg;
    public float hp;
    public float stamina;

    public Sprite characterImage;
}
