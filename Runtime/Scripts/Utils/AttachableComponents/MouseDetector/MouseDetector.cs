// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityGamesToolkit.Runtime
{
    /// <summary>
    /// Detects mouse events on an object and invokes corresponding UnityEvents.
    /// </summary>
    public class MouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent onMouseEnter; // Event invoked when the mouse starts hovering over the object.
        public UnityEvent onMouseOver; // Event invoked while the mouse is hovering over the object.
        public UnityEvent onMouseExit; // Event invoked when the mouse is no longer pointing at the object.
        public UnityEvent onMouseDown; // Event invoked when the mouse button is pressed over the object.
        public UnityEvent onMouseUp; // Event invoked when the mouse button is released over the object.
        public UnityEvent onMouseHold; // Event invoked while the mouse button is held down over the object.

        private bool isMouseOver; // Flag to track if the mouse is currently over the object.
        private bool isMouseHeld; // Flag to track if the mouse button is being held down over the object.

        /// <summary>
        /// Event handler when the mouse enters the object's collider.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnter?.Invoke();
            Debug.Log("Mouse Enter: " + gameObject.name);
            InvokeRepeating(nameof(ContinuousMouseOver), 0f, 0.1f); // Invokes the ContinuousMouseOver method with a 0.1s interval.
        }

        /// <summary>
        /// Event handler when the mouse exits the object's collider.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseExit?.Invoke();
            Debug.Log("Mouse Exit: " + gameObject.name);
            isMouseOver = false;
            CancelInvoke(nameof(ContinuousMouseOver)); // Stops invoking the ContinuousMouseOver method.
        }

        /// <summary>
        /// Event handler when the mouse button is pressed over the object's collider.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            onMouseDown?.Invoke();
            Debug.Log("Mouse Down: " + gameObject.name);
            isMouseHeld = true;
        }

        /// <summary>
        /// Event handler when the mouse button is released over the object's collider.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            onMouseUp?.Invoke();
            Debug.Log("Mouse Up: " + gameObject.name);
            isMouseHeld = false;
        }

        /// <summary>
        /// Method called continuously while the mouse is over the object.
        /// </summary>
        private void ContinuousMouseOver()
        {
            onMouseOver?.Invoke();
            Debug.Log("Mouse Over: " + gameObject.name);
            if (isMouseHeld)
            {
                onMouseHold?.Invoke();
                Debug.Log("Mouse Hold: " + gameObject.name);
            }
        }

        /// <summary>
        /// Event handler when the mouse is pressed down over the object.
        /// </summary>
        private void OnMouseDown()
        {
            onMouseDown?.Invoke();
            Debug.Log("Mouse Down: " + gameObject.name);
            isMouseHeld = true;
        }

        /// <summary>
        /// Event handler when the mouse is released over the object.
        /// </summary>
        private void OnMouseUp()
        {
            onMouseUp?.Invoke();
            Debug.Log("Mouse Up: " + gameObject.name);
            isMouseHeld = false;
        }

        /// <summary>
        /// Event handler when the mouse is held down over the object.
        /// </summary>
        private void OnMouseOver()
        {
            if (!isMouseOver)
            {
                onMouseEnter?.Invoke();
                Debug.Log("Mouse Enter: " + gameObject.name);
                isMouseOver = true;
                InvokeRepeating(nameof(ContinuousMouseOver), 0f, 0.1f);
            }
        }

        /// <summary>
        /// Event handler when the mouse is no longer pointing at the object.
        /// </summary>
        private void OnMouseExit()
        {
            onMouseExit?.Invoke();
            Debug.Log("Mouse Exit: " + gameObject.name);
            isMouseOver = false;
            CancelInvoke(nameof(ContinuousMouseOver));
        }
    }
}
