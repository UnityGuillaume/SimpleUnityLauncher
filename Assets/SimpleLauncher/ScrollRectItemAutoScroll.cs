using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//This replace the toggle in the template children of the dropdown.
//Careful this make a lot of assumption (vertical inverted scroll) as it is just for that specific use case
//won't work on other type, will even error out as it doesn't null guard against not having a vertical scrollbar
public class ScrollRectItemAutoScroll : Toggle
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        var dropdown = GetComponentInParent<Dropdown>();
        if (dropdown != null)
        {
            //Ew that's ugly to do that every time but no idea how to easily get that value...
            int num = 0;
            foreach (Transform child in transform.parent)
            {
                if (child == transform) break;
                num++;
            }
            
            ScrollRect scrollRect = dropdown.GetComponentInChildren<ScrollRect>();
            float valuePosition = 1.0f - num / (float) transform.parent.childCount;

            scrollRect.verticalScrollbar.value = valuePosition;
        }
    }
}
