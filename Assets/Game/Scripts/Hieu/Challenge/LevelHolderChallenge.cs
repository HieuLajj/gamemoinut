using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Linq;
public class LevelHolderChallenge : MonoBehaviour
{
    public List<ChallengeHole> ChallengeHole;
    public Color[] colorMain;
}
//#if UNITY_EDITOR
//[CustomEditor(typeof(LevelHolderChallenge))]
//public class testeditor4 : Editor
//{
//    public LevelController controller;
//    public List<string> names = new List<string>();
//    public List<ChallengeHole> hehehe = new List<ChallengeHole>();
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        if (GUILayout.Button("test", GUILayout.Width(90F)))
//        {
//            Sprite image = Resources.Load<Sprite>("Hieu/base_1");
//            var things = (LevelHolderChallenge)target;
//            things.ChallengeHole = target.GetComponentsInChildren<ChallengeHole>().ToList();

//            foreach (ChallengeHole th in things.ChallengeHole)
//            {
//                if (!names.Contains(th.gameObject.name))
//                {
//                    SpriteRenderer sprite = th.GetComponent<SpriteRenderer>();
//                    sprite.sprite = image;





//                    //hehehe.Add(th);
//                    //names.Add(th.name);
//                }
//            }
//            //hehehe = hehehe.OrderBy(go => go.gameObject.name).ToList();
//            //things.colorMain = new Color[hehehe.Count];

//            //for (int i = 0; i < hehehe.Count; i++)
//            //{
//            //    things.colorMain[i] = hehehe[i].currentColor;
//            //}
//        }
//    }
//}
//#endif