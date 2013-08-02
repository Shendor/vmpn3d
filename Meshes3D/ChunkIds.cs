using System;

namespace Meshes3D
{
	class ChunkIds
	{
        public const UInt16 MMAGIC          = 0x3D3D;	/* Mesh magic */
        public const UInt16 M3DMAGIC        = 0x4D4D;	
        public const UInt16 M3D_VERSION     = 0x0002;
        public const UInt16 SMAGIC          = 0x2D2D;	/* Shaper magic */
        public const UInt16 LMAGIC          = 0x2D3D;	/* Lofter magic */
        public const UInt16 MLIBMAGIC       = 0x3DAA;	/* Material library magic */
        public const UInt16 MATMAGIC        = 0x3DFF;	/* Material magic */

        public const UInt16 MESH_VERSION    = 0x3D3E;  /* version chunk */
        public const UInt16 MASTER_SCALE    = 0x0100;

        public const UInt16 NAMED_OBJECT    = 0x4000;

        public const UInt16 N_TRI_OBJ       = 0x4100;

        public const UInt16 POINT_ARRAY     = 0x4110;
        public const UInt16 FACE_ARRAY      = 0x4120;
        public const UInt16 TRI_MAPPINGCOORS= 0x4140; //TEX_VERTS

        public const UInt16 N_DIRECT_LIGHT  = 0x4600;
        public const UInt16 DL_SPOTLIGHT    = 0x4610;

        public const UInt16 N_CAMERA        = 0x4700;
            
        public const UInt16 COLOR_F         = 0x0010; /* RGB */
        public const UInt16 COLOR_24        = 0x0011; /* True color */
	}
}
