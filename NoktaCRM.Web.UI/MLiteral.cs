using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    [DefaultProperty("keyData"), ToolboxData("<{0}:MLiteral runat=server></{0}:MLiteral>")]
    public class MLiteral : Literal
    {
        public string keyData { get; set; } 
        public bool isTooltip { get; set; }

        public void SetText(string key = null)
        {
            try
            {
                keyData = key ?? keyData;
                if (!string.IsNullOrEmpty(keyData))
                    this.Text = ResourceManager.GetResource(keyData);
            }
            catch { this.Text = keyData; }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SetText();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (!isTooltip || string.IsNullOrEmpty(this.keyData))
            {
                base.Render(writer);
                return;
            }
            try
            {
                string span = string.Format("<span class='miniinfo'>{0}</span>",
                    ResourceManager.GetResource(this.keyData));

                writer.WriteLine(span);
            }
            catch { }
        }
    }
}
