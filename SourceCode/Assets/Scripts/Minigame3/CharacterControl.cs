using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 3f;
    public float jumpForce = 100f;
    public float checkDistance = 4.6f;
    public float changeSpeed = 1f;

    public float runInterval = 1f;
    public float runIntervalTime = 0.1f;

    public float cameraSpeed = 2f;

    public Button leftButton;
    public Button rightButton;


    bool onGround = true;
    bool isJump = false;
    bool isChange = false;
    bool turnZoneLock = false;

    Vector3 _newPos;

    [HideInInspector]
    public Vector3 newPos;
    public bool change = false;

    Vector3 _pos, _rightVector;
    [HideInInspector]
    public Vector3 _direction;

    [HideInInspector]
    public bool checkOpen = false;

    [HideInInspector]
    public Vector3 finalPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        

        _pos = Vector3.zero;
        _newPos = Vector3.zero;
        newPos = transform.position;
        _direction = transform.forward;
    }

    private void Update()
    {
        //newPos = transform.position;
        
        if (Input.GetButtonDown("Jump") && onGround && !turnZoneLock)
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && !turnZoneLock && onGround)
        {
           // Debug.Log("Turnleft");
            TurnLeftRight(true);
        }

        if (Input.GetKeyDown(KeyCode.D) && !turnZoneLock && onGround)
        {
           // Debug.Log("Turnright");
            TurnLeftRight(false);
        }

        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * speed);
        transform.forward = Vector3.Lerp(transform.forward, _direction, cameraSpeed * Time.deltaTime);
    
        finalPos = transform.position;
        if (!change) {
            finalPos.x = Mathf.Lerp(transform.position.x, newPos.x, Time.deltaTime * changeSpeed);
            
        }
        else
        {
            finalPos.z = Mathf.Lerp(transform.position.z, newPos.z, Time.deltaTime * changeSpeed);
            
        }

        //float xIn = finalPos.y + (runInterval + runInterval * Mathf.Sin(Time.deltaTime * runIntervalTime)) % runInterval;
        //finalPos.y = Mathf.Lerp(finalPos.y, xIn, Time.deltaTime * runIntervalTime);

        transform.position = finalPos;
    }

    private void FixedUpdate()
    {
        // make object jump
        if (isJump)
        {
            rb.AddForce(jumpForce * transform.up, ForceMode.Impulse);

            onGround = false;
            isJump = false;
        }
    }

    void TurnLeftRight(bool left)
    {
        LayerMask layerMask = 1 << 8;

        if (!left)
        {
            if (!Physics.Raycast(transform.position, transform.right, checkDistance, layerMask))
            {
                Debug.Log("goRight");
                TurnTo(left);
            }

        }

        else
        {
            
            if (!Physics.Raycast(transform.position, -transform.right, checkDistance, layerMask))
            {
                Debug.Log("goLeft");
                TurnTo(left);
            }

        }

    }

    void TurnTo(bool left)
    {
        if (!left) _newPos = transform.right * checkDistance;
        else _newPos = -transform.right * checkDistance;

        newPos += _newPos;
    }

    void Restart()
    {
        // run animation
        RunningGameManager.rgm.Restart();

    }

    private void OnTriggerStay(Collider other)
    {
        


        if (other.tag == "TurnZone")
        {
            // dislay UI
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);

            turnZoneLock = true;

            Vector3 _desPos = other.gameObject.transform.position;
            _desPos.y = transform.position.y;
            if (change)
                _desPos.z = transform.position.z;
            else
                _desPos.x = transform.position.x;


            Vector3 rightVector = other.gameObject.transform.right;

            // turn direction
            if (Input.GetKeyDown(KeyCode.A))
            {
                TurnDirection(true,_desPos,rightVector);   
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                TurnDirection(false, _desPos,rightVector);
            }

            
        }

    }

    void TurnDirection(bool isLeft, Vector3 pos, Vector3 rightVector)
    {
       
        _pos = pos;
        _rightVector = rightVector;
        if (isLeft)
        {
            _rightVector = -_rightVector;
            leftButton.OnSelect(null);
            
        }
        else
        {
            rightButton.OnSelect(null);
        }


        checkOpen = isLeft;
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
        turnZoneLock = false;

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
            onGround = true;

        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Happen");
            // run fall animation
            Restart();
        }

        if (collision.gameObject.layer == 8)
        {
            Debug.Log("Happen");
            Restart();
        }


    }

    public void StartChangePosition()
    {
        Debug.Log("Did change");

        if (_pos == Vector3.zero) return;

        transform.position = _pos;
        _direction = _rightVector;

        _pos = Vector3.zero;
        newPos = transform.position;

        change = !change;
    }




}
