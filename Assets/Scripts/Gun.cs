using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //Private attributes
    private Animator _gunAnimator;
    private SpriteRenderer _gunRenderer;
    private SpriteRenderer _playerSpriteRenderer;
    private Renderer _bulletRender;
    private Player _player;
    //Methods
    private void Start()
    {
        _gunAnimator = GetComponent<Animator>();
        _gunRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>();
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        // Shoot
        if (Input.GetMouseButtonDown(0)) _gunAnimator.SetBool("Shoot", true);
        else _gunAnimator.SetBool("Shoot", false);

        //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; //Because we are in 2d
        Vector3 direction = mousePosition - transform.position; //This is the direction for the gun
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //transform to "º"
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions

        //Gun flip
        if (_playerSpriteRenderer.flipX)
        {
            Vector3 newPosition = new Vector3(_player.transform.position.x - 0.7f, _player.transform.position.y - 0.3f, transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = true;
        }
        else
        {
            Vector3 newPosition = new Vector3(_player.transform.position.x + 0.7f, _player.transform.position.y - 0.3f, transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = false;
        }

        //Shoot
        if (Input.GetMouseButtonDown(0)) Shoot();

    }
    
    private void Shoot ()
    {
        //Make cube
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Size
        bullet.transform.localScale = new Vector2(0.1f, 0.1f);
        //Position
        bullet.transform.position = transform.position + new Vector3 (0.6f, 0.12f,0f);
        //color
        _bulletRender = bullet.GetComponent<Renderer>();
        _bulletRender.material.color = Color.green;

    }

}
