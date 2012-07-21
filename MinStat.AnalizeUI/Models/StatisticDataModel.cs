﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.Models
{
    public class StatisticDataModel
    {
        public IEnumerable<string> Titles { get; set; }
        public IEnumerable<StatisticDataItemModel> Values { get; set; }
    }
}