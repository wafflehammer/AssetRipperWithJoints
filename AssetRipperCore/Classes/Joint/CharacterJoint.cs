﻿using System.Collections.Generic;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Math.Vectors;
using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Project;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Yaml;

namespace AssetRipper.Core.Classes.Joint
{
	public sealed class CharacterJoint : Component
	{
		public CharacterJoint(AssetInfo assetInfo) :
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
			Axis = reader.ReadAsset<Vector3f>();
			AutoConfigureConnectedAnchor = reader.ReadBoolean();
			reader.AlignStream();
			ConnectedAnchor = reader.ReadAsset<Vector3f>();
			SwingAxis = reader.ReadAsset<Vector3f>();
			TwistLimitSpring = reader.ReadAsset< SoftJointLimitSpring>();
			LowTwistLimit = reader.ReadAsset<SoftJointLimit>();
			HighTwistLimit = reader.ReadAsset< SoftJointLimit>();
			SwingLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			Swing1Limit = reader.ReadAsset<SoftJointLimit>();
			Swing2Limit = reader.ReadAsset< SoftJointLimit>();
			EnableProjection = reader.ReadBoolean();
			reader.AlignStream();
			ProjectionDistance = reader.ReadSingle();
			ProjectionAngle = reader.ReadSingle();
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
			node.Add(AxisName, Axis.ExportYaml(container));
			node.Add(AutoConfigureConnectedAnchorName, AutoConfigureConnectedAnchor);
			node.Add(ConnectedAnchorName, ConnectedAnchor.ExportYaml(container));

			node.AddSerializedVersion(GetSerializedVersion(container.ExportVersion));
			node.Add(SwingAxisName, SwingAxis.ExportYaml(container));
			node.Add(TwistLimitSpringName, TwistLimitSpring.ExportYaml(container));
			node.Add(LowTwistLimitName, LowTwistLimit.ExportYaml(container));
			node.Add(HighTwistLimitName, HighTwistLimit.ExportYaml(container));
			node.Add(SwingLimitSpringName, SwingLimitSpring.ExportYaml(container));
			node.Add(Swing1LimitName, Swing1Limit.ExportYaml(container));
			node.Add(Swing2LimitName, Swing2Limit.ExportYaml(container));
			node.Add(ProjectionDistanceName, ProjectionDistance);
			node.Add(ProjectionAngleName, ProjectionAngle);

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
		public const string AxisName = "m_Axis";
		public const string AutoConfigureConnectedAnchorName = "m_AutoConfigureConnectedAnchor";
		public const string ConnectedAnchorName = "m_ConnectedAnchor";
		public const string BreakForceName = "m_BreakForce";
		public const string BreakTorqueName = "m_BreakTorque";
		public const string EnableCollisionName = "m_EnableCollision";
		public const string EnablePreprocessingName = "m_EnablePreprocessing";
		public const string MassScaleName = "m_MassScale";
		public const string ConnectedMassScaleName = "m_ConnectedMassScale";

		public const string SwingAxisName = "m_SwingAxis";
		public const string TwistLimitSpringName = "m_TwistLimitSpring";
		public const string LowTwistLimitName = "m_LowTwistLimit";
		public const string HighTwistLimitName = "m_HighTwistLimit";
		public const string SwingLimitSpringName = "m_SwingLimitSpring";
		public const string Swing1LimitName = "m_Swing1Limit";
		public const string Swing2LimitName = "m_Swing2Limit";
		public const string EnableProjectionName = "m_EnableProjection";
		public const string ProjectionDistanceName = "m_ProjectionDistance";
		public const string ProjectionAngleName = "m_ProjectionAngle";

		public PPtr<Rigidbody.Rigidbody> ConnectedBody { get; private set; }
		public Vector3f Anchor { get; private set; }
		public Vector3f Axis { get; private set; }
		public bool AutoConfigureConnectedAnchor { get; private set; }
		public Vector3f ConnectedAnchor { get; private set; }
		public float BreakForce { get; private set; }
		public float BreakTorque { get; private set; }
		public bool EnableCollision { get; private set; }
		public bool EnablePreprocessing { get; private set; }
		public float MassScale { get; private set; }
		public float ConnectedMassScale { get; private set; }

		public Vector3f SwingAxis { get; private set; }
		public SoftJointLimitSpring TwistLimitSpring { get; private set; }
		public SoftJointLimit LowTwistLimit { get; private set; }
		public SoftJointLimit HighTwistLimit { get; private set; }
		public SoftJointLimitSpring SwingLimitSpring { get; private set; }
		public SoftJointLimit Swing1Limit { get; private set; }
		public SoftJointLimit Swing2Limit { get; private set; }
		public bool EnableProjection { get; private set; }
		public float ProjectionDistance { get; private set; }
		public float ProjectionAngle { get; private set; }


	}
}
