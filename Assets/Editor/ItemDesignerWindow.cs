using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class ItemDesignerWindow : EditorWindow
{
    public static ItemDesignerWindow window;

    Texture2D headerSectionTexture;
    Texture2D bodySectionTexture;

    Rect headerSection;
    Rect bodySection;

    static ItemData itemData;

    GUISkin skin;

    public static ItemData ItemInfo { get { return itemData; } }

    [MenuItem("Window/Item Designer")]
    static void OpenWindow()
    {
        window = (ItemDesignerWindow)GetWindow(typeof(ItemDesignerWindow));
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

        if (itemData.name == null || itemData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This Item needs a [Name] before it can be further modified", MessageType.Warning);
        }
        else if (itemData.Visual2D == null)
        {
            EditorGUILayout.HelpBox("This Item needs a [Sprite] before it can be further modified", MessageType.Warning);
        }
        else if (GUILayout.Button("Create", GUILayout.Height(40)))
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

    void OnGUI()
    {
        DrawSettings((ItemData)ItemDesignerWindow.ItemInfo);
    }

    void DrawSettings(ItemData itemData)
    {
        #region addButtons

        for (int i = 0; i < itemData.maxHP.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max HP");
            int arrayLength = itemData.maxHP.Length;
            itemData.maxHP[arrayLength - 1] = EditorGUILayout.IntField(itemData.maxHP[arrayLength - 1]);
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < itemData.maxMP.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max MP");
            int arrayLength = itemData.maxMP.Length;
            itemData.maxMP[arrayLength - 1] = EditorGUILayout.IntField(itemData.maxMP[arrayLength - 1]);
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < itemData.damageBuff.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Damage Buff");
            int arrayLength = itemData.damageBuff.Length;
            itemData.damageBuff[arrayLength - 1].buffSchool = (Schools)EditorGUILayout.EnumPopup(itemData.damageBuff[arrayLength - 1].buffSchool);
            itemData.damageBuff[arrayLength - 1].buffValue = EditorGUILayout.IntField(itemData.damageBuff[arrayLength - 1].buffValue);
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < itemData.accuracyBuff.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Accuracy Buff");
            int arrayLength = itemData.accuracyBuff.Length;
            itemData.accuracyBuff[arrayLength - 1].buffSchool = (Schools)EditorGUILayout.EnumPopup(itemData.accuracyBuff[arrayLength - 1].buffSchool);
            itemData.accuracyBuff[arrayLength - 1].buffValue = EditorGUILayout.IntField(itemData.accuracyBuff[arrayLength - 1].buffValue);
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < itemData.critBuff.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Crit Rate Buff");
            int arrayLength = itemData.critBuff.Length;
            itemData.critBuff[arrayLength - 1].buffSchool = (Schools)EditorGUILayout.EnumPopup(itemData.critBuff[arrayLength - 1].buffSchool);
            itemData.critBuff[arrayLength - 1].buffValue = EditorGUILayout.IntField(itemData.critBuff[arrayLength - 1].buffValue);
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < itemData.resistanceBuff.Length; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Resistance Buff");
            int arrayLength = itemData.resistanceBuff.Length;
            itemData.resistanceBuff[arrayLength - 1].buffSchool = (Schools)EditorGUILayout.EnumPopup(itemData.resistanceBuff[arrayLength - 1].buffSchool);
            itemData.resistanceBuff[arrayLength - 1].buffValue = EditorGUILayout.IntField(itemData.resistanceBuff[arrayLength - 1].buffValue);
            GUILayout.EndHorizontal();
        }

        if (itemData.maxHP.Length == 0)
        {
            if (GUILayout.Button("Add HP Parameter", GUILayout.Height(30)))
            {
                int arrayLength = itemData.maxHP.Length;
                itemData.maxHP = new int[arrayLength + 1];
            }
        }

        if (itemData.maxMP.Length == 0)
        {
            if (GUILayout.Button("Add MP Parameter", GUILayout.Height(30)))
            {
                int arrayLength = itemData.maxMP.Length;
                itemData.maxMP = new int[arrayLength + 1];
            }
        }

        if (GUILayout.Button("Add Damage Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.damageBuff.Length;
            itemData.damageBuff = new DamageBuff[arrayLength + 1];
        }


        if (GUILayout.Button("Add Accuracy Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.accuracyBuff.Length;
            itemData.accuracyBuff = new AccuracyBuff[arrayLength + 1];
        }

        if (GUILayout.Button("Add Crit Rate Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.critBuff.Length;
            itemData.critBuff = new CritBuff[arrayLength + 1];
        }

        if (GUILayout.Button("Add Resistance Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.resistanceBuff.Length;
            itemData.resistanceBuff = new ResistanceBuff[arrayLength + 1];
        }
        

        #endregion

        if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
            ItemDesignerWindow.window.Close();
        }
    }

    void SaveCharacterData()
    {
        string objectPath = "Assets/Objects/Items/";
        string dataPath = "Assets/Resources/ItemData/Data/";

        switch (itemSetting)
        {
            #region switchHell
            case ItemType.HAT:

                dataPath += "Hats/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Hats/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject hatObject = new GameObject();
                if (!hatObject.GetComponent<Item>())
                {
                    hatObject.AddComponent(typeof(Item));
                }
                hatObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(hatObject, objectPath);
                DestroyImmediate(hatObject);

                break;

            case ItemType.ROBE:

                dataPath += "Robes/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Robes/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject robeObject = new GameObject();
                if (!robeObject.GetComponent<Item>())
                {
                    robeObject.AddComponent(typeof(Item));
                }
                robeObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(robeObject, objectPath);
                DestroyImmediate(robeObject);

                break;

            case ItemType.SHOES:

                dataPath += "Shoes/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Shoes/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject shoesObject = new GameObject();
                if (!shoesObject.GetComponent<Item>())
                {
                    shoesObject.AddComponent(typeof(Item));
                }
                shoesObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(shoesObject, objectPath);
                DestroyImmediate(shoesObject);

                break;

            case ItemType.WAND:

                dataPath += "Wands/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Wands/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject wandObject = new GameObject();
                if (!wandObject.GetComponent<Item>())
                {
                    wandObject.AddComponent(typeof(Item));
                }
                wandObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(wandObject, objectPath);
                DestroyImmediate(wandObject);

                break;

            case ItemType.AMULET:

                dataPath += "Amulets/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Amulets/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject amuletObject = new GameObject();
                if (!amuletObject.GetComponent<Item>())
                {
                    amuletObject.AddComponent(typeof(Item));
                }
                amuletObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(amuletObject, objectPath);
                DestroyImmediate(amuletObject);

                break;

            case ItemType.ATHAME:

                dataPath += "Athames/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Athames/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject athameObject = new GameObject();
                if (!athameObject.GetComponent<Item>())
                {
                    athameObject.AddComponent(typeof(Item));
                }
                athameObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(athameObject, objectPath);
                DestroyImmediate(athameObject);

                break;

            case ItemType.RING:

                dataPath += "Rings/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Rings/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject ringObject = new GameObject();
                if (!ringObject.GetComponent<Item>())
                {
                    ringObject.AddComponent(typeof(Item));
                }
                ringObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(ringObject, objectPath);
                DestroyImmediate(ringObject);

                break;

            case ItemType.DECK:

                dataPath += "Decks/" + ItemDesignerWindow.ItemInfo.name + ".asset";
                AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo, dataPath);

                objectPath += "Decks/" + ItemDesignerWindow.ItemInfo.name + ".prefab";

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject deckObject = new GameObject();
                if (!deckObject.GetComponent<Item>())
                {
                    deckObject.AddComponent(typeof(Item));
                }
                deckObject.GetComponent<Item>().itemData = ItemDesignerWindow.ItemInfo;
                PrefabUtility.SaveAsPrefabAsset(deckObject, objectPath);
                DestroyImmediate(deckObject);

                break;
                #endregion
        }
    }
}
