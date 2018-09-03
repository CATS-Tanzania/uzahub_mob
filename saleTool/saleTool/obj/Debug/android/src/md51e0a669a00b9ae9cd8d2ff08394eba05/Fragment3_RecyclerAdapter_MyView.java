package md51e0a669a00b9ae9cd8d2ff08394eba05;


public class Fragment3_RecyclerAdapter_MyView
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("saleTool.Fragments.Fragment3+RecyclerAdapter+MyView, saleTool", Fragment3_RecyclerAdapter_MyView.class, __md_methods);
	}


	public Fragment3_RecyclerAdapter_MyView (android.view.View p0)
	{
		super (p0);
		if (getClass () == Fragment3_RecyclerAdapter_MyView.class)
			mono.android.TypeManager.Activate ("saleTool.Fragments.Fragment3+RecyclerAdapter+MyView, saleTool", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
