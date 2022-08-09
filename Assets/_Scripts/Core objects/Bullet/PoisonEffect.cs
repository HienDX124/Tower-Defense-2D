using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class PoisonEffect : MonoBehaviour
{
    [SerializeField] private int poisonDur;
    [SerializeField] private float damageEachTime;
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
        do
        {
            Debug.LogWarning("Damage poison");
            yield return ExtensionClass.GetWaitForSeconds(1f);
            enemy.TakeDamage(damageEachTime);
            poisonDur -= 1;
        } while (poisonDur <= 0);
    }

}
