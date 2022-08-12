using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class PoisonEffect : MonoBehaviour
{
    [SerializeField] private float totalPoisonDur;
    [SerializeField] private float totalDamage;
    [SerializeField] private float timeStep;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            StartCoroutine(CauseDamagePoison(e));
        }
    }

    private IEnumerator CauseDamagePoison(Enemy enemy)
    {
        Debug.LogWarning("CauseDamagePoison");
        do
        {
            Debug.LogWarning("Damage poison");
            yield return ExtensionClass.GetWaitForSeconds(timeStep);
            if (enemy) enemy.TakeDamage(totalDamage * (timeStep / totalPoisonDur));
            totalPoisonDur -= timeStep;
        } while (totalPoisonDur > 0);
    }
}
