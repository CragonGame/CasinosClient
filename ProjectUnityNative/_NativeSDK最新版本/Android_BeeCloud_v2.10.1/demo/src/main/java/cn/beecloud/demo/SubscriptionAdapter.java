/**
 * SubscriptionAdapter.java
 * <p/>
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.List;

import cn.beecloud.entity.BCSubscription;

public class SubscriptionAdapter extends BaseAdapter {

    public static final String TAG = "BillListAdapter";

    private List<BCSubscription> subscriptions;
    private LayoutInflater mInflater;

    public SubscriptionAdapter(Context context, List<BCSubscription> subscriptions) {
        this.subscriptions = subscriptions;
        mInflater = LayoutInflater.from(context);
    }

    public void setSubscriptions(List<BCSubscription> subscriptions) {
        this.subscriptions = subscriptions;
    }

    /**
     * How many items are in the data set represented by this Adapter.
     *
     * @return Count of items.
     */
    @Override
    public int getCount() {
        if (subscriptions == null)
            return 0;

        return subscriptions.size();
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
        return subscriptions.get(position);
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
            convertView = mInflater.inflate(R.layout.subscription_item, null);

            viewHolder = new ViewHolder();

            viewHolder.txtSubscriptionId = (TextView) convertView
                    .findViewById(R.id.txtSubscriptionId);
            viewHolder.txtBuyerId = (TextView) convertView
                    .findViewById(R.id.txtBuyerId);
            viewHolder.txtPlanId = (TextView) convertView
                    .findViewById(R.id.txtPlanId);
            viewHolder.txtCardId = (TextView) convertView
                    .findViewById(R.id.txtCardId);
            viewHolder.txtBankName = (TextView) convertView
                    .findViewById(R.id.txtBankName);
            viewHolder.txtLast4 = (TextView) convertView
                    .findViewById(R.id.txtLast4);
            viewHolder.txtIdName = (TextView) convertView
                    .findViewById(R.id.txtIdName);
            viewHolder.txtIdNum = (TextView) convertView
                    .findViewById(R.id.txtIdNum);
            viewHolder.txtMobile = (TextView) convertView
                    .findViewById(R.id.txtMobile);
            viewHolder.txtStatus = (TextView) convertView
                    .findViewById(R.id.txtStatus);
            viewHolder.txtValid = (TextView) convertView
                    .findViewById(R.id.txtValid);

            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }

        BCSubscription subscription = subscriptions.get(position);

        viewHolder.txtSubscriptionId.setText(String.format("订阅记录的唯一标识: %s", subscription.getId()));
        viewHolder.txtBuyerId.setText(String.format("订阅用户id: %s", subscription.getBuyerId()));
        viewHolder.txtPlanId.setText(String.format("订阅计划id: %s", subscription.getPlanId()));
        viewHolder.txtCardId.setText(String.format("支付账户id: %s", subscription.getCardId()));
        viewHolder.txtBankName.setText(String.format("订阅用户银行名称: %s", subscription.getBankName()));
        viewHolder.txtLast4.setText(String.format("订阅用户银行卡号后四位: %s", subscription.getLast4()));
        viewHolder.txtIdName.setText(String.format("订阅用户身份证姓名: %s", subscription.getIdName()));
        viewHolder.txtIdNum.setText(String.format("订阅用户身份证号: %s", subscription.getIdNum()));
        viewHolder.txtMobile.setText(String.format("订阅用户银行预留手机号: %s", subscription.getMobile()));
        viewHolder.txtStatus.setText(String.format("订阅状态: %s", subscription.getStatus()));
        viewHolder.txtValid.setText(String.format("订阅有效: %s", subscription.isValid()));

        return convertView;
    }

    private class ViewHolder {
        public TextView txtSubscriptionId;
        public TextView txtBuyerId;
        public TextView txtPlanId;
        public TextView txtCardId;
        public TextView txtBankName;
        public TextView txtLast4;
        public TextView txtIdName;
        public TextView txtIdNum;
        public TextView txtMobile;
        public TextView txtStatus;
        public TextView txtValid;
    }
}
