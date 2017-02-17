using System;
using System.Web.UI;
using System.ComponentModel;
using System.Xml;
using Utility;
using OboutInc.Calendar2;
using System.Collections.Generic;

namespace NoktaCRM.Web.UI
{
    public class MCalendar : Calendar
    {
        public MCalendar()
        {
            try
            {
                bool turkish = Util.CurrentUserLang == "TR";
                this.CultureName = turkish ? "tr-TR" : "en-US";
                this.FirstDayOfWeek = turkish ? 1 : 0;
                this.TextArrowLeft = ResourceManager.GetResource("nav.previous.month");
                this.TextArrowRight = ResourceManager.GetResource("nav.next.month");
                this.DateFormat = "dd.MM.yyyy";

                this.Columns = 3;
                this.AllowSelectSpecial = false;
                this.MonthHeight = 190;
                this.MonthWidth = 180;
                this.ScrollBy = 1;
                this.ShowErrorAlert = false;
                this.ShowOtherMonthDays = false;
                this.DisableEmbeddedScriptFileResource = true;
            }
            catch { }
        }
        public bool SelectDates(List<DateTime> selDates, bool append = false)
        {
            if (selDates == null || selDates.Count == 0) return false;
            if (!append) this.SelectedDates.Clear();

            foreach (DateTime dt in selDates)
                this.SelectedDates.Add(dt);
            return true;
        }
        public int SelectBetween(DateTime dt1, DateTime dt2, bool setBoundaries = true)
        {
            if (dt2 < dt1) return 0;
            if (setBoundaries) this.DateMin = dt1;
            if (setBoundaries) this.DateMax = dt2;
            
            this.SelectedDates.Clear();
            
            var specials = new List<DateTime>();
            for (int i = 0; i < this.SpecialDates.Count; i++)
                specials.Add(new DateTime(this.SpecialDates[i].Year, this.SpecialDates[i].Month, this.SpecialDates[i].Day));

            while (dt1 <= dt2)
            {
                if (!specials.Contains(dt1)) 
                    this.SelectedDates.Add(dt1);
                dt1 = dt1.AddDays(1d);
            }
            return SelectedDates.Count;
        }
    }
}
