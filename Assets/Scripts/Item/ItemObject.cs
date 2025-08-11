using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        return $"{data.displayName}\n{data.description}";
    }

    public void OnInteract()
    {
        var player = CharacterManager.Instance.Player;
        player.itemData = data;
        player.UseCurrentItem();
        Destroy(gameObject);
    }
}
