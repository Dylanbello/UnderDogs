using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[AddComponentMenu("AI/AILocomotion")]
public class Ailocomotion : MonoBehaviour
{
    [Header("NavagationMeshAgent")]

    [Tooltip("Nav mesh agent component")]
    public NavMeshAgent navMeshAgent;

    [Space(20)]

    [Header("Movement Actions")]

    [Tooltip("Wait time of every action")]
    public float startWaitTime;

    [Tooltip("Wait time when the enemy detect near the player without seeing")]  
    public float timeToRotate;

    [Tooltip("Walking speed")]
    public float speedWalk;

    [Tooltip("Running speed")]
    public float speedRun;
    
    [Space(15)]

    [Header("Enemy Cone of Vision")]

    [Tooltip("Radius of the enemy view")]
    public float viewRadius;

    [Tooltip("Angle of the enemy view")]
    public float viewAngle = 90;

    [Tooltip("To detect the player with the raycast")]
    public LayerMask playerMask;

    [Tooltip("To detect the obstacules with the raycast")]
    public LayerMask obstacleMask;

    [Tooltip("How many rays will cast per degree")]
    public float meshResolution = 1.0f;

    [Tooltip("Number of iterations to get a better performance of the mesh filter when the raycast hit an obstacule")]
    public int edgeIterations = 4;

    [Tooltip(" Max distance to calcule the a minumun and a maximum raycast when hits something")]
    public float edgeDistance = 0.5f;
 
    [Space(15)]

    public Transform[] waypoints;                   //  All the waypoints where the enemy patrols
    int m_CurrentWaypointIndex;                     //  Current waypoint where the enemy is going to
    Transform playerPos;

    [Space (15)]

    public Image QMSymbol;
    public Image EXSymbol;

    Vector3 playerLastPosition = Vector3.zero;      //  Last position of the player when was near the enemy
    Vector3 m_PlayerPosition;                       //  Last position of the player when the player is seen by the enemy
 
    float m_WaitTime;                               //  Variable of the wait time that makes the delay
    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    bool m_playerInRange;                           //  If the player is in range of vision, state of chasing
    bool m_PlayerNear;                              //  If the player is near, state of hearing
    [HideInInspector] public bool m_IsPatrol;       //  If the enemy is patrol, state of patroling
    bool m_CaughtPlayer;                            //  if the enemy has caught the player
    bool m_DetectedPlayer;
    bool canDisplayIcon;
    [SerializeField]GameObject[] dirtTrail;

    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_DetectedPlayer= false;
        m_playerInRange = false;
        m_PlayerNear = false;
        m_WaitTime = startWaitTime; //  Set the wait time variable that will change
        m_TimeToRotate = timeToRotate;
 
        m_CurrentWaypointIndex = 0; //  Set the initial waypoint
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk; //  Set the navemesh speed with the normal speed of the enemy
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); //  Set the destination to the first waypoint
    }
 
    private void Update()
    {
        iconOff();
        EnviromentView();  //  Check whether or not the player is in the enemy's field of vision
 
        if (!m_IsPatrol)
        {
            if (playerPos == null) { playerPos = GameObject.FindWithTag("character1").transform; }

            Chasing();
        }
        else
        {
            playerPos = null;
            Patroling();
        }
    }
 
    private void Chasing()
    {
        //  The enemy is chasing the player
        m_PlayerNear = false;               //  Set false that hte player is near beacause the enemy already sees the player
        playerLastPosition = Vector3.zero;  //  Reset the player near position
 
        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);//  set the destination of the enemy to the player location
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)    //  Control if the enemy arrive to the player location
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, playerPos.position) >= 6f)
            {
                //  Check if the enemy is not near to the player, returns to patrol after the wait time delay
                m_IsPatrol = true;
                m_DetectedPlayer= false;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, playerPos.position) >= 2.5f)
                //  Wait if the current position is not the player position
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
 
    private void Patroling()
    {
        if (m_PlayerNear)
        {
            //  Check if the enemy detect near the player, so the enemy will move to that position
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                
                //The enemy wait for a moment and then go to the last player position
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            canDisplayIcon = true;
            m_PlayerNear = false;//  The player is no near when the enemy is platroling
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    
    IEnumerator ExclamationMark(){yield return new WaitForSeconds(5f);EXSymbol.enabled=false;}
    void iconOff()
    {
        if(m_DetectedPlayer&&canDisplayIcon)
        {
            canDisplayIcon=false;
            EXSymbol.enabled=true;
            StartCoroutine(ExclamationMark());
        }
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
 
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
 
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
 
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }
 
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
 
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius
 
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enmy and the player
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_DetectedPlayer = true;
                    m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                    m_IsPatrol = false;                 //  Change the state to chasing the player
                }
                else
                {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will no chase
                 * */
                m_playerInRange = false;                //  Change the sate of chasing
            }
            if (m_playerInRange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
            }
        }
    }
}