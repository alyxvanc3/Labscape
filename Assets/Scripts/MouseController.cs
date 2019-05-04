using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseController : Controller, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        controllable.SpecialJump();
    }

}
