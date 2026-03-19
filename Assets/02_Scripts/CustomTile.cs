using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New CustomTile Tile", menuName = "Tiles/CustomTile")]
public class CustomTile : Tile
{
    public bool isNormal;   // 이 타일이 정상 타일인지 여부
    public bool isFix; // 이 타일이 수리 가능한 타일인지 여부
    public bool isBuff; // 이 타일이 버프 타일인지 여부
    public string uiName; // UI에 표시할 타일 이름
    public int cost; // 비용
}
