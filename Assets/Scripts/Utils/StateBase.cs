using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    [SerializeField] protected Component ModelBase;
    [SerializeField] protected GameObject ViewBase;

    virtual public void InitState(StateInitParam param)
    {
    }

    virtual public StateResult GetResult()
    {
        return null;
    }

    public void PrepareStart()
    {
        IsPrepared = false;
        StartCoroutine(PrepareProc());
    }
    public bool IsPrepared { get; protected set; } = false;
    protected virtual IEnumerator PrepareProc()
    {
        IsPrepared = true;
        yield break;
    }

    public bool IsFinished { get; protected set; } = false;
    public void TermStart()
    {
        IsFinished = false;
        StartCoroutine(TermProc());
    }

    protected virtual IEnumerator TermProc()
    {
        IsFinished = true;
        yield break;
    }
}
