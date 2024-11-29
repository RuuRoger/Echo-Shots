using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    //Public properties
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float speedBullet;
    
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
        // Shoot animation
        if (Input.GetMouseButtonDown(0)) _gunAnimator.SetBool("Shoot", true);
        else _gunAnimator.SetBool("Shoot", false);

        //Without flip
        if (_playerSpriteRenderer.flipX == false)
        {
            //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; //Because we are in 2d
            Vector3 direction = mousePosition - transform.position; //This is the direction for the gun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //transform to "º"
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions

            //Shoot
            if (Input.GetMouseButtonDown(0)) Shoot();

            //Gun position
            Vector3 newPosition = new Vector3(_player.transform.position.x + 0.7f, _player.transform.position.y - 0.3f, transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = false;

        }
        //With flip
        if (_playerSpriteRenderer.flipX)
        {
            //Use mouse to move the gun (This is problem with 2d. I wanted to use LookAt but doesen't works in 2D...)
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; //Because we are in 2d
            Vector3 direction = (mousePosition - transform.position) * (-1); //This is the direction for the gun
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //transform to "º"
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); //Now we can evite use quaternions

            //Shoot
            if (Input.GetMouseButtonDown(0)) ShootWithFlip();

            //Gun position
            Vector3 newPosition = new Vector3(_player.transform.position.x - 0.7f, _player.transform.position.y - 0.3f , transform.position.z);
            transform.position = newPosition;
            _gunRenderer.flipX = true;
        }

    }

    private void Shoot ()
    {
        //Make cube
        GameObject bullet = GameObject.Instantiate(bulletPrefab,gunBarrel.position, gunBarrel.rotation);
        bullet.transform.position += new Vector3(0.2f, 0f, 0f);
        bullet.GetComponent<Rigidbody2D>().AddForce(gunBarrel.right * speedBullet,ForceMode2D.Impulse);
    }

    private void ShootWithFlip()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        bullet.transform.position += new Vector3( - 0.2f, 0f, 0f);
        bullet.GetComponent<Rigidbody2D>().AddForce( - gunBarrel.right * speedBullet, ForceMode2D.Impulse);
    }

}
