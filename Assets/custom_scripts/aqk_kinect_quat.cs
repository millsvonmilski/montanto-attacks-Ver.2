/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using OSCsharp.Data;

namespace OscSimpl.Examples
{

    /// <summary>
    /// Moves a GameObject in normalized coordinates (ScreenToWorldPoint)
    /// </summary>
    // [AddComponentMenu("UniOSC/MoveGameObject")]
	public class aqk_kinect_quat :  MonoBehaviour
    {

        public string OSCaddress="";

        [Tooltip("make sure that the OscIn objects in the environment have a corresponding tag")]

        public string tagName="";

        public OscIn oscIn;


        private Transform transformToMove;

        //movementModeProp = serializedObject.FindProperty ("movementMode");

        public bool constrainX = false;

        public bool constrainY = false;

        public bool constrainZ = false;

        public Vector3 constaintsXYZ = new Vector3();



        public float rotationSmoothingTime = 0.25f;
        //public float smoothRotMs = 30f;
        //public float smoothQuatMs = 30f;


        //Whether we are currently interpolating or not
        private bool _isLerping;

        //The start and finish positions for the interpolation
        private Quaternion _startRotation;
        private Quaternion _endRotation;

        //The Time.time value when we started the interpolation
        private float _timeStartedLerping;

        private bool _start=false;


        void Start()
        {

            if (OSCaddress.Length==0)
            {
                Debug.Log(transform.name + " : " + GetType() + " : " + "Awake(): need to specify a valid OSC address, e.g. /sheefa,  aborting");
                Destroy(this);
                return;
            }

            if (tagName.Length==0)
            {
                Debug.Log(transform.name + " : " + GetType() + " : " + "Awake(): need to specify a tag name, e.g. fromTouchOSC,  aborting");
                Destroy(this);
                return;
            }

            if (oscIn != null)   // already chosen manually
                return;

            // look for the OscIn object(s) among children of gameroot using tag

            zk_transformTag[] zk_transformTagsCS;

            if (aqkGameRoot.instance.gameObject == null)
            {
                Debug.LogError(transform.name + " : " + GetType() + " : " + "Awake(): can't find GameRoot  parent of OSCrx object(s), aborting");
                Destroy(this);
                return;
            }

            zk_transformTagsCS = aqkGameRoot.instance.gameObject.GetComponentsInChildren<zk_transformTag>();  
            foreach (zk_transformTag tag in zk_transformTagsCS)
            {
                if (tag.tagName.Equals(tagName))
                    oscIn = tag.gameObject.GetComponent<OscIn>();
                Debug.Log(transform.name + " : " + GetType() + " : " + "Matching on tag:"+tagName);
            }

            if (oscIn == null)
            {
                Debug.LogError(transform.name + " : " + GetType() + " : " + "Awake(): can't find locate OscIn object(s) among children of GameRoot, aborting");
                Destroy(this);
                return;
            }
            _start = true;
            OnEnable();   // this can not be called until START has set up the state
        }


        public void OnEnable()
        {

            if (!_start == true)
                return;

            Debug.Log(transform.name + " : " + GetType() + " : " + "OnEnable()");

            if (oscIn != null)
                oscIn.Map( OSCaddress, oscRx );


            if (transformToMove == null)
            {
                Transform hostTransform = GetComponent<Transform>();
                if (hostTransform != null)
                    transformToMove = hostTransform;
            }
            //Debug.Log("ONENABLE  OBJ NAME: " + transformToMove.name);
            _startRotation = _endRotation = transformToMove.localRotation;

        }

        void OnDisable()
        {
            if (oscIn != null)
                oscIn.Unmap( oscRx );
        }


        // changed from fixedUpdate which was too jerky
        new void Update()
        {
             
            if (_isLerping)
            {
                //We want percentage = 0.0 when Time.time = _timeStartedLerping
                //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
                //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
                //"Time.time - _timeStartedLerping" is.
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / rotationSmoothingTime;

                //Perform the actual lerping.  Notice that the first two parameters will always be the same
                //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
                //to start another lerp)
                transformToMove.localRotation = Quaternion.Slerp(_startRotation, _endRotation, percentageComplete);

                //When we've completed the lerp, we set _isLerping to false
                if (percentageComplete >= 1.0f)
                {
                    _isLerping = false;
                }
            }
        }

        void oscRx( OscMessage message )
        {
            float x = 0;
            float y = 0;
            float z = 0;
            float w = 0;

            if (message.args.Count != 4)
            {
                Debug.LogError(transform.name + " : " + GetType() + " : " + "oscRx(): takes four valus: "+OSCaddress+" X Y Z W");
                return;
            }

            if (message.args[0] is float)
                x = (float)message.args[0];

            if (message.args[1] is float)
                y = (float)message.args[1];

            if (message.args[2] is float)
                z = (float)message.args[2];

            if (message.args[3] is float)
                w = (float)message.args[3];
            
            processQuat(x, y, z, w);

            // Get string arguments at index 0 and 1 safely.
            //string text0, text1;
            //            if( message.TryGet( 0, out text0 ) && message.TryGet( 1, out text1 ) ){
            //                Debug.Log( "Received: " + text0 + " " + text1 );
            //            }

            // If you wish to mess with the arguments yourself, you can.
            //            foreach( object a in message.args ) 
            //
            //                if( a is float ) Debug.Log( "Received: " + a );

            // NEVER DO THIS AT HOME
            // Never cast directly, without ensuring that index is inside bounds and encapsulating
            // the cast in try-catch statement.
            //float value = (float) message.args[0]; // No no!
        }
	

		void processQuat(float data0, float data1,float data2, float data3 )
        {
            if (transformToMove == null)
                return;


            float x, y, z, w;
            Quaternion targetRot;

			x = data0;
			y = data2;
			z = data3;
			w = data3;

            targetRot = new Quaternion(x, y, z, w); 

            // if true, oh shit, we'll have to constrain rotation and accept the consequences
            if (constrainX || constrainY || constrainZ)
            {
                float pitch, yaw, roll;
                Vector3 eulers = targetRot.eulerAngles;

                pitch = (constrainX) ? constaintsXYZ.x : eulers.x;
                yaw = (constrainY) ? constaintsXYZ.y : eulers.y;
                roll = (constrainZ) ? constaintsXYZ.z : eulers.z;

                targetRot = Quaternion.Euler(pitch, yaw, roll); 

            }
            _isLerping = true;
            _timeStartedLerping = Time.time;

            //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
            _startRotation = transformToMove.localRotation;
            _endRotation = targetRot;
        }
    }
}