using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Building : MonoBehaviour
{    
    private bool placement;
    public GameObject housePrefab;
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
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y) - 0.27f, 0);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            //Instantiate(housePrefab, transform.position, Quaternion.identity);
        }
        
    }
    Vector3 GetMouseWorldPosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
