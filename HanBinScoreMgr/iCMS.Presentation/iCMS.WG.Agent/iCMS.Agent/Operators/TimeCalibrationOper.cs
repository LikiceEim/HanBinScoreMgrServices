/* ==============================================================================
* 功能描述：配置参数时间校对
* 创 建 者：LF
* 创建日期：2016年2月19日13:28:53
* ==============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Model;
using iCMS.WG.Agent.Common;
using iCMS.WG.Agent.Model.Send;

namespace iCMS.WG.Agent.Operators
{
    public class TimeCalibrationOper : IOperator
    {
        public Model.TimeCalibrationTaskModel timeModel { get; set; }
        public Model.TaskModelBase taskModel
        {
            get
            {
                return timeModel;
            }
            set
            {
                timeModel = (Model.TimeCalibrationTaskModel)value;
            }
        }


        public bool checkCmd()
        {
            throw new NotImplementedException();
        }

        public object doOperator()
        {
            try
            {
                iMesh.tCaliTimeParam caliTimeParam = new iMesh.tCaliTimeParam();
                TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
                caliTimeParam.u64Seconds = Convert.ToUInt64(ts.TotalMilliseconds);
                caliTimeParam.u32Microseconds = 0;
                //向底层发送信息
                if (!iCMS.WG.Agent.ComFunction.Send2WS(caliTimeParam, Common.Enum.EnumRequestWSType.CalibrateTime))
                {

                }
            }
            catch (Exception ex)
            {
                iCMS.WG.Agent.Common.LogHelper.WriteLog(ex);
            }
            return "TimeCalibrationOper";
        }
    }
}
