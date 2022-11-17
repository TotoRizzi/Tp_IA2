using UnityEngine;
using UnityEngine.SceneManagement;
using IA2;
public class Player : Entity
{
    public enum PlayerInputs { MOVE, SHOOT, IDLE, DIE }
    private EventFSM<PlayerInputs> _myFsm;

    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _shootingPoint;
    private void Start()
    {
        _myRb = GetComponent<Rigidbody>();

        var idle = new State<PlayerInputs>("IDLE");
        var moving = new State<PlayerInputs>("Moving");
        var shoot = new State<PlayerInputs>("Shoot");
        var die = new State<PlayerInputs>("DIE");

        StateConfigurer.Create(idle)
            .SetTransition(PlayerInputs.MOVE, moving)
            .SetTransition(PlayerInputs.SHOOT, shoot)
            .SetTransition(PlayerInputs.DIE, die)
            .Done(); //aplico y asigno

        StateConfigurer.Create(moving)
            .SetTransition(PlayerInputs.IDLE, idle)
            .SetTransition(PlayerInputs.DIE, die)
            .SetTransition(PlayerInputs.SHOOT, shoot)
            .Done();

        StateConfigurer.Create(shoot)
            .SetTransition(PlayerInputs.IDLE, idle)
            .SetTransition(PlayerInputs.MOVE, moving)
            .SetTransition(PlayerInputs.DIE, die)
            .Done();

        StateConfigurer.Create(die).Done();

        #region IDLE
        idle.OnEnter += x =>
        {
            Debug.Log("Estoy en Idle");
        };

        idle.OnUpdate += () =>
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                SendInputToFSM(PlayerInputs.MOVE);
            else if (Input.GetMouseButtonDown(0))
                SendInputToFSM(PlayerInputs.SHOOT);
        };
        #endregion

        #region MOVING
        moving.OnEnter += x =>
        {
            Debug.Log("Estoy en Moving");
        };

        moving.OnUpdate += () =>
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                SendInputToFSM(PlayerInputs.IDLE);
            else if (Input.GetMouseButtonDown(0))
                SendInputToFSM(PlayerInputs.SHOOT);
        };
        moving.OnFixedUpdate += () =>
        {
            _myRb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized * _speed;

            Debug.Log("EStoy moving");
        };
        #endregion

        #region SHOOTING

        shoot.OnUpdate += () =>
        {
            Attack();
            SendInputToFSM(PlayerInputs.IDLE);
        };

        #endregion

        #region DIE
        die.OnEnter += (x) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        #endregion

        _myFsm = new EventFSM<PlayerInputs>(idle);
    }

    private void Update()
    {
        _myFsm.Update();

        LookAtMouse();
    }

    private void FixedUpdate()
    {
        _myFsm.FixedUpdate();
    }
    void Attack()
    {
        Instantiate(_bulletPrefab, _shootingPoint.position, transform.rotation);
    }
    void LookAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if (groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    private void SendInputToFSM(PlayerInputs inp)
    {
        _myFsm.SendInput(inp);
    }

    protected override void Die()
    {
        SendInputToFSM(PlayerInputs.DIE);
    }
}
