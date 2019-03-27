/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:41:28
 */
import request from '@/utils/request'

/**
 * 获取待我审批详细数据
 */
export function queryToDoListDetail(data){
  return request({
    //url: 'HanBinScoreService.svc/GetWhatsToDoDetailList',
     url: 'HanBin/ScoreService/GetWhatsToDoDetailList',
    method: 'post',
    data
  })
}

/**
 * 获取上级反馈详细数据
 */
export function querySuperiorFeedbackDetail(data){
  return request({
    //url: 'HanBinScoreService.svc/GetHighLevelFeedBackDetailList',
     url: 'HanBin/ScoreService/GetHighLevelFeedBackDetailList',
    method: 'post',
    data
  })
}

/**
 * 驳回或者同意审批
 */
export function doCheckScoreApply(data){
  return request({
    //url: 'HanBinScoreService.svc/CheckScoreApply',
     url: 'HanBin/ScoreService/CheckScoreApply',
    method: 'post',
    data
  })
}

/**
 * 下载文件
 */
export function downLoadfile(data){
  return request({
    //url: 'HanBinDownloadService.svc/downloadFile/'+data,
     url: 'HanBin/DownloadService/downloadFile/'+data,
    method:'get'
  })
}