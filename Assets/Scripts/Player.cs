using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Public Properties
    public float speedPlayer;

    //Private attributes
    private Animator _animatorPlayer;
    private SpriteRenderer _spriteRendererPlayer;
    private int _livesPlayer;

    //Methods
    private void Start()
    {
        _animatorPlayer = GetComponent<Animator>();
        _spriteRendererPlayer = GetComponent<SpriteRenderer>();
        _livesPlayer = 5;
    }
    private void Update()
    {
        //Player move
        transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * speedPlayer);
        transform.Translate(Vector2.up * Input.GetAxis("Vertical") * Time.deltaTime * speedPlayer);

        //Player animation walking
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) _animatorPlayer.SetBool("Walk", true);
        else _animatorPlayer.SetBool("Walk",false);

        //Active Flip
        if (Input.GetAxis("Horizontal") < 0) _spriteRendererPlayer.flipX = true;
        if (Input.GetAxis("Horizontal") > 0) _spriteRendererPlayer.flipX = false;

        //Game Over
        if (_livesPlayer <= 0) Time.timeScale = 0;

        //UI lives
        Debug.Log("Vidas :" + _livesPlayer);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Green") _livesPlayer--;
        if (collision.gameObject.tag == "Red") _livesPlayer -= 2;

    }

}
