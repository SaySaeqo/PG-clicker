using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LecturerTabUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform content;

    [Header("Lecturers")]
    [SerializeField] private ShopLecturerUI shopLecturerUI;

    public List<ShopLecturerUI> lecturers = new List<ShopLecturerUI>();

    public void AddLecturer(Lecturer lecturer)
    {
        var newLecturer = Instantiate(shopLecturerUI, content);
        lecturers.Add(newLecturer);
        newLecturer.UpdateUI(lecturer);
    }

    public void refreshTitles()
    {
        foreach (ShopLecturerUI lecturer in lecturers)
        {
            lecturer.refreshTitle();
        }
    }
}
