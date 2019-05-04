using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {
	protected IControllable controllable;
    protected bool isPressed = false;
    protected float power = 20f;
    
    public virtual void Initialize(IControllable c){
		controllable = c;
        Input.multiTouchEnabled = false;
	}
	
}
