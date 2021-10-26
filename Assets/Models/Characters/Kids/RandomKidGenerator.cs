using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomKidGenerator : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skinnedRenderer;
    [SerializeField] List<Mesh> meshes = new List<Mesh>();
    [SerializeField] List<Material> materials = new List<Material>();

    void Awake()
    {
        Change();
    }

    void Change()
    {
        int rand = Random.Range(0, meshes.Count);

        skinnedRenderer.material = materials[rand];
        skinnedRenderer.sharedMesh = meshes[rand];
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Change"))
        {
            Change();
        }
    }
}
