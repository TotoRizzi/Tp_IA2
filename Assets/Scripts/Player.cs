using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _shootingPoint;

    Vector3 _moveVelocity;

    private void Start()
    {
        _myRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckInputs();
        LookAtMouse();
    }
    private void FixedUpdate()
    {
        _myRb.velocity = _moveVelocity;
    }
    void CheckInputs()
    {
        Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")));
        if (Input.GetMouseButtonDown(0)) Attack();
    }
    void Attack()
    {
        Instantiate(_bulletPrefab, _shootingPoint.position, transform.rotation);
    }
    void Move(Vector3 moveInput)
    {
        _moveVelocity = moveInput.normalized * _speed;
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
}
