using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Touch : MonoBehaviour
{
    //scale
    float initialFingerDistace;
    Vector3 initialScale;
    float minScaleFactor = 0.2f;
    float maxScaleFactor = 10.0f;

    Operation curOperation;
    

    GameObject curSelected;
    public GameObject CurSelected { get { return curSelected; } set { value = curSelected; } } 

    //move
    Vector2 initialPos;
    Vector2 curPos;
    Vector3 lastPos;

    public static Touch instance;
   
    public float PosModifier;

    GameObject palette;

    float timer = 0f;


    private void Awake()
    {
        instance = this;

        //the default operation （Operation type include move, scale, rotate and color）
        curOperation = Operation.Move;
        
    }
    private void Update()
    {
        //Select Object
        if (Input.touchCount > 0 && Input.touches[0].phase==TouchPhase.Began)
        {
            RaycastHit raycastHit;
            Camera cam=Camera.main;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.touches[0].position), out raycastHit, 50.0f, LayerMask.GetMask("Block"))) 
            {
                Block block = raycastHit.collider.GetComponent<Block>();
                
                if (block != null)
                {
                    //block.SetStatus(true);
                    block.SetSelectedMaterial();

                    if (curSelected != null)
                    {
                        //DeSelect the previous selected object and set its color to its last color
                        curSelected.GetComponent<Block>().SetDeselectedColor();
                    }

                    curSelected = block.gameObject;

                    //show the operation menu
                    UIController.instance.ToggleMenu(true);
                    
                    //play sound effects
                    AudioManager.instance.PlayManipulationSound();

                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return;

                //set the object's color to its previous color
                if (curSelected != null)
                {
                    curSelected.GetComponent<Block>().SetDeselectedColor();
                }
                

                curSelected = null;

                //hide the operation menu
                UIController.instance.ToggleMenu(false);

                //if palette is still active in the scene, hide it, too.
                palette = GameObject.Find("Palette");
                if (palette!=null) ColorPalatte.instance.SetPalatteStatus(false);

            }
        }

        //move Object
        //depends on the angle between start position and end position, the user can move one object along the x, y, and z axis.
        //Abs(angle(startPos, endPos))<30 or Abs(angle(startPos, endPos))>150 ======> move along the y axis
        //(angle < -60 && angle > -120) || (angle > 60 && angle < 120) ======> move along the x axis
        //(angle<-30 && angle>-60) || (angle>120 && angle<150) ======> move along the z axis
        //otherwise, the objects will not be moved.

        if (Input.touchCount==1 && curOperation == Operation.Move)
        {
            UnityEngine.Touch t1 = Input.touches[0];

            if (t1.phase == TouchPhase.Began)
            {
                initialPos = t1.position;
                if(curSelected!=null)
                lastPos = curSelected.transform.position;

            }else if (t1.phase == TouchPhase.Moved)
            {
                curPos = t1.position;

                Vector2 disDirection = curPos - initialPos;

                float angle = Vector2.SignedAngle(Vector2.up, disDirection);

                Vector3 dir;

                if ((Mathf.Abs(angle)<30) || (Mathf.Abs(angle)>150))
                {
                    //move along the y direction
                    dir = new Vector3(0, disDirection.magnitude, 0);
                    dir = (angle > -30 && angle < 30) ? dir : -dir;

                    

                }
                else if ((angle < -60 && angle > -120) || (angle > 60 && angle < 120))
                {
                    //move along the x direction
                    dir = new Vector3(disDirection.magnitude, 0, 0);
                    dir = (angle < -60 && angle > -120) ? dir : -dir;

                    
                }
                else if((angle<-30 && angle>-60) || (angle>120 && angle<150))
                {
                    //move along the z direction;
                    dir = new Vector3(0, 0, disDirection.magnitude);
                    dir = (angle < -30 && angle > -60) ? dir : -dir;
                    
                }
                else
                {
                    dir = Vector3.zero;
                }

                if (curSelected != null)
                {
                    Vector3 moveToDelata = curSelected.transform.position + dir * PosModifier - lastPos;
                   
                    //update the lastPos
                    lastPos = curSelected.transform.position + dir * PosModifier;

                    //set current position
                    Block block = curSelected.GetComponent<Block>();

                    block.SetPosition(moveToDelata*PosModifier);

                }

            }
        }


        //scale object
        if (Input.touchCount == 2 && curOperation==Operation.Scale)
        {
            UnityEngine.Touch t1 = Input.touches[0];
            UnityEngine.Touch t2 = Input.touches[1];

            if(t1.phase==TouchPhase.Began || t2.phase == TouchPhase.Began)
            {
                initialFingerDistace = Vector2.Distance(t1.position, t2.position);
                if(curSelected!=null)
                initialScale = curSelected.transform.localScale;

            }
            else if(t1.phase==TouchPhase.Moved || t1.phase == TouchPhase.Moved)
            {
                float currFingerDistance = Vector2.Distance(t1.position, t2.position);
                float scaleFactor = currFingerDistance / initialFingerDistace;

                if (curSelected == null) return;

                if(minScaleFactor<(initialScale*scaleFactor).x && (initialScale * scaleFactor).x < maxScaleFactor)
                {
                    Block block = curSelected.GetComponent<Block>();

                    block.SetScale(initialScale * scaleFactor);
                    

                }
                        
            }

        }

        //rotate object
        if(Input.touchCount>0 && curOperation == Operation.Rotate)
        {


            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) {

                
                timer = 0.2f;
                
                return;
            }

            //the input is so sensitive that even though the touch is on eventsystem object, the selected object will rotate
            //therefore, here a timer delay is used to prevent the object from rotating when the touch is over eventsystem.

            if (timer > 0.0001f)
            {

                timer--;
                return;
            }

            if (curSelected != null)
            {
                UnityEngine.Touch t1 = Input.touches[0];
                if (t1.phase == TouchPhase.Began)
                {
                    initialPos = t1.position;
                }
                else if (t1.phase == TouchPhase.Ended)
                {
                    curPos = t1.position;
                    float angle = Mathf.Abs(Vector2.SignedAngle(Vector2.up, curPos - initialPos));

                    curSelected.GetComponent<Block>().Rotate(angle);
            
                    //play sound effects
                    AudioManager.instance.PlayManipulationSound();
                }
            }
        }
    }


    public void SetOperation(Operation operation)
    {
        curOperation = operation;
    }


}
