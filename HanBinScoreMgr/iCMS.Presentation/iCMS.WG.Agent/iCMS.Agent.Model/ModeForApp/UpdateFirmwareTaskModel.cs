using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iCMS.WG.Agent.Model
{
    public class UpdateFirmwareTaskModel : TaskModelBase
    {
        /// <summary>
        /// 待升级WS的MAC地址结合
        /// </summary>
        public List<string> macList { set; get; }

        /// <summary>
        /// 升级文件的byte数组
        /// </summary>
        public byte[]  updateFile{ set; get; }

        public override string operatorName
        {
            get
            {
                return "UpdateFirmwareOper";
            }

        }
    }



}
