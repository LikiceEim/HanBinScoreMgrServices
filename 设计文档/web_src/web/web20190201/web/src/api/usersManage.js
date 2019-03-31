/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:45:16
 */
import request from '@/utils/request'

/**
 * 查询用户列表接口
 */
export function queryUsersList(query) {
  debugger;
  return request({
    url: 'HanBinSystemService.svc/GetUserInfo',
    // url: 'HanBin/SystemService/GetUserInfo',
    method: 'post',
    data: query
  })
}

/**
 * 添加用户接口
 * 
 */
export function addUserList(data) {
  return request({
    url: 'HanBinSystemService.svc/AddUser',
     //url: 'HanBin/SystemService/AddUser',
    method: 'post',
    data: data
  })
}

/**
 * 编辑
 */
export function editUserInfo(data) {
  return request({
    url: 'HanBinSystemService.svc/EditUser',
    // url: 'HanBin/SystemService/EditUser',
    method: 'post',
    data
  })
}

/**
 * 删除用户
 */
export function deleteUserInfo(id) {
  return request({
    url: 'HanBinSystemService.svc/DeleteUser',
    // url: 'HanBin/SystemService/DeleteUser',
    method: 'post',
    data: id
  })
}

/**
 * 启用、禁用
 */
export function forbiddenUser(data) {
  return request({
    url: 'HanBinSystemService.svc/ChangeUseStatus',
    // url: 'HanBin/SystemService/ChangeUseStatus',
    method:'post',
    data
  })
}

/**
 * 获取单位名称列表
 */
export function queryUnitName() {
  return request({
    url: 'HanBinSystemService.svc/GetOrganSummary',
     //url: 'HanBin/SystemService/GetOrganSummary',
    method: 'post'
  })
}

/**
 * 获取角色
 */
export function queryRoleList(){
  return request({
    url: 'HanBinSystemService.svc/GetRoleInfoList',
    // url: 'HanBin/SystemService/GetRoleInfoList',
    method: 'post'
  })
}

/**
 * 重置密码
 */
export function resetPWD(data){
  return request({
    url: 'HanBinSystemService.svc/ResetPWD',
     //url: 'HanBin/SystemService/ResetPWD',
    method: 'post',
    data
  })
}

/**
 * 编辑密码
 */
export function UpdatePWD(data){
  return request({
    url: 'HanBinSystemService.svc/UpdatePWD',
    //url: 'HanBin/SystemService/UpdatePWD',
    method: 'post',
    data
  })
}