/************************************************************************************
 *Copyright (c) 2016iLine All Rights Reserved.
 *命名空间： iCMS.Common.Component.Tool
 *文件名：  SecurityControll2Folder
 *创建人：  LF  
 *创建时间：2016年12月28日15:49:05
 *描述：为指定文件(夹)分派完全控制权限
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
  

    public enum AuthorizedUsers
    {
        [Description("IIS_IUSRS")]
        IIS_IUSRS,
        [Description("Users")]
        Users,
        [Description("Everyone")]
        Everyone,
        [Description("Administrator")]
        Administrator,
        [Description("Administrators")]
        Administrators,
        [Description("LOCAL SERVICE")]
        LOCAL_SERVICE,
        [Description("CREATOR OWNER")]
        CREATOR_OWNER,
        [Description("IUSR")]
        IUSR,
        [Description("SERVICE")]
        SERVICE,
        [Description("SYSTEM")]
        SYSTEM,
        [Description("NETWORK SERVICE")]
        NETWORK_SERVICE
    }


   public static class SecurityControll2Folder
    {
        /// <summary>
        ///为文件夹添加users，IIS_IUSRS用户组的完全控制权限
        /// </summary>
        /// <param name="dirPath"></param>
      public static void AddSecurityControll2Folder(string dirPath, AuthorizedUsers user)
       {
           //获取文件夹信息
           DirectoryInfo dir = new DirectoryInfo(dirPath);
           //获得该文件夹的所有访问权限
           System.Security.AccessControl.DirectorySecurity dirSecurity = dir.GetAccessControl(AccessControlSections.All);
           //设定文件ACL继承
           InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
           //添加IIS_IUSRS用户组的访问权限规则 完全控制权限
           FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(EnumHelper.GetDescription(user), FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);
           bool isModified = false;
           dirSecurity.ModifyAccessRule(AccessControlModification.Add, fileSystemAccessRule, out isModified);
           //设置访问权限
           dir.SetAccessControl(dirSecurity);
       }
    }
}
