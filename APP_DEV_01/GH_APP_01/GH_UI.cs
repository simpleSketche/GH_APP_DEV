using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasshopper.GUI;
using Grasshopper.Kernel;
using Grasshopper.GUI.Colours;
using Rhino.Geometry;
    
namespace GH_APP_01
{
    public class GH_UI : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_UI class.
        /// </summary>

        public GH_UI()
          : base("GH_UI",
                "GH_UI",
                "Change GH UI interface.",
                "Practice",
                "Something")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("COLOR RESET", "COLOR RESET", "COLOR RESET", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas backdrop Color", "Canvas_backdrop", "Canvas backdrop Color", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas grid Color", "Canvas_grid", "Canvas grid Color", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas edge Color", "Canvas_edge", "Canvas edge Color", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas shade Color", "Canvas_shade", "Canvas shade Color", GH_ParamAccess.item);
            pManager.AddBooleanParameter("SAVE COLOR", "SAVE COLOR", "SAVE COLOR", GH_ParamAccess.item);
            pManager.AddTextParameter("File Path", "File Path", "File Path", GH_ParamAccess.item);

            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("message", "message", "message", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool reset = true;
            bool saveCols = true;
            string filePath = null;
            string message = null;
            var jsonEXPO = "";
            Color backCols = new Color();
            Color gridCols = new Color();
            Color edgeCols = new Color();
            Color shadeCols = new Color();


            DA.GetData(0, ref reset);
            DA.GetData(1, ref backCols);
            DA.GetData(2, ref gridCols);
            DA.GetData(3, ref edgeCols);
            DA.GetData(4, ref shadeCols);
            DA.GetData(5, ref saveCols);
            DA.GetData(6, ref filePath);

            if (!reset)
            {
                Grasshopper.GUI.Canvas.GH_Skin.canvas_grid = Color.FromArgb(30, 0, 0, 0);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_back = Color.FromArgb(255, 212, 208, 200);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_edge = Color.FromArgb(255, 0, 0, 0);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_shade = Color.FromArgb(80, 0, 0, 0);

                GHUIcolor canvas_back = new GHUIcolor()
                {
                    UIName = "Canvas_backdrop",
                    UIColor = Color.FromArgb(30, 0, 0, 0)
                };
                GHUIcolor canvas_grid = new GHUIcolor()
                {
                    UIName = "Canvas_grid",
                    UIColor = Color.FromArgb(30, 0, 0, 0)
                };
                GHUIcolor canvas_edge = new GHUIcolor()
                {
                    UIName = "Canvas_edge",
                    UIColor = Color.FromArgb(30, 0, 0, 0)
                };
                GHUIcolor canvas_shade = new GHUIcolor()
                {
                    UIName = "Canvas_shade",
                    UIColor = Color.FromArgb(30, 0, 0, 0)
                };
                Dictionary<String, Color> ColorData = new Dictionary<String, Color>();
                ColorData.Clear();
                ColorData.Add(canvas_back.UIName, canvas_back.UIColor);
                ColorData.Add(canvas_grid.UIName, canvas_grid.UIColor);
                ColorData.Add(canvas_edge.UIName, canvas_edge.UIColor);
                ColorData.Add(canvas_shade.UIName, canvas_shade.UIColor);

                var json = ColorData.Select(d => string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
                jsonEXPO = "{" + string.Join(",", json) + "}";
                message = jsonEXPO;

            }
            else if (reset)
            {
                Grasshopper.GUI.Canvas.GH_Skin.canvas_grid = backCols;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_back = gridCols;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_edge = edgeCols;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_shade = shadeCols;

                GHUIcolor canvas_back = new GHUIcolor()
                {
                    UIName = "Canvas_backdrop",
                    UIColor = gridCols
                };
                GHUIcolor canvas_grid = new GHUIcolor()
                {
                    UIName = "Canvas_grid",
                    UIColor = gridCols
                };
                GHUIcolor canvas_edge = new GHUIcolor()
                {
                    UIName = "Canvas_edge",
                    UIColor = edgeCols
                };
                GHUIcolor canvas_shade = new GHUIcolor()
                {
                    UIName = "Canvas_shade",
                    UIColor = shadeCols
                };

                Dictionary<String, Color> ColorData = new Dictionary<String, Color>();
                ColorData.Clear();
                ColorData.Add(canvas_back.UIName, canvas_back.UIColor);
                ColorData.Add(canvas_grid.UIName, canvas_grid.UIColor);
                ColorData.Add(canvas_edge.UIName, canvas_edge.UIColor);
                ColorData.Add(canvas_shade.UIName, canvas_shade.UIColor);

                var json = ColorData.Select(d => string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
                jsonEXPO = "{" + string.Join(",", json) + "}";
                message = jsonEXPO;
            }

            if (saveCols)
            {
                /*System.IO.File.WriteAllText(@"C:\Users\yanga\AppData\Roaming\Grasshopper\Libraries\MyJunk\test.json", jsonEXPO);*/
                System.IO.File.WriteAllText(@filePath, jsonEXPO);
            }
            else if (!saveCols)
            {
                filePath = string.Empty;
            }

            DA.SetData("message", message);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("450c164a-42ff-423b-82e3-90c16a61c7e7"); }
        }
    }
    public class GHUIcolor
    {
        public string UIName { get; set; }
        public Color UIColor { get; set; }
    }
}