using UnityEngine;

public class UISkillTree : MonoBehaviour
{
    // This will be a more complex UI, but the basic structure is here.

    public Transform skillTreeGrid;
    public GameObject skillNodePrefab;

    // This would be called when the skill tree UI is opened.
    public void PopulateSkillTree(SkillData[] allSkills)
    {
        // Clear existing nodes
        foreach (Transform child in skillTreeGrid)
        {
            Destroy(child.gameObject);
        }

        // Create a UI node for each skill
        foreach (var skill in allSkills)
        {
            GameObject node = Instantiate(skillNodePrefab, skillTreeGrid);
            // Here you would set the skill name, icon, connections, etc.
            // You'd also add a button listener to handle skill unlocking.
        }
    }
}
