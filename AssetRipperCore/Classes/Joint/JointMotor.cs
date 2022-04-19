
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public struct JointMotor : IAssetReadable, IYamlExportable
	{

		public void Read(AssetReader reader)
		{
			TargetVelocity = reader.ReadSingle();
			Force = reader.ReadSingle();
			FreeSpin = reader.ReadSingle();
		}

		public YamlNode ExportYaml(IExportContainer container)
		{
			YamlMappingNode node = new YamlMappingNode();
			node.Add("targetVelocity", TargetVelocity);
			node.Add("force", Force);
			node.Add("freeSpin", FreeSpin);
			return node;
		}

		public float TargetVelocity { get; private set; }
		public float Force { get; private set; }
		public float FreeSpin { get; private set; }
	}
}
