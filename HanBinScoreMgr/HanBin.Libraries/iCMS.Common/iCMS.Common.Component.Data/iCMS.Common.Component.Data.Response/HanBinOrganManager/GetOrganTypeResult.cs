using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Response.HanBinOrganManager
{
    /// <summary>
    /// 获取单位类型返回结果类
    /// </summary>
    public class GetOrganTypeResult
    {
        public List<CategoryInfo> CategoryList { get; set; }

        public GetOrganTypeResult()
        {
            CategoryList = new List<CategoryInfo>();
        }
    }

    public class CategoryInfo
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public List<OrganTypeInfo> OrganTypeList { get; set; }

        public CategoryInfo()
        {
            OrganTypeList = new List<OrganTypeInfo>();
        }
    }

    public class OrganTypeInfo
    {
        public int OrganTypeID { get; set; }

        public string OrganTypeName { get; set; }
    }
}
