
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HELPSMobileFrontEnd.Adapters
{
	public class TaskListAdapter : BaseAdapter<WorkshopSets>
	{
		Activity context = null;
		IList<WorkshopSets> WorkShops = new List<WorkshopSets>();

		public TaskListAdapter (Activity context, IList<WorkshopSets> workshops) : base ()
		{
			this.context = context;
			this.WorkShops = workshops;
		}

		public override WorkshopSets this[int position]
		{
			get { return WorkShops[position]; }
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count
		{
			get { return WorkShops.Count; }
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get our object for position
			var item = WorkShops[position];			

			//Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
			// gives us some performance gains by not always inflating a new view
			// will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()
			var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.TaskListItem, parent, false)) as LinearLayout;

			// Find references to each subview in the list item's view
			var tvWorkshops = view.FindViewById<TextView>(Resource.Id.tvWorkshops);

			//Assign item's values to the various subviews
			tvWorkshops.SetText (item.name, TextView.BufferType.Normal);

			//Finally return the view
			return view;
		}
	}
}

