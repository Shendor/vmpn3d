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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MeshUtilities
{
	public class Reader3ds
    {
        #region Properties
        private List<MeshGeometry3D> _meshes;
		private List<Camera>         _cameras;
		private List<Light>          _lights;
		
		private Model3DGroup   _currentModelGroup;
		private MeshGeometry3D _currentMesh;
		private Light          _currentLight;

		private string _currentFileName;
		private bool _addDefaultLight;
        private Material _defaultMaterial;


		public List<MeshGeometry3D> Meshes
		{
			get { return _meshes; }
		}

		public List<Camera> Cameras
		{
			get { return _cameras; }
		}

		public List<Light> Lights
		{
			get { return _lights; }
		}

        /// <summary>
        /// Set the material that will be applied to the read objects
        /// </summary>
        public Material DefaultMaterial
        {
            get { return _defaultMaterial; }
            set { _defaultMaterial = value; }
        }

		/// <summary>
		/// Add defult light (Direction=0, 0, -1) if there are no lights defined in 3ds file
		/// </summary>
		public bool AddDefaultLight
		{
			get { return _addDefaultLight; }
			set { _addDefaultLight = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
		public Reader3ds()
		{
            _meshes = new List<MeshGeometry3D>();
            _cameras = new List<Camera>();
            _lights = new List<Light>();

			SolidColorBrush defaultBrush = new SolidColorBrush(Colors.Silver);
            defaultBrush.Opacity = 1;
            _defaultMaterial = new DiffuseMaterial(defaultBrush);

			_addDefaultLight = false;
        }
        #endregion

        #region ReadFile
        /// <summary>
		/// Reads 3ds file and returns its data in MeshGeometry3D object
		/// </summary>
		/// <param name="fileName">3ds file Name</param>
		/// <returns>read MeshGeometry3D object</returns>
		public Model3DGroup ReadFile(string fileName)
		{
			using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				_currentFileName = fs.Name;
				BinaryReader r = new BinaryReader(fs);

				while (r.PeekChar() != -1) // Until the end
					ReadChunk(r);

				r.Close();
			}

            _currentModelGroup = new Model3DGroup();
			_currentModelGroup.Children.Clear();

			if (_addDefaultLight)
			{
				if (_lights.Count == 0)
				{
					// Add default light
					_lights.Add(new DirectionalLight(Colors.White, new Vector3D(0, 0, -1)));
				}

				_currentModelGroup.Children.Add((Light)_lights[0]);
			}

			for (int i = 0; i < _meshes.Count; i++)
			{
				MeshGeometry3D oneMesh = _meshes[i];

				GeometryModel3D oneGeometryModel = new GeometryModel3D(oneMesh, _defaultMaterial);

				_currentModelGroup.Children.Add(oneGeometryModel);
			}

            return _currentModelGroup;
        }
        #endregion

        #region ReadChunk - main
        /// <summary>
        /// Main chunk reading method
        /// </summary>
        /// <param name="r">BinaryReader</param>
        /// <returns>readed chunk lenghth</returns>
		private long ReadChunk(BinaryReader r)
		{
			UInt16 chunkHeader;
			Int32 chunkLength;
			byte[] skipedBytes;
			long bytesLeft;

			chunkHeader = r.ReadUInt16();
			chunkLength = r.ReadInt32();

			switch (chunkHeader)
			{
				case ChunkIds.M3D_VERSION:
					int version = r.ReadInt32(); // just read
					break;

				case ChunkIds.MESH_VERSION:
					int meshVersion = r.ReadInt32(); // just read
					break;

                case ChunkIds.MASTER_SCALE:
					int scale = r.ReadInt32(); // just read
					break;

                case ChunkIds.NAMED_OBJECT:
					_currentMesh = new MeshGeometry3D();

					_meshes.Add(_currentMesh);

					string meshName = ReadString(r); // just read
					break;

                case ChunkIds.POINT_ARRAY:
					Int16 vertexCount;

					vertexCount = r.ReadInt16();

					for (int i = 0; i < vertexCount; i++)
					{
						Point3D currentVertex = new Point3D();

						currentVertex.X = r.ReadSingle();
						currentVertex.Y = r.ReadSingle();
						currentVertex.Z = r.ReadSingle();

						_currentMesh.Positions.Add(currentVertex);
					}

					break;

                case ChunkIds.FACE_ARRAY:
					Int32 facesCount;

					facesCount = r.ReadInt16();

					for (int i = 0; i < facesCount; i++)
					{
						Int16 vertex1, vertex2, vertex3; // indexed to Posiotions (points)

						vertex1 = r.ReadInt16();
						vertex2 = r.ReadInt16();
						vertex3 = r.ReadInt16();

						Int16 faceType = r.ReadInt16(); // Should be used to reorder the vertexes but in Avalon this only messed the objects

                        _currentMesh.TriangleIndices.Add(vertex1);
                        _currentMesh.TriangleIndices.Add(vertex2);
                        _currentMesh.TriangleIndices.Add(vertex3);
					}

					break;

                case ChunkIds.TRI_MAPPINGCOORS:
                    int coordinatsCount;

                    coordinatsCount = r.ReadInt16();

                    for (int i = 0; i < coordinatsCount; i++)
                    {
                        Single tu, tv;

                        tu = r.ReadSingle();
                        tv = r.ReadSingle();

                        _currentMesh.TextureCoordinates.Add(new Point(tu, tv));
                    }
                    break;

                case ChunkIds.N_CAMERA:
					PerspectiveCamera newCamera;
                    Vector3D camearLookAt;
                    Point3D cameraPosition;
					Single cameraAngle;
					Single cameraFieldOfView;

					// Read properties
                    cameraPosition = new Point3D();
					cameraPosition.X = r.ReadSingle();
					cameraPosition.Y = r.ReadSingle();
					cameraPosition.Z = r.ReadSingle();

					camearLookAt = new Vector3D();
					camearLookAt.X = r.ReadSingle();
					camearLookAt.Y = r.ReadSingle();
					camearLookAt.Z = r.ReadSingle();

					cameraAngle = r.ReadSingle(); // Not used

					cameraFieldOfView = r.ReadSingle();

					// Set camera properties
					newCamera = new PerspectiveCamera();

					newCamera.Position = cameraPosition;
					newCamera.LookDirection = camearLookAt;
					newCamera.FieldOfView = (double)cameraFieldOfView;

					// Set default properties (not read from 3ds)
					newCamera.UpDirection = new Vector3D(0, 0, 1);
					newCamera.NearPlaneDistance = 1.0;
					newCamera.FarPlaneDistance = 500.0;

					_cameras.Add(newCamera);
					break;

                case ChunkIds.N_DIRECT_LIGHT:
					SpotLight newSpotLight;
					Point3D lightPosition;

					lightPosition = new Point3D();
					lightPosition.X = r.ReadSingle();
					lightPosition.Y = r.ReadSingle();
					lightPosition.Z = r.ReadSingle();

					newSpotLight = new SpotLight(); // Starts with spot light because of Position (later can be changes to some other light type - DirectionalLight
					newSpotLight.Position = lightPosition;

					_currentLight = newSpotLight;

					bytesLeft = chunkLength - 6 - 3 * sizeof(Single);

					while (bytesLeft > 0) // Real all sub-chunks
						bytesLeft -= ReadLightChunk(r);

					_lights.Add(_currentLight);
					break;

                case ChunkIds.M3DMAGIC:
                case ChunkIds.MMAGIC:
                case ChunkIds.N_TRI_OBJ:
					//Skip it
					break;

				default:
					skipedBytes = r.ReadBytes(chunkLength - 6); // skip unhandled chunk
					break;
			}

			return chunkLength;
        }
        #endregion

        #region ReadLightChunk
        /// <summary>
        /// Reads light chunk
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
		private long ReadLightChunk(BinaryReader r)
		{
			UInt16 chunkHeader;
			Int32 chunkLength;
			byte[] skipedBytes;

			chunkHeader = r.ReadUInt16();
			chunkLength = r.ReadInt32();

			switch (chunkHeader)
			{
                case ChunkIds.DL_SPOTLIGHT:
					Point3D lightTarget;
					Vector3D lightDirection;
					Single lightHotspot;
					Single lightFalloff;

					lightTarget = new Point3D();
					lightTarget.X = r.ReadSingle();
					lightTarget.Y = r.ReadSingle();
					lightTarget.Z = r.ReadSingle();

					lightHotspot = r.ReadSingle();
					lightFalloff = r.ReadSingle();

					lightDirection = lightTarget - ((SpotLight)_currentLight).Position;
					lightDirection.Normalize();

					// In Avalon CTP March 2005 only DirectionalLight is supported
					DirectionalLight tempLight = new DirectionalLight();
					tempLight.Direction = lightDirection;
					tempLight.Color = _currentLight.Color;

					_currentLight = tempLight;
					break;

                case ChunkIds.COLOR_F:
					_currentLight.Color = ReadRGBColor(r);
					break;

                case ChunkIds.COLOR_24:
					_currentLight.Color = ReadTrueColor(r);
					break;

				default:
					skipedBytes = r.ReadBytes(chunkLength - 6); // skip unhandled chunk
					break;
			}

			return chunkLength;
		}
		#endregion

		#region ReadRGBColor, ReadTrueColor
        /// <summary>
        /// Reads color chunk - as RGB
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
		private Color ReadRGBColor(BinaryReader r)
		{
			Color newColor;

			newColor = new Color();

			newColor.A = 255; // no transparency
			newColor.R = Convert.ToByte(r.ReadSingle() * 255);
			newColor.G = Convert.ToByte(r.ReadSingle() * 255);
			newColor.B = Convert.ToByte(r.ReadSingle() * 255);

			return newColor;
		}

        /// <summary>
        /// Reads color chunk - as TrueColor
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
		private Color ReadTrueColor(BinaryReader r)
		{
			Color newColor;

			newColor = new Color();

			newColor.A = 255; // no transparency
			newColor.R = r.ReadByte();
			newColor.G = r.ReadByte();
			newColor.B = r.ReadByte();

			return newColor;
		}
		#endregion

        #region ReadString
        /// <summary>
		/// Reads characters until '\0' and packs them into string - unlimited size
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		private string ReadString(BinaryReader r)
		{
			return ReadString(r, int.MaxValue);
		}

		/// <summary>
		/// Reads characters until '\0' and packs them into string - max size
		/// </summary>
		/// <param name="r"></param>
		/// <param name="maxLength">max string length; maxInt for unlimited</param>
		/// <returns></returns>
		private string ReadString(BinaryReader r, int maxLength)
		{
			string retString;
			char oneChar;

			retString = "";

			oneChar = r.ReadChar();
			while (oneChar != '\0' && maxLength-- > 0)
			{
				retString += oneChar;
				oneChar = r.ReadChar();
			}

			return retString;
        }
        #endregion
    }
}