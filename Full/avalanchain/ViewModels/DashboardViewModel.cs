using System.Collections.Generic;

namespace avalanchain
{
	public class DashboardViewModel
	{
		public List<SampleCategory> Items { 
			get 
			{ 
				return SamplesDefinition.SamplesCategoryList;
			} 
		}
	}
}