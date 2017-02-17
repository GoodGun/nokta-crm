using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    public class MDatePicker : MTextbox
    {
        public string DateFormat { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public bool HasDate { get { return SelectedDate != DateTime.MinValue; } }
        public DateTime SelectedDate
        {
            get { return this.Text.Replace(".", "").ToDateTime(); }
            set { this.Text = value == DateTime.MinValue ? "" : DateToString(value); }
        }
        
        public MDatePicker()
        {
            DateFormat = "dd.mm.yyyy";
            //Width = new Unit(75, UnitType.Pixel);
        }
        public override int MaxLength { get { return 10; } set { base.MaxLength = 10; } }
        protected override void Render(HtmlTextWriter oWriter)
        {
            string cssClass = " date-picker ";
            this.CssClass = cssClass + (string.IsNullOrEmpty(this.CssClass) ? "" : this.CssClass);
            this.Attributes.Add("data-date-format", DateFormat);
            if (MinDate != DateTime.MinValue)
                this.Attributes.Add("data-start", DateToString(MinDate));
            if (MaxDate != DateTime.MinValue)
                this.Attributes.Add("data-end", DateToString(MaxDate));

            if (SelectedDate != DateTime.MinValue)
                this.Attributes.Add("data-date", DateToString(SelectedDate));
            
            base.Render(oWriter);
        }
        public static string DateToString(DateTime val)
        {
            return string.Format("{0}.{1}.{2}", val.Day.ToString().PadLeft(2, '0'), val.Month.ToString().PadLeft(2, '0'), val.Year);
        }
    }
}
