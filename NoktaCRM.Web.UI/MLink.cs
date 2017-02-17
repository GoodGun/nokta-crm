using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    public class MLink : HyperLink
    {
        public string keyText { get; set; }
        public string keyDataHeader { get; set; }
        public string keyTooltip { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                if (!string.IsNullOrEmpty(this.keyText))
                    this.Text = ResourceManager.GetResource(keyText);
                if (!string.IsNullOrEmpty(this.keyDataHeader))
                    this.Attributes.Add("data-header", ResourceManager.GetResource(keyDataHeader));
                this.AddTooltip(keyTooltip);
            }
            catch { }
        }
    }
}
