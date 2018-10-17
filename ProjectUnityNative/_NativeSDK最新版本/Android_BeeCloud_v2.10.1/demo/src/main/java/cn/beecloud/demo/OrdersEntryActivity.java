package cn.beecloud.demo;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import cn.beecloud.demo.util.DisplayUtils;

public class OrdersEntryActivity extends Activity {
    Button btnQueryBills;
    Button btnQueryRefunds;
    Button btnQuerySubscriptions;
    Button btnRefundStatus;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_orders_entry);

        DisplayUtils.initBack(this);

        btnQueryBills = (Button) findViewById(R.id.btnQueryBills);
        btnQueryBills.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(OrdersEntryActivity.this, BillListActivity.class);
                startActivity(intent);
            }
        });

        btnQueryRefunds = (Button) findViewById(R.id.btnQueryRefunds);
        btnQueryRefunds.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(OrdersEntryActivity.this, RefundOrdersActivity.class);
                startActivity(intent);
            }
        });

        btnQuerySubscriptions = (Button) findViewById(R.id.btnQuerySubscriptions);
        btnQuerySubscriptions.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(OrdersEntryActivity.this, SubscriptionListActivity.class);
                startActivity(intent);
            }
        });

        btnRefundStatus = (Button) findViewById(R.id.btnRefundStatus);
        btnRefundStatus.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(OrdersEntryActivity.this, RefundStatusActivity.class);
                startActivity(intent);
            }
        });
    }

    public void forwardAuth(View view) {
        Intent intent = new Intent(OrdersEntryActivity.this, VerifyCardActivity.class);
        startActivity(intent);
    }

}
