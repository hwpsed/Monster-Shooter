using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroup : MonoBehaviour
{
    public List<SkillButton> skillButton;
    public SkillButton selectedSkill;
    public List<GameObject> objectToSwap;

    public void Subcribe(SkillButton button)
    {
        if (skillButton == null)
        {
            skillButton = new List<SkillButton>();
        }
        skillButton.Add(button);
    }
    

    public void OnSkillExit(SkillButton button)
    {
        ResetSkill();
    }

    public void OnSkillSelected(SkillButton button)
    {
        if(selectedSkill != null)
        {
            selectedSkill.Deselect();
        }

        selectedSkill = button;
        selectedSkill.Select();
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectToSwap.Count; i++)
        {
            if (i == index)
            {
                objectToSwap[i].SetActive(true);
            } 
            else
            {
                objectToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetSkill()
    {
        foreach (SkillButton button in skillButton)
        {
            if(selectedSkill != null && button == selectedSkill) { continue; }
        }
    }
}
