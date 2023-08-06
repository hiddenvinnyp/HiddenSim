using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombinedMesh : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        //CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        //int i = 0;
        //while (i < meshFilters.Length)
        //{
        //    combine[i].mesh = meshFilters[i].sharedMesh;
        //    combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        //    meshFilters[i].gameObject.SetActive(false);

        //    i++;
        //}

        //Mesh mesh = new Mesh();
        //mesh.CombineMeshes(combine);
        //transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        //transform.gameObject.SetActive(true);

        for (int i = 1; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].sharedMesh != null)
            {
                MeshCollider collider = gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = meshFilters[i].sharedMesh;
                //collider. = meshFilters[i].transform.localToWorldMatrix;
            }
        }

        
    }
}
