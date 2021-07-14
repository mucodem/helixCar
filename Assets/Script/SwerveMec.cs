using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwervingMechanic
{
    public class SwerveMec : MonoBehaviour
    {
        #region Singleton
        public static SwerveMec instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion
        public bool Pos { get; set; } = false; // if the game only use swerve for position set initial value true , 
                                               //for rotation set initial value false    
        public float ClampMaxValue { get; set; } // min value will be minus of max. 
        public float LerpMultiplier { get; set; }//lerp speed adjuster
        public bool Timer { get; set; } = true; // just a needed boolean
     

        float startPosX  , clampedAngle;
        public void SwipeSwerveMec(Transform obj)
        {
            if (Timer) 
            {              
                //clampedAngle = 360 - ClampMaxValue;
                startPosX = Input.mousePosition.x;
                Timer = false;// timer must be set to true from instance or child class whenever you need 
                              //--> for example input mouse-up and touch.phase ended
            }

            float deltaMousePos = Input.mousePosition.x - startPosX;// how much mouse dragged

            if (Pos)//position swerve
            {
                float xPos = obj.position.x;
                xPos = Mathf.Lerp(xPos , xPos + (5 * deltaMousePos / Screen.width), Time.deltaTime * LerpMultiplier);
                xPos = Mathf.Clamp(xPos, -ClampMaxValue , ClampMaxValue);
         
                obj.position = new Vector3(xPos, obj.position.y , obj.position.z);

            }
            else ///rotation swerve
            {
                /*------------------------------rotate around itself 1--------------------------*/
                obj.Rotate(LerpMultiplier * deltaMousePos * Time.deltaTime * Vector3.up);

                /* ---------------------------rotate around itself  2---------------------*/
                /* float zRot = obj.localEulerAngles.x;
                 Debug.Log(zRot);
                 zRot = Mathf.Lerp(zRot, zRot - (360 * deltaMousePos / Screen.width), Time.deltaTime * LerpMultiplier);

                 if(zRot > 180 && zRot < clampedAngle)
                 {
                     zRot = clampedAngle;
                 }
                 else if(zRot < 180 && zRot > ClampMaxValue)
                 {
                     zRot = ClampMaxValue;
                 } 

                 obj.localEulerAngles = new Vector3(zRot, obj.localEulerAngles.y , obj.localEulerAngles.z);
                 /*------------------------------------------------------------------------*/


                /* --------------------------- rotate around itself 3 -----------------------------*/
                /*float zRot = Mathf.Lerp(0, (360 * deltaMousePos / Screen.width), Time.deltaTime * LerpMultiplier);
                zRot = Mathf.Clamp(zRot, -ClampMaxValue, ClampMaxValue);
                Debug.Log(zRot);
                obj.eulerAngles -= new Vector3(0, -zRot, 0);
                /*----------------------------------------------------------------------------------*/
            }
        }     
    }
}

