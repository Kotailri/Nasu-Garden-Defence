using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BwoStateEnum
{
    Chase,
    Wander,
    Follow
}

[RequireComponent(typeof(Rigidbody2D))]
public class NeggpalBwo : MonoBehaviour
{
    public IBwoState CurrentState;
    public BwoStateEnum CurrentBwoStateEnum;

    [Space(10f)]
    public float StateDuration;

    private float currentStateTime = 0f;
    private int stateRepeatCounter = 0;
    private readonly int stateRepeatMax = 3;

    [HideInInspector]
    public Rigidbody2D RB;
    [HideInInspector]
    public float movespeed;

    private readonly Dictionary<BwoStateEnum, IBwoState> states = new() 
    {
        { BwoStateEnum.Chase, new BwoChaseState() },
        { BwoStateEnum.Wander, new BwoWanderState() },
        { BwoStateEnum.Follow, new BwoFollowState() }
    };
    private float CumulativeStateChances = 0f;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        foreach (var state in states)
        {
            CumulativeStateChances += state.Value.GetStateChance();
        }

        movespeed = Global.keystoneItemManager.BwoMovespeed;
        ChangeState(states[0]);
    }

    private bool ChangeState(IBwoState state)
    {

        if (state == CurrentState)
        {
            return false;
        }

        CurrentState?.OnStateEnd(this);
        CurrentState = state;
        CurrentState.OnStateStart(this);

        
        return true;
    }

    private IBwoState GetNewStateExluding(BwoStateEnum exclude)
    {
        List<BwoStateEnum> _stateslist = new();
        foreach (var state in states)
        {
            _stateslist.Add(state.Key);
        }
        _stateslist.Remove(exclude);

        return states[_stateslist[Random.Range(0, _stateslist.Count)]];
    }

    private IBwoState GetNewState()
    {
        List<BwoStateEnum> _stateslist = new();
        foreach (var state in states)
        {
            _stateslist.Add(state.Key);
        }

        BwoStateEnum newBwoStateEnum = _stateslist[Random.Range(0, _stateslist.Count)];
        IBwoState newIBwoState = states[newBwoStateEnum];

        float randomValue = Random.Range(0f, CumulativeStateChances);
        float cumulativeWeight = 0f;
        foreach (var state in states)
        {
            cumulativeWeight += state.Value.GetStateChance();
            if (randomValue < cumulativeWeight)
            {
                newBwoStateEnum = state.Key;
                newIBwoState = state.Value;
                break;
            }
        }

        CurrentBwoStateEnum = newBwoStateEnum;

        if (newIBwoState == CurrentState) 
        { 
            stateRepeatCounter++;
            if (stateRepeatCounter == stateRepeatMax)
            {
                stateRepeatCounter = 0;
                return GetNewStateExluding(newBwoStateEnum);
            }
        }

        return newIBwoState;
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        currentStateTime += Time.deltaTime;
        CurrentState.OnStateUpdate(this);

        if (currentStateTime > StateDuration || CurrentState.IsEndStateTriggered()) 
        {
            currentStateTime = 0;
            ChangeState(GetNewState());
        }
    }
}

public interface IBwoState
{
    public void OnStateStart(NeggpalBwo bwo);
    public void OnStateUpdate(NeggpalBwo bwo);
    public void OnStateEnd(NeggpalBwo bwo);
    public bool IsEndStateTriggered();
    public float GetStateChance();
}

public class BwoChaseState : IBwoState
{
    GameObject currentEnemyTarget;
    float DistanceToTarget = 0.5f;
    float FrontalDistance = 4f;

    public void OnStateStart(NeggpalBwo bwo)
    {
        Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
        float minDistance = Mathf.Infinity;
        GameObject currentEnemy = null;
        foreach (GameObject enemy in Global.GetActiveEnemies())
        {
            float dist = Vector2.Distance(enemy.transform.position, bwo.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                currentEnemy = enemy;
            }
        }

        currentEnemyTarget = currentEnemy;
    }

