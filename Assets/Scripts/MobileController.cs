using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MobileController : Controller, IPointerDownHandler {

    #region IPointerDownHandler implementation

    public void OnPointerDown (PointerEventData eventData)
	{
        controllable.SpecialJump();
    }

	#endregion


}
