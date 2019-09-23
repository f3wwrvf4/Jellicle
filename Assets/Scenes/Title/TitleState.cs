using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleState : StateBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class InitParam : StateInitParam
    {
        public System.String initString;
    }
    public class Result : StateResult
    {
        public System.String resultString;
    }

    public void InitState(InitParam initParam, BootState.Result bootResult)
    {
        Debug.Log("init:" + initParam.initString);
        Debug.Log("result:" + bootResult.resultString);
    }
}
