using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Reference -
//https://www.youtube.com/watch?v=RXB7wKSoupI

public class EnemyRandomMovement : MonoBehaviour {

    NavMeshAgent _navMeshAgent;
    NavMeshPath _path;
    public float TimeForNewPath;
    bool _inCoRoutine;
    Vector3 _target;
    bool _validPath;

    // Use this for initialization
    void Start () {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
    }

 

    // Update is called once per frame
    void Update () {
        if (!_inCoRoutine)
            StartCoroutine(MoveAround());
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 _pos = new Vector3(x, 0, z);
        return _pos;
    }

    IEnumerator MoveAround()
    {
        _inCoRoutine = true;
        yield return new WaitForSeconds(TimeForNewPath);
        GetNewPath();
        _validPath = _navMeshAgent.CalculatePath(_target, _path);
        //if (!validPath) Debug.Log("Found an invalid Path");

        while (!_validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            _validPath = _navMeshAgent.CalculatePath(_target, _path);
        }
        _inCoRoutine = false;
    }

    void GetNewPath()
    {
        _target = getNewRandomPosition();
        _navMeshAgent.SetDestination(_target);
    }
}
