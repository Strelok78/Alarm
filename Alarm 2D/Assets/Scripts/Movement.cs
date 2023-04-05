using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed;

    private Animator _animator;
    private float normalYRotation;

    private void Awake()
    {
        normalYRotation = transform.rotation.y;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("IsRuning", Input.GetKey(KeyCode.A));
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y - 180, transform.rotation.z, transform.rotation.w);
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("IsRuning", Input.GetKey(KeyCode.D));
            transform.rotation = new Quaternion(transform.rotation.x, normalYRotation, transform.rotation.z, transform.rotation.w);
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            _animator.SetBool("IsRuning", false);
        }
    }
}