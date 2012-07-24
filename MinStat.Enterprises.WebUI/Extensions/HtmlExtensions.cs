using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace MinStat.Enterprises.WebUI.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Pagination(this HtmlHelper html, int count, int pageSize, int currentPage, object parameters)
        {
            StringBuilder anchorLink = new StringBuilder("?");

            foreach(PropertyInfo pi in parameters.GetType().GetProperties())
            {
                anchorLink.AppendFormat("{0}={1}&", pi.Name, pi.GetValue(parameters, null));
            }


            TagBuilder ul = new TagBuilder("ul");
            string lis = String.Empty;
            int pageCounts = (int)Math.Ceiling((decimal) count/pageSize);
            if(pageCounts <= 10)
            {
                for(int i = 1; i <= pageCounts; i++)
                {
                    TagBuilder li = new TagBuilder("li");
                    TagBuilder anchor = new TagBuilder("a");
                    anchor.Attributes.Add("href", String.Format("{0}Page={1}", anchorLink, i));
                    anchor.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    li.InnerHtml = anchor.ToString();
                    lis += li.ToString();
                }
            }
            ul.InnerHtml = lis;

            TagBuilder paginationDiv = new TagBuilder("div");
            paginationDiv.AddCssClass("pagination");
            paginationDiv.InnerHtml = ul.ToString();

            return new MvcHtmlString(paginationDiv.ToString());
        }
    }
}