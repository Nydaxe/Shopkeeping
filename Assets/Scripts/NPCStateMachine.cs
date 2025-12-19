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

    [SerializeField] NPCPathingMachine pathing;
    [SerializeField] float idleDuration = 1.5f;

    float idleTimer;
    int roamIndex;

    void Start()
    {
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

    void ChangeState(NPCState newState)
    {
        idleTimer = 0f;
        currentState = newState;
    }

    void UpdateIdle()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            ChangeState(NPCState.Roam);
        }
    }

    void UpdateRoam()
    {
        if (roamingPoints.Count == 0)
            ChangeState(NPCState.Idle);

        if (!pathing.moving)
        {
            Transform target = roamingPoints[roamIndex];
            pathing.Go(target.position);
            pathing.OnFinishedMovement += FinishedRoaming;

            roamIndex = (roamIndex + 1) % roamingPoints.Count;
        }

    }

    void UpdateGoTo()
    {
        //TODO Add shopping code here
    }

    void FinishedRoaming()
    {
        pathing.OnFinishedMovement -= FinishedRoaming;
        ChangeState(NPCState.Idle);
    }

    void OnDisable()
    {
        pathing.OnFinishedMovement -= FinishedRoaming;
    }
}
