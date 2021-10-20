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
        window.minSize = new Vector2(600, 220);
        //window.maxSize
        window.Show();
    }

    void OnEnable()
    {
        InitData();
        InitVisuals();
        skin = Resources.Load<GUISkin>("GUI Styles/ItemDesignerSkin");
    }

    public static void InitData()
    {
        itemData = (ItemData)ScriptableObject.CreateInstance(typeof(ItemData));
    }

    void InitVisuals()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, new Color(0f, 0f, 0.75f, 1f));
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
        headerSection.height = 60;

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

        GUILayout.Label("Equipment Creator", skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }

    void DrawBody()
    {
        GUILayout.BeginArea(bodySection);

        GUILayout.Label("Base Item Properties", skin.GetStyle("Header2"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Name", skin.GetStyle("Body1"));
        itemData.name = EditorGUILayout.TextField(itemData.name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Equipment Type", skin.GetStyle("Body1"));
        itemData.itemType = (ItemType)EditorGUILayout.EnumPopup(itemData.itemType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item School", skin.GetStyle("Body1"));
        itemData.school = (Schools)EditorGUILayout.EnumPopup(itemData.school);
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Level Restriction", skin.GetStyle("Body1"));
        itemData.levelRestriction = EditorGUILayout.IntField(itemData.levelRestriction);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("2D Visual", skin.GetStyle("Body1"));
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

    Texture2D valueSectionTexture;
    Texture2D buttonSectionTexture;

    Rect valueSection;
    Rect addSection;
    Rect removeSection;
    Rect finishSection;

    BuffType[] setBuff = new BuffType[0];
    Schools[] setSchool = new Schools[0];
    int[] setValue = new int[0];

    public static void OpenWindow(ItemType itemType)
    {
        itemSetting = itemType;
        window = (AddedSettings)GetWindow(typeof(AddedSettings));
        window.minSize = new Vector2(400, 600);
        window.Show();
    }

    void OnEnable()
    {
        InitVisuals();
    }

    void InitVisuals()
    {
        valueSectionTexture = new Texture2D(1, 1);
        valueSectionTexture.SetPixel(0, 0, new Color(0f, 0.5f, 0f, 1f));
        valueSectionTexture.Apply();

        buttonSectionTexture = new Texture2D(1, 1);
        buttonSectionTexture.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f, 1f));
        buttonSectionTexture.Apply();
    }

    void OnGUI()
    {
        DrawLayouts();
        DrawSettings((ItemData)ItemDesignerWindow.ItemInfo); 
    }

    void DrawLayouts()
    {
        valueSection.x = 0;
        valueSection.y = 0;
        valueSection.width = Screen.width;
        valueSection.height = Screen.height - 220;

        addSection.x = 0;
        addSection.y = valueSection.height;
        addSection.width = Screen.width;
        addSection.height = 80;

        removeSection.x = 0;
        removeSection.y = valueSection.height + addSection.height;
        removeSection.width = Screen.width;
        removeSection.height = 80;

        finishSection.x = 0;
        finishSection.y = valueSection.height + addSection.height + removeSection.height;
        finishSection.width = Screen.width;
        finishSection.height = 40;

        GUI.DrawTexture(valueSection, valueSectionTexture);
        GUI.DrawTexture(addSection, buttonSectionTexture);
        GUI.DrawTexture(removeSection, buttonSectionTexture);
        GUI.DrawTexture(finishSection, buttonSectionTexture);
    }

    void DrawSettings(ItemData itemData)
    {
        #region addButtons

        GUILayout.BeginArea(valueSection);
        for (int i = 0; i < itemData.BuffArray.Length; i++)
        {
            GUILayout.BeginHorizontal();
            int arrayLength = itemData.BuffArray.Length;
            GUILayout.Label("Buff " + (i + 1));
            setBuff[i] = (BuffType)EditorGUILayout.EnumPopup(setBuff[i]);
            setSchool[i] = (Schools)EditorGUILayout.EnumPopup(setSchool[i]);
            setValue[i] = EditorGUILayout.IntField(setValue[i]);
            itemData.BuffArray[i] = (Buff)ScriptableObject.CreateInstance("Buff");
            itemData.BuffArray[i].Init(setBuff[i], setSchool[i], setValue[i]);
            GUILayout.EndHorizontal();
        }

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
        GUILayout.EndArea();

        GUILayout.BeginArea(addSection);
        if (GUILayout.Button("Add Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.BuffArray.Length;
            Buff[] tempArray = itemData.BuffArray;
            itemData.BuffArray = new Buff[arrayLength + 1];
            BuffType[] tempBuff = setBuff;
            setBuff = new BuffType[arrayLength + 1];
            Schools[] tempSchool = setSchool;
            setSchool = new Schools[arrayLength + 1];
            int[] valueArray = setValue;
            setValue = new int[arrayLength + 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                itemData.BuffArray[i] = tempArray[i];
                setValue[i] = valueArray[i];
                setBuff[i] = tempBuff[i];
                setSchool[i] = tempSchool[i];
            }
        }

        if (itemData.maxHP.Length == 0)
        {
            if (GUILayout.Button("Add HP Parameter", GUILayout.Height(20)))
            {
                int arrayLength = itemData.maxHP.Length;
                itemData.maxHP = new int[arrayLength + 1];
            }
        }

        if (itemData.maxMP.Length == 0)
        {
            if (GUILayout.Button("Add MP Parameter", GUILayout.Height(20)))
            {
                int arrayLength = itemData.maxMP.Length;
                itemData.maxMP = new int[arrayLength + 1];
            }
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(removeSection);
        if (GUILayout.Button("Remove Buff", GUILayout.Height(30)))
        {
            int arrayLength = itemData.BuffArray.Length;
            if(arrayLength > 0)
            {
                Buff[] tempArray = itemData.BuffArray;
                itemData.BuffArray = new Buff[arrayLength - 1];
                BuffType[] tempBuff = setBuff;
                setBuff = new BuffType[arrayLength - 1];
                Schools[] tempSchool = setSchool;
                setSchool = new Schools[arrayLength - 1];
                int[] valueArray = setValue;
                setValue = new int[arrayLength - 1];
                for (int i = 0; i < arrayLength - 1; i++)
                {
                    itemData.BuffArray[i] = tempArray[i];
                    setValue[i] = valueArray[i];
                    setBuff[i] = tempBuff[i];
                    setSchool[i] = tempSchool[i];
                }
            }         
        }

        if (itemData.maxHP.Length > 0)
        {
            if (GUILayout.Button("Remove HP Parameter", GUILayout.Height(20)))
            {
                int arrayLength = itemData.maxHP.Length;
                itemData.maxHP = new int[arrayLength - 1];
            }
        }

        if (itemData.maxMP.Length > 0)
        {
            if (GUILayout.Button("Remove MP Parameter", GUILayout.Height(20)))
            {
                int arrayLength = itemData.maxMP.Length;
                itemData.maxMP = new int[arrayLength - 1];
            }
        }
        GUILayout.EndArea();

        #endregion
        GUILayout.BeginArea(finishSection);
        if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
            ItemDesignerWindow.window.Close();
        }
        GUILayout.EndArea();
    }

    void SaveCharacterData()
    {
        string objectPath = "Assets/Objects/Items/";
        string dataPath = "Assets/Objects/ItemData/";

        for(int i = 0; i < ItemDesignerWindow.ItemInfo.BuffArray.Length; i++)
        {
            string buffPath = "Assets/Objects/BuffData/" + ItemDesignerWindow.ItemInfo.name + " Buff " + (i + 1) + ".asset";
            AssetDatabase.CreateAsset(ItemDesignerWindow.ItemInfo.BuffArray[i], buffPath);
        }

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
