using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LecturerTabUI : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private RectTransform content;

    [Header("Lecturers")]
    [SerializeField] private ShopLecturerUI shopLecturerUI;

    public void AddLecturer(Lecturer lecturer)
    {
        var newLecturer = Instantiate(shopLecturerUI, content);
        newLecturer.UpdateUI(lecturer);
    }
}
