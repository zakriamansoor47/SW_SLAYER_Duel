using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.Natives;
using System.Text.Json;

namespace SLAYER_Duel;

public partial class SLAYER_Duel : BasePlugin
{
    private string GetMapTeleportPositionConfigPath()
    {
        if (Core.Configuration.BasePathExists)
        {
            return $"{Core.Configuration.BasePath}/Duel_TeleportPositions.json";
        }
        return $"{Core.PluginPath}/Duel_TeleportPositions.json";
    }
    private (Vector?, QAngle?) GetPositionFromFile(int TeamNum)
    {
        var mapData = Duel_Positions[Core.Engine.GlobalVars.MapName]; // Get Current Map Teleport Positions
        if (TeamNum == 2 && mapData.ContainsKey("T_Pos") && mapData["T_Pos"] != "") // If player team is Terrorist then get the T_Pos from File
        {
            string[] Data = mapData["T_Pos"].Split(";"); // Seperate Position and Rotation
            string[] Position = Data[0].Split(" "); // Split Coordinates with space " "
            string[] Rotation = Data[1].Split(" "); // Split Rotation with space " "
            return (new Vector(float.Parse(Position[0]), float.Parse(Position[1]), float.Parse(Position[2])), new QAngle(float.Parse(Rotation[0]), float.Parse(Rotation[1]), float.Parse(Rotation[2]))); // Return Coordinates in Vector
        }
        else if(TeamNum == 3 && mapData.ContainsKey("CT_Pos") && mapData["CT_Pos"] != "") // If player team is C-Terrorist then get the CT_Pos from File
        {
            string[] Data = mapData["CT_Pos"].Split(";"); // Seperate Position and Rotation
            string[] Position = Data[0].Split(" "); // Split Coordinates with space " "
            string[] Rotation = Data[1].Split(" "); // Split Rotation with space " "
            return (new Vector(float.Parse(Position[0]), float.Parse(Position[1]), float.Parse(Position[2])), new QAngle(float.Parse(Rotation[0]), float.Parse(Rotation[1]), float.Parse(Rotation[2]))); // Return Coordinates in Vector
        }
        return (null, null);
    }
    private void LoadPositionsFromFile()
    {
        if (!File.Exists(GetMapTeleportPositionConfigPath()))
		{
			return;
		}
		
		var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(GetMapTeleportPositionConfigPath()));
		
		if(data != null)
		{
			Duel_Positions = data;
		}
    }
}