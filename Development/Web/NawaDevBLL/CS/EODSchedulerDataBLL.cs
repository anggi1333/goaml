// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace NawaDevBLL
{
	[Serializable()]public class EODSchedulerDataBLL
	{
		public NawaDAL.EODScheduler objScheduler;
		public List<NawaDAL.EODSchedulerDetail> objSchedulerDetail;
	}
	
}
