/// 
/// Copyright (c) 2005 by Andrej Benedik
///
/// This software is provided 'as-is', without any express or implied warranty.
/// In no event will the authors be held liable for any damages arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, including commercial applications,
/// and to alter it and redistribute it freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software. If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
/// 
/// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
///
/// 3. This notice may not be removed or altered from any source distribution.
///

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Meshes3D
{
	public class XamlExporter
	{
		#region XamlTemplate constants
		public const string XAML_TEMPLATE_MESH_RESOURCES = "<!-- IMPORT_MESH_RESOURCES -->";
		public const string XAML_TEMPLATE_CAMERA         = "<!-- IMPORT_CAMERA -->";
		public const string XAML_TEMPLATE_CHILDREN       = "<!-- IMPORT_CHILDREN -->";

        public const string NEW_LINE = "\r\n";
        #endregion

		#region Properties
		private string _doubleFormatString;      
		/// <summary>
		/// Gets or sets format string used to format double values in ExportAsXaml method
		/// </summary>
		public string DoubleFormatString
		{
			get
			{
				if (_doubleFormatString == null)
					_doubleFormatString = "#0.#####"; // By default format to 5 decimals
				
				return _doubleFormatString;
			}
			set { _doubleFormatString = value; }
		}
	
		private string _xamlTemplate;
        /// <summary>
        /// Gets or sets XamlTemplate that is used as template where models, cameras, ... are inserted.
        /// Template should countain XamlTemplate constants to define insertion positions
        /// </summary>
		public string XamlTemplate
		{
			get { return _xamlTemplate; }
			set { _xamlTemplate = value; }
		}

        private string _defaultMaterialXaml;
        /// <summary>
        /// String used to represent material for all meshes - if not set default value is used (Silver material)
        /// </summary>
        public string DefaultMaterialXaml
        {
            get
            {
                if (_defaultMaterialXaml != null && _defaultMaterialXaml != "")
                    return _defaultMaterialXaml;
                else
                    return "<GeometryModel3D.Material><DiffuseMaterial><DiffuseMaterial.Brush><SolidColorBrush Color=\"Silver\"/></DiffuseMaterial.Brush></DiffuseMaterial></GeometryModel3D.Material>";
            }

            set { _defaultMaterialXaml = value; }
        }
		#endregion

        #region LoadXamlTemplate
        /// <summary>
        /// Loads xaml template from file
        /// </summary>
        /// <param name="filePath">filePath</param>
		public void LoadXamlTemplate(string filePath)
		{
			StreamReader xamlTemplateFile = File.OpenText(filePath);

			XamlTemplate = xamlTemplateFile.ReadToEnd();
        }
        #endregion

        #region ExportModel3DGroup
        /// <summary>
		/// Get text prepared for use in XAML.
		/// <returns>text</returns>
		/// <param name="modelsToExport">modelsToExport</param> 
        /// <param name="viewportCamera">if set add camera xaml to output</param> 
		public string ExportModel3DGroup(Model3DGroup modelsToExport, Camera viewportCamera)
		{
			string outputXaml;

			StringBuilder allMeshResourcesXaml;
			StringBuilder allMeshChildrenXaml;
            string cameraXaml = null;
			StringBuilder oneModelChild;

			StringBuilder positions;
			StringBuilder triangleIndices;
			StringBuilder textureCoordinates;
			StringBuilder normals;

			Model3D oneModel3D;

			if (_xamlTemplate == null || _xamlTemplate == "") throw new Exception("ExportModel3DGroup exception: There is no XamlTemplate defined!");

			// Set to ensure "." as decimal separator
			System.Globalization.CultureInfo savedCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
			System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			try
			{
				allMeshResourcesXaml = new StringBuilder();
				allMeshChildrenXaml = new StringBuilder();

                if (viewportCamera != null) // Write xaml for camera
                {
                    if (viewportCamera is PerspectiveCamera)
                    {
                        PerspectiveCamera viewportPerspectiveCamera = viewportCamera as PerspectiveCamera;

                        cameraXaml = string.Format("<PerspectiveCamera Position=\"{1:" + DoubleFormatString + "},{2:" + DoubleFormatString + "},{3:" + DoubleFormatString + "}\"{0}" +
                                                   "LookDirection=\"{4}\"{0}" +
                                                   "Up=\"{5}\"{0}" +
                                                   "NearPlaneDistance=\"{6}\"{0}" +
                                                   "FarPlaneDistance=\"{7}\"{0}" +
                                                   "FieldOfView=\"{8}\" />",
                                                   NEW_LINE, viewportPerspectiveCamera.Position.X, viewportPerspectiveCamera.Position.Y, viewportPerspectiveCamera.Position.Z, viewportPerspectiveCamera.LookDirection.ToString(), viewportPerspectiveCamera.UpDirection.ToString(), viewportPerspectiveCamera.NearPlaneDistance.ToString(), viewportPerspectiveCamera.FarPlaneDistance.ToString(), viewportPerspectiveCamera.FieldOfView.ToString());
                    }
                }

				for (int i = 0; i < modelsToExport.Children.Count; i++)
				{
					oneModel3D = modelsToExport.Children[i];

					oneModelChild = new StringBuilder();

					if (oneModel3D is Light)
					{
                        string lightXaml;
                        Light oneLight;

                        oneLight = oneModel3D as Light;

                        // Light type - DirectionalLight or AmbientLight
                        if (oneLight is DirectionalLight)
                        {
                            DirectionalLight oneDirectionalLight = oneModel3D as DirectionalLight;
                            lightXaml = string.Format("<DirectionalLight Color=\"{0}\" Direction=\"{1}\" />{2}", oneDirectionalLight.Color.ToString(), oneDirectionalLight.Direction.ToString(), NEW_LINE);
                        }
                        else
                            lightXaml = string.Format("<AmbientLight Color=\"{0}\" />{1}", oneLight.Color.ToString(), NEW_LINE);

                        allMeshChildrenXaml.Append(lightXaml);
					}
					else if (oneModel3D is GeometryModel3D)
					{
						GeometryModel3D oneGeometryModel3D;
						MeshGeometry3D oneMeshObject;

						oneGeometryModel3D = oneModel3D as GeometryModel3D;
						oneMeshObject = oneGeometryModel3D.Geometry as MeshGeometry3D;


						positions = new StringBuilder();
						positions.Append("Positions=\"");

						foreach (Point3D onePoint in oneMeshObject.Positions)
							positions.Append(string.Format("{0:" + DoubleFormatString + "},{1:" + DoubleFormatString + "},{2:" + DoubleFormatString + "} ", onePoint.X, onePoint.Y, onePoint.Z));

						positions.Append("\"" + NEW_LINE);


						triangleIndices = new StringBuilder();
						triangleIndices.Append("TriangleIndices=\"");

						for (int j = 0; j < oneMeshObject.TriangleIndices.Count; j += 3)
							triangleIndices.Append(string.Format("{0} {1} {2}  ", oneMeshObject.TriangleIndices[j], oneMeshObject.TriangleIndices[j + 1], oneMeshObject.TriangleIndices[j + 2]));

						triangleIndices.Append("\"" + NEW_LINE);


						textureCoordinates = new StringBuilder();
						textureCoordinates.Append("TextureCoordinates=\"");

						if (oneMeshObject.TextureCoordinates != null)
						{
							foreach (Point onePoint in oneMeshObject.TextureCoordinates)
								textureCoordinates.Append(string.Format("{0:" + DoubleFormatString + "},{1:" + DoubleFormatString + "} ", onePoint.X, onePoint.Y));
						}

						textureCoordinates.Append("\"" + NEW_LINE);


						normals = new StringBuilder();
						normals.Append("Normals=\"");

						if (oneMeshObject.Normals != null)
						{
							foreach (Vector3D oneNormal in oneMeshObject.Normals)
								normals.Append(string.Format("{0:" + DoubleFormatString + "},{1:" + DoubleFormatString + "},{2:" + DoubleFormatString + "} ", oneNormal.X, oneNormal.Y, oneNormal.Z));
						}

						normals.Append("\"" + NEW_LINE);

						allMeshResourcesXaml.Append(string.Format("<MeshGeometry3D x:Key=\"mesh_{0}\"{1}", i,NEW_LINE));
						allMeshResourcesXaml.Append(positions);
						allMeshResourcesXaml.Append(triangleIndices);
						allMeshResourcesXaml.Append(textureCoordinates);
						allMeshResourcesXaml.Append(normals);
						allMeshResourcesXaml.Append("/>" + NEW_LINE);

						allMeshChildrenXaml.Append(string.Format("<GeometryModel3D Geometry=\"{{StaticResource mesh_{0}}}\">{1}", i,NEW_LINE));
                        allMeshChildrenXaml.Append(DefaultMaterialXaml + NEW_LINE);
						allMeshChildrenXaml.Append("</GeometryModel3D>" + NEW_LINE);
					}
				}

                // Inserts xaml text into template
				outputXaml = _xamlTemplate;

                if (cameraXaml != null)
                    outputXaml = outputXaml.Replace(XAML_TEMPLATE_CAMERA, cameraXaml);                 

				outputXaml = outputXaml.Replace(XAML_TEMPLATE_MESH_RESOURCES, allMeshResourcesXaml.ToString());
				outputXaml = outputXaml.Replace(XAML_TEMPLATE_CHILDREN, allMeshChildrenXaml.ToString());
			}
			finally
			{
                // Reset cultural settings
				System.Threading.Thread.CurrentThread.CurrentCulture = savedCulture;
			}

			return outputXaml;
		}
		#endregion
	}
}
