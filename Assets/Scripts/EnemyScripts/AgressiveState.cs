using UnityEngine;

public class AgressiveState : IBehaviourState
{
    private EnemyMovementController movementController;
    private EnemyCombatSystem combatSystem;
    private GameObject target;
    public AgressiveState(EnemyMovementController movementController, EnemyCombatSystem combatSystem, GameObject target)
    {
        this.movementController = movementController;
        this.combatSystem = combatSystem;
        this.target = target;
    }
    public void Entry()
    {
        movementController.Move(target);
    }
    public void Update()
    {
        if (Vector2.Distance(target.transform.position, combatSystem.transform.position) < 1.5f)
        {
            if (combatSystem.IsAttacking == false)
            {
                combatSystem.Attack();
                movementController.ResetPath();
            }
        }
        else
        {
            if (movementController.HasPath == false)
            {
                if (combatSystem.IsAttacking == false)
                {
                    movementController.Move(target);
                }
            }
        }
    }

    public void Exit()
    {

    }
}
