using System.Windows.Controls;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using _3DTools;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using PNCreator.PNObjectsIerarchy;
using System.Windows;

namespace PNCreator.Controls
{
    public class PNViewport : Grid
    {
        private const double CAMERA_DEFAULT_SCALE = 0.064;

        private Viewport3D viewport;
        private Canvas content2D;
        private TrackballDecorator trackball;
        private ModelVisual3D light;
        private Model3DGroup lights;
        private readonly EventPublisher eventPublisher;

        public PNViewport()
        {
            eventPublisher = App.GetObject<EventPublisher>();
            eventPublisher.Register((MeshTransformChangedEventArgs args) =>
               TranslateContent2D());

            InitializeComponent();

            SetDirectionalLight();
            AllowTranslateContent2D = true;

            MouseMove += PNViewport_MouseMove;
            MouseWheel += PNViewport_MouseWheel;
            SizeChanged += PNViewport_SizeChanged;

        }

        void PNViewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AllowTranslateContent2D)
            {
                TranslateContent2D();
            }
        }

        void PNViewport_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            PNViewport_MouseMove(sender, e);
        }
        
           
        void PNViewport_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point point = e.GetPosition(trackball.Viewport3D);
            MousePosition3D = MathUtils.Convert2DPoint(point, this.Visual, this.Trackball.Move);
            
            if (AllowTranslateContent2D)
            {
                TranslateContent2D();
            }
        }

        public void SetCameraView(CameraView view)
        {
            switch (view)
            {
                case CameraView.Perspective:
                    {
                        Trackball.SetCameraView(CameraView.Perspective, new Vector3D(0, 0, -600), 25);
                        Trackball.ZoomRatio = 1;
                        ScaleCamera(CAMERA_DEFAULT_SCALE);
                    }
                    break;
                case CameraView.Front:
                    {
                        Trackball.SetCameraView(CameraView.Front, new Vector3D(0, 0, -600));
                        Trackball.ZoomRatio = 2;
                    }
                    break;
            }
            TranslateContent2D();
        }

        /// <summary>
        /// Translate Visual objects in content 2D
        /// </summary>
        public void TranslateContent2D()
        {
            foreach (Visual3D visual in viewport.Children)
            {
                if (visual is ITranslationContent2D)
                {
                    ((ITranslationContent2D)visual).TranslateContent2D(viewport);
                }
            }
        }

        private void InitializeComponent()
        {
            trackball = new TrackballDecorator();
            viewport = new Viewport3D();
            content2D = new Canvas();
            content2D.IsHitTestVisible = false;
            content2D.ClipToBounds = true;
            Trackball.Focusable = true;

            light = new ModelVisual3D();
            lights = new Model3DGroup();
            SetAmbientLight(Color.FromArgb(0x33, 0xCC, 0xCC, 0xCC));
            SetDirectionalLight();
            light.Content = lights;
            lights.Transform = trackball.Transform;

            viewport.Children.Add(light);

            trackball.Content = viewport;
            Children.Add(trackball);
            Children.Add(content2D);

            trackball.Camera = new PerspectiveCamera()
            {
                FarPlaneDistance = 18000
            };
        }

        /// <summary>
        /// Add PN object
        /// </summary>
        /// <param name="pnObject">PN object</param>
        public void AddPNObject(PNObject pnObject)
        {
            Viewport3D.Children.Add(pnObject);
            if (pnObject.NameInCanvas != null)
            {
                Content2D.Children.Add(pnObject.NameInCanvas);
            }
            if (pnObject.ValueInCanvas != null)
            {
                Content2D.Children.Add(pnObject.ValueInCanvas);
            }
           
            eventPublisher.ExecuteEvents(new PNObjectChangedEventArgs(pnObject));
        }

        public void AddMesh(ModelVisual3D model)
        {
            Viewport3D.Children.Add(model);
        }

        /// <summary>
        /// Remove PN object
        /// </summary>
        /// <param name="pnObject">PN object</param>
        public void RemovePNObject(PNObject pnObject)
        {
            Viewport3D.Children.Remove(pnObject);
            if (pnObject.NameInCanvas != null)
            {
                Content2D.Children.Remove(pnObject.NameInCanvas);
            }
            if (pnObject.ValueInCanvas != null)
            {
                Content2D.Children.Remove(pnObject.ValueInCanvas);
            }
        }

        public void ClearAll()
        {
            viewport.Children.Clear();
            viewport.Children.Add(light);
            content2D.Children.Clear();
        }

        public void SetAmbientLight(Color lightColor)
        {
            lights.Children.Clear();
            lights.Children.Add(new AmbientLight(lightColor));
        }

        public void SetDirectionalLight()
        {
            SetAmbientLight(Color.FromArgb(0x33, 0x33, 0x33, 0x33));
            lights.Children.Add(new DirectionalLight(Colors.White, new Vector3D(-0.01, -0.01, -0.6)));
        }

        /// <summary>
        /// Set camera to indicated position
        /// </summary>
        /// <param name="position">Camera position point</param>
        public void SetCameraPosition(Point3D position)
        {
            trackball.ZoomToSelectedObject(position);
            TranslateContent2D();
        }

        public Visual3D Visual
        {
            get
            {
                return light;
            }
        }


        public Viewport3D Viewport3D
        {
            get
            {
                return trackball.Viewport3D;
            }
        }

        public Canvas Content2D
        {
            get
            {
                return content2D;
            }
        }

        public TrackballDecorator Trackball
        {
            get
            {
                return trackball;
            }
        }

        public bool AllowTranslateContent2D
        {
            get;
            set;
        }


        public Point3D MousePosition3D
        {
            get;
            set;
        }

        public void ScaleCamera(double scaleRatio)
        {
            Trackball.Scale.ScaleX =
            Trackball.Scale.ScaleY =
            Trackball.Scale.ScaleZ = scaleRatio;
        }
    }
}
