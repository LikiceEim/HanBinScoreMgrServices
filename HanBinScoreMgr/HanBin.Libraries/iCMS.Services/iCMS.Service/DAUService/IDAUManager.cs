using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.DAUAgent;
using iCMS.Common.Component.Data.Request.DAUService;
using iCMS.Common.Component.Data.Response.DAUService;
using iCMS.Common.Component.Data.Response.DAUAgent;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Service.Web.DAUService
{
    public interface IDAUManager
    {
        /// <summary>
        /// 设置采集量定义
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> SetDAUMDF(SetDAUMDFParameter param);

        /// <summary>
        /// 启动采集
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> StartCollection(StartCollectionParameter param);

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> StopCollection(StopCollectionParameter param);

        /// <summary>
        /// 获取单个采集单元的测量定义
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<GetDAUMDFResult> GetDAUMDF(GetDAUMDFParameter param);

        /// <summary>
        /// 上传波形数据
        /// </summary>
        /// <param name="param"></param>
        void UploadWaveData(UploadWaveDataParameter param);

        /// <summary>
        /// 上传转速数据
        /// </summary>
        /// <param name="param"></param>
        void UploadSpeedData(UploadSpeedDataParameter param);

        /// <summary>
        /// 上传温度数据
        /// </summary>
        /// <param name="param"></param>
        void UploadTemperatureData(UploadTemperatureDataParameter param);

        /// <summary>
        /// 上传采集单元信息
        /// </summary>
        /// <param name="param"></param>
        void UploadDAUInfo(UploadDAUInfoParameter param);

        /// <summary>
        /// 上传采集单元当前状态
        /// </summary>
        /// <param name="param"></param>
        void UploadCurrentDAUStates(UploadCurrentDAUStatesParameter param);

        /// <summary>
        /// 验证DAUAgent是否可用
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<ResponseResult> IsDAUAgentAccess(IsDAUAgentAccessParameter param);

        BaseResponse<GetDAUInfoListByDAUIDResult> GetDAUInfoListByDAUID(GetDAUInfoListByDAUIDParameter param);

        BaseResponse<GetAllWSInfoByDAUIDResult> GetAllWSInfoByDAUID(GetAllWSInfoByDAUIDParameter param);

        BaseResponse<GetAvailableTemperatureWSByDAUIDResult> GetAvailableTemperatureWSByDAUID(GetAvailableTemperatureWSByDAUIDParameter param);

        BaseResponse<GetUsableChannelByDAUIDResult> GetUsableChannelByDAUID(GetUsableChannelByDAUIDParameter param);
    }
}