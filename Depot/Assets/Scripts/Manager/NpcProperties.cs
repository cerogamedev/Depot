using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName ="NPCs")]
public class NpcProperties : ScriptableObject
{
    public string npcName;
    public string npcStory;

    public int Power;
    public int Speed;
    public int Clumsy;
    public int Focus;
    public int Organisation;
    public int Loyalty;

}
