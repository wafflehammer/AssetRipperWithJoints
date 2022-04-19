using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Math.Vectors;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project;
using AssetRipper.Yaml;
using System.Collections.Generic;

namespace AssetRipper.Core.Classes.Joint

{
	public sealed class SpringJoint : Component
	{
		public SpringJoint(AssetInfo assetInfo) :
			base(assetInfo)
		{
		}

		private static int GetSerializedVersion(UnityVersion version)
		{
			// TODO:
			return 2;
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
			Anchor = reader.ReadAsset<Vector3f>();
			AutoConfigureConnectedAnchor = reader.ReadBoolean();
			reader.AlignStream();
			ConnectedAnchor = reader.ReadAsset<Vector3f>();

			Spring = reader.ReadSingle();
			Damper = reader.ReadSingle();
			MinDistance = reader.ReadSingle();
			MaxDistance = reader.ReadSingle();
			Tolerance = reader.ReadSingle();

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
			node.Add(AnchorName, Anchor.ExportYaml(container));
			node.Add(AutoConfigureConnectedAnchorName, AutoConfigureConnectedAnchor);
			node.Add(ConnectedAnchorName, ConnectedAnchor.ExportYaml(container));

			node.AddSerializedVersion(GetSerializedVersion(container.ExportVersion));
			node.Add(SpringName, Spring);
			node.Add(DamperName, Damper);
			node.Add(MinDistanceName, MinDistance);
			node.Add(MaxDistanceName, MaxDistance);
			node.Add(ToleranceName, Tolerance);

			node.Add(BreakForceName, BreakForce);
			node.Add(BreakTorqueName, BreakTorque);
			node.Add(EnableCollisionName, EnableCollision);
			node.Add(EnablePreprocessingName, EnablePreprocessing);
			node.Add(MassScaleName, MassScale);
			node.Add(ConnectedMassScaleName, ConnectedMassScale);
			return node;
		}
		public const string ConnectedBodyName = "m_ConnectedBody";
		public const string AnchorName = "m_Anchor";
		public const string AutoConfigureConnectedAnchorName = "m_AutoConfigureConnectedAnchor";
		public const string ConnectedAnchorName = "m_ConnectedAnchor";
		public const string BreakForceName = "m_BreakForce";
		public const string BreakTorqueName = "m_BreakTorque";
		public const string EnableCollisionName = "m_EnableCollision";
		public const string EnablePreprocessingName = "m_EnablePreprocessing";
		public const string MassScaleName = "m_MassScale";
		public const string ConnectedMassScaleName = "m_ConnectedMassScale";

		public const string SpringName = "m_Spring";
		public const string DamperName = "m_Damper";
		public const string MinDistanceName = "m_MinDistance";
		public const string MaxDistanceName = "m_MaxDistance";
		public const string ToleranceName = "m_Tolerance";


		public PPtr<Rigidbody.Rigidbody> ConnectedBody { get; private set; }
		public Vector3f Anchor { get; private set; }
		public bool AutoConfigureConnectedAnchor { get; private set; }
		public Vector3f ConnectedAnchor { get; private set; }
		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }

		public float Spring { get; private set; }
		public float Damper { get; private set; }
		public float MinDistance { get; private set; }
		public float MaxDistance { get; private set; }
		public float Tolerance { get; private set; }
	}
}
