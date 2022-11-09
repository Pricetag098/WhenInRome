using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetType : MonoBehaviour
{
    public enum Type
    {
        player,
        skeleton,
        satyr,
        minotaur
    }
    public Type type;
}
