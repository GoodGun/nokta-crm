using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;


namespace Utility
{
	public class ExceptionManager
	{ 
		#region Functions

		/// <summary>
		/// Publishes the details of a message		(Overload #1) 
		/// </summary>
		/// <param name="ex">The exception thrown.</param>
		public static void Publish(Exception ex, string subject = null)
		{
            try
            {
                if (ex != null)
                    Publish(GetDetailedMessage(ex), subject);
            }
            catch { }
		}


		/// <summary>
		/// Publishes the details of a message		(Overload #4)
		/// </summary>
		/// <param name="LogType">Enumeration, indicating the priority of the Exception.</param>
		/// <param name="strMessage">The message which has been stripped off the exception that has been caught/thrown.</param>
		public static void Publish(string strMessage, string subject = null)
		{
			// Ignoring Error display errors:
			if (strMessage.IndexOf("Thread was being aborted") > 0) return;

			// If dangerous Cookies faced, clear the Cookies.
			if (strMessage.IndexOf("A potentially dangerous Request.Cookies value was detected") >= 0)
			{
				try
				{
					System.Web.HttpContext.Current.Request.Cookies.Clear();
					System.Web.HttpContext.Current.Response.Cookies.Clear();
					System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.Url.ToString());
				}
				catch(Exception){}
			}

			// To manually force the Garbage Collector to free unused memory in case of an OutOfMemory Exception.
			if (strMessage.IndexOf("Exception of type System.OutOfMemoryException was thrown.") >= 0) 	
			{
				GC.Collect();
				strMessage = strMessage.Replace("Exception of type System.OutOfMemoryException was thrown.", "Exception of type System.OutOfMemoryException was thrown.<br><b>(GC.Collect() çaðýrýldý.)");
			}
            if (ConfigManager.Current.IsTestEnvironment)
                System.Web.HttpContext.Current.Response.Write(strMessage);
			LogString(strMessage, subject);
		}

		/// <summary>
		/// The core function, that logs a given (formatted) string to the Event Log, Database, File, Email.
		/// </summary>
		/// <param name="LogType">Enumeration, indicating the priority of the Exception.</param>
		/// <param name="strMessage">The message which has been stripped off the exception that has been caught/thrown.</param>
		private static void LogString( string strMessage, string subject = null)
		{
			string strFormattedMessage = string.Format("(Error Level {0}){7}At : {6}{7}On : {1}-{4} / <a href='{2}'>{2}</a>{7}IP : {5}{7}{3}", "", Util.MachineName, System.Web.HttpContext.Current.Request.Url, strMessage, System.Threading.Thread.CurrentThread.Name, Util.CurrentUserIP, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), System.Environment.NewLine);

