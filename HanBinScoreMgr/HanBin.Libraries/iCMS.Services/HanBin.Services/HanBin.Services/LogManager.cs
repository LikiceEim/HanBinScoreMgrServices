using HanBin.Core.DB.Models;
using HanBin.Frameworks.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services
{
    public class LogManager
    {
        private IRepository<HBUser> userRepoitory;
        private IRepository<Organization> organRepository;
        private IRepository<HBRole> roleRepository;
        private IRepository<SysLog> sysLogRepository;


        public LogManager()
        {
            this.userRepoitory = new Repository<HBUser>();
            this.organRepository = new Repository<Organization>();
            this.roleRepository = new Repository<HBRole>();
            this.sysLogRepository = new Repository<SysLog>();
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="operationUserID"></param>
        /// <param name="content"></param>
        public void AddOperationLog(int operationUserID, string content, string ip = null)
        {
            var currentUser = userRepoitory.GetDatas<HBUser>(t => t.UserID == operationUserID, true).FirstOrDefault();
            if (currentUser == null)
            {
                return;
            }

            var role = roleRepository.GetDatas<HBRole>(t => t.RoleID == currentUser.RoleID, true).FirstOrDefault();
            if (role == null)
            {
                return;
            }
            var organ = organRepository.GetDatas<Organization>(t => t.OrganID == currentUser.OrganizationID, true).FirstOrDefault();
            if (organ == null)
            {
                return;
            }

            var log = new SysLog();
            log.OperationUserID = currentUser.UserID;
            log.UserToken = currentUser.UserToken;
            log.RoleID = currentUser.RoleID;
            log.RoleName = role.RoleName;
            log.Content = content;
            log.OrganID = organ.OrganID;
            log.OrganName = organ.OrganFullName;
            log.IP = ip;
            log.HTTPType = "POST";
            log.OperationDate = DateTime.Now;

            var res = sysLogRepository.AddNew<SysLog>(log);
        }
    }
}
