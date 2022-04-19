
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public struct JointLimits : IAssetReadable, IYamlExportable
	{

		public void Read(AssetReader reader)
		{
			Min = reader.ReadSingle();
			Max = reader.ReadSingle();
			Bounciness = reader.ReadSingle();
			BounceMinVelocity = reader.ReadSingle();
			ContactDistance = reader.ReadSingle();
		}

		public YamlNode ExportYaml(IExportContainer container)
		{
			YamlMappingNode node = new YamlMappingNode();
			node.Add("min", Min);
			node.Add("max", Max);
			node.Add("bounciness", Bounciness);
			node.Add("bounceMinVelocity", BounceMinVelocity);
			node.Add("contactDistance", ContactDistance);
			return node;
		}

		public float Min { get; private set; }
		public float Max { get; private set; }
		public float Bounciness { get; private set; }
		public float BounceMinVelocity { get; private set; }
		public float ContactDistance { get; private set; }
	}
}
