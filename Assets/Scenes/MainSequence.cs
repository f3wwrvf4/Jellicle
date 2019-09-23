using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSequence : StateManager<MainSequence, MainSequence.StateID>
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Main seq.");

        SetPrefabs(stateObjectMap);
        SetState(StateID.Boot, null);
    }

    [Serializable]
    public enum StateID
    {
        Boot,
        Title,
        Game
    }


    /*
     * ID-prefabのマッピング、基底クラスに持っていきたい
     */
    [Serializable]
    class MainStatePair : StatePrefab { };  // inspector での表示のために継承

    [SerializeField]
    List<MainStatePair> stateObjectMap;

#if UNITY_EDITOR
    [ContextMenu("Setup State")]
    private void Method()
    {
        foreach (var ID in System.Enum.GetValues(typeof(StateID)))
        {
            if(stateObjectMap.Find(N=>N.stateID.Equals(ID))==null)
            {

            }
        }
    }
#endif
}
