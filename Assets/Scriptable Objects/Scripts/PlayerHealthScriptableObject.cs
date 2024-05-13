using UnityEngine;

[CreateAssetMenu]
public class PlayerHealthScriptableObject : ScriptableObject
{

    public float health = 100;

    public void ChangeHealth(float amount)
    {
        health += amount;
    }
}
