using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCollection", menuName = "Cards/Simple Card Collection")]
public class CardCollection : ScriptableObject
{
    [Header("Collection Info")]
    public string collectionName;

    [Header("Description")]
    [TextArea(2,5)]
    public string description;

    [Header("White Cards")]
    [TextArea(2, 5)]
    public List<string> whiteCards = new List<string>();

    [Header("Black Cards")]
    public List<BlackCard> blackCards = new List<BlackCard>();
}
