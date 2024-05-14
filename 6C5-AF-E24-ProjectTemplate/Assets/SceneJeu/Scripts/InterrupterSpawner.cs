using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterupterSpawner : MonoBehaviour
{

    private float z_PositionMin = 0f;
    private float z_PositionMax = 9f;

    private float x_PositionMin = -4f;
    private float x_PositionMax = 4.5f;

    private float y_Position = 0.25f;

    public GameObject interupterPrefab;

    private GameObject interupterActual;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            despawnInterrupter();
            spawnInterrupter();
        }
    }

    private void spawnInterrupter()
    {
        
        interupterActual = Instantiate(interupterPrefab, new Vector3(Random.Range(x_PositionMin,x_PositionMax),y_Position,Random.Range(z_PositionMin,z_PositionMax)), Quaternion.Euler(0, 0, 0));

    }

    private void despawnInterrupter()
    {
        if( interupterActual != null)
        {
            Destroy(interupterActual);
        }
    }
}
