using UnityEngine;

public class CarController : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private Vector2 swipe;
    private bool isSwiping;

    private void Update()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
                HandleMouseInput();
        #endif
        GetTouchInput();
    }
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

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            startTouchPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            endTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            endTouchPos = Input.mousePosition;
            swipe = endTouchPos - startTouchPos;
            isSwiping = false;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        if (swipe.magnitude >50) //swipe distance
        {
            float x = swipe.x;
            float y = swipe.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0)
                {
                    Debug.Log("Swipe RIght");
                }
                else
                {
                    Debug.Log("SwipeLeft");
                }
            }
        }
    }
}
