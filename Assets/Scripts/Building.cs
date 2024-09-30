using UnityEngine;
using UnityEngine.InputSystem;

public class Building : MonoBehaviour
{    
    private bool placement;
    void Start()
    {
        placement = true;
    }
    void Update()
    {
        if(placement)
        {
            transform.position = GetMouseWorldPosition();
            if(Input.GetMouseButtonDown(0))
            {
                placement = false;
            }
        }
        
    }
    Vector3 GetMouseWorldPosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
