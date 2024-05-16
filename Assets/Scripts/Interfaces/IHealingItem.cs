using UnityEngine;

public interface IHealingItem
{
    int HealAmount { get; set; }
    void OnHeal();
}