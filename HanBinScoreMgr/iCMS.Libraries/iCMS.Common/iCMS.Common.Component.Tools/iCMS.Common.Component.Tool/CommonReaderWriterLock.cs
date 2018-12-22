/***********************************************************************
 * Copyright (c) 2018@ILine All Rights Reserved.
 * 命名空间：iCMS.Common.Component.Tool
 * 文件名：  CommonReaderWriterLock
 * 创建人：  张辽阔
 * 创建时间：2018-01-19
 * 描述：公共读写锁类
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Tool
{
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2018-01-19
    /// 创建记录：公共读写锁类
    /// </summary>
    public class CommonReaderWriterLock
    {
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-19
        /// 创建记录：读写锁
        /// </summary>
        private readonly ReaderWriterLock UserReaderWriterLock = new ReaderWriterLock();

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-19
        /// 创建记录：获取读取锁
        /// </summary>
        /// <param name="readLockFunc"></param>
        public void ReadLock(Action readLockFunc)
        {
            if (readLockFunc != null)
            {
                try
                {
                    UserReaderWriterLock.AcquireReaderLock(10000);
                    readLockFunc();
                    UserReaderWriterLock.ReleaseReaderLock();
                }
                catch (Exception e)
                {
                    UserReaderWriterLock.ReleaseReaderLock();
                    LogHelper.WriteLog(e);
                }
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-01-19
        /// 创建记录：获取写入锁
        /// </summary>
        /// <param name="readLockFunc"></param>
        public void WriteLock(Action readLockFunc)
        {
            if (readLockFunc != null)
            {
                try
                {
                    UserReaderWriterLock.AcquireWriterLock(10000);
                    readLockFunc();
                    UserReaderWriterLock.ReleaseWriterLock();
                }
                catch (Exception e)
                {
                    UserReaderWriterLock.ReleaseWriterLock();
                    LogHelper.WriteLog(e);
                }
            }
        }
    }
}