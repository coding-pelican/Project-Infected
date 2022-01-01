using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public bool isPlayerMeleeAttacking = false;//근접공격중?
    [Range(0, 5)]
    public float meleeAttackDelayTime;//후딜
    [Range(0, 200)]
    public int meleeDamage = 100;//근접공격력

    IEnumerator readyToMeleeAttackCoroutine;

    private void Start() {
        readyToMeleeAttackCoroutine = ReadyToMeleeAttack();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.instance.isPlayerDied || !Input.GetMouseButton(0) || isPlayerMeleeAttacking)
        {
            return;
        }
        isPlayerMeleeAttacking = true;
        StartCoroutine(readyToMeleeAttackCoroutine);
        int randomDamage = Random.Range(-meleeDamage / 2, meleeDamage / 2 + 1);
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(meleeDamage + randomDamage);
        }
        Debug.Log("Attack");
    }
    IEnumerator ReadyToMeleeAttack()
    {
        yield return new WaitForSeconds(meleeAttackDelayTime);
        isPlayerMeleeAttacking = false;
        Debug.Log("Attack Ready");
    }
}