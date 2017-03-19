using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public class Mode
    {
        public virtual void Update() { }
        public virtual void CreateGameObject(GameObject obj) { }
        public virtual void DeleteGameObject(GameObject obj) { }
        public virtual void DeleteAll(string tag) { }
        public virtual void ChangeGameObjectColor(GameObject obj, Color color) { }
        public virtual void IncreaseGameObjectSize(GameObject obj) { }
        public virtual void DecreaseGameObjectSize(GameObject obj) { }
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
            if (obj.transform.localScale.x < 0.2 &&
                obj.transform.localScale.y < 0.2 &&
                obj.transform.localScale.z < 0.2)
            {
                obj.transform.localScale += new Vector3(0.05F, 0.05F, 0.05F);
            }
        }

        public override void DecreaseGameObjectSize(GameObject obj)
        {
            if (obj.transform.localScale.x > 0.05 &&
                obj.transform.localScale.y > 0.05 &&
                obj.transform.localScale.z > 0.05)
            {
                obj.transform.localScale -= new Vector3(0.05F, 0.05F, 0.05F);
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
    }

    public class RotateMode : Mode
    {
        public enum Direction { x, y, z };

        private GameObject FocusedObject;
        private Direction Axis;

        public RotateMode(GameObject focusedObject, Direction axis)
        {
            FocusedObject = focusedObject;
            Axis = axis;
        }

        public override void Update()
        {
            switch (Axis)
            {
                case Direction.x:
                    FocusedObject.transform.Rotate(60 * Time.deltaTime, 0, 0);
                    break;

                case Direction.y:
                    FocusedObject.transform.Rotate(0, 60 * Time.deltaTime, 0);
                    break;

                case Direction.z:
                    FocusedObject.transform.Rotate(0, 0, 60 * Time.deltaTime);
                    break;
            }
        }

        public override void CreateGameObject(GameObject obj) { }
        public override void DeleteGameObject(GameObject obj) { }
        public override void DeleteAll(string tag) { }
        public override void ChangeGameObjectColor(GameObject obj, Color color) { }
        public override void IncreaseGameObjectSize(GameObject obj) { }
        public override void DecreaseGameObjectSize(GameObject obj) { }
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

    public void OnIncreaseGameObjectSize(GameObject focusedObject)
    {
        mode.IncreaseGameObjectSize(focusedObject);
    }

    public void OnDecreaseGameObjectSize(GameObject focusedObject)
    {
        mode.DecreaseGameObjectSize(focusedObject);
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

    public void ChangeToRotateModeX(GameObject focusedObject)
    {
        if (mode is StaticMode && (focusedObject != null))
        {
            mode = new RotateMode(focusedObject, RotateMode.Direction.x);
        }
    }

    public void ChangeToRotateModeY(GameObject focusedObject)
    {
        if (mode is StaticMode && (focusedObject != null))
        {
            mode = new RotateMode(focusedObject, RotateMode.Direction.y);
        }
    }

    public void ChangeToRotateModeZ(GameObject focusedObject)
    {
        if (mode is StaticMode && (focusedObject != null))
        {
            mode = new RotateMode(focusedObject, RotateMode.Direction.z);
        }
    }
}
