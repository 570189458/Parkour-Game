using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float init_speed;
    private float maxSpeed = 10;
    private float speedAddDistance = 100;
    private float speedAddRate = 0.5f;
    private float speedAddCount;
    public float jumpValue;
    public float gravity;
    public InputDirection inputDirection;
    public Position standPosition;
    public Position fromPosition;
    Vector3 mousePos;
    bool activeInput;
    Vector3 xDirection;
    Vector3 moveDirection;
    CharacterController characterController;
    public bool isRoll = false;

    public bool canDoubleJumo = false;
    bool DoubleJump = false;
    bool isQuickMove = false;
    float saveSpeed;
    float quickMoveDuration = 3;
    public float quickMoveTimeLeft;
    IEnumerator quickMoveCor;

    float magnetDuration = 15;
    public float magnetTimeLeft;
    IEnumerator magnetCor;
    public GameObject MagnetCollider;

    float shoeDuration = 10;
    public float shoeTimeLeft;
    IEnumerator shoeCor;

    float multiplyDuration = 10;
    public float mulipltTimeLeft;
    IEnumerator multiplyCor;

    //public Text statusText;
    public Text Text_Magnet;
    public Text Text_Shoe;
    public Text Text_Star;
    public Text Text_Double;

    public static PlayerControl _instance;

    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        init_speed = 5;
        anim = GetComponent<Animation>();
        characterController = GetComponent<CharacterController>();
        StartCoroutine(UpdateAction());
        standPosition = Position.Middle;
    }

    // Update is called once per frame
    void Update()
    {
        

        //statusText.text = GetTime(shoeTimeLeft);
        UpdateItemTime();
    }

    private void UpdateItemTime()
    {
        Text_Magnet.text = GetTime(magnetTimeLeft);
        Text_Shoe.text = GetTime(shoeTimeLeft);
        Text_Star.text = GetTime(quickMoveTimeLeft);
        Text_Double.text = GetTime(mulipltTimeLeft);
    }

    private string GetTime(float time)
    {
        if (time <= 0)
            return "";
        //return Mathf.RoundToInt(time).ToString();
        return ((int)time+1).ToString()+"s";
    }

    private void SetSpeed(float newSpeed)
    {
        if(newSpeed<=maxSpeed)
        {
            speed = newSpeed;
        }
        else
        {
            speed = maxSpeed;
        }
    }

    private void UpdateSpeed()
    {
        speedAddCount += speed * Time.deltaTime;
        if(speedAddCount>=speedAddDistance)
        {
            SetSpeed(speed + speedAddRate);
            speedAddCount = 0;
        }
    }

    IEnumerator UpdateAction()
    {
        while (GameAttribute._instance.life>0)
        {
            if (GameController._instance.isPlay && !GameController._instance.isPause)
            {
                
                GetInputDirection();
                //PlayerAnimation();
                MoveLeftRight();
                MoveForward();
                UpdateSpeed();
            }
            else
            {
                anim.Stop();
            }
            yield return 0;
        }
        Debug.Log("GameOver");
        speed = 0;
        GameController._instance.isPlay = false;
        AnimationManager._instance.animationHandler = AnimationManager._instance.PlayDead;
        yield return new WaitForSeconds(3);
        UIController._instance.ShowRestartUI();
        UIController._instance.HidePauseUI();
    }

    void MoveForward()
    {
        if(inputDirection==InputDirection.Down)
        {
            AnimationManager._instance.animationHandler = AnimationManager._instance.PlayRoll;
        }
        if(characterController.isGrounded)
        {
            moveDirection = Vector3.zero;
            if(AnimationManager._instance.animationHandler != AnimationManager._instance.PlayRoll&&
                AnimationManager._instance.animationHandler != AnimationManager._instance.PlayTurnLeft&&
                AnimationManager._instance.animationHandler != AnimationManager._instance.PlayTurnRight)
            {
                AnimationManager._instance.animationHandler = AnimationManager._instance.PlayRun;
            }
            if(inputDirection==InputDirection.Up)
            {
                JumpUp();
                if(canDoubleJumo)
                {
                    DoubleJump = true;
                }
            }

        }
        else
        {
            if(inputDirection==InputDirection.Down)
            {
                QuickGround();
            }
            if(inputDirection==InputDirection.Up)
            {
                if(DoubleJump)
                {
                    JumpDouble();
                    DoubleJump = false;
                }
            }
            if(AnimationManager._instance.animationHandler != AnimationManager._instance.PlayJumpUp
                && AnimationManager._instance.animationHandler != AnimationManager._instance.PlayRoll
                && AnimationManager._instance.animationHandler != AnimationManager._instance.PlayDoubleJump)
            {
                AnimationManager._instance.animationHandler = AnimationManager._instance.PlayJumpLoop;
            }

        }

        //transform.Translate(new Vector3(0, 0, speed*Time.deltaTime));
        moveDirection.z = speed;
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move((xDirection * 5 + moveDirection) * Time.deltaTime);
    }

    public void Play()
    {
        GameController._instance.isPause = false;
        GameController._instance.isPlay = true;
        StartCoroutine(UpdateAction());
    }

    public void ResetAll()
    {
        speed = init_speed;
        inputDirection = InputDirection.None;
        activeInput = false;
        standPosition = Position.Middle;
        xDirection = Vector3.zero;
        moveDirection = Vector3.zero;
        isRoll = false;
        canDoubleJumo = false;
        isQuickMove = false;
        quickMoveTimeLeft = 0;
        magnetTimeLeft = 0;
        shoeTimeLeft = 0;
        mulipltTimeLeft = 0;

        gameObject.transform.position = new Vector3(0, 0, -64);
        Camera.main.transform.position = new Vector3(0, 5, -70);
        Debug.Log("Reset");
        AnimationManager._instance.animationHandler = AnimationManager._instance.PlayRun;
    }

    void QuickGround()
    {
        moveDirection.y -= jumpValue * 3;
    }

    void JumpDouble()
    {
        AnimationManager._instance.animationHandler = AnimationManager._instance.PlayDoubleJump;
        moveDirection.y += jumpValue * 1.3f;
    }

    void JumpUp()
    {
        AnimationManager._instance.animationHandler = AnimationManager._instance.PlayJumpUp;

        moveDirection.y += jumpValue;
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



    public void QuickMove()
    {
        if (quickMoveCor != null)
            StopCoroutine(quickMoveCor);
        quickMoveCor = QuickMoveCoroutine();
        StartCoroutine(quickMoveCor);
    }

    public void UseMagnet()
    {
        if(magnetCor!=null)
        {
            StopCoroutine(magnetCor);
        }
        magnetCor = MagnetCoroutine();
        StartCoroutine(magnetCor);
    }

    public void UseShoe()
    {
        if(shoeCor!=null)
        {
            StopCoroutine(shoeCor);
        }
        shoeCor = ShoeCoroutine();
        StartCoroutine(shoeCor);
    }

    public void Multiply()
    {
        if(multiplyCor!=null)
        {
            StopCoroutine(multiplyCor);
        }
        multiplyCor = MultiplyCoroutine();
        StartCoroutine(multiplyCor);
    }

    private bool CanPlay()
    {
        return !GameController._instance.isPause && GameController._instance.isPlay;
    }

    IEnumerator MultiplyCoroutine()
    {
        mulipltTimeLeft = multiplyDuration;
        GameAttribute._instance.multiply = 2;
        while(mulipltTimeLeft>=0)
        {
            if(CanPlay())
                mulipltTimeLeft -= Time.deltaTime;
            yield return null;
        }
        GameAttribute._instance.multiply = 1;
    }

    IEnumerator ShoeCoroutine()
    {
        shoeTimeLeft = shoeDuration;
        PlayerControl._instance.canDoubleJumo = true;
        while(shoeTimeLeft>=0)
        {
            if (CanPlay())
                shoeTimeLeft -= Time.deltaTime;
            yield return null;
        }
        PlayerControl._instance.canDoubleJumo = false;
    }

    IEnumerator MagnetCoroutine()
    {
        magnetTimeLeft = magnetDuration;
        MagnetCollider.SetActive(true);
        while(magnetTimeLeft>=0)
        {
            if (CanPlay())
                magnetTimeLeft -= Time.deltaTime;
            yield return null;
        }
        MagnetCollider.SetActive(false);
    }

    IEnumerator QuickMoveCoroutine()
    {
        quickMoveTimeLeft = quickMoveDuration;
        if(isQuickMove==false)
            saveSpeed = speed;
        speed = 20;
        isQuickMove = true;
        //yield return new WaitForSeconds(quickMoveDuration);
        while(quickMoveTimeLeft>=0)
        {
            if (CanPlay())
                quickMoveTimeLeft -= Time.deltaTime;
            yield return null;
        }
        speed = saveSpeed;
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
                    AudioManager._instance.PlaySlideAudio();
                    inputDirection = InputDirection.Up;
                }
                else if (angleY >= 135)
                {
                    AudioManager._instance.PlaySlideAudio();
                    inputDirection = InputDirection.Down;
                }
                else if (angleX <= 45)
                {
                    AudioManager._instance.PlaySlideAudio();
                    inputDirection = InputDirection.Right;
                }
                else if (angleX >= 135)
                {
                    AudioManager._instance.PlaySlideAudio();
                    inputDirection = InputDirection.Left;
                }
                activeInput = false;
                Debug.Log(inputDirection);

            }
        }
    }
}
