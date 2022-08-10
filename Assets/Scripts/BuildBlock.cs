using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BuildBlock : MonoBehaviour
{
    public BlockInfo[] blockInfos;

    private ARRaycastManager raycastManager;
    public LayerMask blockLayer;

    GameObject blockHost;
    public static BuildBlock instance;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        blockHost = GameObject.Find("BlockHost");
        instance = this;
    }

    public void OnBuildButtonPressed()
    {
        //play sound effects
        AudioManager.instance.PlayUITap();

        List<ARRaycastHit> arHits = new List<ARRaycastHit>();
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Shape curShape = ShapeMenu.instance.curShape;

            Block block = hitInfo.collider.GetComponent<Block>();

            int modifier = 1;
            if (block.shape == Shape.Cylinder && hitInfo.normal==Vector3.up)
            {
                //the height of the cylinder is 2 unit, to avoid overlaps of new instantiated objects, a modifier is used.
                modifier = 2;
            }
            
            
            Vector3 buildablePosition = hitInfo.normal* modifier + hitInfo.transform.position;
            Quaternion buildableRotation = hitInfo.transform.rotation;
            Build(buildablePosition, buildableRotation);
        }
        else
        {
            raycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), arHits, TrackableType.Planes);

            if (arHits.Count > 0)
            {
                Vector3 buildablePosition = new Vector3
                    (
                        Mathf.Round(arHits[0].pose.position.x / 1) * 1,
                        Mathf.Round(arHits[0].pose.position.y / 1) * 1,
                        Mathf.Round(arHits[0].pose.position.z / 1) * 1
                    );
                Quaternion buildableRotation = arHits[0].pose.rotation;
                Build(buildablePosition, buildableRotation);
            }
        }


       
    }


    void Build(Vector3 pos, Quaternion rotation)
    {
        Shape shapeToSpawn = ShapeMenu.instance.curShape;
        GameObject prefabToSpawn=null;
        foreach (var blockInfo in blockInfos)
        {
            if (shapeToSpawn == blockInfo.shape)
            {
                prefabToSpawn = blockInfo.shapePrefab;
               
            }
        }
        if(prefabToSpawn!=null)
        Instantiate(prefabToSpawn, pos, rotation,blockHost.transform);
    }

    public void DeleteSelected()
    {
        //play sound effects
        AudioManager.instance.PlayUITap();


        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }

    public void ToggleRigidbody(bool rigidbodyOn)
    {
        
        int count = blockHost.transform.childCount;
        
        if (rigidbodyOn)
        {
            
            for (int i = 0; i < count; i++)
            {
                Rigidbody rb = blockHost.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    blockHost.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    
                }
                
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                Rigidbody rb = blockHost.transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                   
                }
                
            }
        }
        
    }

}
