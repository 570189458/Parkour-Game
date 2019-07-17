using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public enum Position
{
    Left,
    Middle,
    Right
}

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public InputDirection inputDirection;
    public Position standPosition;
    public Position fromPosition;
    Vector3 mousePos;
    bool activeInput;
    Vector3 xDirection;
    Vector3 moveDirection;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(UpdateAction());
        standPosition = Position.Middle;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(0, 0, speed*Time.deltaTime));
        moveDirection.z = speed;

        characterController.Move((xDirection * 5 + moveDirection) * Time.deltaTime);
    }

    IEnumerator UpdateAction()
    {
        while (true)
        {
            GetInputDirection();
            //PlayerAnimation();
            MoveLeftRight();
            yield return 0;
        }
    }

    public void MoveLeft()
    {
        if (standPosition != Position.Left)
        {
            GetComponent<Animation>().Stop();
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayTurnLeft;

            xDirection = Vector3.left;

            if (standPosition == Position.Middle)
            {
                standPosition = Position.Left;
                fromPosition = Position.Middle;
            }
            else if (standPosition == Position.Right)
            {
                standPosition = Position.Middle;
                fromPosition = Position.Right;
            }
        }
    }

    public void MoveRight()
    {
        if (standPosition != Position.Right)
        {
            GetComponent<Animation>().Stop();
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayTurnRight;

            xDirection = Vector3.right;

            if (standPosition == Position.Middle)
            {
                standPosition = Position.Right;
                fromPosition = Position.Middle;
            }
            else if (standPosition == Position.Left)
            {
                standPosition = Position.Middle;
                fromPosition = Position.Left;
            }
        }
    }

    void MoveLeftRight()
    {
        if (inputDirection == InputDirection.Left)
        {
            MoveLeft();
        }
        if (inputDirection == InputDirection.Right)
        {
            MoveRight();
        }
        if(standPosition==Position.Left)
        {
            if(transform.position.x<=-1.7f)
            {
                xDirection = Vector3.zero;
                transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
            }
        }

        if(standPosition==Position.Middle)
        {
            if(fromPosition==Position.Left)
            {
                if(transform.position.x>0)
                {
                    xDirection = Vector3.zero;
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                }
            }
            else if(fromPosition==Position.Right)
            {
                if(transform.position.x<0)
                {
                    xDirection = Vector3.zero;
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                }
            }
        }

        if (standPosition == Position.Right)
        {
            if (transform.position.x >= 1.7f)
            {
                xDirection = Vector3.zero;
                transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);
            }
        }
    }

    public void PlayerAnimation()
    {
        if (inputDirection == InputDirection.Left)
        {
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayTurnLeft;
        }
        else if (inputDirection == InputDirection.Right)
        {
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayTurnRight;
        }
        else if (inputDirection == InputDirection.Up)
        {
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayJumpUp;
        }
        else if (inputDirection == InputDirection.Down)
        {
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayRoll;
        }
    }

    public void GetInputDirection()
    {
        inputDirection = InputDirection.None;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && activeInput)
        {
            Vector3 vec = Input.mousePosition - mousePos;
            if (vec.magnitude > 20)
            {
                var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.up)) * Mathf.Rad2Deg;
                var angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.right)) * Mathf.Rad2Deg;
                if (angleY <= 45)
                {
                    inputDirection = InputDirection.Up;
                }
                else if (angleY >= 135)
                {
                    inputDirection = InputDirection.Down;
                }
                else if (angleX <= 45)
                {
                    inputDirection = InputDirection.Right;
                }
                else if (angleX >= 135)
                {
                    inputDirection = InputDirection.Left;
                }
                activeInput = false;
                Debug.Log(inputDirection);

            }
        }
    }
}
