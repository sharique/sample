using Sample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample.Site.Install
{
    public partial class Install1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Title = "Site Installation ";
            }
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            var repo = Repository.Instance;
            repo.SaveSetting("installed", "no");
            repo.SaveSetting("sitename", tbSitename.Text);
            repo.SaveSetting("installed", "yes");

            lblMsg.Text = "Settings saved.";
        }
    }
}