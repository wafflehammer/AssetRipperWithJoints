using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;

using AssetRipper.Core.Project;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public struct JointDrive : IAssetReadable, IYamlExportable
	{

		private static int GetSerializedVersion(UnityVersion version)
		{
			// TODO:
			return 2;
		}

		public void Read(AssetReader reader)
		{
			PositionSpring = reader.ReadSingle();
			PositionDamper = reader.ReadSingle();
			MaximumForce = reader.ReadSingle();
		}

		public YamlNode ExportYaml(IExportContainer container)
		{
			YamlMappingNode node = new YamlMappingNode();
			node.AddSerializedVersion(GetSerializedVersion(container.ExportVersion));
			node.Add("positionSpring", PositionSpring);
			node.Add("positionDamper", PositionDamper);
			node.Add("maximumForce", MaximumForce);
			return node;
		}



		public float PositionSpring { get; private set; }
		public float PositionDamper { get; private set; }
		public float MaximumForce { get; private set; }
	}
}
