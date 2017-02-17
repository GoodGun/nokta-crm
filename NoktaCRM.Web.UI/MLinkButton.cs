using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    [DefaultProperty("keyText"), ToolboxData(@"<{0}:MButton runat=""server""></{0}:MButton>")]
    public class MLinkButton : LinkButton
    {
        public string keyTooltip { get; set; }
        public string keyText { get; set; }
        public string keyIcon { get; set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                if (!string.IsNullOrEmpty(keyText))
                    this.Text = ResourceManager.GetResource(keyText);

                this.AddTooltip(keyTooltip);
            }
            catch { this.Text = keyText; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(keyIcon) && !this.Text.StartsWith("<i class"))
                this.Text = string.Format("<i class='{0}'></i> {1}", keyIcon, Text);
            base.Render(writer);
        }
    }
}
