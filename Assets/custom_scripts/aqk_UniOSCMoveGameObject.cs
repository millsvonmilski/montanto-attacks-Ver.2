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
// using OSCsharp.Data;

namespace OscSimpl.Examples
{
	public class aqk_UniOSCMoveGameObject :  MonoBehaviour {




		public string OSCaddress = "";		// automatically creates an OSC address using the object's name, making:  "/objName/position"

        [Tooltip("make sure that the OscIn objects in the environment have a corresponding tag")]

        public string tagName="undefined";

        public OscIn oscIn;

		public enum PosMapping { direct, offset }
		private Transform transformToMove;

        //movementModeProp = serializedObject.FindProperty ("movementMode");

        private Vector3 targetPos = new Vector3();
//		private Vector3 startPos = new Vector3(); // Never used warning

 
 

        [Header("")]
        public bool Xenable = true;
		public float xscale = 1;
        public PosMapping Xmapping = PosMapping.direct;
        private float _xpos;

        [Header("")]
        public bool Yenable = true;
		public float yscale = 1;
        public PosMapping Ymapping = PosMapping.direct;
        private float _ypos;

        [Header("")]
        public bool Zenable = true;
		public float zscale = 1;
        public PosMapping Zmapping = PosMapping.direct;
        private float _zpos;


        [Header("")]

        public float positionSmoothingTime = 0.25f;
        //public float smoothRotMs = 30f;
        //public float smoothQuatMs = 30f;


        //Whether we are currently interpolating or not
        private bool _isLerping;

        //The start and finish positions for the interpolation
        private Vector3 _startPosition;
        private Vector3 _endPosition;

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

            if (oscIn != null)   // already chosen manually
                return;

            // look for the OscIn object(s) among children of gameroot using tag

            GameObject gob = aqkGameRoot.instance.gameObject;
            zk_transformTag[] zk_transformTagsCS;

            if (gob == null)
            {
                Debug.LogError(transform.name + " : " + GetType() + " : " + "Awake(): can't find GameRoot  parent of OSCrx object(s), aborting");
                Destroy(this);
                return;
            }

            zk_transformTagsCS = gob.GetComponentsInChildren<zk_transformTag>();  
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

			if(transformToMove == null){
				Transform hostTransform = GetComponent<Transform>();
				if(hostTransform != null) transformToMove = hostTransform;
			}
           //Debug.Log("ONENABLE  OBJ NAME: " + transformToMove.name);

            _xpos = transform.position.x;
            _ypos = transform.position.y;
            _zpos = transform.position.z;

		}

        void OnDisable()
        {
            if (oscIn != null)
                oscIn.Unmap( oscRx );
        }



        //We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
        new void Update()
        {
            //base.FixedUpdate ();
            if(_isLerping)
            {
                //We want percentage = 0.0 when Time.time = _timeStartedLerping
                //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
                //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
                //"Time.time - _timeStartedLerping" is.
                float timeSinceStarted = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStarted / positionSmoothingTime;

                //Perform the actual lerping.  Notice that the first two parameters will always be the same
                //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
                //to start another lerp)
                transformToMove.localPosition = Vector3.Lerp (_startPosition, _endPosition, percentageComplete);

                //When we've completed the lerp, we set _isLerping to false
                if(percentageComplete >= 1.0f)
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

            if (message.args.Count != 3)
            {
                Debug.LogError(transform.name + " : " + GetType() + " : " + "oscRx(): takes three valus: "+OSCaddress+" X Y Z");
                return;
            }
                
            if (message.args[0] is float)
                x = (float)message.args[0];
            
            if (message.args[1] is float)
                y = (float)message.args[1];
            
            if (message.args[2] is float)
                z = (float)message.args[2];

            processPosition(x, y, z);

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


//		// this method is called by the UnityOSCReceiver script
//		public void OSCrx(OSC.NET.OSCMessage message)
//		{	
//			string address = message.Address;
//
//			ArrayList args = message.Values;
//
//			if (OSCaddress == "") return;
//
//
//			if (address.Contains (OSCaddress)) 
//			{
//				if (args.Count != 3 )
//				{ 
//					Debug.LogError(transform.name + " " + GetType() + ":OSCrx()  bad arg count, expects 3 floats"+ args.Count);
//					return;
//				}
//
//				if ( args[0].GetType() != typeof(float) ||  args[1].GetType() != typeof(float) ||  args[2].GetType() != typeof(float) )
//				{
//					Debug.LogError(transform.name + " " + GetType() + ":OSCrx()  bad arg type(s), expects 3 floats"+ args.Count);
//					return;
//				}
//				float x = (float) args[0];
//				float y = (float) args[1];
//				float z = (float) args[2];
//
//				processData(x, y, z);
//
//			}
//			//Debug.Log ("OSCRX:  address: " + address + " items: ");
//		}	


        public void processPosition(float data0, float data1,float data2 )
		{
			if(transformToMove == null) return;
			//OscMessage msg = (OscMessage)args.Packet;

            float x = transformToMove.transform.localPosition.x;
            float y = transformToMove.transform.localPosition.y;
            float z = transformToMove.transform.localPosition.z;

//            // handle for y no matter what
//            if (msg.Data.Count == 1)  // going to need to compute Y
//            {
//                if (Yenable)
//                {
//                    y = (Ymapping == PosMapping.direct) ? yscale * data0 : _ypos + yscale * data0;
//                }
//            }
//            else if (msg.Data.Count == 2)  // going to need to compute for XY
//            {
//                if (Xenable)
//                {
//                    x = (Xmapping == PosMapping.direct) ? xscale * data0 : _xpos + xscale * data0;
//                }
//                if (Zenable)
//                {
//                    z = (Ymapping == PosMapping.direct) ? zscale * data1 : _zpos + zscale * data1;
//                }
//
//            }
//            else if (msg.Data.Count == 3)  // going to need to compute  XYZ
//            {
                if (Xenable)
                {
                    x = (Xmapping == PosMapping.direct) ? xscale * data0 : _xpos + xscale * data0;
                }
                if (Yenable)
                {
                    y = (Ymapping == PosMapping.direct) ? yscale * data1 : _ypos + yscale * data1;
                }
                if (Zenable)
                {
                    z = (Ymapping == PosMapping.direct) ? zscale * data2 : _zpos + zscale * data2;
                }
//            }
//            else
//            {
//                Debug.LogError("qk_OscMoveGameObject.OSCrx: expects 1, two, or three values");
//                return;
//            }


            targetPos = new Vector3 (x,y,z); 
            _isLerping = true;
            _timeStartedLerping = Time.time;

            //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
            _startPosition = transformToMove.localPosition;
            _endPosition = targetPos;
		}
    }
}