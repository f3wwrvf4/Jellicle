using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootState : StateBase
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BootRoutine()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("...1");
        yield return new WaitForSeconds(1);
        Debug.Log("...2");
        yield return new WaitForSeconds(1);
        Debug.Log("...3");
        yield return new WaitForSeconds(1);

        TitleState.InitParam param = new TitleState.InitParam();
        param.initString = "fugafuga";

        MainSequence.Instance.SetState(MainSequence.StateID.Title, param);
    }

    public class InitParam : StateInitParam
    { }

    public class Result : StateResult
    {
        public System.String resultString;
    }

    public void InitState(InitParam param)
    { }

    override public StateResult GetResult()
    {
        var result = new Result();
        result.resultString = "hogehoge";
        return result;
    }
}
