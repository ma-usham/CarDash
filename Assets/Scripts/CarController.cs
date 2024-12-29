using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    private float moveInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.forward *moveInput * speed;
    }
}
