using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterupterSpawner : MonoBehaviour
{

    private float z_PositionMin = 7f;
    private float z_PositionMax = 4f;

    private float x_PositionMin = -4f;
    private float x_PositionMax = 0f;

    private float y_Position = -6;

    public GameObject interupterPrefab;

    public bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveInterrupter()
    {
        Vector3 newPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.identity;
        newPosition = new Vector3(Random.Range(x_PositionMin, x_PositionMax), y_Position, Random.Range(z_PositionMin, z_PositionMax));
        newRotation = Quaternion.Euler(0, 90, 0);
        interupterPrefab.transform.localPosition = newPosition;
       
    }

  
}