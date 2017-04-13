﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    /* Class that defines the behavior of Model while not in DragMode. */
    public class Mode
    {
        /* Magic numbers */
        private float CreationDistance = 1.5F;
        private float MinScale = 0.05F;
        private float MaxScale = 0.3F;
        private float SizeIncrement = 0.05F;

        /* Whether physics is on or not */
        protected static bool PhysicsOn = false;

        /* This function is called once per frame. */
        public virtual void Update() { }

        /* Code that need to be executed before the object is destroyed by the garbage collector. */
        public virtual void CleanUp() { }

        /* Create a new GameObject with the same properties as obj.
         * Does nothing is obj is not a prefab object. */
        public virtual void CreateGameObject(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            GameObject newObject = Instantiate(obj, headPosition + CreationDistance * gazeDirection, obj.transform.rotation);

            if (PhysicsOn)
            {
                AddRigidBody(newObject);
            }
        }

        /* Delete obj. Does nothing if obj is not a prefab object. */
        public virtual void DeleteGameObject(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            Destroy(obj);
        }

        /* Delete all objects tagged with tag.
         * tag must be a valid prefab tag. */
        public virtual void DeleteAll(string tag)
        {
            Debug.Assert(IsPrefabTag(tag));

            GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject element in elements)
            {
                DeleteGameObject(element);
            }
        }

        /* Change the color of obj to be color.
         * Does nothing if obj is not a prefab. */
        public virtual void ChangeGameObjectColor(GameObject obj, Color color)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            obj.GetComponent<Renderer>().material.color = color;
        }

        /* Increase the size of obj by an increment.
         * Does nothing if obj is not a prefab. */
        public virtual void IncreaseGameObjectSize(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            if (obj.transform.localScale.x < MaxScale - (SizeIncrement / 2) &&
                obj.transform.localScale.y < MaxScale - (SizeIncrement / 2) &&
                obj.transform.localScale.z < MaxScale - (SizeIncrement / 2))
            {
                obj.transform.localScale += new Vector3(SizeIncrement, SizeIncrement, SizeIncrement);
            }
        }

        /* Decrease the size of the obj by an increment.
         * Does nothing if obj is not a prefab */
        public virtual void DecreaseGameObjectSize(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            if (obj.transform.localScale.x > MinScale + (SizeIncrement /2) &&
                obj.transform.localScale.y > MinScale + (SizeIncrement / 2) &&
                obj.transform.localScale.z > MinScale + (SizeIncrement / 2))
            {
                obj.transform.localScale -= new Vector3(SizeIncrement, SizeIncrement, SizeIncrement);
            }
        }

        // TODO: 90 degrees or 45 degrees?
        // TODO: Get rid of these magic numbers.
        /* Rotate obj around the y axis clockwise by 45 degrees.
         * Does nothing is obj is not a prefab. */
        public virtual void TurnObject(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            obj.transform.rotation = Quaternion.AngleAxis(90, Vector3.up) * obj.transform.rotation;
        }

        /* Rotate obj around the z axis clockwise by 45 degrees.
         * Does nothing is obj is not a prefab. */
        public virtual void FlipObject(GameObject obj)
        {
            if (!IsPrefab(obj))
            {
                return;
            }

            float currentRotation = obj.transform.localRotation.eulerAngles.z;
            float offBy = currentRotation % 90;
            obj.transform.Rotate(0, 0, 90 - offBy);
        }

        /* Turn on physics for all prefabs. */
        public virtual void TurnOnPhysics()
        {
            if (PhysicsOn)
            {
                return;
            }

            PhysicsOn = true;

            GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                AddRigidBody(obj);
            }
        }

        /* Turn off physics for all prefabs. */
        public virtual void TurnOffPhysics()
        {
            if (!PhysicsOn)
            {
                return;
            }

            PhysicsOn = false;

            GameObject[] objs = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                RemoveRigidBody(obj);
            }
        }

        /* Turn on physics for the designated object, if it is a prefab.
         * Should only be called when physics is on. */
        protected void AddRigidBody(GameObject obj)
        {
            Debug.Assert(PhysicsOn == true);

            if (IsPrefab(obj))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (!rb)
                {
                    rb = obj.AddComponent<Rigidbody>();
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
            }
        }

        /* Remove the rigid body from the designated object, if it is a prefab. */
        protected void RemoveRigidBody(GameObject obj)
        {
            if (IsPrefab(obj))
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

    /* Class that defines the behavior of Model while it is in DragMode. */
    public class DragMode : Mode
    {
        /* The object we are dragging. */
        private GameObject FocusedObject;

        /* The distance of the object from the user's head. */
        private float Offset;

        /* focusedObject must be a prefab. */
        public DragMode(GameObject focusedObject)
        {
            Debug.Assert(IsPrefab(focusedObject));

            RemoveRigidBody(focusedObject);
            FocusedObject = focusedObject;

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            Vector3 offsetVector = Vector3.Project((focusedObject.transform.position - headPosition), gazeDirection);
            Offset = offsetVector.magnitude;
        }

        /* Update the location of FocusedObject based on the user's head position and gaze direction. */
        public override void Update()
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            FocusedObject.transform.position = headPosition + Offset * gazeDirection;
        }

        /* Turn on physics for FocusedObject if needed. */
        public override void CleanUp()
        {
            if (PhysicsOn)
            {
                AddRigidBody(FocusedObject);
            }
        }

        /* Disable the following functionalities. */
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

    /* List of valid tags for Prefab objects. */
    static private HashSet<string> PrefabTags = new HashSet<string> { "Sphere", "Cube", "Cylinder", "Pyramid", "Slope" };

    /* Helper function for determining whether a tag is a valid prefab tag. */
    static public bool IsPrefabTag(string tag)
    {
        return PrefabTags.Contains(tag);
    }

    /* Helper function for determining whether a GameObject is a prefab */
    static public bool IsPrefab(GameObject obj)
    {
        if (!obj)
        {
            return false;
        }

        return IsPrefabTag(obj.tag);
    }

    /* Mode of Model */
    private Mode mode;

    /* Prefabs. */
    public GameObject Cube;
    public GameObject Sphere;
    public GameObject Cylinder;
    public GameObject Pyramid;
    public GameObject Slope;

    private GameObject ClipBoard;

    /* Used for initialization. */
    void Start()
    {
        mode = new Mode();
        ClipBoard = null;
    }

    /* Called once per frame. */
    void Update()
    {
        mode.Update();
    }

    /* Functions implementing voice commands */
    //================================================================================

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

    public void OnClearCanvas()
    {
        foreach (string tag in PrefabTags)
        {
            mode.DeleteAll(tag);
        }
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

    // TODO: discuss the intuitiveness of this command.
    // i.e. what should happen when the user says copy while not looking at a prefab?
    // TODO: does these commands work very well?
    public void OnCopy(GameObject focusedObject)
    {
        if (IsPrefab(focusedObject))
        {
            ClipBoard = focusedObject;
        }
    }

    // TODO: Currently, paste works only when the original copied object
    // still exists, which is a bit counter-intuitive.
    // However, Unity does not offer an easy way to copy a GameObject without
    // showing it on the screen. What should we do?
    public void OnPaste()
    {
        mode.CreateGameObject(ClipBoard);
    }

    public void ToggleMode(GameObject focusedObject)
    {
        mode.CleanUp();

        if (mode is DragMode)
        {
            mode = new Mode();
        }
        else
        {
            if (!focusedObject)
            {
                return;
            }
            mode = new DragMode(focusedObject);
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

    //================================================================================
}
