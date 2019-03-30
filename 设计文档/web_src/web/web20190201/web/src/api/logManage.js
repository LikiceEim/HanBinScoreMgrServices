/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:43:55
 */
import request from '@/utils/request'

/**
 * 查询日志
 */
export function queryLog(data){
  return request({
    //url: 'HanBinSystemService.svc/QueryLog',
     url: 'HanBin/SystemService/QueryLog',
    method: 'post',
    data:data
  })
}