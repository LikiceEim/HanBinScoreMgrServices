/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:41:38
 */
import request from '@/utils/request'

/**
 * 获取备份列表
 */
export function queryBackupList(data){
return request({
    //url: 'HanBinSystemService.svc/GetBackupLogList',
     url: 'HanBin/SystemService/GetBackupLogList',
    method: 'post',
    data
})
}

/**
 * 执行备份操作
 */
export function doBackup(){
    return request({
      //url: 'HanBinSystemService.svc/BackupDB',
        url: 'HanBin/SystemService/BackupDB',
        method: 'post'
    })
}

/**
 * 删除操作
 */
export function deleteBackUp(data){
    return request({
        //url: 'HanBinSystemService.svc/DeleteBackup',
         url: 'HanBin/SystemService/DeleteBackup',
        method: 'post',
        data
    })
}