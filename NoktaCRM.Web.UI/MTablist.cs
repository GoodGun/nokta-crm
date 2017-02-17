using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;
using System.Text;

namespace NoktaCRM.Web.UI
{
    public class MTab : Panel
    {
        private bool _isActive = false;
        private bool _isContainer = false;
        private string _keyTitle;

        public bool IsActive { get { return _isActive; } set { _isActive = value; } }
        public bool IsContainer { get { return _isContainer; } set { _isContainer = value; } }
        public string keyTitle { get { return _keyTitle; } set { _keyTitle = value; } }
        
        public MTab()
        {
            IsContainer = false;
            ClientIDMode = System.Web.UI.ClientIDMode.Static;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (IsContainer)
                this.CssClass = "tab-content";
            else
                this.CssClass = "tab-pane" + (IsActive ? " active" : "");
            base.Render(writer);
        }
    }
}
