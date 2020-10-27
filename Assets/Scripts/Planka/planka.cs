using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class planka : MonoBehaviour
{
    public BoxCollider pol;
    public ParticleSystem shepa;
    public AudioSource audio;

    private MeshRenderer mesh;
    private bool isBuild = false;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        DeBuildPlanka();
    }

    public void BuildPlanka()
    {
        if (mesh==null || pol == null) { return; }

        pol.isTrigger = false;
        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1);
        isBuild = true;

        transform.tag = "Untagged";

    }

    public void DeBuildPlanka()
    {
        if (mesh == null || pol == null) { return; }

        pol.isTrigger = true;
        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0);
        isBuild = false;
        transform.tag = "Planka";
    }

    public void PreviewPlanka()
    {
        if (isBuild) return;

        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0.5f);
      
    }

    public void DePreviewPlanka()
    {
        if (isBuild) return;

        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { PreviewPlanka(); }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { DePreviewPlanka(); }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            DeBuildPlanka();
            audio.Play();
            shepa.Play();

        }
    }
}
