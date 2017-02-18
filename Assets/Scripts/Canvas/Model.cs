using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public class Mode
    {
        public virtual void Update() { }
        public virtual void CreateGameObject(GameObject obj) { }
        public virtual void DeleteAll(string tag) { }
    }

    public class CreateMode : Mode
    {
        public override void Update() { }

        public override void CreateGameObject(GameObject obj)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Instantiate(obj, headPosition + gazeDirection, obj.transform.rotation);
        }

        public override void DeleteAll(string tag)
        {
            GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject element in elements)
            {
                Destroy(element);
            }
        }
    }

    public class DragMode : Mode
    {
        private GameObject FocusedObject;
        private float Offset;

        public DragMode(GameObject focusedObject)
        {
            FocusedObject = focusedObject;
        
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Vector3 offsetVector = Vector3.Project((focusedObject.transform.position - headPosition), gazeDirection);
            Offset = offsetVector.magnitude;
        }

        public override void Update()
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            FocusedObject.transform.position = headPosition + Offset * gazeDirection;
        }

        public override void CreateGameObject(GameObject obj) { }

        public override void DeleteAll(string tag) { }
    }

    public Mode mode;

    public GameObject cube;
    public GameObject sphere;

    // Use this for initialization
    void Start()
    {
        mode = new CreateMode();
    }

    // Update is called once per frame
    void Update()
    {
        mode.Update();
    }

    public void OnCreateCube()
    {
        mode.CreateGameObject(cube);
    }

    public void OnCreateSphere()
    {
        mode.CreateGameObject(sphere);
    }

    public void OnDeleteAllCubes()
    {
        mode.DeleteAll("Cube");
    }

    public void OnDeleteAllSpheres()
    {
        mode.DeleteAll("Sphere");
    }

    public void OnClearCanvas()
    {
        mode.DeleteAll("Cube");
        mode.DeleteAll("Sphere");
    }

    public void ToggleMode(GameObject focusedObject)
    {
        if (mode is CreateMode && (focusedObject != null))
        {
            mode = new DragMode(focusedObject);
        }
        else if (mode is DragMode)
        {
            mode = new CreateMode();
        }
    }
}
