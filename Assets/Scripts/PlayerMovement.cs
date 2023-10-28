using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] GameObject playerSprite;
    [SerializeField] Animator playerAnimator;
    Rigidbody2D body;
    bool up = false;
    bool left = false;
    bool right = false;
    bool down = false;
    Vector2 direction;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        up = Input.GetButton("Up");
        left = Input.GetButton("Left");
        right = Input.GetButton("Right");
        down = Input.GetButton("Down");
        if (up && direction.y != 1)
            Up();
        if(!up && direction.y == 1)
            Down();
        if (down && direction.y != -1)
            direction += -Vector2.up;
        if (!down && direction.y == -1)
            direction += Vector2.up;
        if(right && direction.x != 1)
            direction += Vector2.right;
        if (!right && direction.x == 1)
            direction += -Vector2.right;
        if (left && direction.x != -1)
            direction += -Vector2.right;
        if (!left && direction.x == -1)
            direction += Vector2.right;

        if (direction.x > 0)
            playerSprite.transform.localScale = new Vector3(Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
        if(direction.x < 0)
            playerSprite.transform.localScale = new Vector3(-Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
    }

    public void Up()
    {
        direction += Vector2.up;
    }

    public void Down()
    {
        direction += Vector2.down;
    }

    public void Left()
    {
        direction += Vector2.left;
    }

    public void Right()
    {
        direction += Vector2.right;
    }


    private void FixedUpdate()
    {
        body.velocity = direction.normalized * speed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
    }
}
