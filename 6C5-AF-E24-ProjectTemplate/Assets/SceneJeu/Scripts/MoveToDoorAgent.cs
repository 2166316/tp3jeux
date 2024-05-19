using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Sentis.Layers;
using UnityEngine;

public class MoveToDoorAgent : Agent
{
    [SerializeField] private Transform targetInterrupteurLocation;
    [SerializeField] private Transform targetDoorLocation1;
    [SerializeField] private Transform targetDoorLocation2;
    [SerializeField] private Renderer targetDoorLocation1Renderer;
    [SerializeField] private Renderer targetDoorLocation2Renderer;

    private float speed = 20;

    [SerializeField] private Material succesMaterial;
    [SerializeField] private Material failureMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Renderer floorRenderer;
    [SerializeField] private Renderer interupteurRenderer;

    private InterupterSpawner interupterSpawner;
    private DoorSpawner doorSpawner;

    private Vector3 doorObjectivePosition;

    private Vector3 lastLocalPostition;

    private void Start()
    {
        interupterSpawner = GetComponentInParent<InterupterSpawner>();
        doorSpawner = GetComponentInParent<DoorSpawner>();
        this.MaxStep = 0;
        
    }

    private void Update()
    {

        if (interupterSpawner.isActivated)
        {
            interupteurRenderer.material.color = Color.green;
        }
        else
        {
            interupteurRenderer.material.color = Color.red;
        }

       // DoorValues doorValues = targetDoorLocation1.GetComponent<DoorValues>();
       
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
        if (other.CompareTag("Interrupteur"))
        {
            if (interupterSpawner.isActivated)
            {
                AddReward(-4f);
                interupterSpawner.isActivated = false;
                //StopCoroutine(givePointsIfGettingCloser());

            }
            else
            {
                AddReward(2f);
                interupterSpawner.isActivated = true;
                if (targetDoorLocation2.GetComponent<DoorValues>().isOpen == true)
                {
                    targetDoorLocation2Renderer.material.color = Color.green;
                }
                else
                {
                    targetDoorLocation1Renderer.material.color = Color.green;
                }
                //StartCoroutine(givePointsIfGettingCloser());


            }
        }

        if (other.CompareTag("Door"))
        {
            if (interupterSpawner.isActivated)
            {
                DoorValues doorValues = other.GetComponent<DoorValues>();
                if (doorValues.isOpen)
                {
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
            AddReward(-4f);
            floorRenderer.material = failureMaterial;
            EndEpisode();
        }
        //Debug.Log(other.tag);
    }

    public override void OnEpisodeBegin()
    {
        StopCoroutine(givePointsIfGettingCloser());
        targetDoorLocation1Renderer.material.color = Color.gray;
        targetDoorLocation2Renderer.material.color = Color.gray;
        //same position
        transform.localPosition = new Vector3(Random.Range(-2f,0f), -4.83f, Random.Range(7f, 8f));

        interupterSpawner.moveInterrupter();
        doorSpawner.moveDoor();

        //reset l'intérupteur
        interupterSpawner.isActivated = false;


        if (targetDoorLocation2.GetComponent<DoorValues>().isOpen == true)
        {
            doorObjectivePosition = targetDoorLocation2.localPosition;
        }
        else
        {
            doorObjectivePosition = targetDoorLocation1.localPosition;
        }
        //StopAllCoroutines();
       // StartCoroutine(givePointsIfGettingCloser());
        //floorRenderer.material = defaultMaterial;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> contActions = actionsOut.ContinuousActions;
        contActions[0] = Input.GetAxisRaw("Horizontal") * 0.2f;
        contActions[1] = Input.GetAxisRaw("Vertical") * 0.2f;
    }

    public IEnumerator givePointsIfGettingCloser()
    {
        /*if ( Vector3.Distance(transform.localPosition, doorObjectivePosition) < Vector3.Distance(lastLocalPostition, doorObjectivePosition))
        {
            AddReward(1f);
        }
        else
        {
            AddReward(-3f);
        }*/

       yield return new WaitForSeconds(0.1f);
        givePointsIfGettingCloser();
        lastLocalPostition = transform.localPosition;
    }
}
