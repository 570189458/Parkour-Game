using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternSystem : EditorWindow
{
    [MenuItem("Window/AddPatternToSystem")]
    static void AddPatternToSystem()
    {
        var gameManager = GameObject.Find("GameManager");
        if(gameManager!=null)
        {
            var patternManager = gameManager.GetComponent<PatternManager>();
            if(Selection.gameObjects.Length==1)
            {
#pragma warning disable CS0618 // 类型或成员已过时
                var item = Selection.gameObjects[0].transform.FindChild("Item");
#pragma warning restore CS0618 // 类型或成员已过时
                if(item!=null)
                {
                    Pattern pattern = new Pattern();
                    foreach (var child in item)
                    {
                        Transform childTransform = child as Transform;
                        if(childTransform!=null)
                        {
#pragma warning disable CS0618 // 类型或成员已过时
                            var prefab = UnityEditor.PrefabUtility.GetPrefabParent(childTransform.gameObject);
#pragma warning restore CS0618 // 类型或成员已过时
                            if (prefab != null)
                            {
                                PatternItem patternItem = new PatternItem
                                {

                                    gameObject = prefab as GameObject,
                                    pos = childTransform.transform.localPosition
                                };
                                pattern.patternItems.Add(patternItem);
                            }
                        }
                    }
                    patternManager.patterns.Add(pattern);
                }
            }
        }
    }
}
