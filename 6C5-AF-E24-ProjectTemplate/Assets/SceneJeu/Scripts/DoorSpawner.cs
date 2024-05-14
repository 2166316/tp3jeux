using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorSpawner : MonoBehaviour
{

    private Vector3 doorScale;

    private Vector3 doorPositionBeginXAxis;
    private Vector3 doorPositionEndXAxis;

    private Vector3 doorPositionBeginYAxisRight;
    private Vector3 doorPositionEndYAxisRight;

    private Vector3 doorPositionBeginYAxisLeft;
    private Vector3 doorPositionEndYAxisLeft;

    private Quaternion doorRotation;

    public GameObject doorPrefab;

    private GameObject doorActual;  
    

    void Start()
    {
        

        //taille porte
        doorScale = new Vector3 (3.3f, 3.8f, 0.28f);

        //mur face
        doorPositionBeginXAxis = new Vector3 (-4.5f, 2.4284f, 11.11f);
        doorPositionEndXAxis = new Vector3 (5f, 2.4284f, 11.11f);

        //mur droite
        doorPositionBeginYAxisRight = new Vector3(6.91f, 2.4284f, 8f);
        doorPositionEndYAxisRight = new Vector3(6.91f, 2.4284f, -2f);

        //mur gauche
        doorPositionBeginYAxisLeft = new Vector3(-6.83f, 2.4284f, 8f);
        doorPositionEndYAxisLeft = new Vector3(-6.83f, 2.4284f, -2f);

        //rotation porte
        doorRotation = new Quaternion(0f,90f,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) {
            despawnDoor();
            spawnDoor();
            wait2Seconds();
        }
    }

    private void spawnDoor()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 4);
        switch (randomNumber)
        {
            //center
            case 1:
                doorActual = Instantiate(doorPrefab, new Vector3(Random.Range(doorPositionBeginXAxis.x, doorPositionEndXAxis.x), doorPositionBeginXAxis.y, doorPositionBeginXAxis.z), Quaternion.Euler(0, 0, 0));
                break;
            //right
            case 2:
                doorActual = Instantiate(doorPrefab, new Vector3(doorPositionBeginYAxisRight.x, doorPositionBeginYAxisRight.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z)), Quaternion.Euler(0, 90, 0));
                break;
            //left
            case 3:
                doorActual = Instantiate(doorPrefab, new Vector3(doorPositionBeginYAxisLeft.x, doorPositionBeginYAxisLeft.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z)), Quaternion.Euler(0, 90, 0));
                break;

            default:
                Debug.Log("Erreur: " + randomNumber);
                break;

        }
       // Debug.Log("Rotation: " + doorActual.transform.rotation.eulerAngles);
    }

    private void despawnDoor()
    {
        Destroy(doorActual);
    }

    private IEnumerator wait2Seconds()
    {
        spawnDoor();
        yield return new WaitForSeconds(3f);

    }
}
