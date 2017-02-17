using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    public class MRegexValidator : RegularExpressionValidator
    {
        private string _keyError = "*";
        public string keyError
        {
            get { return _keyError; }
            set { _keyError = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(CssClass))
                CssClass = "vld";

            try
            {
                if (string.IsNullOrEmpty(ErrorMessage) && !string.IsNullOrEmpty(keyError))
                    ErrorMessage = keyError == "*" ? "*" : ResourceManager.GetResource(keyError);
            }
            catch (Exception) { }
            base.OnInit(e);
            Page.Validators.Add(this);
        }
    }
}