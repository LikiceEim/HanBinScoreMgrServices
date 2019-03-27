/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:44:19
 */
import request from '@/utils/request'

/**
 * 新增单位
 */
export function addUnit(data){
  return request({
    //url: 'HanBinSystemService.svc/AddOrganizationRecord',
     url: 'HanBin/SystemService/AddOrganizationRecord',
    method: 'post',
    data:data
  })
}

/**
 * 获取单位类型信息
 */
export function queryUnitType() {
  return request({
    //url: 'HanBinSystemService.svc/GetOrganTypeList',
     url: 'HanBin/SystemService/GetOrganTypeList',
    method: 'post'
  })
}

export function queryUnitData(data) {
  return request({
    //url: 'HanBinSystemService.svc/GetOrganList',
     url: 'HanBin/SystemService/GetOrganList',
    method: 'post',
    data
  })
}

/**
 * 获取地区列表
 */
export function GetAreaList(){
  return request({
    //url: 'HanBinSystemService.svc/GetAreaList',
     url: 'HanBin/SystemService/GetAreaList',
    method: 'post'
  })
}

/**
 * 更新操作
 */
export function updateUnitInfo(data){
  return request({
    //url: 'HanBinSystemService.svc/EditOrganizationRecord',
     url:'HanBin/SystemService/EditOrganizationRecord',
    method: 'post',
    data
  })
}

/**
 * 删除操作
 */
export function deleteUnitInfo(data){
  return request({
    //url: 'HanBinSystemService.svc/DeleteOrganRecord',
     url:'HanBin/SystemService/DeleteOrganRecord',
    method: 'post',
    data
  })
}

/**
 * 获取单位详情
 */
export function quertDetailUnitInfo(data){
  return request({
    //url: 'HanBinSystemService.svc/GetOrganDetailInfo',
     url: 'HanBin/SystemService/GetOrganDetailInfo',
    method: 'post',
    data
  })
}