using AssetRipper.Core.IO.Asset;

using AssetRipper.Core.Project;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public struct SoftJointLimitSpring : IAssetReadable, IYamlExportable
	{

		public void Read(AssetReader reader)
		{
			Spring = reader.ReadSingle();
			Damper = reader.ReadSingle();
		}

		public YamlNode ExportYaml(IExportContainer container)
		{
			YamlMappingNode node = new YamlMappingNode();
			node.Add("spring", Spring);
			node.Add("damper", Damper);
			return node;
		}

		public float Spring { get; private set; }
		public float Damper { get; private set; }
	}
}
