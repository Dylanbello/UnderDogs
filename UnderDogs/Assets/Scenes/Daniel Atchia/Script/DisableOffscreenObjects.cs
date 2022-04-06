using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DisableOffscreenObjects : MonoBehaviour
{
    public Camera[] cams;
    public Transform geometryRoot;
    public bool includeDisabled;
    public bool enableDisabled;
    public BoxCollider[] forceInclude;
    public BoxCollider[] forceRemove;
    public LayerMask ignoreLayers;
    private int objectsInView;
    private int objectsForceInclude;
    private int objectsForceRemove;

    private void Start()
    {
        InvokeRepeating("RemoveUnseenObjects", 1, 0.2f);
    }
    public void RemoveUnseenObjects()
    {
        objectsInView = 0;
        objectsForceInclude = 0;
        objectsForceRemove = 0;
        cams = GetComponentsInChildren<Camera>();
        forceInclude = transform.Find("ForceInclude").GetComponents<BoxCollider>();
        forceRemove = transform.Find("ForceRemove").GetComponents<BoxCollider>();
        MeshRenderer[] sceneObjs = geometryRoot.GetComponentsInChildren<MeshRenderer>(includeDisabled);
        Debug.Log(string.Format("{0} objects in scene", sceneObjs.Length));

        foreach (MeshRenderer obj in sceneObjs)
        {
            bool inView = false;

            if (ignoreLayers == (ignoreLayers | (1 << obj.gameObject.layer)))
            {
                // Ignore this layer
            }
            else
            {
                // force include?
                bool alreadyIncluded = ShowIfInclude(obj);

                // force exclude
                bool alreadyRemoved = HideIfRemove(obj);

                // Debug.Log(string.Format("{0} is not in Include or Exclude", obj.gameObject.name));

                if (!(alreadyIncluded || alreadyRemoved))
                {
                    Vector3[] points = new Vector3[8];
                    points[0] = obj.bounds.min;
                    points[1] = obj.bounds.max;
                    points[2] = new Vector3(points[0].x, points[0].y, points[1].z);
                    points[3] = new Vector3(points[0].x, points[1].y, points[0].z);
                    points[4] = new Vector3(points[1].x, points[0].y, points[0].z);
                    points[5] = new Vector3(points[0].x, points[1].y, points[1].z);
                    points[6] = new Vector3(points[1].x, points[0].y, points[1].z);
                    points[7] = new Vector3(points[1].x, points[1].y, points[0].z);

                    for (int i = 0; i < cams.Length; i++)
                    {
                        if (ObjectInView(cams[i], points))
                        {
                            inView = true;
                        }
                    }


                    if (inView)
                    {
                        obj.gameObject.SetActive(true);
                    }
                    else
                    {
                        obj.gameObject.SetActive(false);
                    }
                }
            }
        }

        MeshRenderer[] visibleSceneObjs = geometryRoot.GetComponentsInChildren<MeshRenderer>(false);
        Debug.Log(string.Format("{0} objects visible in scene. {1} in view. {2} force include. {3} force removed.", visibleSceneObjs.Length, objectsInView, objectsForceInclude, objectsForceRemove));
    }

    public void ResetVisibleObjects()
    {
        MeshRenderer[] sceneObjs = geometryRoot.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer obj in sceneObjs)
        {
            obj.gameObject.SetActive(true);
        }
    }

    private bool ShowIfInclude(MeshRenderer obj)
    {
        foreach (BoxCollider inc in forceInclude)
        {
            if (inc.bounds.Contains(obj.transform.position))
            {
                obj.gameObject.SetActive(true);
                objectsForceInclude++;
                return true;
            }
        }
        return false;
    }

    private bool HideIfRemove(MeshRenderer obj)
    {
        foreach (BoxCollider excl in forceRemove)
        {
            if (excl.bounds.Contains(obj.transform.position))
            {
                obj.gameObject.SetActive(false);
                objectsForceRemove++;
                return true;
            }
        }
        return false;
    }

    private bool ObjectInView(Camera cam, Vector3[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(points[i]);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                objectsInView++;
                return true;
            }
        }

        return false;
    }
}
