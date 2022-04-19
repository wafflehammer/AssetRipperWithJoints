using System.Collections.Generic;
using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Classes.Rigidbody;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public sealed class FixedJoint : Component
	{
		public FixedJoint(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		/// <summary>
		/// 2017.0.0 and greater
		/// </summary>
		private static bool IsReadMassScale(UnityVersion version)
		{
			return version.IsGreaterEqual(2017, 0);
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
			ConnectedBody.Read(reader);

			BreakForce = reader.ReadSingle();
			BreakTorque = reader.ReadSingle();
			EnableCollision = reader.ReadBoolean();
			EnablePreprocessing = reader.ReadBoolean();
			if (IsReadMassScale(reader.Version))
			{
				reader.AlignStream();
				MassScale = reader.ReadSingle();
				ConnectedMassScale = reader.ReadSingle();
			}
		}

	

		protected override YamlMappingNode ExportYamlRoot(IExportContainer container)
		{
			YamlMappingNode node = base.ExportYamlRoot(container);
			node.Add(ConnectedBodyName, ConnectedBody.ExportYaml(container));

			node.Add(BreakForceName, BreakForce);
			node.Add(BreakTorqueName, BreakTorque);
			node.Add(EnableCollisionName, EnableCollision);
			node.Add(EnablePreprocessingName, EnablePreprocessing);
			node.Add(MassScaleName, MassScale);
			node.Add(ConnectedMassScaleName, ConnectedMassScale);
			return node;
		}
		public const string ConnectedBodyName = "m_ConnectedBody";

		public const string BreakForceName = "m_BreakForce";
		public const string BreakTorqueName = "m_BreakTorque";
		public const string EnableCollisionName = "m_EnableCollision";
		public const string EnablePreprocessingName = "m_EnablePreprocessing";
		public const string MassScaleName = "m_MassScale";
		public const string ConnectedMassScaleName = "m_ConnectedMassScale";

		public PPtr<AssetRipper.Core.Classes.Rigidbody.Rigidbody> ConnectedBody;

		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }
	}
}
