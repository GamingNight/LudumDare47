using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFluteListenerScenario : MonoBehaviour
{

    private void Update() {
        RunScenario();
    }

    protected abstract void RunScenario();

    public abstract void InitScenario();
}
