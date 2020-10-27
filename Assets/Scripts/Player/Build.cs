using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Build : MonoBehaviour
{
    private bool isBuild = false;

    public void ChangeIsBuildTrue()
    {
        isBuild = true;
    }
    public void ChangeIsBuildFalse()
    {
        isBuild = false;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Planka") && isBuild)
        {
            other.GetComponent<planka>().BuildPlanka();
            isBuild = false;
        }
    }
}
