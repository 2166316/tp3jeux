using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorSpawner : MonoBehaviour
{

    private Vector3 doorScale;

    private Vector3 doorPosition1BeginXAxis;
    private Vector3 doorPosition1EndXAxis;

    private Vector3 doorPosition2BeginXAxis;
    private Vector3 doorPosition2EndXAxis;

    private Vector3 doorPositionBeginYAxisRight;
    private Vector3 doorPositionEndYAxisRight;

    private Vector3 doorPositionBeginYAxisLeft;
    private Vector3 doorPositionEndYAxisLeft;

    public GameObject doorPrefab1;
    public GameObject doorPrefab2;

    void Start()
    {
        //taille porte
        doorScale = new Vector3(3.3f, 3.8f, 0.28f);

        //mur face
        doorPosition1BeginXAxis = new Vector3(-6.5f, -4.1f, 13.79f);
        doorPosition1EndXAxis = new Vector3(-4f, -4.1f, 13.79f);
        doorPosition2BeginXAxis = new Vector3(-0.5f, -4.1f, 13.79f);
        doorPosition2EndXAxis = new Vector3(3.5f, -4.1f, 13.79f);

        //mur droite
        doorPositionBeginYAxisRight = new Vector3(5.27f, -4.1f, 10f);
        doorPositionEndYAxisRight = new Vector3(5.271f, -4.1f, 2f);

        //mur gauche
        doorPositionBeginYAxisLeft = new Vector3(-8.57f, -4.1f, 10f);
        doorPositionEndYAxisLeft = new Vector3(-8.57f, -4.1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveDoor()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 4);
        Vector3 newPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.identity;

        //Debug.Log("test");
        /*(switch (randomNumber)
        {
            //center
            case 1:
                newPosition = new Vector3(Random.Range(doorPositionBeginXAxis.x, doorPositionEndXAxis.x), doorPositionBeginXAxis.y, doorPositionBeginXAxis.z);
                newRotation = Quaternion.Euler(0,0,0);
                break;
            //right
            case 2:
                newPosition =new Vector3(doorPositionBeginYAxisRight.x, doorPositionBeginYAxisRight.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z));
                newRotation = Quaternion.Euler(0, 90, 0);
                break;
            //left
            case 3:
                newPosition =new Vector3(doorPositionBeginYAxisLeft.x, doorPositionBeginYAxisLeft.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z));
                newRotation = Quaternion.Euler(0, 90, 0);
                break;

            default:
                Debug.Log("Erreur: " + randomNumber);
                break;

        }*/
        //left
        newPosition = new Vector3(Random.Range(doorPosition1BeginXAxis.x, doorPosition1EndXAxis.x), doorPosition1BeginXAxis.y, doorPosition1BeginXAxis.z);
        newRotation = Quaternion.Euler(0, 0, 0);
        doorPrefab1.transform.localPosition = newPosition;
        doorPrefab1.transform.localRotation = newRotation;



         random = new System.Random();
        /*if (randomNumber == 1)
        {
            randomNumber = random.Next(2, 4);
        }else if (randomNumber == 2)
        {
            randomNumber = 1;
        }
        else
        {
            randomNumber = random.Next(1, 3);
        }*/
         newPosition = Vector3.zero;
         newRotation = Quaternion.identity;
        /* switch (randomNumber)
         {
             //center
             case 1:
                 newPosition = new Vector3(Random.Range(doorPositionBeginXAxis.x, doorPositionEndXAxis.x), doorPositionBeginXAxis.y, doorPositionBeginXAxis.z);
                 newRotation = Quaternion.Euler(0, 0, 0);
                 break;
             //right
             case 2:
                 newPosition = new Vector3(doorPositionBeginYAxisRight.x, doorPositionBeginYAxisRight.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z));
                 newRotation = Quaternion.Euler(0, 90, 0);
                 break;
             //left
             case 3:
                 newPosition = new Vector3(doorPositionBeginYAxisLeft.x, doorPositionBeginYAxisLeft.y, Random.Range(doorPositionEndYAxisLeft.z, doorPositionBeginYAxisLeft.z));
                 newRotation = Quaternion.Euler(0, 90, 0);
                 break;

             default:
                 Debug.Log("Erreur: " + randomNumber);
                 break;

         }*/
        //right
        newPosition = new Vector3(Random.Range(doorPosition2BeginXAxis.x, doorPosition2EndXAxis.x), doorPosition2EndXAxis.y, doorPosition2EndXAxis.z);
        newRotation = Quaternion.Euler(0, 0, 0);

        doorPrefab2.transform.localPosition = newPosition;
        doorPrefab2.transform.localRotation = newRotation;
        // Debug.Log("Rotation: " + doorActual.transform.rotation.eulerAngles);

        int randomNumberD = random.Next(1, 3);
        DoorValues door2Val = doorPrefab2.GetComponent<DoorValues>();
        DoorValues door1Val = doorPrefab1.GetComponent<DoorValues>();
        Renderer door2renderer = doorPrefab2.GetComponent<Renderer>();
        Renderer door1renderer = doorPrefab1.GetComponent<Renderer>();
        /* switch (randomNumberD)
         {
             case 1:
                 door1Val.isOpen = true;
                 door2Val.isOpen = false;
                 //door2renderer.material.color = Color.grey;
                 //door1renderer.material.color = Color.green;
                 break;

             case 2:
                 door1Val.isOpen = false;
                 door2Val.isOpen = true;
                 //door2renderer.material.color = Color.green;
                // door1renderer.material.color = Color.grey;
                 break;

             default:
                 Debug.Log("Erreur: " + randomNumberD);
                 break;
         }*/
        door1Val.isOpen = false;
        door2Val.isOpen = true;
    }

   /* private IEnumerator wait2Seconds()
    {
        spawnDoor();
        yield return new WaitForSeconds(3f);

    }*/
}