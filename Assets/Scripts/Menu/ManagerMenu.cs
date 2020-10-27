using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerMenu : MonoBehaviour
{
    public Color[] color1;
    public Color[] color2;
    public Color[] color3;


    public Animator anim;
    public GameObject[] panels;

    public MeshRenderer mesh;


    public void ButtonCustomization()
    {
        anim.SetBool("MoveCustom", true);
        panels[1].SetActive(false);

    }
    public void panelCustom()
    {
        panels[0].SetActive(true);
    }

    public void MenuReturn()
    {
        panels[0].SetActive(false);
        anim.SetBool("MoveCustom", false);
    }
    public void MenuVizable()
    {
        panels[1].SetActive(true);
    }

    public void ChangeColor1(int index)
    {
        if (index >= color1.Length) return;
        mesh.sharedMaterial.SetColor("c1", color1[index]);
    }
    public void ChangeColor2(int index)
    {
        if (index >= color2.Length) return;
        mesh.sharedMaterial.SetColor("c2", color2[index]);
    }
    public void ChangeColor3(int index)
    {
        if (index >= color3.Length) return;
        mesh.sharedMaterial.SetColor("c3", color3[index]);
    }
}
