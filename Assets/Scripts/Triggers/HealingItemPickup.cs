using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItemPickup : MonoBehaviour, IPickup, IHealingItem
{
    [SerializeField] private int healAmount;
    [SerializeField] GameObject visualCue;
    public int HealAmount { get; set; }

    void Awake()
    {
        HealAmount = healAmount;
    }
    public void OnPickup()
    {
        Destroy(gameObject);
    }

    public void OnDrop()
    {
    }

    public void OnHeal()
    {
        Debug.Log("Healing player");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            if (visualCue != null)
            {
                visualCue.SetActive(true);
            }

            if (InputManager.Interact)
            {
                other.gameObject.GetComponent<PlayerHitbox>().Heal(HealAmount);
                OnHeal();
                OnPickup();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            if (visualCue != null)
            {
                visualCue.SetActive(false);
            }
        }
    }
}
