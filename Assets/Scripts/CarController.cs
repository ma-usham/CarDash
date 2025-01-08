using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private Vector2 swipe;
    private bool isSwiping;

     private const float leftLane = -1.1f;
     private const float centerLane=0f;
     private const float rightLane = 1.1f;

    [SerializeField] private float SmoothFactor;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float tiltAngleZ = 15f; // Tilt angle for z-axis
    [SerializeField] private float tiltAngleY = 10f; // Tilt angle for y-axis
    [SerializeField] private float tiltDuration = 0.2f;

    private void Update()
    {
        GetTouchInput();
        Movement();
    }

    #region MOVEMENT

    void Movement()
    {
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }
    #endregion

    void GetTouchInput()
    {
        Debug.Log("DetectingInput");
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPos = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    endTouchPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPos = touch.position;
                    swipe = endTouchPos - startTouchPos;
                    isSwiping = false;
                    DetectSwipe();
                    break;
            }
        }
    }


    void DetectSwipe()
    {
        if (swipe.magnitude >50) //swipe distance
        {

            if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                if (swipe.x> 0)
                {
                    Debug.Log("rightSwipe");
                    if (transform.position.x == leftLane) SwipeRight(centerLane);
                    if(transform.position.x == centerLane) SwipeRight(rightLane);

                }
                else
                {
                    Debug.Log("LeftSwipe");
                   if(transform.position.x == centerLane) SwipeLeft(leftLane);
                   if(transform.position.x == rightLane) SwipeLeft(centerLane);
                }
            }
        }
    }


    void SwipeLeft(float targetLane)
    {
        transform.DORotate(new Vector3(0, -tiltAngleY, tiltAngleZ), tiltDuration)
        .OnComplete(() =>
        {
            transform.DORotate(Vector3.zero, tiltDuration);
        });

        transform.DOMoveX(targetLane, SmoothFactor);
    }
    void SwipeRight(float targetLane)
    {
        transform.DORotate(new Vector3(0, tiltAngleY, -tiltAngleZ), tiltDuration)
         .OnComplete(() =>
         {
             transform.DORotate(Vector3.zero, tiltDuration);
         });

        transform.DOMoveX(targetLane, SmoothFactor);
    }
}
