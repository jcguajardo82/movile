using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Setc.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Setc.Controls
{
    public class RecyclerViewOnScrollListtener : RecyclerView.OnScrollListener
    {
        private SwipeRefreshLayout _swipeRefreshLayout;
        private LinearLayoutManager _linearLayoutManager;//Layout manager
        private OrdenesListAdapter _adapter;//recyclerView adapter
        private Android.OS.Handler handler;
        public delegate void InsertData();//Add more data commission
        private InsertData _InsertDataEvent; //Load more events
        private bool _IsLoadingMore;
        public RecyclerViewOnScrollListtener(SwipeRefreshLayout swipeRefreshLayout, Android.OS.Handler handle, LinearLayoutManager linearLayoutManager, OrdenesListAdapter recyclerViewAdapter, InsertData InsertDataEvent, bool IsLoadingMore)
        {
            _swipeRefreshLayout = swipeRefreshLayout;
            _linearLayoutManager = linearLayoutManager;
            _adapter = recyclerViewAdapter;
            _InsertDataEvent = InsertDataEvent;
            handler = handle;
            _IsLoadingMore = IsLoadingMore;
        }

        // Triggered when the sliding state of RecyclerView changes
        //There are 3 kinds of sliding states, 0: ScrollStateIdle finger leaves the screen 1ScrollStateDragging: finger touches the screen 2ScrollStateSetting
        public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
        {
            base.OnScrollStateChanged(recyclerView, newState);
            System.Diagnostics.Debug.Write("test", "newState:" + newState);
        }

        // Triggered when RecyclerView is active
        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            System.Diagnostics.Debug.Write("Sliding");
            int lastVisibleItemPosition = _linearLayoutManager.FindLastVisibleItemPosition();

            if (lastVisibleItemPosition + 1 == _adapter.ItemCount)
            {
                System.Diagnostics.Debug.Write("test", "loadding has been completed");
                bool isRefreshing = _swipeRefreshLayout.Refreshing;
                if (isRefreshing)
                {
                    _adapter.NotifyItemRemoved(_adapter.ItemCount);
                    return;
                }
                if (!_IsLoadingMore)
                {
                    _IsLoadingMore = true;
                    handler.PostDelayed(() =>
                    {
                        _InsertDataEvent();
                        System.Diagnostics.Debug.Write("test", "Loading more has been completed");
                        _IsLoadingMore = false;
                    }, 3000);
                }
            }
        }
    }
}
