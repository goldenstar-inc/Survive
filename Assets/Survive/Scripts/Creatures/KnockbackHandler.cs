using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KnockbackHandler : MonoBehaviour
{
    private StateHandler stateHandler;
    private Rigidbody2D rb;
    private HealthHandler healthHandler;
    private ForceMode2D forceMode;
    private float knockbackForce = 5f;
    public void Init(
        Rigidbody2D rb,
        StateHandler stateHandler,
        HealthHandler healthHandler
        )
    {
        this.rb = rb;
        this.stateHandler = stateHandler;
        this.healthHandler = healthHandler;
        healthHandler.OnDamageTaken += ApplyKnockback;
        forceMode = ForceMode2D.Impulse;
    }

    private void ApplyKnockback(int _, int __, HealthComponent ___, Vector3 attackerPosition)
    {
        if (rb != null)
        {
            Vector2 direction = (transform.position - attackerPosition).normalized;
            rb.AddForce(direction * knockbackForce, forceMode);
            stateHandler.SetState(CreatureState.KnockedBack);
            StartCoroutine(RecoverFromKnockback());
        }
    }
    private IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        stateHandler.SetState(CreatureState.Normal);
    }
    private void OnDestroy()
    {
        if (healthHandler != null)
        {
            healthHandler.OnDamageTaken -= ApplyKnockback;
        }
    }
}
