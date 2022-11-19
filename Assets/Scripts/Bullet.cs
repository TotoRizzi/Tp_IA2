using UnityEngine;
using System.Linq;
public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    [SerializeField] float _radius;
    [SerializeField] float _timer;

    float _currentTimer;
    SpatialGrid _grid;
    private void Start()
    {
        _grid = FindObjectOfType<SpatialGrid>();
    }
    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _currentTimer += Time.deltaTime;
        if (_currentTimer >= _timer)
            Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
    void Explode()
    {
        var col = _grid.Query(
                transform.position + new Vector3(-_radius, 0, -_radius),
                transform.position + new Vector3(_radius, 0, _radius),
                x =>
                {
                    var position2d = x - transform.position;
                    position2d.y = 0;
                    return position2d.sqrMagnitude < _radius * _radius;
                }).Where(x => x.myRole == ROLE.TANK && x.myFaction == Faction.ENEMY).OrderBy(x => x.CurrentLife).Take(3);

        if (col.Any())
        {
            foreach (var item in col)
            {
                item.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }
}
