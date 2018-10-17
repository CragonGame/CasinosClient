/**
 * ShoppingListAdapter.java
 * <p/>
 * Created by xuanzhui on 2015/7/27.
 * Copyright (c) 2015 BeeCloud. All rights reserved.
 */
package cn.beecloud.demo;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;
import java.util.Map;

public class ShoppingListAdapter extends BaseAdapter {

    private LayoutInflater mInflater;
    private List<Map<String, Object>> mDatas;

    public ShoppingListAdapter(Context context, List<Map<String, Object>> data) {
        mInflater = LayoutInflater.from(context);
        mDatas = data;
    }

    @Override
    public int getCount() {
        return mDatas.size();
    }

    @Override
    public Object getItem(int position) {
        return mDatas.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        ViewHolder viewHolder;

        if (convertView == null) {
            convertView = mInflater.inflate(R.layout.list_item_shopping_cart, parent, false);

            viewHolder = new ViewHolder();
            viewHolder.txtViewName = (TextView) convertView.findViewById(R.id.txtViewName);
            viewHolder.txtViewDesc = (TextView) convertView.findViewById(R.id.txtViewDesc);
            viewHolder.imageView = (ImageView) convertView.findViewById(R.id.imageView);

            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }
        Map<String, Object> map = mDatas.get(position);
        viewHolder.txtViewName.setText(map.get("name").toString());
        viewHolder.txtViewDesc.setText(map.get("desc").toString());
        viewHolder.imageView.setImageResource((Integer) map.get("icon"));
        return convertView;
    }

    static class ViewHolder {
        TextView txtViewName;
        TextView txtViewDesc;
        ImageView imageView;
    }
}
