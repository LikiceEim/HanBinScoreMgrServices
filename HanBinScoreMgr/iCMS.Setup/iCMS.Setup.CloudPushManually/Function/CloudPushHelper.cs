using iCMS.Cloud.Common;
using iCMS.Cloud.Interface;
using iCMS.Cloud.JiaXun.Commons;
using iCMS.Cloud.JiaXun.DataConvert;
using iCMS.Common.Component.Data.Enum;
using iCMS.Communication.PushManually;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Setup.CloudPushManually.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iCMS.Setup.CloudPushManually.Function
{
    public class CloudPushHelper
    {
        public static bool CloudSend(ICMSTreeNode treeNode)
        {
            CloudDataProvider dataProvider = new CloudDataProvider();
            string cloudData = string.Empty;

            switch (treeNode.DataType)
            {
                case EnumCloudOperationType.Enterprise:
                    cloudData = dataProvider.GetEnterprise(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Enterprises);
                    break;
                case EnumCloudOperationType.Workshop:
                    cloudData = dataProvider.GetWorkshop(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Workshops);
                    break;
                case EnumCloudOperationType.Device:
                    cloudData = dataProvider.GetDevice(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Devices);
                    break;
                case EnumCloudOperationType.Measuresite:
                    //推送测点时候同时需要推送测点相应的WS
                    cloudData = dataProvider.GetMeasureSite(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Measuresites);

                    ///推送MeasureSite时，同时推送相应的WS 
                    cloudData = string.Empty;
                    IRepository<MeasureSite> measureSiteRepositiory = new Repository<MeasureSite>();
                    MeasureSite msite = measureSiteRepositiory.GetByKey(treeNode.TableID);
                    if (msite != null && msite.WSID.HasValue)
                    {
                        cloudData = dataProvider.GetWS(msite.WSID.Value);
                        ExecSend(cloudData, CommonVariate.Cloud_URL_Sensors);
                    }

                    break;
                case EnumCloudOperationType.VibSignal:
                    cloudData = dataProvider.GetVibSignal(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_Signals);
                    break;
                case EnumCloudOperationType.BatteryVoltageAlarmSet:
                    cloudData = dataProvider.GetVolateAlmSet(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_AlarmThresholds);
                    break;
                case EnumCloudOperationType.DeviceTemperatureAlarmSet:
                    cloudData = dataProvider.GetDeviceTempeAlmSet(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_AlarmThresholds);
                    break;
                case EnumCloudOperationType.SignalAlarmSet:
                    cloudData = dataProvider.GetVibSignalAlmSet(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_AlarmThresholds);
                    break;
                case EnumCloudOperationType.WirelessGateway:
                    cloudData = dataProvider.GetWG(treeNode.TableID);
                    ExecSend(cloudData, CommonVariate.Cloud_URL_WirelessGateways);
                    break;
                default:
                    break;
            }

            //3.推送
            return true;
        }

        public static void ExecSend(string cloudData, string url)
        {
            CommunicationHelper.SendData(cloudData, url, CommonVariate.Cloud_URL_Method_Add);
        }
    }
}
