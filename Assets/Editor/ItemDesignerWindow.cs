using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDesignerWindow : EditorWindow
{
    [MenuItem("Window/Item Designer")]
    static void OpenWindow()
    {
        ItemDesignerWindow window = (ItemDesignerWindow)GetWindow(typeof(ItemDesignerWindow));
        window.minSize = new Vector2(600, 300);
        //window.maxSize
        window.Show();
    }
}
