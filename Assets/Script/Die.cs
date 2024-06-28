using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Die
{
    [Serializable]
    public class DieFace
    {
        public DieFaceTag Tag;
        public DieFace(){}

        public DieFace(DieFace dieFace)
        {
            Tag = dieFace.Tag;
        }
    }
    public enum DieFaceTag
    {
        None,
        Attack,
        Dodge,
        Defend,
        Recover
    }

    public enum Action
    {
        None,
        Attack,
        Defend,
        Recover
    }

    public List<DieFace> dieFaces;
    public List<DieFaceTag> dieFaceTags=>dieFaces.Select(p=>p.Tag).ToList();
    public DieFaceTag resultTag;
    public DieFace Roll()
    {
        var resultIndexes= Utility.SelectNumbers(dieFaces.Count, 1);
        var index= resultIndexes[0];
        var resultFace = dieFaces[index];
        resultTag = resultFace.Tag;
        return new DieFace(resultFace);
    }
}