using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using UnityEditor.Animations;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,
        MOVE,
        WIN
    }


    public int playerId = 0;
    public Player player; // The Rewired Player
    public float playerSpeed = 20;
    public float rotationSpeed;
    public float slowFactor = 10;
    public LayerMask wallCollisionMask;
    public float drawPathLength = 10f;
    public LineRenderer lineRenderer;

    public float decelLerpSpeed = 3f;
    public float accelLerpSpeed = 10f;
    private Rigidbody myRb;

    public PlayerState playerState = PlayerState.IDLE;

    private int maxReflectionCount = 3;
    private float maxStepDistance = 100;
    // Start is called before the first frame update

    private bool touchStarted = false;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 currentHeading;
    private Vector3 previousHeading;
    private Vector3 inputDirection;
    bool canMove = false;

    public bool stopMoving = false;
    public Animator animator;
    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (playerState)
        {
            case PlayerState.IDLE:
                //StopMove
                //Play Idle animation
                HandleInput();
                animator.SetBool("Run", false);
                break;
            case PlayerState.MOVE:
                HandleCollision();
                HandleInput();
                HandleMovement();
                animator.SetBool("Run", true);
                break;
            case PlayerState.WIN:
                //Stop Move
                //Play Win Animation
                animator.SetBool("Run", false);
                HandleInput();
                HandleMovement();
                break;
            default:
                break;
        }

        
    }


    public void SetTouchStarted(bool value)
    {
        touchStarted = value;
    }

    public void SetStopMoving(bool value)
    {
        stopMoving = value;
    }

    public void OnGameWon()
    {
        playerState = PlayerState.WIN;
    }

    public void DrawPath(Vector3 position, Vector3 direction, int reflectionsRemaining, int index)
    {
        position.y = 0.5f;
        lineRenderer.SetPosition(0, position);
        //lineRenderer.SetPosition(1, position + currentHeading * 5);

        Vector3 direction2 = Vector3.zero;

        // DrawPath(position, direction, reflectionsRemaining - 1, index + 1);

        //Ray ray2 = new Ray(transform.position, previousHeading);
        //RaycastHit hitInfo2;


        //if (Physics.SphereCast(position, 0.2f, direction, out hit, 10)) //(Physics.Raycast(ray2, out hitInfo2, 5f, collisionMask))
        //{

        //}

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.SphereCast(position, 0.35f, direction, out hit, drawPathLength, wallCollisionMask)) //(Physics.Raycast(ray, out hit, 10f))
        {
            direction2 = Vector3.Reflect(direction, hit.normal);
            var hitPoint = hit.point;
            hitPoint.y = 0.5f;
            lineRenderer.SetPosition(1, hitPoint);
            var distance = drawPathLength - (hitPoint - position).magnitude;
            lineRenderer.SetPosition(2, hitPoint + direction2 * (distance + 2.5f));


            //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
            //newRotation.x = 0;
            //newRotation.z = 0;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
        }
        else
        {
            lineRenderer.SetPosition(1, position + direction * drawPathLength / 2);
            lineRenderer.SetPosition(2, position + direction * drawPathLength);

            //var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
            //newRotation.x = 0;
            //newRotation.z = 0;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
        }


    }

    public void HandleInput()
    {
        if(stopMoving)
        {
            inputDirection = Vector3.zero;

            points.Clear();
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            if ((Time.timeScale != 1.0f) && (Time.fixedDeltaTime != 0.01f)) //the game is running in slow motion  
            {
                //reset the values  
                Time.timeScale = 1.0f;
                //Time.fixedDeltaTime = Time.fixedDeltaTime * slowFactorValue;
                Time.fixedDeltaTime = 0.01f;
                //Time.maximumDeltaTime = Time.maximumDeltaTime * slowFactorValue;
                Time.maximumDeltaTime = 0.3333333f;

            }
        }
        else
        {
            inputDirection = new Vector3(player.GetAxisRaw("MoveHorizontal"), 0, player.GetAxisRaw("MoveVertical"));

            float horizontalAxis = player.GetAxisRaw("MoveHorizontal");
            float verticalAxis = player.GetAxisRaw("MoveVertical");

            if (touchStarted)
            {
                if (Time.timeScale == 1.0f)
                {

                    float newTimeScale = Time.timeScale / slowFactor;
                    //assign the 'newTimeScale' to the current 'timeScale'  
                    Time.timeScale = newTimeScale;
                    //proportionally reduce the 'fixedDeltaTime', so that the Rigidbody simulation can react correctly  
                    Time.fixedDeltaTime = Time.fixedDeltaTime / slowFactor;
                    //The maximum amount of time of a single frame  
                    Time.maximumDeltaTime = Time.maximumDeltaTime / slowFactor;


                }

                if (inputDirection != Vector3.zero)
                {
                    canMove = false;
                    //inputDirection = Camera.main.transform.TransformDirection(inputDirection);
                    //inputDirection.Normalize();

                    //inputDirection.y = 0.0f;

                    //currentHeading = inputDirection;


                    //assuming we only using the single camera:

                    lineRenderer.positionCount = 3;

                    var camera = Camera.main;

                    //camera forward and right vectors:
                    var forward = camera.transform.forward;
                    var right = camera.transform.right;

                    //project forward and right vectors on the horizontal plane (y = 0)
                    forward.y = 0f;
                    right.y = 0f;
                    forward.Normalize();
                    right.Normalize();


                    var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

                    currentHeading = desiredMoveDirection.normalized;

                    DrawPath(this.transform.position, currentHeading, maxReflectionCount, 0);
                    
                }

                playerState = PlayerState.MOVE;
            }
            else
            {
                previousHeading = currentHeading;
                canMove = true;
                points.Clear();
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPositions(points.ToArray());
                if ((Time.timeScale != 1.0f) && (Time.fixedDeltaTime != 0.01f)) //the game is running in slow motion  
                {
                    //reset the values  
                    Time.timeScale = 1.0f;
                    //Time.fixedDeltaTime = Time.fixedDeltaTime * slowFactorValue;
                    Time.fixedDeltaTime = 0.01f;
                    //Time.maximumDeltaTime = Time.maximumDeltaTime * slowFactorValue;
                    Time.maximumDeltaTime = 0.3333333f;

                }
            }
        }

        
        
        
    }

    private void HandleCollision()
    {
        if (stopMoving)
        {
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, 0.2f, transform.forward, out hitInfo, 0.5f, wallCollisionMask)) //(Physics.Raycast(ray, out hitInfo, 0.5f, collisionMask))
        {
            // Debug.Log("Hit Body : " + hitInfo.normal);
            OnHitObject(hitInfo);


        }
    }

    private void OnHitObject(RaycastHit hitInfo)
    {
        currentHeading = Vector3.Reflect(currentHeading, hitInfo.normal).normalized;
        previousHeading = Vector3.Reflect(previousHeading, hitInfo.normal).normalized;
    }
    private void HandleMovement()
    {
        if(!stopMoving)
        {
            if (canMove)
            {
                myRb.velocity = currentHeading * playerSpeed;
                //myRb.velocity = Vector3.Slerp(myRb.velocity, currentHeading * playerSpeed, accelLerpSpeed * Time.deltaTime);
                if (currentHeading != Vector3.zero)
                {
                    var newRotation = Quaternion.LookRotation(currentHeading).eulerAngles;
                    newRotation.x = 0;
                    newRotation.z = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
                }


            }
            else
            {
                //myRb.velocity = Vector3.Slerp(myRb.velocity, previousHeading * playerSpeed, accelLerpSpeed * Time.deltaTime);
                myRb.velocity = previousHeading * playerSpeed;
                if (previousHeading != Vector3.zero)
                {
                    var newRotation = Quaternion.LookRotation(previousHeading).eulerAngles;
                    newRotation.x = 0;
                    newRotation.z = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
                }

            }
        }
        else
        {
            myRb.velocity = Vector3.Slerp(myRb.velocity, Vector3.zero, decelLerpSpeed * Time.deltaTime);// previousHeading *  Mathf.Lerp(playerSpeed, 0, 100 * Time.deltaTime);
            Debug.Log("myRB.velocity : " + myRb.velocity);
            if (previousHeading != Vector3.zero)
            {
                var newRotation = Quaternion.LookRotation(previousHeading).eulerAngles;
                newRotation.x = 0;
                newRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationSpeed * Time.deltaTime);
            }
        }
        
            
    }
}
