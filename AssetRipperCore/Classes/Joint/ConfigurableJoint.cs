﻿using System.Collections.Generic;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Math.Vectors;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Classes.Misc;

using AssetRipper.Core.IO.Extensions;

using AssetRipper.Core.Project;
using AssetRipper.Yaml;
using AssetRipper.Core.Parser.Files.SerializedFiles;

namespace AssetRipper.Core.Classes.Joint
{
	public sealed class ConfigurableJoint : Component
	{
		public ConfigurableJoint(AssetInfo assetInfo) :
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

			SecondaryAxis = reader.ReadAsset<Vector3f>();
			XMotion = (ConfigurableJointMotion)reader.ReadInt32();
			YMotion = (ConfigurableJointMotion)reader.ReadInt32();
			ZMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularXMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularYMotion = (ConfigurableJointMotion)reader.ReadInt32();
			AngularZMotion = (ConfigurableJointMotion)reader.ReadInt32();
			LinearLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			LinearLimit = reader.ReadAsset<SoftJointLimit>();
			AngularXLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			LowAngularXLimit = reader.ReadAsset<SoftJointLimit>();
			HighAngularXLimit = reader.ReadAsset<SoftJointLimit>();
			AngularYZLimitSpring = reader.ReadAsset<SoftJointLimitSpring>();
			AngularYLimit = reader.ReadAsset<SoftJointLimit>();
			AngularZLimit = reader.ReadAsset<SoftJointLimit>();
			TargetPosition = reader.ReadAsset<Vector3f>();
			TargetVelocity = reader.ReadAsset<Vector3f>();
			XDrive = reader.ReadAsset<JointDrive>();
			YDrive = reader.ReadAsset<JointDrive>();
			ZDrive = reader.ReadAsset<JointDrive>();
			TargetRotation = reader.ReadAsset<Quaternionf>();
			TargetAngularVelocity = reader.ReadAsset<Vector3f>();
			RotationDriveMode = (RotationDriveMode)reader.ReadInt32();
			AngularXDrive = reader.ReadAsset<JointDrive>();
			AngularYZDrive = reader.ReadAsset<JointDrive>();
			SlerpDrive = reader.ReadAsset<JointDrive>();
			ProjectionMode = (JointProjectionMode)reader.ReadInt32();
			ProjectionDistance = reader.ReadSingle();
			ProjectionAngle = reader.ReadSingle();
			ConfiguredInWorldSpace = reader.ReadBoolean();
			SwapBodies = reader.ReadBoolean();
			reader.AlignStream();
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
			node.Add(SecondaryAxisName, SecondaryAxis.ExportYaml(container));
			node.Add(XMotionName, (int)XMotion);
			node.Add(YMotionName, (int)YMotion);
			node.Add(ZMotionName, (int)ZMotion);
			node.Add(AngularXMotionName, (int)AngularXMotion);
			node.Add(AngularYMotionName, (int)AngularYMotion);
			node.Add(AngularZMotionName, (int)AngularZMotion);
			node.Add(LinearLimitSpringName, LinearLimitSpring.ExportYaml(container));
			node.Add(LinearLimitName, LinearLimit.ExportYaml(container));
			node.Add(AngularXLimitSpringName, AngularXLimitSpring.ExportYaml(container));
			node.Add(LowAngularXLimitName, LowAngularXLimit.ExportYaml(container));
			node.Add(HighAngularXLimitName, HighAngularXLimit.ExportYaml(container));
			node.Add(AngularYZLimitSpringName, AngularYZLimitSpring.ExportYaml(container));
			node.Add(AngularYLimitName, AngularYLimit.ExportYaml(container));
			node.Add(AngularZLimitName, AngularZLimit.ExportYaml(container));
			node.Add(TargetPositionName, TargetPosition.ExportYaml(container));
			node.Add(TargetVelocityName, TargetVelocity.ExportYaml(container));
			node.Add(XDriveName, XDrive.ExportYaml(container));
			node.Add(YDriveName, YDrive.ExportYaml(container));
			node.Add(ZDriveName, ZDrive.ExportYaml(container));
			node.Add(TargetRotationName, TargetRotation.ExportYaml(container));
			node.Add(TargetAngularVelocityName, TargetAngularVelocity.ExportYaml(container));
			node.Add(RotationDriveModeName, (int)RotationDriveMode);
			node.Add(AngularXDriveName, AngularXDrive.ExportYaml(container));
			node.Add(AngularYZDriveName, AngularYZDrive.ExportYaml(container));
			node.Add(SlerpDriveName, SlerpDrive.ExportYaml(container));
			node.Add(ProjectionModeName, (int)ProjectionMode);
			node.Add(ProjectionDistanceName, ProjectionDistance);
			node.Add(ProjectionAngleName, ProjectionAngle);
			node.Add(ConfiguredInWorldSpaceName, ConfiguredInWorldSpace);
			node.Add(SwapBodiesName, SwapBodies);

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

