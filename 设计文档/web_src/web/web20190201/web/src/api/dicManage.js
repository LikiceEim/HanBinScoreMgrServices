/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:41:53
 */
import request from '@/utils/request'

/**
 * 获取字典积分列表
 */
export function queryScoreList(){
    return request({
        //url: 'HanBinScoreService.svc/GetScoreItemList',
        url: 'HanBin/ScoreService/GetScoreItemList',
        method: 'post'
    })
}

/**
 * 新增积分项目
 */
export function addScoreInfo(data) {
    return request({
        //url: 'HanBinScoreService.svc/AddScoreItem',
        url: 'HanBin/ScoreService/AddScoreItem',
        method: 'post',
        data
    })
}

/**
 * 编辑积分项目
 */
export function editScoreInfo(data){
    return request({
        //url: 'HanBinScoreService.svc/EditScoreItem',
        url: 'HanBin/ScoreService/EditScoreItem',
        method: 'post',
        data
    })
}

/**
 * 删除积分项目
 */
export function deleteScoreInfo(data){
    return request({
        //url: 'HanBinScoreService.svc/DeleteScoreItem',
        url: 'HanBin/ScoreService/DeleteScoreItem',
        method: 'post',
        data
    })
}