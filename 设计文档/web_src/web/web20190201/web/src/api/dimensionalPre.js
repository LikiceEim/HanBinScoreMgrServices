/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:42:02
 */
import request from '@/utils/request'

/**
 * 获取区域平均分
 */
export function queryAreaAverageScore(){
    return request({
        //url: 'HanBinScoreService.svc/AreaAverageScore',
        url: 'HanBin/ScoreService/AreaAverageScore',
        method: 'post'
    })
}

/**
 * 获取年龄平均分
 */
export function queryAgeAverageScore(data){
    return request({
        //url: 'HanBinScoreService.svc/AgeAverageScore',
        url: 'HanBin/ScoreService/AgeAverageScore',
        method: 'post',
        data
    })
}

/**
 * 获取区域平均分
 */
export function queryOrganAverageScore(data) {
    return request({
        //url: 'HanBinScoreService.svc/OrganAverageScore',
        url: 'HanBin/ScoreService/OrganAverageScore',
        method: 'post',
        data
    })
}

export function queryOrganCategoryAverageScore(data){
    return request({
        //url: 'HanBinScoreService.svc/OrganCategoryAverageScore',
        url: 'HanBin/ScoreService/OrganCategoryAverageScore',
        method: 'post',
        data
    })
}