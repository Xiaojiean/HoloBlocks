using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    public class Mode
    {
        public static bool gravityOn = false;

        public virtual void Update() { }
        public virtual void CreateGameObject(GameObject obj) { }
        public virtual void DeleteGameObject(GameObject obj) { }
        public virtual void DeleteAll(string tag) { }
        public virtual void ChangeGameObjectColor(GameObject obj, Color color) { }
        public virtual void IncreaseGameObjectSize(GameObject obj) { }
        public virtual void DecreaseGameObjectSize(GameObject obj) { }
        public virtual void TurnObject(GameObject obj) { }
        public virtual void FlipObject(GameObject obj) { }
        public virtual void TurnOnPhysics() { }
        public virtual void TurnOffPhysics() { }
    }

    public class StaticMode : Mode
    {
        public override void Update() { }

        public override void CreateGameObject(GameObject obj)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Instantiate(obj, headPosition + gazeDirection, obj.transform.rotation);
        }

        public override void DeleteGameObject(GameObject obj)
        {
            Destroy(obj);
        }

        public override void DeleteAll(string tag)
        {
            GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject element in elements)
            {
                Destroy(element);
            }
        }

        public override void ChangeGameObjectColor(GameObject obj, Color color)
        {
            obj.GetComponent<Renderer>().material.color = color;
        }

        public override void IncreaseGameObjectSize(GameObject obj)
        {
            if (obj.transform.localScale.x < 0.175F &&
                obj.transform.localScale.y < 0.175F &&
                obj.transform.localScale.z < 0.175F)
            {
                obj.transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            }
        }

        public override void DecreaseGameObjectSize(GameObject obj)
        {
            if (obj.transform.localScale.x > 0.075F &&
                obj.transform.localScale.y > 0.075F &&
                obj.transform.localScale.z > 0.075F)
            {
                Debug.Log("Original scale:");
                Debug.Log(obj.transform.localScale.x);
                Debug.Log(obj.transform.localScale.y);
                Debug.Log(obj.transform.localScale.z);
                Debug.Log("\n");
                obj.transform.localScale -= new Vector3(0.05F, 0.05F, 0.05F);
                Debug.Log("New scale:");
                Debug.Log(obj.transform.localScale.x);
                Debug.Log(obj.transform.localScale.y);
                Debug.Log(obj.transform.localScale.z);
                Debug.Log("\n\n");
            }
        }

        public override void TurnObject(GameObject obj)
        {
            obj.transform.Rotate(0, 45, 0);
        }

        public override void FlipObject(GameObject obj)
        {
            obj.transform.Rotate(0, 0, 45);
        }

        public override void TurnOnPhysics()
        {
            gravityOn = true;
            GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                if (obj.tag == "Sphere" || obj.tag == "Cube" || obj.tag == "Pyramid" || obj.tag == "Slope" || obj.tag == "Cylinder")
                {
                    Rigidbody rb = obj.AddComponent<Rigidbody>();
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
            }
        }

        public override void TurnOffPhysics()
        {
            gravityOn = false;
            GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                if (obj.tag == "Sphere" || obj.tag == "Cube" || obj.tag == "Pyramid" || obj.tag == "Slope" || obj.tag == "Cylinder")
                {
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.velocity = Vector3.zero;
                        Destroy(rb);
                    }
                }
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
        public override void DeleteGameObject(GameObject obj) { }
        public override void DeleteAll(string tag) { }
        public override void ChangeGameObjectColor(GameObject obj, Color color) { }
        public override void IncreaseGameObjectSize(GameObject obj) { }
        public override void DecreaseGameObjectSize(GameObject obj) { }
        public override void TurnObject(GameObject obj) { }
        public override void FlipObject(GameObject obj) { }
        public override void TurnOnPhysics() { }
        public override void TurnOffPhysics() { }
    }

    public Mode mode;

    // Prefabs for GameObjects
    public GameObject Cube;
    public GameObject Sphere;
    public GameObject Cylinder;
    public GameObject Pyramid;
    public GameObject Slope;

    // Use this for initialization
    void Start()
    {
        mode = new StaticMode();
    }

    // Update is called once per frame
    void Update()
    {
        mode.Update();
    }

    public void OnCreateCube()
    {
        mode.CreateGameObject(Cube);
    }

    public void OnCreateSphere()
    {
        mode.CreateGameObject(Sphere);
    }

    public void OnCreateCylinder()
    {
        mode.CreateGameObject(Cylinder);
    }

    public void OnCreatePyramid()
    {
        mode.CreateGameObject(Pyramid);
    }

    public void OnCreateSlope()
    {
        mode.CreateGameObject(Slope);
    }

    public void OnDelete(GameObject focusedObject)
    {
        mode.DeleteGameObject(focusedObject);
    }

    public void OnDeleteAllCubes()
    {
        mode.DeleteAll("Cube");
    }

    public void OnDeleteAllSpheres()
    {
        mode.DeleteAll("Sphere");
    }

    public void OnDeleteAllCylinders()
    {
        mode.DeleteAll("Cylinder");
    }

    public void OnDeleteAllPyramids()
    {
        mode.DeleteAll("Pyramid");
    }

    public void OnDeleteAllSlopes()
    {
        mode.DeleteAll("Slope");
    }

    public void OnClearCanvas()
    {
        mode.DeleteAll("Cube");
        mode.DeleteAll("Sphere");
        mode.DeleteAll("Cylinder");
        mode.DeleteAll("Pyramid");
        mode.DeleteAll("Slope");
    }

    public void OnChangeColorToRed(GameObject focusedObject)
    {
        mode.ChangeGameObjectColor(focusedObject, Color.red);
    }

    public void OnChangeColorToYellow(GameObject focusedObject)
    {
        mode.ChangeGameObjectColor(focusedObject, Color.yellow);
    }

    public void OnChangeColorToBlue(GameObject focusedObject)
    {
        mode.ChangeGameObjectColor(focusedObject, Color.blue);
    }

    public void OnChangeColorToGreen(GameObject focusedObject)
    {
        mode.ChangeGameObjectColor(focusedObject, Color.green);
    }

    public void OnChangeColorToWhite(GameObject focusedObject)
    {
        mode.ChangeGameObjectColor(focusedObject, Color.white);
    }

    public void OnIncreaseGameObjectSize(GameObject focusedObject)
    {
        mode.IncreaseGameObjectSize(focusedObject);
    }

    public void OnDecreaseGameObjectSize(GameObject focusedObject)
    {
        mode.DecreaseGameObjectSize(focusedObject);
    }

    public void OnTurnObject(GameObject focusedObject)
    {
        mode.TurnObject(focusedObject);
    }

    public void OnFlipObject(GameObject focusedObject)
    {
        mode.FlipObject(focusedObject);
    }

    public void ChangeToStaticMode()
    {
        mode = new StaticMode();
    }

    public void ChangeToDragMode(GameObject focusedObject)
    {
        if (mode is StaticMode && (focusedObject != null))
        {
            mode = new DragMode(focusedObject);
        }
    }

    public void ToggleStaticAndDragMode(GameObject focusedObject)
    {
        if (mode is StaticMode && focusedObject.tag != "MainCamera" && focusedObject.tag != "Untagged")
        {
            if (Mode.gravityOn)
            {
                Rigidbody rb = focusedObject.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.velocity = Vector3.zero;
                    Destroy(rb);
                }
            }
            mode = new DragMode(focusedObject);
        }
        else if (mode is DragMode)
        {
            if (Mode.gravityOn)
            {
                Rigidbody rb = focusedObject.AddComponent<Rigidbody>();
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            mode = new StaticMode();
        }
    }

    public void TurnOnPhysics()
    {
        mode.TurnOnPhysics();
    }

    public void TurnOffPhysics()
    {
        mode.TurnOffPhysics();
    }
}
