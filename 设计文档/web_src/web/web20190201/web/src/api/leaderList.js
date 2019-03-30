/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:43:40
 */
import request from '@/utils/request'

/**
 * 获取单位分类
 */
export function queryUnitType(){
    return request({
        //url: 'HanBinSystemService.svc/GetOrganSummary',
         url: 'HanBin/SystemService/GetOrganSummary',
        method: 'post'
    })
}

/**
 * 获取干部职位
 */
export function queryCarreLevel() {
    return request({
        //url: 'HanBinSystemService.svc/GetPositionSummary',
         url: 'HanBin/SystemService/GetPositionSummary',
        method: 'post'
    })
}

/**
 * 获取干部列表信息
 */
export function quertLeaderList(data) {
    return request({
        //url: 'HanBinSystemService.svc/GetOfficerList',
         url: 'HanBin/SystemService/GetOfficerList',
        method: 'post',
        data
    })
}

/**
 * 获取级别接口
 */
export function queryLevelList() {
    return request({
        //url: 'HanBinSystemService.svc/GetLevelSummary',
         url: 'HanBin/SystemService/GetLevelSummary',
        method: 'post'
    })
}

/**
 * 新增干部
 */
export function addLeaderInfo(data) {
    return request({
        //url: 'HanBinSystemService.svc/AddOfficerRecord',
         url: 'HanBin/SystemService/AddOfficerRecord',    
        method: 'post',
        data
    })
}

/**
 * 编辑干部
 */
export function editLeaderInfo(data){
    return request({
        //url: 'HanBinSystemService.svc/EditOfficerRecord',
        url: 'HanBin/SystemService/EditOfficerRecord', 
        method: 'post',
        data
    })
}

/**
 * 获取干部详情信息
 */
export function getLeaderInfoDetiel(data){
    return request({
        //url: 'HanBinSystemService.svc/GetOfficerDetailInfo',
         url: 'HanBin/SystemService/GetOfficerDetailInfo',
        method: 'post',
        data
    })
}

/**
 * 获取干部详情积分信息
 */
export function getLeaderInfoScore(data){
    return request({
        //url: 'HanBinSystemService.svc/GetOfficerScoreDetailInfo',
         url: 'HanBin/SystemService/GetOfficerScoreDetailInfo',
        method:'post',
        data
    })
}

/**
 * 删除干部
 */
export function deleteLeaderInfo(data){
    return request({
        //url: 'HanBinSystemService.svc/DeleteOfficerRecord',
         url: 'HanBin/SystemService/DeleteOfficerRecord',
        method: 'post',
        data
    })
}

/**
 * 退休干部
 */
export function retireLeaderInfo(data){
    return request({
        //url: 'HanBinSystemService.svc/SetOfficerOffService',
         url: 'HanBin/SystemService/SetOfficerOffService',
        method: 'post',
        data
    })
}

/**
 * 撤销积分
 */
export function undoInitInfo(data){
    return request({
        //url: 'HanBinSystemService.svc/CancelScoreApply',
         url: 'HanBin/SystemService/CancelScoreApply',
        method: 'post',
        data
    })
}

/**
 * 删除上传文件
 */
export function deleteFile(data){
    return request({
        //url: 'HanBinScoreService.svc/DeleteFile',
         url: 'HanBin/ScoreService/DeleteFile',
        method: 'post',
        data
    })
}

/**
 * 二级管理员获取本单位员工
 */
export function secondLevelGetLeaderList(data){
    return request({
        //url: 'HanBinScoreService.svc/DeleteFile',
         url: 'HanBin/SystemService/GetAllOfficerListPerSecondAdmin',
        method: 'post',
        data
    })
}