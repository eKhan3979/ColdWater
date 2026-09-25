package crc64d7c513f792956e2b;


public class ListaTreinosAdapterViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ColdWater.Adapters.ListaTreinosAdapterViewHolder, ColdWater", ListaTreinosAdapterViewHolder.class, __md_methods);
	}

	public ListaTreinosAdapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ListaTreinosAdapterViewHolder.class) {
			mono.android.TypeManager.Activate ("ColdWater.Adapters.ListaTreinosAdapterViewHolder, ColdWater", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
