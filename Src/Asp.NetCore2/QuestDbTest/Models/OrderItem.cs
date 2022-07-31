﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrmTest
{
    [SqlSugar.SugarTable("OrderDetail")]
    public class OrderItem
    {
        //不支持自增和主键 （标识主键是用来更新用的）
        [SqlSugar.SugarColumn(IsPrimaryKey =true)]
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public decimal? Price { get; set; }
        [SqlSugar.SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; }
    }
}
