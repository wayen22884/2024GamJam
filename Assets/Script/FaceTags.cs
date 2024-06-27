using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create FaceTags", fileName = "FaceTags", order = 0)]
public class FaceTags : ScriptableObject
{
    public List<FaceTag> List = new();
}
[Serializable]
public class FaceTag
{
    public Die.DieFaceTag DieFace;
    public Sprite Sprite;
}