
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    public float impulseForce = 3f;

    private bool ignoreNextCollision = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.singleton.Addscore(1);

        if (ignoreNextCollision)
        {
            return;
        }

        rb.linearVelocity = Vector3.zero;

        rb.AddForce(
            Vector3.up * impulseForce,
            ForceMode.Impulse
        );

        ignoreNextCollision = true;

        Invoke(nameof(AllowNextCollision), 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }
}
