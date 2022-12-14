using System;
using UnityEngine;

//[ExecuteInEditMode]
public enum Faction { ALLY, ENEMY}
public class GridEntity : Entity
{
    public event Action<GridEntity> OnMove = delegate { };
    public bool onGrid;
    public Faction myFaction;
    Renderer _rend;

    [SerializeField] float _attackCD;
    [SerializeField] float _attackRange;
    public string myName;
    [SerializeField] float _viewRange;
    [SerializeField] float _timer;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootingPoint;

    float _currentTimer;
    float _currentAttackCD;
    Player _player;
    SpatialGrid _grid;
    Action _currentBehaviuor;
    public float CurrentLife { get { return _currentHP; } }
    public float MaxLife { get { return _maxHP; } }
    private void Awake()
    {
        _rend = GetComponent<Renderer>();
    }
    protected override void Start()
    {
        base.Start();
        _player = GameManager.Instance.player;
        _grid = FindObjectOfType<SpatialGrid>();
        _grid.AddEntity(this);
        GameManager.Instance.AddGridEntity(this);
        _rend.material.color = myFaction == Faction.ENEMY ? Color.red : Color.blue;
        GameManager.Instance.UpdateGridEntityCurrentLife();

        if (myFaction == Faction.ENEMY)
            _currentBehaviuor = EnemyBehaviour;
        else 
            _currentBehaviuor = delegate { };
    }

    void Update()
    {
        //if (onGrid)
        //    _rend.material.color = Color.red;
        //else
        //    _rend.material.color = Color.gray;

        //Optimization: Hacer esto solo cuando realmente se mueve y no en el update

        _currentBehaviuor();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        GameManager.Instance.UpdateGridEntityCurrentLife();
    }
    void EnemyBehaviour()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < _attackRange)
            Attack();
        else if (Vector3.Distance(_player.transform.position, transform.position) < _viewRange)
            Move();
    }
    private void Attack()
    {
        _currentAttackCD += Time.deltaTime;
        if (_currentAttackCD >= _attackCD)
        {
            Instantiate(_bulletPrefab, _shootingPoint.position, transform.rotation);
            _currentAttackCD = 0;
        }
    }
    public void Move()
    {
        Vector3 dir = _player.transform.position - transform.position;
        transform.position += dir.normalized * _speed * Time.deltaTime;
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));

        _currentTimer += Time.deltaTime;
        if (_currentTimer >= _timer)
        {
            OnMove(this);
            _currentTimer = 0;
        }
    }

    protected override void Die()
    {
        _grid.RemoveEntity(this);
        GameManager.Instance.RemoveGridEntity(this);
        Destroy(gameObject, .1f);
    }
}
