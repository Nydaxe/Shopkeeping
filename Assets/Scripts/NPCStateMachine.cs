using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NPCStateMachine : MonoBehaviour
{
    public enum NPCState
    {
        Idle,
        Roam,
        GoTo
    }

    public NPCState currentState;

    public List<Transform> roamingPoints;

    public NPCPathingMachine pathing;
    [SerializeField] float idleDuration = 1.5f;

    public Tile tileToGoTo;

    public bool allowRoaming;

    float idleTimer;
    int roamIndex;

    void Start()
    {
        allowRoaming = true;
        ChangeState(NPCState.Idle);
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                UpdateIdle();
                break;

            case NPCState.Roam:
                UpdateRoam();
                break;

            case NPCState.GoTo:
                UpdateGoTo();
                break;
        }
    }

    public void ChangeState(NPCState newState)
    {
        idleTimer = 0f;
        currentState = newState;

        if (currentState == NPCState.Roam && roamingPoints.Count > 0)
        {
            Transform target = roamingPoints[roamIndex];
            pathing.OnFinishedMovement += FinishedRoaming;
            pathing.Go(target.position);

            roamIndex = (roamIndex + 1) % roamingPoints.Count;
        }

        if (currentState == NPCState.GoTo && tileToGoTo != null)
        {
            pathing.OnFinishedMovement += FinishedGoing;
            pathing.Go(tileToGoTo.centerPosition);
        }
    }

    void UpdateIdle()
    {
        if(allowRoaming == false)
        {
            return;
        }
        
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            ChangeState(NPCState.Roam);
        }
    }

    void UpdateGoTo()
    {

    }

    void UpdateRoam()
    {

    }

    void FinishedRoaming()
    {
        pathing.OnFinishedMovement -= FinishedRoaming;
        ChangeState(NPCState.Idle);
    }

    void FinishedGoing()
    {
        pathing.OnFinishedMovement -= FinishedRoaming;
        ChangeState(NPCState.Idle);
    }

    void OnDisable()
    {
        pathing.OnFinishedMovement -= FinishedRoaming;
    }
}
