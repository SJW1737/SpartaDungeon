using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;

    private Coroutine speedBoostCo;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    public void UseCurrentItem()
    {
        if (itemData == null) return;
        if (itemData.speedBoost == null) return;

        ApplySpeedBoost(itemData.speedBoost.duration, itemData.speedBoost.multiplier);

        itemData = null;
    }

    private void ApplySpeedBoost(float duration, float multiplier)
    {
        if (controller == null) return;
        if (speedBoostCo != null) StopCoroutine(speedBoostCo);

        speedBoostCo = StartCoroutine(SpeedBoostRountin(duration, multiplier));
    }

    private IEnumerator SpeedBoostRountin(float duration, float multiplier)
    {
        float original = controller.moveSpeed;
        controller.moveSpeed = original * multiplier;

        yield return new WaitForSeconds(duration);

        controller.moveSpeed = original;
        speedBoostCo = null;
    }
}
