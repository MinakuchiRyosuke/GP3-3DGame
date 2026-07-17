using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float rotateSpeed = 20;
    [SerializeField] int hp = 2;
    [SerializeField] float invincibleTimeMax = 0.5f;
    [SerializeField] float knockbackSpeed = 5;

    Rigidbody rb;

    public Collider playerCollider { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = playerCollider.bounds.center - rb.position;

        bool isSeenPlayer = true;
        if(Physics.Raycast(rb.position, direction.normalized,
            out var hitInfo))
        {
            if(hitInfo.collider != playerCollider)
            {
                // プレイヤー以外の障害物に当たった場合は見えない1
                isSeenPlayer = false;
            }
        }

        if (isSeenPlayer)
        {
            var subVec = playerCollider.bounds.center - rb.position;
            subVec.y = 0;

            rb.linearVelocity = subVec.normalized * moveSpeed;

            // プレイヤーの方向を向く
            var rotateTarget = subVec.normalized;
            Vector3 forward = transform.forward;
            transform.forward = Vector3.Slerp(forward, rotateTarget,
                rotateSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        var attackObj = collision.gameObject.GetComponent<AttackObject>();
        if (attackObj != null)
        {
            hp -= attackObj.power;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
