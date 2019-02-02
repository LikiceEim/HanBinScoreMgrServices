/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:43:01
 */
import request from '@/utils/request'

/**
 * 上传文件
 */
export function uploadFile(data,name){
    console.log(data);
    debugger;
    return request({
        url: 'HanBinFileService.svc/UploadFile/' + name,
        // url: 'HanBin/FileService/UploadFile/' + name,
        method: 'post',
        headers: {
            post: {
            'Content-Type': 'multipart/form-data'
            }
        },
        data
    })
}

/**
 * 提交积分
 */
export function submitApply(data){
    return request({
        url: 'HanBinScoreService.svc/AddScoreApply',
        // url: 'HanBin/ScoreService/AddScoreApply',
        method: 'post',
        data
    })
}