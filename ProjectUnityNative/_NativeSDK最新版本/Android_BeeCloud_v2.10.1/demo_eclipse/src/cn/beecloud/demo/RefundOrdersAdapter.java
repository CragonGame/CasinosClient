/**
 * RefundOrdersAdapter.java
 * <p/>
 * Created by xuanzhui on 2015/8/3.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.Date;
import java.util.List;

import cn.beecloud.entity.BCRefundOrder;
import cn.beecloud.entity.BCReqParams;

public class RefundOrdersAdapter extends BaseAdapter {
    private static final String TAG = "RefundOrdersAdapter";

    private List<BCRefundOrder> refunds;
    private LayoutInflater mInflater;

    public RefundOrdersAdapter(Context context, List<BCRefundOrder> refunds) {
        this.refunds = refunds;
        mInflater = LayoutInflater.from(context);
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        if (refunds == null)
            return 0;

        return refunds.size();
    }

    /**
     * Get the data item associated with the specified position in the data set.
     *
     * @param position Position of the item whose data we want within the adapter's
     *                 data set.
     * @return The data at the specified position.
     */
    @Override
    public Object getItem(int position) {
        return refunds.get(position);
    }

    /**
     * Get the row id associated with the specified position in the list.
     *
     * @param position The position of the item within the adapter's data set whose row id we want.
     * @return The id of the item at the specified position.
     */
    @Override
    public long getItemId(int position) {
        return position;
    }

    /**
     * Get a View that displays the data at the specified position in the data set. You can either
     * create a View manually or inflate it from an XML layout file. When the View is inflated, the
     * parent View (GridView, ListView...) will apply default layout parameters unless you use
     * {@link android.view.LayoutInflater#inflate(int, android.view.ViewGroup, boolean)}
     * to specify a root view and to prevent attachment to the root.
     *
     * @param position    The position of the item within the adapter's data set of the item whose view
     *                    we want.
     * @param convertView The old view to reuse, if possible. Note: You should check that this view
     *                    is non-null and of an appropriate type before using. If it is not possible to convert
     *                    this view to display the correct data, this method can create a new view.
     *                    Heterogeneous lists can specify their number of view types, so that this View is
     *                    always of the right type (see {@link #getViewTypeCount()} and
     *                    {@link #getItemViewType(int)}).
     * @param parent      The parent that this view will eventually be attached to
     * @return A View corresponding to the data at the specified position.
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        final ViewHolder viewHolder;

        if (convertView == null) {
            convertView = mInflater.inflate(R.layout.refund_order_item, null);

            viewHolder = new ViewHolder();

            viewHolder.txtId = (TextView) convertView
                    .findViewById(R.id.txtId);
            viewHolder.txtBillNum = (TextView) convertView
                    .findViewById(R.id.txtBillNum);
            viewHolder.txtRefundNum = (TextView) convertView
                    .findViewById(R.id.txtRefundNum);
            viewHolder.txtTotalFee = (TextView) convertView
                    .findViewById(R.id.txtTotalFee);
            viewHolder.txtRefundFee = (TextView) convertView
                    .findViewById(R.id.txtRefundFee);
            viewHolder.txtChannel = (TextView) convertView
                    .findViewById(R.id.txtChannel);
            viewHolder.txtSubChannel = (TextView) convertView
                    .findViewById(R.id.txtSubChannel);
            viewHolder.txtTitle = (TextView) convertView
                    .findViewById(R.id.txtTitle);
            viewHolder.txtRefundFinish = (TextView) convertView
                    .findViewById(R.id.txtRefundFinish);
            viewHolder.txtRefundResult = (TextView) convertView
                    .findViewById(R.id.txtRefundResult);
            viewHolder.txtRefundCreatedTime = (TextView) convertView
                    .findViewById(R.id.txtRefundCreatedTime);
            viewHolder.txtOptional = (TextView) convertView
                    .findViewById(R.id.txtOptional);

            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }


        BCRefundOrder bcRefundOrder = refunds.get(position);

        viewHolder.txtId.setText(String.format("退款订单唯一标识符: %s", bcRefundOrder.getId()));
        viewHolder.txtBillNum.setText(String.format("支付订单号: %s", bcRefundOrder.getBillNum()));
        viewHolder.txtRefundNum.setText(String.format("退款单号: %s", bcRefundOrder.getRefundNum()));
        viewHolder.txtTotalFee.setText(String.format("订单支付金额/元: %.2f", (bcRefundOrder.getTotalFee()/100.0)));
        viewHolder.txtRefundFee.setText(String.format("订单退款金额/元: %.2f", (bcRefundOrder.getRefundFee()/100.0)));
        viewHolder.txtChannel.setText(String.format("支付渠道: %s",
                BCReqParams.BCChannelTypes.getTranslatedChannelName(bcRefundOrder.getChannel())));
        viewHolder.txtSubChannel.setText(String.format("支付渠道: %s",
                BCReqParams.BCChannelTypes.getTranslatedChannelName(bcRefundOrder.getSubChannel())));
        viewHolder.txtTitle.setText(String.format("订单标题: %s", bcRefundOrder.getTitle()));
        viewHolder.txtRefundFinish.setText(String.format("退款订单是否受理完成: %s",
                (bcRefundOrder.isRefundFinished()?"是":"否")));
        viewHolder.txtRefundResult.setText(String.format("是否接受退款并完成退款: %s",
                (bcRefundOrder.getRefundResult()?"是":"否")));
        viewHolder.txtRefundCreatedTime.setText(String.format("退款订单生成时间: %s",
                new Date(bcRefundOrder.getRefundCreatedTime())));
        viewHolder.txtOptional.setText(String.format("退款订单生成时间: %s", bcRefundOrder.getOptional()));

        Log.w(TAG, "渠道返回的详细信息(需要通过QueryParams发起query请求，并且设置needDetail为true)，按需处理: "
                + bcRefundOrder.getMessageDetail());

        return convertView;
    }

    private class ViewHolder {
        public TextView txtId;
        public TextView txtBillNum;
        public TextView txtRefundNum;
        public TextView txtTotalFee;
        public TextView txtRefundFee;
        public TextView txtChannel;
        public TextView txtSubChannel;
        public TextView txtTitle;
        public TextView txtRefundFinish;
        public TextView txtRefundResult;
        public TextView txtRefundCreatedTime;
        public TextView txtOptional;
    }
}
