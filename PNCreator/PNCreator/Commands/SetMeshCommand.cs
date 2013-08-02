using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Commands
{
    public class SetMeshCommand
    {
        public void SetMeshForPNObject(PNObject pnObject)
        {
            if (pnObject is Arc3D)
            {
                string fileName = DialogBoxes.OpenDialog(DocumentFormat.Texture);
                if (fileName != null)
                    ((Arc3D)pnObject).SetTexture(fileName.Replace(PNDocument.ApplicationPath, ""));
            }
            else
            {
                string fileName = DialogBoxes.OpenDialog(DocumentFormat.Mesh);
                if (fileName != null)
                    ((Shape3D)pnObject).SetMesh(fileName.Replace(PNDocument.ApplicationPath, ""));
            }
        }

        public string GetRelativeMeshPath(DocumentFormat meshType)
        {
            string fileName = DialogBoxes.OpenDialog(meshType);
            if (fileName != null)
            {
                return fileName.Replace(PNDocument.ApplicationPath, "");
            }
            return null;
        }
    }
}
