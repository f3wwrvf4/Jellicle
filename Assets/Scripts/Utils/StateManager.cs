using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateInitParam
{
}

public class StateResult
{
}

[System.Serializable]
public class StateManager<TManager, TStateID> : MonoBehaviour
    where TManager : StateManager<TManager, TStateID>
    where TStateID : System.Enum
{
    static public TManager Instance { get; private set; }
    protected void Awake()
    {
        Instance = (TManager)this;
    }

    protected void SetPrefabs<TPrefab>(List<TPrefab> prefabs)
        where TPrefab : StatePrefab
    {
        statePrefabs = new List<StatePrefab>();
        foreach (var Elem in prefabs)
        {
            StatePrefab pair = new StatePrefab();
            pair.stateID = Elem.stateID;
            pair.statePrefab = Elem.statePrefab;
            statePrefabs.Add(pair);
        }
    }

    public void SetState(TStateID seq, StateInitParam initParam)
    {
        StartCoroutine(ChangeSequece(seq, initParam));
    }

    [System.Serializable]
    protected class StatePrefab
    {
        [SerializeField] public TStateID stateID;
        [SerializeField] public GameObject statePrefab;

    }
    [SerializeField]
    List<StatePrefab> statePrefabs;

    StateBase currentState;
    IEnumerator ChangeSequece(TStateID next, StateInitParam initParam)
    {
        StateResult stateResult = null;

        if (currentState)
        {
            currentState.TermStart();
            while (!currentState.IsFinished)
            {
                yield return null;
            }
            stateResult = currentState.GetResult();
            GameObject.Destroy(currentState.gameObject);
            currentState = null;
        }

        GameObject prefab = statePrefabs.Find(N => N.stateID.Equals(next)).statePrefab;
        GameObject gob = GameObject.Instantiate(prefab, this.transform);
        currentState = gob.GetComponent<StateBase>();

        Debug.Log(currentState.GetType().ToString());

        if(initParam!=null)
        {
            Type[] types = null;
            object[] parameters = null;
            if (stateResult!=null)
            {
                types = new Type[] { initParam.GetType(), stateResult.GetType() };
                parameters = new object[] { initParam, stateResult };
            }
            else
            {
                types = new Type[] { initParam.GetType() };
                parameters = new object[] { initParam };
            }
            var methodInfo = currentState.GetType().GetMethod("InitState", types);
            methodInfo?.Invoke(currentState, parameters);
        }


        currentState.PrepareStart();
        while (!currentState.IsPrepared)
        {
            yield return null;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Setup State")]
    protected GameObject CreatePrefab(String Name)
    {
        return null;
    }
#endif
}
