using UnityEngine;

public class IdleState: IBehaviourState
{
    [SerializeField] private EnemyMovementController movementController;
    public IdleState(EnemyMovementController movementController)
    {
        this.movementController = movementController;
    }
    public void Entry()
    {
        movementController.MoveToRandomPosition();
    }
    public void Update()
    {
        if (movementController.HasPath == false)
        {
            movementController.MoveToRandomPosition();
        }
    }

    public void Exit()
    {
        movementController.ResetPath();
    }
}
