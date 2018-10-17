/**
 * BCQueryLimit.java
 * 通用按条件查询的限制条件
 * Created by xuanzhui on 2016/7/28.
 * Copyright (c) 2016 BeeCloud. All rights reserved.
 */
package cn.beecloud.entity;

public class BCBaseCriteria {
    //Query objects created before or equal to the given UNIX timestamp in ms.
    private Long created_before;

    //Query objects created after or equal to the given UNIX timestamp in ms.
    private Long created_after;

    //If count_only is true, only the total count of all objects that match your filters will be returned.
    private Boolean count_only;

    //Skip some objects for pagination
    private Integer skip;

    //Limit the number of objects
    private Integer limit;

    /**
     * 限制记录创建在createdBefore时间戳之前
     */
    public void setCreatedBefore(Long createdBefore) {
        this.created_before = createdBefore;
    }

    /**
     * 限制记录创建在createdAfter时间戳之后
     */
    public void setCreatedAfter(Long createdAfter) {
        this.created_after = createdAfter;
    }

    /**
     * 设置是否只需要返回满足条件的记录总个数，如果为TRUE，那么查询结果只包含总个数信息
     */
    public void setCountOnly(Boolean countOnly) {
        this.count_only = countOnly;
    }

    /**
     * 设置本次查询需要跳过的记录条数，和limit一起用于实现分页
     */
    public void setSkip(Integer skip) {
        this.skip = skip;
    }

    /**
     * 设置本次查询需要返回的最大记录条数，默认为10
     */
    public void setLimit(Integer limit) {
        this.limit = limit;
    }
}
