using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

    private void CreateGameObject(GameObject obj)
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        Instantiate(obj, headPosition + gazeDirection, obj.transform.rotation);
    }

    private void deleteAll(string tag)
    {
        GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject element in elements)
        {
            Destroy(element);
        }
    }

    public void OnCreateCube()
    {
        CreateGameObject(cube);
    }

    public void OnCreateSphere()
    {
        CreateGameObject(sphere);
    }

    public void OnDeleteAllCubes()
    {
        deleteAll("Cube");
    }

    public void OnDeleteAllSpheres()
    {
        deleteAll("Sphere");
    }

    public void OnClearCanvas()
    {
        deleteAll("Cube");
        deleteAll("Sphere");
    }

}
