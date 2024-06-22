using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BwoStateEnum
{
    Chase,
    Idle,
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
    private readonly int stateRepeatMax = 2;

    public Rigidbody2D RB;
    public float movespeed;

    private readonly Dictionary<BwoStateEnum, IBwoState> states = new() 
    {
        //{ BwoStateEnum.Idle, new BwoIdleState() },
        { BwoStateEnum.Chase, new BwoChaseState() },
        { BwoStateEnum.Wander, new BwoWanderState() },
        { BwoStateEnum.Follow, new BwoFollowState() }
    };

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
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
}

public class BwoIdleState : IBwoState
{
    public void OnStateStart(NeggpalBwo bwo)
    {
        bwo.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Global.keystoneItemManager.CanBwoShoot = true;
    }

    public void OnStateEnd(NeggpalBwo bwo) { }
    public void OnStateUpdate(NeggpalBwo bwo) { }

    public bool IsEndStateTriggered()
    {
        return false;
    }
}

public class BwoChaseState : IBwoState
{
    GameObject currentEnemyTarget;
    float DistanceToTarget = 0.5f;
    float FrontalDistance = 2f;

    public void OnStateStart(NeggpalBwo bwo)
    {
        Global.keystoneItemManager.CanBwoShoot = true;
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
        }
        else
        {
            bwo.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public bool IsEndStateTriggered()
    {
        return currentEnemyTarget == null;
    }
}

public class BwoWanderState : IBwoState
{
    Vector2 wanderLocation;
    public float DistanceToTarget = 1.0f;
    private bool stateEndTriggered = false;

    public void OnStateStart(NeggpalBwo bwo)
    {
        Global.keystoneItemManager.CanBwoShoot = true;
        stateEndTriggered = false;
        MoveTowardsNewLocation(bwo);
    }

    public void OnStateEnd(NeggpalBwo bwo) 
    {
        //bwo.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void MoveTowardsNewLocation(NeggpalBwo bwo)
    {
        wanderLocation = new Vector2(Random.Range(Global.XRange.min, Global.XRange.max), Random.Range(Global.YRange.min, Global.YRange.max));
        bwo.RB.velocity = (wanderLocation - (Vector2)bwo.transform.position).normalized * bwo.movespeed;
    }

    public void OnStateUpdate(NeggpalBwo bwo)
    {
        if (Vector2.Distance(bwo.gameObject.transform.position, wanderLocation) <= DistanceToTarget)
        {
            //stateEndTriggered = true;
            MoveTowardsNewLocation(bwo);
        }

        if (bwo.RB.velocity.x < 0)
        {
            bwo.GetComponent<SpriteRenderer>().flipX = true;
            Global.keystoneItemManager.CanBwoShoot = false;
        }
        else
        {
            bwo.GetComponent<SpriteRenderer>().flipX = false;
            Global.keystoneItemManager.CanBwoShoot = true;
        }
    }

    public bool IsEndStateTriggered()
    {
        return stateEndTriggered;
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
        Global.keystoneItemManager.CanBwoShoot = true;
    }

    public void OnStateUpdate(NeggpalBwo bwo)
    {
        bwo.RB.velocity = ((Vector2)Global.playerTransform.position - (Vector2)bwo.transform.position).normalized * bwo.movespeed;
        if (Vector2.Distance(bwo.gameObject.transform.position, Global.playerTransform.position) <= minDistance)
        {
            bwo.RB.velocity = Vector2.zero;
            bwo.GetComponent<SpriteRenderer>().flipX = false;
            Global.keystoneItemManager.CanBwoShoot = true;
        }
        else
        {
            if (bwo.RB.velocity.x < 0)
            {
                bwo.GetComponent<SpriteRenderer>().flipX = true;
                Global.keystoneItemManager.CanBwoShoot = false;
            }
            else
            {
                bwo.GetComponent<SpriteRenderer>().flipX = false;
                Global.keystoneItemManager.CanBwoShoot = true;
            }
        }
    }
}

