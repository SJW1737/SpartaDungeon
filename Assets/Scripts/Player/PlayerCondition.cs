using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private Condition health;

    [Header("Test Keys")]
    public KeyCode damagekey = KeyCode.K;
    public KeyCode healkey = KeyCode.H;
    public float damageAmount = 10f;
    public float healAmount = 10f;

    private void Start()
    {
        health = uiCondition.health;
    }

    //체력 테스트 코드
    private void Update()
    {
        if (Input.GetKeyDown(damagekey))
            TakeDamage(damageAmount);

        if (Input.GetKeyDown(healkey))
            Heal(healAmount);

        if (health.curValue <= 0f)
            Die();
    }

    public void TakeDamage(float amount)
    {
        health.Subtract(amount);
        if (health.curValue <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    private void Die()
    {
        Debug.Log("사망");
    }
}