		public const string SecondaryAxisName = "m_SecondaryAxis";
		public const string XMotionName = "m_XMotion";
		public const string YMotionName = "m_YMotion";
		public const string ZMotionName = "m_ZMotion";
		public const string AngularXMotionName = "m_AngularXMotion";
		public const string AngularYMotionName = "m_AngularYMotion";
		public const string AngularZMotionName = "m_AngularZMotion";
		public const string LinearLimitSpringName = "m_LinearLimitSpring";
		public const string LinearLimitName = "m_LinearLimit";
		public const string AngularXLimitSpringName = "m_AngularXLimitSpring";
		public const string LowAngularXLimitName = "m_LowAngularXLimit";
		public const string HighAngularXLimitName = "m_HighAngularXLimit";
		public const string AngularYZLimitSpringName = "m_AngularYZLimitSpring";
		public const string AngularYLimitName = "m_AngularYLimit";
		public const string AngularZLimitName = "m_AngularZLimit";
		public const string TargetPositionName = "m_TargetPosition";
		public const string TargetVelocityName = "m_TargetVelocity";
		public const string XDriveName = "m_XDrive";
		public const string YDriveName = "m_YDrive";
		public const string ZDriveName = "m_ZDrive";
		public const string TargetRotationName = "m_TargetRotation";
		public const string TargetAngularVelocityName = "m_TargetAngularVelocity";
		public const string RotationDriveModeName = "m_RotationDriveMode";
		public const string AngularXDriveName = "m_AngularXDrive";
		public const string AngularYZDriveName = "m_AngularYZDrive";
		public const string SlerpDriveName = "m_SlerpDrive";
		public const string ProjectionModeName = "m_ProjectionMode";
		public const string ProjectionDistanceName = "m_ProjectionDistance";
		public const string ProjectionAngleName = "m_ProjectionAngle";
		public const string ConfiguredInWorldSpaceName = "m_ConfiguredInWorldSpace";
		public const string SwapBodiesName = "m_SwapBodies";

		public PPtr<Rigidbody.Rigidbody> ConnectedBody;
		public Vector3f Anchor;
		public Vector3f Axis;
		public bool AutoConfigureConnectedAnchor;
		public Vector3f ConnectedAnchor;
		public float BreakForce;
		public float BreakTorque;
		public bool EnableCollision;
		public bool EnablePreprocessing;
		public float MassScale;
		public float ConnectedMassScale;

		public Vector3f SecondaryAxis { get; private set; }
		public ConfigurableJointMotion XMotion { get; private set; }
		public ConfigurableJointMotion YMotion { get; private set; }
		public ConfigurableJointMotion ZMotion { get; private set; }
		public ConfigurableJointMotion AngularXMotion { get; private set; }
		public ConfigurableJointMotion AngularYMotion { get; private set; }
		public ConfigurableJointMotion AngularZMotion { get; private set; }
		public SoftJointLimitSpring LinearLimitSpring { get; private set; }
		public SoftJointLimit LinearLimit { get; private set; }
		public SoftJointLimitSpring AngularXLimitSpring { get; private set; }
		public SoftJointLimit LowAngularXLimit { get; private set; }
		public SoftJointLimit HighAngularXLimit { get; private set; }
		public SoftJointLimitSpring AngularYZLimitSpring { get; private set; }
		public SoftJointLimit AngularYLimit { get; private set; }
		public SoftJointLimit AngularZLimit { get; private set; }
		public Vector3f TargetPosition { get; private set; }
		public Vector3f TargetVelocity { get; private set; }
		public JointDrive XDrive { get; private set; }
		public JointDrive YDrive { get; private set; }
		public JointDrive ZDrive { get; private set; }
		public Quaternionf TargetRotation { get; private set; }
		public Vector3f TargetAngularVelocity { get; private set; }
		public RotationDriveMode RotationDriveMode { get; private set; }
		public JointDrive AngularXDrive { get; private set; }
		public JointDrive AngularYZDrive { get; private set; }
		public JointDrive SlerpDrive { get; private set; }
		public JointProjectionMode ProjectionMode { get; private set; }
		public float ProjectionDistance { get; private set; }
		public float ProjectionAngle { get; private set; }
		public bool ConfiguredInWorldSpace { get; private set; }
		public bool SwapBodies { get; private set; }
	}
}
