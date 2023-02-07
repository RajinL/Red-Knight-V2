using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine damageRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    public void Damage()
    {
        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
        }

        damageRoutine = StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {

        spriteRenderer.material = damageMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        damageRoutine = null;
    }
}
