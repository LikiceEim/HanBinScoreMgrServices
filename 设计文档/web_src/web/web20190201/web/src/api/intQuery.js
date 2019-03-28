/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:43:08
 */
import request from '@/utils/request'

/**
 * 查询积分列表
 */
export function queryIntScoreList(data){
    return request({
        //url: 'HanBinScoreService.svc/QuerySocre',
        url: 'HanBin/ScoreService/QuerySocre',
        method: 'post',
        data
    })
}