using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
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
        Destroy(gameObject);
    }
}
