using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class WaitForSecondsLauncher : MonoBehaviour
{
    public ScriptMachine targetMachine;
    public float delay = 3f;
    public string eventName = "AfterWait";

    public void StartWait()
    {
        StartCoroutine(WaitThenTrigger());
    }

    IEnumerator WaitThenTrigger()
    {
        yield return new WaitForSeconds(delay);
        CustomEvent.Trigger(targetMachine.gameObject, eventName);
    }
}
