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



// strips off last item in OSC address path, and calls corresponding method by name for all scripts of game object and children
// note,  this object can only handle at most, one datum in the received OSC message


namespace OscSimpl.Examples{

    /// <summary>
    /// Moves a GameObject in normalized coordinates (ScreenToWorldPoint)
    /// </summary>
    // [AddComponentMenu("UniOSC/MoveGameObject")]
    public class zkOSCapplyMess :  MonoBehaviour {

        private string _oscMatchAddr = "";
        private bool _start = false;
        public bool alsoSendToChildren = false;

        public bool debug = false;


        public string OSCaddress = "";      // automatically creates an OSC address using the object's name, making:  "/objName/position"

        [Tooltip("make sure that the OscIn objects in the environment have a corresponding tag")]

        public string tagName="";

        public OscIn oscIn;
         


        void Start()
        {

            if (OSCaddress.Length==0)
            {
                Debug.Log(transform.name + " : " + GetType() + " : " + "Awake(): need to specify a valid OSC address, e.g. /sheefa,  aborting");
                Destroy(this);
                return;
            }

			// no OscIn object specified, look locally
			if (oscIn == null) 
			{   // already chosen manually
				// look for the OscIn object(s) among children of gameroot using tag

				GameObject gob = aqkGameRoot.instance.gameObject;
				zk_transformTag[] zk_transformTagsCS;

				if (gob == null) {
					Debug.LogError (transform.name + " : " + GetType () + " : " + "Awake(): can't find GameRoot  parent of OSCrx object(s), aborting");
					Destroy (this);
					return;
				}

				zk_transformTagsCS = gob.GetComponentsInChildren<zk_transformTag> ();  
				foreach (zk_transformTag tag in zk_transformTagsCS) {
					if (tag.tagName.Equals (tagName))
						oscIn = tag.gameObject.GetComponent<OscIn> ();
					Debug.Log (transform.name + " : " + GetType () + " : " + "Matching on tag:" + tagName);
				}

				if (oscIn == null) {
					Debug.LogError (transform.name + " : " + GetType () + " : " + "Awake(): can't find locate OscIn object(s) among children of GameRoot, aborting");
					Destroy (this);
					return;
				}
			}

			checkOSCprependedPath ();
			_start = true;
            OnEnable();   // this can not be called until START has set up the state
        }

       void OnValidate()
        {
            if (!_start)
                return;
			if (OSCaddress != _oscMatchAddr)
                checkOSCprependedPath();
         }


        public void OnEnable()
        {
            if (!_start == true)
                return;

            //Debug.Log(transform.name + " : " + GetType() + " : " + "OnEnable()");
            if (oscIn != null)                   
				oscIn.onAnyMessage.AddListener( OnOSCMessage );  // Subscribe to all OSC messages
        }


        void OnDisable()
        {
            // Unsubscribe from messsages
            oscIn.onAnyMessage.RemoveListener( OnOSCMessage );   // unsubsccribe
        }

//		void Update()
//        {
//        }
//
		void checkOSCprependedPath()
		{
			if ( !OSCaddress.Contains("/") || !OSCaddress.StartsWith("/")  )               
			{
				Debug.LogError(string.Format("{0}.Awake():  OSC path format error: {1}, aborting", GetType(), OSCaddress), transform);
				_oscMatchAddr = "";
				return;
			}

			if (OSCaddress.EndsWith("/*"))    // all good
				_oscMatchAddr = OSCaddress.TrimEnd(new char[] { '/', '*' });
			else if (OSCaddress.EndsWith("/"))
			{
				_oscMatchAddr = OSCaddress.TrimEnd(new char[] { '/' });
				OSCaddress += "*";
			}
			else
			{
				_oscMatchAddr = OSCaddress;
				OSCaddress += "/*";
			}
		}

		public void OnOSCMessage(OscMessage msg )
        {
			string address = msg.address;
            int matchLen = _oscMatchAddr.Length;
            string methodName;
			string sval;
			float fval;
			int ival;
  
			if (msg.args.Count > 1)
            {
				Debug.LogWarning(transform.name + ": "+ GetType() + ".OnOSCMessage():  can only have maximum of one datum in message, aborting ");
                return;
            }
 
            if (address.Length <= matchLen+1)
                return;

 
            if (debug)
            {
				Debug.Log("MESS: " + address + "   count: " + msg.args.Count);
 
            }

            if (_oscMatchAddr == address.Substring(0, matchLen))
            {

                if (debug) Debug.Log("\t\taddress match: " + address.Substring(0, matchLen));
                methodName = address.Substring(matchLen + 1);
            }
            else
                return;
            
            // address matched, using remainder of address as method name

            if (debug)  Debug.Log("\t\tMESSAGE MATCHED: " +      _oscMatchAddr + " for method: "  + methodName);
                   // send message to all scripts on this gameobject, and its children
           // gameObject.BroadcastMessage(methodName, (float)msg.Data[0], SendMessageOptions.DontRequireReceiver);  

  
			// OSC address only, no data to include in message
			if (msg.args.Count == 0)
            {

                gameObject.SendMessage(methodName, null, SendMessageOptions.DontRequireReceiver);  
                if (alsoSendToChildren)
                {
                    Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
                    foreach (Transform t in transforms)
                    {
                        t.SendMessage(methodName, null, SendMessageOptions.DontRequireReceiver);  
                    }
                }

                return;
            }


			// is the datum is a string
			if( msg.TryGet( 0, out sval ) ) 
				{
					gameObject.SendMessage(methodName, sval, SendMessageOptions.DontRequireReceiver);  

					if (alsoSendToChildren)
					{
						Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
						foreach (Transform t in transforms)
						{
							t.SendMessage(methodName, sval, SendMessageOptions.DontRequireReceiver);  
						}
					}
					return;
				}


			// is the datum is a float
			if( msg.TryGet( 0, out fval )) // is it a string
			{
				gameObject.SendMessage(methodName, fval, SendMessageOptions.DontRequireReceiver);  

				if (alsoSendToChildren)
				{
					Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
					foreach (Transform t in transforms)
					{
						t.SendMessage(methodName, fval, SendMessageOptions.DontRequireReceiver);  
					}
				}
				return;
			}


			// is the datum is an int, cast as float
			if( msg.TryGet( 0, out ival )) // is it a string
			{
				gameObject.SendMessage(methodName, (float) ival, SendMessageOptions.DontRequireReceiver);  

				if (alsoSendToChildren)
				{
					Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
					foreach (Transform t in transforms)
					{
						t.SendMessage(methodName, (float) ival, SendMessageOptions.DontRequireReceiver);  
					}
				}
				return;
			}


		}
    }
}