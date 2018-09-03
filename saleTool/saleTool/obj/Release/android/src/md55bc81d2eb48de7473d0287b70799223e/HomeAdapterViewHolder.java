package md55bc81d2eb48de7473d0287b70799223e;


public class HomeAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("saleTool.HomeAdapterViewHolder, saleTool", HomeAdapterViewHolder.class, __md_methods);
	}


	public HomeAdapterViewHolder ()
	{
		super ();
		if (getClass () == HomeAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("saleTool.HomeAdapterViewHolder, saleTool", "", this, new java.lang.Object[] {  });
	}

	public HomeAdapterViewHolder (android.view.View p0)
	{
		super ();
		if (getClass () == HomeAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("saleTool.HomeAdapterViewHolder, saleTool", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
