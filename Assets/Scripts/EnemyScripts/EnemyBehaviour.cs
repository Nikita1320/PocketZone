using System.Collections.Generic;
using UnityEngine;

public enum TypeState
{
    Idle,
    Agressive
}
public class EnemyBehaviour : MonoBehaviour,IDieListener
{
    [SerializeField] private EnemyCombatSystem combatSystem;
    [SerializeField] private EnemyMovementController movementController;
    [SerializeField] private float detectionDistance;
    [SerializeField] private Health player;
    private Dictionary<TypeState, IBehaviourState> states = new();
    [SerializeField] private TypeState currentState;

    public EnemyCombatSystem CombatSystem { get => combatSystem;  }
    public EnemyMovementController MovementController { get => movementController;  }
    private void Start()
    {
        InitializeState();
    }

    private void Update()
    {
        if (player == null || player.IsDie == true)
        {
            if (currentState != TypeState.Idle)
            {
                ChangeState(TypeState.Idle);
            }
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < detectionDistance)
            {
                if (currentState != TypeState.Agressive)
                {
                    ChangeState(TypeState.Agressive);
                }
            }
        }
        states[currentState].Update();
    }
    public void ChangeState(TypeState nextState)
    {
        if (states[currentState] != null)
        {
            states[currentState].Exit();
        }

        currentState = nextState;

        states[currentState].Entry();
    }
    public void Initialize(Health player)
    {
        this.player = player;
        player.GetComponent<Health>().Died += () => ChangeState(TypeState.Idle);
    }
    private void InitializeState()
    {
        states.Add(TypeState.Idle, new IdleState(movementController));
        states.Add(TypeState.Agressive, new AgressiveState(movementController,combatSystem,player.gameObject));
        SetDefaultState();
    }
    private void SetDefaultState()
    {
        currentState = TypeState.Idle;
        states[currentState].Entry();
    }

    public void OnDie()
    {
        this.enabled = false;
    }
}
