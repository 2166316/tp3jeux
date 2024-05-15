using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveToDoorAgent : Agent
{
    [SerializeField] private Transform targetInterrupteurLocation;
    [SerializeField] private Transform targetDoorLocation1;
    [SerializeField] private Transform targetDoorLocation2;


    private float speed = 20;

    [SerializeField] private Material succesMaterial;
    [SerializeField] private Material failureMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Renderer floorRenderer;

    private InterupterSpawner interupterSpawner;
    private DoorSpawner doorSpawner;

    private void Start()
    {
        interupterSpawner = GetComponentInParent<InterupterSpawner>();
        doorSpawner = GetComponentInParent<DoorSpawner>();
        
    }


    public override void OnActionReceived(ActionBuffers actions)
    {

        interupterSpawner = GetComponentInParent<InterupterSpawner>();

        //First action is X move
        float moveX = actions.ContinuousActions[0];
        //second action is Z move
        float moveZ = actions.ContinuousActions[1];
        //Pour minimiser la durée..
        AddReward(-0.01f);
        //Move!!!
        transform.Translate(new UnityEngine.Vector3(moveX, 0, moveZ) * Time.deltaTime * speed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //the position of the agent in the local space (x,y,z)
        sensor.AddObservation(transform.localPosition);
        //the position of the goal in the local space (x,y,z)
        
        sensor.AddObservation(targetInterrupteurLocation.localPosition);

        sensor.AddObservation(targetDoorLocation1.localPosition);

        sensor.AddObservation(targetDoorLocation2.localPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(interupterSpawner.isActivated);
        Debug.Log(other.tag);

        if (other.CompareTag("Interrupteur"))
        {
            if (interupterSpawner.isActivated)
            {
                AddReward(-10f);
                interupterSpawner.isActivated = false;
            }
            else
            {
                Debug.Log("test1");
                AddReward(5f);
                interupterSpawner.isActivated = true;
            }
        }

        if (other.CompareTag("Door"))
        {
            if (interupterSpawner.isActivated)
            {
                Debug.Log("test2");
                DoorValues doorValues = other.GetComponent<DoorValues>();
                Debug.Log(doorValues.isOpen);
                if (doorValues.isOpen)
                {
                    Debug.Log("test3");
                    AddReward(10f);
                    floorRenderer.material = succesMaterial;
                    EndEpisode();
                }
                else
                {
                    AddReward(-5f);
                    floorRenderer.material = failureMaterial;
                    EndEpisode();
                }
            }
            else
            {
                AddReward(-5f);
                floorRenderer.material = failureMaterial;
                EndEpisode();
            }
        }

        if (other.CompareTag("Wall"))
        {
            AddReward(-5f);
            floorRenderer.material = failureMaterial;
            EndEpisode();
        }
        //Debug.Log(other.tag);
    }

    private IEnumerator wait2Seconds()
    {
        
        yield return new WaitForSeconds(2f);
        floorRenderer.material = defaultMaterial;

    }

    public override void OnEpisodeBegin()
    {
        //same position
        transform.localPosition = new Vector3(Random.Range(-2f,0f), -4.83f, Random.Range(7f, 8f));

        interupterSpawner.moveInterrupter();
        doorSpawner.moveDoor();

        //reset l'intérupteur
        interupterSpawner.isActivated = false;
        //floorRenderer.material = defaultMaterial;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> contActions = actionsOut.ContinuousActions;
        contActions[0] = Input.GetAxisRaw("Horizontal") * 0.2f;
        contActions[1] = Input.GetAxisRaw("Vertical") * 0.2f;
    }
}
