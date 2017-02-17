using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.Common;
using System.Data;
using System.Data.SqlClient;
using Utility;
using System.Threading;

namespace BusinessObjects
{
    public sealed class LogManager
    {
        public static void LogSerialVisit(int MemberID, int SerialID)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@MemberID", MemberID), new SqlParameter("@SerialID", SerialID) };
                ThreadPool.QueueUserWorkItem(
                    delegate
                    {
                        EntityManager.ExecuteQuery("LogSerialVisit", parameters);
                    });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
