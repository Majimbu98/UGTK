using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject that stores VFX data.
/// </summary>
[CreateAssetMenu(fileName = "VFXData", menuName = "ScriptableObjects/VFXData", order = 1)]
public class VFXData : ScriptableObject
{
    [SerializeField] private List<VFXinfo> characterList = new List<VFXinfo>();

    /// <summary>
    /// Gets the list of VFX information.
    /// </summary>
    public List<VFXinfo> CharacterList => characterList;

    /// <summary>
    /// Loads VFX data from a CSV file.
    /// </summary>
    /// <param name="filePath">The path of the CSV file.</param>
    public void Load(string filePath)
    {
        characterList.Clear();

        string[] lines = System.IO.File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            if (values.Length == 3)
            {
                string vfxName = values[0].Trim();
                string effect = values[1].Trim();
                string category = values[2].Trim();

                VFXinfo vfxInfo = new VFXinfo(vfxName, effect, category);
                characterList.Add(vfxInfo);
            }
        }
    }

    /// <summary>
    /// Saves VFX data to a CSV file.
    /// </summary>
    /// <param name="filePath">The path of the CSV file to save.</param>
    public void Save(string filePath)
    {
        List<string> lines = new List<string>();
        lines.Add("VFX name,Effect,Category");

        foreach (VFXinfo vfxInfo in characterList)
        {
            string category = string.Empty;
            switch (vfxInfo.category)
            {
                case Category.EnemyBossRelated:
                    category = "EnemyBossRelated";
                    break;
                case Category.OtherEnvironmental:
                    category = "OtherEnvironmental";
                    break;
                case Category.OrinnRelated:
                    category = "OrinnRelated";
                    break;
            }

            string line = $"{vfxInfo.VFXName},{vfxInfo.VFXInfo},{category}";
            lines.Add(line);
        }

        System.IO.File.WriteAllLines(filePath, lines.ToArray());
    }
}
