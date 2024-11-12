using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Riddle", menuName = "SoulRiddle")]
public class Riddle : ScriptableObject
{
    public string riddleText;    // Text for the riddle
    public bool isGoodSoul;      // True if the soul is good (heaven), false if bad (hell)
}
