/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Frameworks.Core.DB
 *文件名：  DBDependencyRegistrar
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
namespace iCMS.Frameworks.Core.DB
{
    /// <summary>
    /// 
    /// </summary>
    public class DBDependencyRegistrar
    {
       
      //  private static DBDependencyRegistrar m_instance = null;
        private static readonly object m_s_lock = new object();

        public DBDependencyRegistrar()
        {
           // ServiceLocator.RegisterService<IUSERService, USERService>();
        }
    }
}
