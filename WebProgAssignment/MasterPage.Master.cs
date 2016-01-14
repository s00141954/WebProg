using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProgAssignment
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                btnLogOut.Text = "Log In";
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Session.Abandon();
                Response.Redirect("LogIn.aspx");
            }
            else
            {
                Response.Redirect("LogIn.aspx");
            }
        }
    }
}