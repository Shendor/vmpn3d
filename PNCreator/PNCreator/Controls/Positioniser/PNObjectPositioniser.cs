using _3DTools;
using System.Windows.Media.Media3D;

namespace PNCreator.Controls.Positioniser
{
    public class PNObjectPositioniser : ScreenSpaceLines3D
    {
        private Grid3DParameters grid3dParameters;

        public PNObjectPositioniser()
        {
            Build();
        }

        public void Build()
        {
            grid3dParameters = new Grid3DParameters();
            Build(grid3dParameters);
        }

        public void Build(Grid3DParameters grid3dParameters)
        {
            if (grid3dParameters.RowsQuantity > 0)
            {
                this.grid3dParameters = grid3dParameters;
                Points.Clear();
                for (int row = 0; row < grid3dParameters.RowsQuantity; row++)
                {
                    if (grid3dParameters.YCellSize > 0)
                    {
                        for (double y = 0; y <= grid3dParameters.Height; y += grid3dParameters.YCellSize)
                        {
                            // x line
                            Points.Add(new Point3D(0 + grid3dParameters.XPosition,
                                                    y + grid3dParameters.YPosition,
                                                    row * grid3dParameters.ZCellSize + grid3dParameters.ZPosition));

                            Points.Add(new Point3D(grid3dParameters.Width + grid3dParameters.XPosition,
                                                    y + grid3dParameters.YPosition,
                                                    row * grid3dParameters.ZCellSize + grid3dParameters.ZPosition));
                        }
                    }
                    if (grid3dParameters.XCellSize > 0)
                    {
                        for (double x = 0; x <= grid3dParameters.Length; x += grid3dParameters.XCellSize)
                        {
                            // y line
                            Points.Add(new Point3D(x + grid3dParameters.XPosition,
                                                    0 + grid3dParameters.YPosition,
                                                    row * grid3dParameters.ZCellSize + grid3dParameters.ZPosition));

                            Points.Add(new Point3D(x + grid3dParameters.XPosition,
                                                    grid3dParameters.Length + grid3dParameters.YPosition,
                                                    row * grid3dParameters.ZCellSize + grid3dParameters.ZPosition));
                        }
                    }
                }
            }
            Color = grid3dParameters.Color;
            Thickness = grid3dParameters.Thickness;
        }

        public void Destroy()
        {
            Points.Clear();
        }

        public void SetVisible(bool isVisible)
        {
            if (!isVisible)
            {
                Points.Clear();
            }
            else
            {
                Build(grid3dParameters);
            }
        }
    }
}
