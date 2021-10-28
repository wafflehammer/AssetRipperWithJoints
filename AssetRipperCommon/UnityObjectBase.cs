﻿using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Classes.Object;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.Layout;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using System.IO;

namespace AssetRipper.Core
{
	/// <summary>
	/// The artificial base class for all generated Unity classes with Type ID numbers<br/>
	/// In other words, the classes that inherit from Object
	/// </summary>
	public class UnityObjectBase : UnityAssetBase, IUnityObjectBase
	{
		public AssetInfo AssetInfo { get; set; }
		public ISerializedFile File => AssetInfo.File;
		public virtual ClassIDType ClassID => AssetInfo.ClassID;
		public long PathID => AssetInfo.PathID;
		public UnityGUID GUID => AssetInfo.GUID;
		public virtual string ExportPath => Path.Combine(AssetsKeyword, ClassID.ToString());
		public virtual string ExportExtension => AssetExtension;
		public HideFlags ObjectHideFlags { get; set; }

		public const string AssetsKeyword = "Assets";
		protected const string AssetExtension = "asset";

		public UnityObjectBase() { }
		public UnityObjectBase(AssetLayout layout) { }
		public UnityObjectBase(AssetInfo assetInfo)
		{
			AssetInfo = assetInfo;
		}

		public YAMLDocument ExportYAMLDocument(IExportContainer container)
		{
			YAMLDocument document = new YAMLDocument();
			YAMLMappingNode root = document.CreateMappingRoot();
			root.Tag = ClassID.ToInt().ToString();
			root.Anchor = container.GetExportID(this).ToString();
			YAMLMappingNode node = ExportYAMLRoot(container);
			root.Add(container.ExportLayout.GetClassName(ClassID), node);
			return document;
		}

		public override YAMLNode ExportYAML(IExportContainer container)
		{
			return ExportYAMLRoot(container);
		}

		protected virtual YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			return node;
		}

		public virtual UnityObjectBase Convert(IExportContainer container)
		{
			return this;
		}
	}
}
