using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    AiState state;
    // Start is called before the first frame update
    void Start()
    {
        if(state != null)
        ChangeState(state);
    }

    // Update is called once per frame
    void Update()
    {
        if(state!=null)
            state.OnUpdate();
    }

    public void ChangeState(AiState newState)
    {
        if(state!=null)
        state.OnExit();
        newState.OnEnter();
        state = newState;
    }
}
