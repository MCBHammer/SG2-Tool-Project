using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class ItemDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D bodySectionTexture;

    Rect headerSection;
    Rect bodySection;

    static ItemData itemData;
    static AddedData addedData;

    GUISkin skin;

    public static ItemData ItemInfo { get { return itemData; } }
    public static AddedData AddedInfo { get { return addedData; } }

    [MenuItem("Window/Item Designer")]
    static void OpenWindow()
    {
        ItemDesignerWindow window = (ItemDesignerWindow)GetWindow(typeof(ItemDesignerWindow));
        window.minSize = new Vector2(600, 300);
        //window.maxSize
        window.Show();
    }

    void OnEnable()
    {
        InitData();
        InitVisuals();
    }

    public static void InitData()
    {
        itemData = (ItemData)ScriptableObject.CreateInstance(typeof(ItemData));
    }

    void InitVisuals()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, new Color(0f, 0f, 1f, 1f));
        headerSectionTexture.Apply();

        bodySectionTexture = new Texture2D(1, 1);
        bodySectionTexture.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f, 1f));
        bodySectionTexture.Apply();
    }

    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawBody();
    }

    void DrawLayouts() 
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        bodySection.x = 0;
        bodySection.y = headerSection.height;
        bodySection.width = Screen.width;
        bodySection.height = Screen.height - headerSection.height;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(bodySection, bodySectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Equipment Creator");

        GUILayout.EndArea();
    }

    void DrawBody()
    {
        GUILayout.BeginArea(bodySection);

        GUILayout.Label("Base Item Properties");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Name");
        itemData.name = EditorGUILayout.TextField(itemData.name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Equipment Type");
        itemData.itemType = (ItemType)EditorGUILayout.EnumPopup(itemData.itemType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item School");
        itemData.school = (Schools)EditorGUILayout.EnumPopup(itemData.school);
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Level Restriction");
        itemData.levelRestriction = EditorGUILayout.IntField(itemData.levelRestriction);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("2D Visual");
        itemData.Visual2D = (Sprite)EditorGUILayout.ObjectField(itemData.Visual2D, typeof(Sprite), false);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            AddedSettings.OpenWindow(itemData.itemType);
        }

        GUILayout.EndArea();
    }
}

public class AddedSettings : EditorWindow
{
    static AddedSettings window;
    static ItemType itemSetting;

    public static void OpenWindow(ItemType itemType)
    {
        itemSetting = itemType;
        window = (AddedSettings)GetWindow(typeof(AddedSettings));
        window.minSize = new Vector2(400, 300);
        window.Show();
    }
}
