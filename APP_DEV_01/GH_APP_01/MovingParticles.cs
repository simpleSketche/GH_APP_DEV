using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;


namespace GH_APP_01
{
    public class MovingParticles : GH_Component
    {

        List<Point3d> currentPositions = new List<Point3d>();
        List<Vector3d> currentVectors = new List<Vector3d>();

        public MovingParticles()
          : base("Moving Particles",
                "Particles",
                "Particles moving.",
                "Practice",
                "Something")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "Points", "Points", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Reset", "Reset", "Reset", GH_ParamAccess.item);
            pManager.AddVectorParameter("Velocity", "Velocity", "Velocity", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Particles", "Particles", "Particles", GH_ParamAccess.list);
            pManager.AddVectorParameter("Velocities", "Velocities", "Velocities", GH_ParamAccess.list);

        }

        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool isReset = false;
            double randVel;
            List<Point3d> points = new List<Point3d>();
            Random rand = new Random();
            DA.GetData("Reset", ref isReset);
            DA.GetDataList("Points", points);

            if (isReset)
            {
                currentPositions = points;
                currentVectors.Clear();
                foreach(Point3d pt in points)
                {
                    currentVectors.Add(new Vector3d(0, 0, 0));
                }
            }
            else
            {
                Vector3d velocity = new Vector3d(0, 0, 0);
                DA.GetData("Velocity", ref velocity);
                for (int i = 0; i < points.Count; i++)
                {
                    randVel = rand.NextDouble();
                    randVel = Math.Max(0.1, randVel);
                    velocity *= randVel;
                    currentPositions[i] += velocity;
                    currentVectors[i] = velocity;
                }
            }
            DA.SetDataList("Particles", currentPositions);
            DA.SetDataList("Velocities", currentVectors);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("f67cddf8-ff35-433e-a5e9-39e53ce79b0b"); }
        }
    }
}