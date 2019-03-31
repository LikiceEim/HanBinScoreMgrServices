/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:42:52
 */
import request from '@/utils/request'

export function fetchList(query) {
  return request({
    url: '/article/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取首页头部统计
 */
export function queryAllPageList(){
  return request({
    url: 'HanBinScoreService.svc/SystemStatSummary',
     //url: 'HanBin/ScoreService/SystemStatSummary',
    method: 'post'
  })
}

/**
 * 获取首页红榜数据
 */
export function queryRedBoardData(data){
  return request({
    url: 'HanBinScoreService.svc/GetHonourBoard',
     //url: 'HanBin/ScoreService/GetHonourBoard',
    method: 'post',
    data
  })
}

/**
 * 获取首页黑榜数据
 */
export function queryBlackBoardData(data){
  return request({
    url: 'HanBinScoreService.svc/GetBlackBoard',
    //url: 'HanBin/ScoreService/GetBlackBoard',
    method: 'post',
    data
  })
}

/**
 * 获取待办事项列表
 */
export function queryToDoList(data){
  return request({
    url: 'HanBinScoreService.svc/GetWhatsToDoSummary',
    // url: 'HanBin/ScoreService/GetWhatsToDoSummary',
    method: 'post',
    data
  })
}

/**
 * 获取上级反馈列表
 */
export function querySuperiorFeedbackList(data) {
  return request({
    url: 'HanBinScoreService.svc/GetHighLevelFeedBackSummary',
    // url: 'HanBin/ScoreService/GetHighLevelFeedBackSummary',
    method: 'post',
    data
  })
}

/**
 * 获取变更公示列表
 */
export function queryScoreChangeHistory(data) {
  return request({
    url: 'HanBinScoreService.svc/GetScoreChangeHistory',
    //url: 'HanBin/ScoreService/GetScoreChangeHistory',
    method: 'post',
    data
  })
}

/**
 * 获取积分公示列表
 */
export function queryScorePublicShow(data){
  return request({
    url: 'HanBinScoreService.svc/ScorePublicShow',
    //url: 'HanBin/ScoreService/ScorePublicShow',
    method: 'post',
    data
  })
}