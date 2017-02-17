using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;
using System.Collections.Generic;

namespace NoktaCRM.Web.UI
{
    public class UIUtil
    {
        public static NumberFormatInfo DefaultFormatter = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };
        //public static Dictionary<string, NumberFormatInfo> NumberFormatters = new Dictionary<string, NumberFormatInfo>()
        //{
        //    {"TR", new NumberFormatInfo{NumberDecimalSeparator = ",", NumberGroupSeparator = "."}}, 
        //    {"EN", new NumberFormatInfo{NumberDecimalSeparator = ".", NumberGroupSeparator = ","}}
        //};
    }

    public enum numberType { Decimal = 0, Int = 1 }
    public class MNumberbox : MTextbox
    {
        public MNumberbox() { this.NumberType = numberType.Int; }//this.MinValue = 0; this.MaxValue = 999999999; 
        private static bool isTurkish = Util.CurrentUserLang == "TR";
        
        public numberType NumberType { get { object temp = ViewState["numberType"]; return temp == null ? numberType.Int : (numberType)temp; } set { ViewState["numberType"] = value; } }
        public decimal MinValue { get { object temp = ViewState["MinValue"]; return temp == null ? 0M : (decimal)temp; } set { ViewState["MinValue"] = value; } }
        public decimal MaxValue { get { object temp = ViewState["MaxValue"]; return temp == null ? 999999999M : (decimal)temp; } set { ViewState["MaxValue"] = value; } }
        public decimal SliderStep { get { object temp = ViewState["SliderStep"]; return temp == null ? 0.01M : (decimal)temp; } set { if (value == 0) value = 0.01m; ViewState["SliderStep"] = value; } }
        public bool ShowSlider { get { object temp = ViewState["ShowSlider"]; return temp == null ? false : (bool)temp; } set { ViewState["ShowSlider"] = value; } }

        public decimal ValueDecimal
        {
            get { return ToDecimal(this.Text); }
            set { this.Text = SetText(value); }
        }
        public int ValueInt
        {
            get { return Convert.ToInt32(ValueDecimal); }
            set { this.Text = SetText(value); }
        }
        public long ValueLong
        {
            get { return Convert.ToInt64(ValueDecimal); }
            set { this.Text = SetText(value); }
        }
        public byte ValueByte
        {
            get { return Convert.ToByte(ValueDecimal); }
            set { this.Text = SetText(value); }
        }
        //public static NumberFormatInfo numInfo { get { return UIUtil.DefaultFormatter; } }
        public static string sepThousand = isTurkish ? "." : ",";
        public static string sepDecimal = isTurkish ? "," : ".";

        private decimal ToDecimal(string val)
        {
            if (string.IsNullOrEmpty(val)) return 0;
            // Turkish: 123.456,78
            // English: 123,456.78
            //return Convert.ToDecimal(val.Replace(sepThousand, "").Replace(sepDecimal, ","), numInfo);
            return Convert.ToDecimal(val, UIUtil.DefaultFormatter);
        }
        private string SetText(object o)
        {
            return Convert.ToDecimal(o).ToString(UIUtil.DefaultFormatter);
        }
        protected override void Render(HtmlTextWriter oWriter)
        {
            string cssClass = " mask-" + this.NumberType.ToString().ToLowerInvariant();
            this.CssClass = this.CssClass.Replace(cssClass, "") + cssClass;

            this.Attributes["data-v-min"] = MinValue.ToString().Replace(".", "").Replace(",", ".");
            this.Attributes["data-v-max"] = MaxValue.ToString().Replace(".", "").Replace(",", ".");
            this.Attributes["data-m-dec"] = this.NumberType == numberType.Int ? "0" : "2";
            this.AddTooltip(string.Concat(SetText(MinValue), " - ", SetText(MaxValue)), false);

            if (ShowSlider)
            {
                try
                {
                    base.Render(oWriter);

                    oWriter.Write(string.Format(@" <span class='sliderleft'>{4}</span> <div class='slider slider-basic' data-xmin='{0}' data-xmax='{1}' data-xstep='{2}' data-xitem='{3}'></div> <span class='sliderright'>{5}</span>",
                                          this.MinValue.ToString().Replace(".", "").Replace(",", "."),
                                          this.MaxValue.ToString().Replace(".", "").Replace(",", "."),
                                          this.NumberType == numberType.Int ? SliderStep.ToString() : SliderStep.ToString().Replace(",", "."),
                                          this.ClientID,
                                          this.NumberType == numberType.Int ? MinValue.ToString() : MinValue.ToString("N2"),
                                          this.NumberType == numberType.Int ? MaxValue.ToString() : MaxValue.ToString("N2")));
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            else
                base.Render(oWriter);

        }
    }
}
