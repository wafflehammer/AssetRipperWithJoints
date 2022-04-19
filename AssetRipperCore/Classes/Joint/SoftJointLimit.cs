
using AssetRipper.Core.IO.Asset;

using AssetRipper.Core.Project;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public struct SoftJointLimit : IAssetReadable, IYamlExportable
	{

		public void Read(AssetReader reader)
		{
			Limit = reader.ReadSingle();
			Bounciness = reader.ReadSingle();
			ContactDistance = reader.ReadSingle();
		}

		public YamlNode ExportYaml(IExportContainer container)
		{
			YamlMappingNode node = new YamlMappingNode();
			node.Add("limit", Limit);
			node.Add("bounciness", Bounciness);
			node.Add("contactDistance", ContactDistance);
			return node;
		}

		public float Limit { get; private set; }
		public float Bounciness { get; private set; }
		public float ContactDistance { get; private set; }
	}
}
