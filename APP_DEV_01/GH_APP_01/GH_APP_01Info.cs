using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace GH_APP_01
{
    public class GH_APP_01Info : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "GHAPP01";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("19dcd291-5708-4f69-b11c-c945d69c04fb");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
