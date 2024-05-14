using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalPlayerPosition", menuName = "Scriptable Objects/GlobalPlayerPosition")]
public class GlobalPlayerPosition : ScriptableObject
{
    public static Vector3 PlayerPosition { get; set; } = new Vector3(0, 0, 0);
}
