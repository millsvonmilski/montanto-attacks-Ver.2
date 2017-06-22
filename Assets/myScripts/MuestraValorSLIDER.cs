//using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]

public class MuestraValorSLIDER : MonoBehaviour 
{
	public Slider slider;
	public controllerGENERAL controladorNave;

	public void Start()
	{
		slider =  slider.GetComponent<Slider>();

		GameObject controladorObjectNave = GameObject.FindWithTag ("MainCamera"); 
		
			if (controladorObjectNave != null) 
			{
				controladorNave = controladorObjectNave.GetComponent<controllerGENERAL> ();
			} 

			//Adds a listener to the main slider and invokes a method when the value changes.
    		slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    { 
			controladorNave.nombreAsteroids = (int) slider.value;
			Debug.Log(slider.value);
    }
	public void UpdateLabel (float value)
	{
		Text lbl = GetComponent<Text>();
		if (lbl != null)
			//lbl.text = Mathf.RoundToInt (value * 100) + "%";
			lbl.text = slider.value.ToString();
			Debug.Log("A VER QUE PASA " + lbl.text);
	}
}


