using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    [DefaultProperty("keyText"), ToolboxData(@"<{0}:MButton runat=""server""></{0}:MButton>")]
    public class MButton : Button
    {
        public string keyTooltip { get; set; }
        public string keyText { get; set; }
        public string keyClientConfirm { get; set; }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                if (!string.IsNullOrEmpty(keyText))
                    this.Text = ResourceManager.GetResource(keyText);
                if (!string.IsNullOrEmpty(keyClientConfirm))
                    this.Attributes.Add("onclick", "return confirm('" + ResourceManager.GetResource(keyClientConfirm) + "');");

                this.AddTooltip(keyTooltip);
            }
            catch { this.Text = keyText; }
        }
    }
}
