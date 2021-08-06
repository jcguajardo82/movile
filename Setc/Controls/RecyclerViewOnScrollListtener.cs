using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Setc.Adapters;

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

        public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
        {
            base.OnScrollStateChanged(recyclerView, newState);
        }

        // Triggered when RecyclerView is active
        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            int lastVisibleItemPosition = _linearLayoutManager.FindLastVisibleItemPosition();

            if (lastVisibleItemPosition + 1 == _adapter.ItemCount)
            {
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
                        _IsLoadingMore = false;
                    }, 2000);
                }
            }
        }
    }
}
