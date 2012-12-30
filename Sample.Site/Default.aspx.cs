using Sample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample.Site
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Repository.Instance.GetSettingValue("installed") != "yes")
            {
                Response.Redirect("~/Install/Install.aspx");
            }
        }
    }
}