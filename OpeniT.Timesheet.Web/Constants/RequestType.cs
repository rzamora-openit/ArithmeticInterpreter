using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Constants
{
	public static class RequestType
	{
		public const string COVL = "CoVL";
		public const string VACATIONLEAVE = "VL";
		public const string EMERGENCYLEAVE = "EL";
		public const string SICKLEAVE = "SL";
		public const string TIMEOFF = "TO";
		public const string BIRTHDAYLEAVE = "BL";
		public const string LEAVEWITHOUTPAY = "LWOP";	

		public const string COVL_STRING = "Carry Over VL";
		public const string VACATIONLEAVE_STRING = "Vacation Leave";
		public const string EMERGENCYLEAVE_STRING = "Emergency Leave";
		public const string SICKLEAVE_STRING = "Sick Leave";
		public const string TIMEOFF_STRING = "Time Off";
		public const string BIRTHDAYLEAVE_STRING = "Birthday Leave";
		public const string LEAVEWITHOUTPAY_STRING = "Leave w/o Pay";

		public static string Convert(string value)
		{
			switch (value)
			{
				case COVL: return COVL_STRING;
				case VACATIONLEAVE: return VACATIONLEAVE_STRING;
				case EMERGENCYLEAVE: return EMERGENCYLEAVE_STRING;
				case SICKLEAVE: return SICKLEAVE_STRING;
				case TIMEOFF: return TIMEOFF_STRING;
				case BIRTHDAYLEAVE: return BIRTHDAYLEAVE_STRING;
				case LEAVEWITHOUTPAY: return LEAVEWITHOUTPAY_STRING;
				case COVL_STRING: return COVL;
				case VACATIONLEAVE_STRING: return VACATIONLEAVE;
				case EMERGENCYLEAVE_STRING: return EMERGENCYLEAVE;
				case SICKLEAVE_STRING: return SICKLEAVE;
				case TIMEOFF_STRING: return TIMEOFF;
				case BIRTHDAYLEAVE_STRING: return BIRTHDAYLEAVE;
				case LEAVEWITHOUTPAY_STRING: return LEAVEWITHOUTPAY;
				default: return value;
			}
		}

	}
}
