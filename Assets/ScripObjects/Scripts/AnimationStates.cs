using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationStates", menuName = "ScriptableObjects/AnimationStates", order = 1)]
public class SlimeAnimationStates : MonoBehaviour
{
    public AnimationState[] states;

    public AnimationState GetState(string stateName)
    {
        foreach (AnimationState state in states)
        {
            if (state.name == stateName)
            {
                return state;
            }
        }
        return null;
    }
}
