using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpritesHolder", menuName = "ScriptableObjects/SpritesHolder", order = 1)]
public class SpriteHolder : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;

    public Sprite GetSpriteByName(string spriteName)
    {
        return sprites.Find(e => string.Equals(e.name, spriteName));
    }
}
