using Sample.Data;
using Sample.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

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
            repo.SaveSetting("site-name", tbSitename.Text);
            repo.SaveSetting("site-slogan", tbSiteSlogan.Text);
            Membership.CreateUser(tbUname.Text, tbPass.Text, tbEmail.Text);
            repo.SaveSetting("installed", "yes");

            lblMsg.Text = "Settings saved.";
        }
    }
}