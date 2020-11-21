using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SkillButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public SkillGroup skillGroup;
    public UnityEvent onSkillSelected;
    public UnityEvent onSkillDeselected;

    public void OnPointerExit(PointerEventData eventData)
    {
        skillGroup.OnSkillSelected(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        skillGroup.OnSkillSelected(this);
    }

    public void Select()
    {
        if (onSkillSelected != null)
        {
            onSkillSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onSkillDeselected != null)
        {
            onSkillDeselected.Invoke();
        }
    }
}
