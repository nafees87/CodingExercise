using System;

namespace PackageDependencyCheckerApp
{
    public partial class PackageAnalyser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAnalyse_Click(object sender, EventArgs e)
        {
           
            PackageDependencyChecker x = new PackageDependencyChecker();
            
            sortedPackages.Text = x.Processor(txtDependencies.Value);
        }
    }
}