            if (!subject.NullOrEmpty())
                strFormattedMessage = string.Concat(subject, Environment.NewLine, strFormattedMessage);
			AlertByEmail(strMessage);	//Changed
			
		}

		/// <summary>
		/// Sends the details of an exception to the related distribution list.
		/// </summary>
		/// <param name="LogType">Enumeration of type Enumerations.ADLogType</param>
		/// <param name="strException">Exception details</param>
		/// <returns>Success flag.</returns>
		private static bool AlertByEmail(string strException)
		{
			bool bResult = false;
			
			try
			{
				Util.SendMailAsnyc("support@meramobile.com.pk", ConfigManager.Current.ExceptionEmailTo,  "Error occurred", strException); 

                bResult = true;
			}
			catch(Exception)
			{
				bResult = false;
			}

			return bResult;
		}


		private static string GetDetailedMessage(Exception ex)
		{
			return GetDetailedMessage(ex, true);
		}

		/// <summary>
		/// Strips an exception and forms a detailed warning message
		/// (Recursive, that is if the exception has an inner exception,
		///	its detailed message is appended at the end of the former
		/// exception message)
		/// </summary>
		/// <param name="ex">Exception the details of which is to be logged</param>
		/// <returns>The stripped message out of exception</returns>
		private static string GetDetailedMessage(Exception ex, bool bGeneralDetails)
		{
			System.Text.StringBuilder sMessage = new System.Text.StringBuilder();
			System.Web.HttpContext oContext = System.Web.HttpContext.Current;

			string sNone = @"
<tr>
	<td colspan='2'>None.</td>
</tr>";
			string sEmptyLine = @"
<tr>
	<td colspan='2'>&nbsp;</td>
</tr>";

			try
			{
				sMessage.AppendFormat("<TABLE id='tblWarning' cellSpacing='0' cellPadding='2' width='100%' align='center' border='2' borderColor='orange' bgColor='whitesmoke'>");

				#region RelatedFunctionDetails
				sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Exception Details:</b></font></td>
</tr>");

				
				sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Message :&nbsp;</td>
	<td width=80%><font color='#ff0000'>{0}</font>&nbsp;</td>
</tr>
<tr>
	<td align=right bgcolor=#ffe4b5>Source :&nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>
<tr>
	<td align=right bgcolor=#ffe4b5>Stacktrace :&nbsp;</td>
	<td width=80%>{2}&nbsp;</td>
</tr>", ex.Message, ex.Source, ex.StackTrace);

				if (ex.TargetSite != null)
				{
					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Related Function Details:</b></font></td>
</tr>");
					// ex.TargetSite is the method which throws the current Exception.
					ParameterInfo [] arrParameters = ex.TargetSite.GetParameters();
					if (arrParameters.Length.Equals(0))
						sMessage.Append(sNone);

					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Related Function Name :&nbsp;</td>
	<td width=80%>{0}&nbsp;</td>
</tr>", ex.TargetSite.Name);

					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Parameters:</b></font></td>
</tr>");

					for (int i = 0; i < arrParameters.Length; i++)
							sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>{0} : &nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>", arrParameters[i].ParameterType.ToString(), arrParameters[i].Name);
					sMessage.Append(sEmptyLine);
				}	
				#endregion

				#region UserDetails
				//************ User Details **********
				if (bGeneralDetails)
				{

					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Custom Variables:</b></font></td>
</tr>");
					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Time :&nbsp;</td>
	<td width=80%>{0}&nbsp;</td>
</tr>", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Occurred In :&nbsp;</td>
	<td width=80%>{0}&nbsp;</td>
</tr>", System.Web.HttpContext.Current.Server.MachineName);

					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Page Address :&nbsp;</td>
	<td width=80%><a href='{0}'>{0}</a>&nbsp;</td>
</tr>", System.Web.HttpContext.Current.Request.Url);

					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>User IP :&nbsp;</td>
	<td width=80%>{0}&nbsp;</td>
</tr>", Util.CurrentUserIP);
		
					sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>Current Thread :&nbsp;</td>
	<td width=80%>{0}&nbsp;</td>
</tr>", System.Threading.Thread.CurrentThread.Name);
					sMessage.Append(sEmptyLine);
				}
				//************ User Details **********
				#endregion

				#region ServerVariables
				//************ Server Variables *****************
				if(bGeneralDetails)
				{
					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Server Variables:</b></font></td>
</tr>");

                    string[] Keys = { "SERVER_PROTOCOL", "SERVER_PORT", "CONTENT_LENGTH", "HTTPSLOCAL_ADDR", "SERVER_SOFTWARE", "HTTP_USER_AGENT", "HTTP_HOST", "HTTP_REFERER" };
					System.Collections.Specialized.NameValueCollection nameVal = System.Web.HttpContext.Current.Request.ServerVariables;
			
					foreach(string sKey in Keys)
						sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>{0} : &nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>", sKey, nameVal[sKey]);

					sMessage.Append(sEmptyLine);
				}
				//************ Server Variables *****************
				#endregion

				#region FormVariables
				//************ Request.Form **********
				if (bGeneralDetails && oContext != null)
				{
					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Request.Form Variables:</b></font></td>
</tr>");
					if (oContext.Request.Form.Count.Equals(0))
						sMessage.Append(sNone);

					foreach(string sKey in oContext.Request.Form)
						if (sKey.Equals("__VIEWSTATE").Equals(false))
							sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>{0} : &nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>", sKey, oContext.Request.Form[sKey]);
					sMessage.Append(sEmptyLine);
				}
				//************ Request.Form **********
#endregion

				#region QueryStringVariables
				//************ Request.QueryString **********
				if (bGeneralDetails && oContext != null)
				{
					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Request.QueryString Variables:</b></font></td>
</tr>");
					if (oContext.Request.QueryString.Count.Equals(0))
						sMessage.Append(sNone);

					foreach(string sKey in oContext.Request.QueryString)
						sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>{0} : &nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>", sKey, oContext.Request.QueryString[sKey]);
					sMessage.Append(sEmptyLine);
				}
				//************ Request.QueryString **********
				#endregion

				#region CookieVariables
				//************ Request.Cookies **********
				if (bGeneralDetails && oContext != null)
				{
					sMessage.AppendFormat(@"
<tr>
	<td colspan='2' bgcolor='#fafad2'><font color='#080000'><b>Request.Cookies Variables:</b></font></td>
</tr>");
					if (oContext.Request.Cookies.Count.Equals(0))
						sMessage.Append(sNone);

					foreach(string sKey in oContext.Request.Cookies)
						sMessage.AppendFormat(@"
<tr>
	<td align=right bgcolor=#ffe4b5>{0} : &nbsp;</td>
	<td width=80%>{1}&nbsp;</td>
</tr>", sKey, oContext.Request.Cookies[sKey].Value);
					sMessage.Append(sEmptyLine);
				}
				//************ Request.Cookies **********
				#endregion

				sMessage.Append(@"
</table>");
			}
			catch(Exception)
			{
			}

			// If inner Exception exists, append it to the end recursively.
			if (ex.InnerException != null)
				sMessage.AppendFormat(@"
<br>
	<table width='100%' bgcolor='#cccccc'>
		<tr>
			<td rowspan=2 width=20>&nbsp;</td>
			<td><b>Inner Exception</b></td>
		</tr>
		<tr>
			<td>{0}</td>
		</tr>
	</table>", GetDetailedMessage(ex.InnerException, false));

			return sMessage.ToString();
		}
		
		#endregion
	}
}
