using System.Collections;
using UnityEngine.EventSystems;	
using UnityEngine;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public Draggable.Slot typeOfItem = Draggable.Slot.BLUE;

	public void OnPointerEnter (PointerEventData eventData) {
		//Debug.Log ("OnPointerEnter");
		if (eventData.pointerDrag == null)
			return;
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			d.placeholderParent = this.transform;
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		//Debug.Log ("OnPointerExit");
		if (eventData.pointerDrag == null)
			return;
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null && d.placeholderParent == this.transform) {
			d.placeholderParent = this.transform;
		}
	}

	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			if (typeOfItem == d.typeOfItem || typeOfItem == Draggable.Slot.AUSWURFSPAKET || typeOfItem == Draggable.Slot.BLUE) {
				d.parentToReturnTo = this.transform;
			}
		}
	}
}