    public void OnStateEnd(NeggpalBwo bwo) { }

    public void OnStateUpdate(NeggpalBwo bwo)
    {
        if (currentEnemyTarget == null)
        {
            bwo.RB.velocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(bwo.gameObject.transform.position, currentEnemyTarget.transform.position - new Vector3(FrontalDistance,0,0)) > DistanceToTarget)
        {
            bwo.RB.velocity = ((currentEnemyTarget.transform.position - new Vector3(FrontalDistance, 0, 0)) - bwo.transform.position).normalized * bwo.movespeed;
        }
        else
        {
            bwo.RB.velocity = Vector2.zero;
        }

        if (currentEnemyTarget.transform.position.x < bwo.transform.position.x)
        {
            bwo.GetComponent<SpriteRenderer>().flipX = true;
            Global.keystoneItemManager.IsBwoFacingAttackDirection = false;
        }
        else
        {
            Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
            bwo.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public bool IsEndStateTriggered()
    {
        return currentEnemyTarget == null;
    }

    public float GetStateChance()
    {
        return 0.8f;
    }
}

public class BwoWanderState : IBwoState
{
    Vector2 wanderLocation;
    public float DistanceToTarget = 1.0f;
    private bool stateEndTriggered = false;

    private float xWanderRange = 2f;
    private float yWanderRange = 10f;

    public void OnStateStart(NeggpalBwo bwo)
    {
        Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
        bwo.GetComponent<SpriteRenderer>().flipX = false;
        stateEndTriggered = false;
        MoveTowardsNewLocation(bwo);
    }

    public void OnStateEnd(NeggpalBwo bwo) { }

    private void MoveTowardsNewLocation(NeggpalBwo bwo)
    {
        float wanderLocationX = Mathf.Clamp(Random.Range(Global.playerTransform.position.x - xWanderRange, Global.playerTransform.position.x + xWanderRange), 
            Global.XRange.min, Global.XRange.max);

        float wanderLocationY = Mathf.Clamp(Random.Range(Global.playerTransform.position.y - yWanderRange, Global.playerTransform.position.y + yWanderRange),
            Global.YRange.min, Global.YRange.max);
       
        wanderLocation = new Vector2(wanderLocationX, wanderLocationY);
        bwo.RB.velocity = (wanderLocation - (Vector2)bwo.transform.position).normalized * bwo.movespeed;
    }

    public void OnStateUpdate(NeggpalBwo bwo)
    {
        if (Vector2.Distance(bwo.gameObject.transform.position, wanderLocation) <= DistanceToTarget)
        {
            MoveTowardsNewLocation(bwo);
        }
    }

    public bool IsEndStateTriggered()
    {
        return stateEndTriggered;
    }

    public float GetStateChance()
    {
        return 0.05f;
    }
}

public class BwoFollowState : IBwoState
{
    float minDistance = 1.5f;

    public bool IsEndStateTriggered()
    {
        return false;
    }

    public void OnStateEnd(NeggpalBwo bwo) { }

    public void OnStateStart(NeggpalBwo bwo) 
    {
        Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
    }

    public void OnStateUpdate(NeggpalBwo bwo)
    {
        bwo.RB.velocity = ((Vector2)Global.playerTransform.position - (Vector2)bwo.transform.position).normalized * bwo.movespeed;
        if (Vector2.Distance(bwo.gameObject.transform.position, Global.playerTransform.position) <= minDistance)
        {
            bwo.RB.velocity = Vector2.zero;
            bwo.GetComponent<SpriteRenderer>().flipX = false;
            Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
        }
        else
        {
            if (bwo.RB.velocity.x < 0)
            {
                bwo.GetComponent<SpriteRenderer>().flipX = true;
                Global.keystoneItemManager.IsBwoFacingAttackDirection = false;
            }
            else
            {
                bwo.GetComponent<SpriteRenderer>().flipX = false;
                Global.keystoneItemManager.IsBwoFacingAttackDirection = true;
            }
        }
    }

    public float GetStateChance()
    {
        return 0.15f;
    }
}

