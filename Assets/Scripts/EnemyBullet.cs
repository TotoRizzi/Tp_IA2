using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _damage;

    private void Start()
    {
        Destroy(gameObject, 5);
    }
    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
            player.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
