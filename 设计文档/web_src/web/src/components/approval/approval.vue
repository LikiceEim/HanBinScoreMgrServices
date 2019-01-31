/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-22 23:07:49
 */
<template>
  <div>
    <div class="craeteTop">
      通知公示
    </div>
    <div>
      <Tabs :active-key="keyActive" :value="keyActive">
        <Tab-pane v-if="isToDoListShow" label="待我审批" key="key1" name="key1">
          <template v-for="(item, index) in ToDoList">
            <div :key="index" style="font-size:14px;">
              <div style="margin-left:20px;line-height:30px;font-size:15px;color:#000;">
                <span style="display:inline-block;width:8px;height:8px;border-radius:4px;background-color:#00c4f0;"></span>
                <!-- <span>政法委管理员提交了一个申请</span> -->
                <span>{{item.ProposeName}}<span>提交了一个申请</span></span>
              </div>
              <div style="margin-left:35px;line-height:30px;">
                <!-- <span>说明：关于安康招商局张某同志的积分变更申请，材料已经上床</span> -->
                <span v-if="item.UploadFileList.length>0"><span>说明：关于</span>{{item.OrganFullName}}{{item.OfficerName}}<span>的积分变更申请，材料已经上传</span></span>
                <span v-else-if="item.UploadFileList.length==0"><span>说明：关于</span>{{item.OrganFullName}}{{item.OfficerName}}<span>的积分变更申请，材料未上传</span></span>
                <span style="float:right;margin-right:30px;">{{item.AddDate}}</span>
              </div>
              <table>
                <tr style="background-color:#cccccc;color:#fff;">
                  <td style="width:10%;">姓名</td>
                  <td style="width:25%;">身份证号</td>
                  <td style="width:20%;">单位名称</td>
                  <td style="width:10%;">职位</td>
                  <td style="width:20%;">变更事项</td>
                  <td style="width:15%;">操作</td>
                </tr>
                <tr>
                  <td>{{item.OfficerName}}</td>
                  <td>{{item.IdentifyCardNumber}}</td>
                  <td>{{item.OrganFullName}}</td>
                  <td>{{item.PositionName}}</td>
                  <td><span>{{item.ItemDescription}}</span><span>{{item.ItemScore}}</span></td>
                  <td>
                    <i-button :approvalid="item.ApplyID" type="primary" @click="handleDoApproval">审批</i-button>
                    <i-button v-if="item.UploadFileList.length>=1" type="primary" :UploadFileList="item.jsonList" :approvalid="item.ApplyID" @click="handleDownloadFile">下载</i-button>
                    <!-- <i-button v-else type="primary" :approvalid="item.ApplyID" @click="handleDownloadFile">下载</i-button> -->
                  </td>
                </tr>
              </table>
            </div>
          </template>
          <div class="unltlistfyq">
            <Page :total="ToDototal" :current="ToDocurrent" @on-change="handleChangeToDoPage" :page-size="ToDopageSize" show-elevator />
          </div>
        </Tab-pane>
        <Tab-pane v-if="isScoreChangeShow" label="上级反馈" key="key2" name="key2">
          <template v-for="(item, index) in SuperiorFeedbackList">
            <div :key="index" style="font-size:14px;">
              <div style="margin-left:20px;line-height:30px;font-size:15px;color:#000;">
                <span style="display:inline-block;width:8px;height:8px;border-radius:4px;background-color:#00c4f0;"></span>
                <span v-if="item.ApproveStatus==1"><span>{{item.ProcessuUserName}}</span>同意了您的申请</span>
                <span v-else-if="item.ApproveStatus==2"><span>{{item.ProcessuUserName}}</span>驳回了您的申请</span>
              </div>
              <div style="margin-left:35px;line-height:30px;">
                <!-- <span>说明：关于安康招商局张某同志的积分变更申请，材料已经上床</span> -->
                <span>{{item.RejectReason}}</span>
                <!-- <span style="float:right;margin-right:30px;">{{item.AddDate}}</span> -->
                <span style="float:right;margin-right:30px;">{{item.LastUpdateDate}}</span>
              </div>
              <table>
                <tr style="background-color:#cccccc;color:#fff;">
                  <td style="width:10%;">姓名</td>
                  <td style="width:25%;">身份证号</td>
                  <td style="width:20%;">单位名称</td>
                  <td style="width:10%;">职位</td>
                  <td style="width:20%;">变更事项</td>
                  <td style="width:15%;">操作</td>
                </tr>
                <tr>
                  <td>{{item.OfficerName}}</td>
                  <td>{{item.IdentifyCardNumber}}</td>
                  <td>{{item.OrganFullName}}</td>
                  <td>{{item.PositionName}}</td>
                  <td><span>{{item.ItemDescription}}</span><span>{{item.ItemScore}}</span></td>
                  <td>
                    <span v-if="item.ApproveStatus==1">已同意</span>
                    <span v-else-if="item.ApproveStatus==2" style="color:red;">已驳回</span>
                    <i-button v-if="item.UploadFileList.length>=1" type="primary" :UploadFileList="item.jsonList" :approvalid="item.ApplyID" @click="handleDownloadFile">下载</i-button>
                  </td>
                </tr>
              </table>
            </div>
          </template>
          <div class="unltlistfyq">
            <Page :total="SuperiorFeedbacktotal" :current="SuperiorFeedbackcurrent" @on-change="handleChangeSuperiorFeedbackPage" :page-size="SuperiorFeedbackpageSize" show-elevator />
          </div>
        </Tab-pane>
        <Tab-pane label="变更公示" key="key3" name="key3">
            <template v-for="(item, index) in PublicChangeList">
              <span :key="index" style="display:inline-block;width:100%;font-size:14px;">
                <span style="width:100%;display:inline-block;padding-left:10px;display:flex;align-items: center;line-height:40px;border-bottom:1px #ccc solid;">
                  <span v-if="item.ItemScore<0" style="color:#ccc;width:10%;">{{item.ItemScore}}</span>
                  <span v-else-if="item.ItemScore>=0" style="color:#00b576;width:10%;">+{{item.ItemScore}}</span>
                  <span style="width:80%;" :title="item.Content" class="item-content-Content">{{item.Content}}</span>
                  <span style="color:#ccc;width:10%;min-width:170px;">{{item.AddDate}}</span>
                </span>
              </span>
            </template>
            <div class="unltlistfyq">
              <Page :total="PublicChangetotal" :current="PublicChangecurrent" @on-change="handlePublicChangePage" :page-size="PublicChangepageSize" show-elevator />
            </div>
        </Tab-pane>
        <Tab-pane label="积分公示" key="key4" name="key4">
          <i-table :columns="PublicScore" :data="PublicScoreData"></i-table>
          <div class="unltlistfyq">
            <Page :total="total" :current="page" @on-change="handleChangePage" :page-size="pageSize" show-elevator />
          </div>
        </Tab-pane>
      </Tabs>
      <Modal
        v-model="ApprovalDialog"
        title="审核">
        <span>审批意见</span>
        <i-input type="textarea" :rows="4" v-model="ApprovalInput" placeholder="请输入..."></i-input>
        <div slot="footer">
          <i-button @click="doCheckApprovalAction(2)">驳回</i-button>
          <i-button @click="doCheckApprovalAction(1)" type="primary">同意</i-button>
        </div>
      </Modal>
    </div>
  </div>
</template>

<script>
import Cookies from 'js-cookie';
import {
  queryToDoListDetail, 
  querySuperiorFeedbackDetail, 
  doCheckScoreApply,
  downLoadfile
  } from '@/api/approval'
import {
  queryScoreChangeHistory, 
  queryScorePublicShow
} from '@/api/homePages'

import service from '@/utils/request'
import { setTimeout } from 'timers';
export default {
  name: 'approval',
  data(){
    return{
      isToDoListShow:false,
      isScoreChangeShow:false,
      // 待我审批分页
      ToDototal: null,
      ToDocurrent:1,
      ToDopageSize:15,
      // 上级审批分页
      SuperiorFeedbacktotal: null,
      SuperiorFeedbackcurrent:1,
      SuperiorFeedbackpageSize:15,
      // 变更公示
      PublicChangetotal: null,
      PublicChangecurrent: 1,
      PublicChangepageSize:15,
      // 选中面板
      keyActive:'key1',
      // 页码
      page: 1,
      // 页数
      pageSize: 15,
      total:null,
      // 待我审批数组
      ToDoList: [],
      // 上级反馈数组
      SuperiorFeedbackList: [],
      // 变更公示数组
      PublicChangeList: [],
      // 积分公示数组
      ScoreShowList: [],
      // 审核弹出框
      ApprovalDialog: false,
      // 审批意见
      ApprovalInput: null,
      // 审批弹出框ID
      ApprovalID: null,
      // 默认的条数
      RankNumber: 10,
      // 积分公示列
      PublicScore:[
        {
          title: '积分排名',
          key: 'CurrentScore'
        },
        {
          title: '排名',
          key: 'Rank'
        },
        {
          title: '姓名',
          key: 'Name'
        },
        {
          title: '性别',
          key: 'Gender',
          render: (h,params)=>{
            return h('span',
              this.formatSex(params.row.Gender)
            )
          }
        },
        {
          title: '出生日期',
          key: 'Birthday'
        },
        {
          title: '所在单位',
          key: 'OrganFullName'
        },
        {
          title: '现任职务',
          key: 'PositionName'
        },
        {
          title: '级别',
          key: 'LevelName'
        },
        {
          title: '任职时间',
          key: 'OnOfficeDate'
        }
      ],
      // 积分公示数据
      PublicScoreData: [],
    }
  },
  created(){
    var roleID = Cookies.get('RoleID');
    if(roleID == 1){
      this.isScoreChangeShow = true;
      this.isToDoListShow = true;
    }else if(roleID == 3){
      this.isScoreChangeShow = false;
      this.isToDoListShow = true;
    }else if(roleID == 4){
      this.isScoreChangeShow = true;
      this.isToDoListShow = false;
    }
    debugger;
    // 判断是哪个面板
    var type = this.$route.query.type;
    if(type == 1) {
      this.keyActive = 'key1';
    }else if(type == 2) {
      this.keyActive = 'key2';
    }else if(type == 3) {
      this.keyActive = 'key3';
    }else if(type == 4) {
      this.keyActive = 'key4';
    }
    // 获取待我审批列表
    this.getToDoListDetail();
    // 获取上级反馈列表
    this.getSuperiorFeedbackDetail();
    // 获取变更公示
    this.getScoreChangeHistory();
    // 获取积分公示
    this.getScorePublicShow();
  },
  methods:{
    // 格式化性别
    formatSex(data){
      if(data == '1'){
        return '男';
      }else if(data == '2'){
        return '女';
      }
    },
    // 翻页
    handleChangePage(val){
      debugger;
      this.page = val;
      this.getScorePublicShow();
    },
    // 获取积分公示
    getScorePublicShow(){
      debugger;
      var CurrentUserID = Cookies.get('UserID');
      var _data = {
        Page: this.page,
        PageSize: this.pageSize,
        CurrentUserID:CurrentUserID
      }
      queryScorePublicShow(_data).then(res => {
        debugger;
        if(res.IsSuccessful == true) {
          if(res.Result == null){
            this.total = 0;
            this.PublicScoreData = 0;
            return false;
          }
          this.total = res.Result.Total;
          var row = res.Result.OfficerScoreShowList;
          for(let i = 0; i < row.length; i++){
            var time = row[i].Birthday.split(' ')[0];
            row[i].Birthday = time;
            var time1 = row[i].OnOfficeDate.split(' ')[0];
            row[i].OnOfficeDate = time1;
          }
          this.PublicScoreData = row;
        }else{
          this.$Message.error(res.Reason);
        }
      })
    },
    // 获取变更公示
    getScoreChangeHistory(){
      debugger;
      var CurrentUserID = Cookies.get('UserID');
      var _data = {
        RankNumber: this.RankNumber,
        Page: this.PublicChangecurrent,
        PageSize:this.PublicChangepageSize,
        CurrentUserID:CurrentUserID
      }
      queryScoreChangeHistory(_data).then(res => {
        debugger;
        if(res.IsSuccessful == true){
          if(res.Result == null){
            this.PublicChangetotal = 0;
            this.PublicChangeList = [];
            return false;
          }
          this.PublicChangetotal = res.Result.Total;
          var row = res.Result.ScoreChangeHisList;
          var list = [];
          for(let i =  0; i < row.length; i++) {
            var tempObj = {};
            tempObj.ItemScore = row[i].ItemScore;
            tempObj.Content = row[i].Content;
            // var date = row[i].AddDate.split(' ')[0];
            // tempObj.AddDate = date;
            tempObj.AddDate = row[i].AddDate;
            list.push(tempObj);
          }
          this.PublicChangeList = list;
        }else{
          this.$Message.error(res.Reason);
        }
      })
    },
    handlePublicChangePage(val){
      debugger;
      this.PublicChangecurrent = val;
      this.getScoreChangeHistory();
    },
    // 获取上级反馈列表
    getSuperiorFeedbackDetail(){
      debugger;
      var CurrentUserID = Cookies.get('UserID');
      var _data = {
        Page:this.SuperiorFeedbackcurrent,
        PageSize:this.SuperiorFeedbackpageSize,
        CurrentUserID:CurrentUserID
      }
      querySuperiorFeedbackDetail(_data).then(res => {
        debugger;
        if(res.IsSuccessful == true) {
          if(res.Result == null){
            this.SuperiorFeedbackList = [];
            this.SuperiorFeedbacktotal = 0;
            return false;
          }
          this.SuperiorFeedbacktotal = res.Result.Total;
          var row = res.Result.ApplovedApplyDetailList;
          for(let i = 0; i < row.length; i++) {
            var jsonList = JSON.stringify(row[i].UploadFileList);
            row[i].jsonList = jsonList;
          }
          this.SuperiorFeedbackList = row;
        }else{
          this.$Message.error(res.Reason);
        }
      })
    },
    handleChangeSuperiorFeedbackPage(val){
      debugger;
      this.SuperiorFeedbackcurrent = val;
      this.getSuperiorFeedbackDetail();
    },
    // 审批操作
    handleDoApproval(evt){
      debugger;
      // 设置参数
      var id = evt.currentTarget.getAttribute('approvalid');
      this.ApprovalID = id;
      this.ApprovalInput = null;
      this.ApprovalDialog = true;
    },
    // 执行审批动作
    doCheckApprovalAction(type){
      debugger;
      var ApplyID = this.ApprovalID;
      var ApplyStatus = type;
      var RejectReason = this.ApprovalInput;
      var ProcessUserID = Cookies.get('UserID');
      // 判断是否驳回
      if(type == 2){
        if(RejectReason == null){
          this.$Message.error('请填写驳回意见！')
          return false;
        }
      }
      var _data = {
        ApplyID,
        ApplyStatus,
        RejectReason,
        ProcessUserID
      }
      doCheckScoreApply(_data).then(res => {
        debugger;
        if(res.IsSuccessful == true){
          this.$Message.success('审批成功！');
          // 刷新页面
          this.ApprovalDialog = false;
          this.getToDoListDetail();
          // 获取上级反馈列表
          this.getSuperiorFeedbackDetail();
          this.getScoreChangeHistory();
          this.getScorePublicShow();
        }else{
          // this.$Message.error('审批失败！');
          this.$Message.error(res.Reason);
        }
      })
    },
    // 下载操作
    handleDownloadFile(evt){
      debugger;
      var jsonlist = evt.currentTarget.getAttribute('UploadFileList');
      var list = JSON.parse(jsonlist);
      for(let i = 0; i < list.length; i++) {
        var name = list[i];
        // let link = document.createElement('a');
        // var e = document.createEvent("MouseEvents"); //创建鼠标事件对象
        // e.initEvent("click", false, false); //初始化事件对象
        // link.style.display = 'none';
        // link.href = 'http://192.168.0.105:2892/HanBin/DownloadService/downloadFile/'+name;
        var url = service;
        var baseUrl = url.defaults.baseURL;
        // link.href = 'http://111.231.200.224:8842/DownloadService.svc/downloadFile/'+name;
        // link.href = baseUrl + 'DownloadService.svc/downloadFile/'+name;
        // link.setAttribute('download', name);
        // link.download = name; //设置下载文件名
        // document.body.appendChild(link);
        // link.dispatchEvent(e); //给指定的元素，执行事件click事件
        // setTimeout(()=>{
        //   link.click();
        // },1000)

        // var url = baseUrl + 'DownloadService/downloadFile/'+name;
        var url = baseUrl + 'DownloadService.svc/downloadFile/'+name;
        const iframe = document.createElement("iframe");
        iframe.style.display = "none"; // 防止影响页面
        iframe.style.height = 0; // 防止影响页面
        iframe.src = url; 
        document.body.appendChild(iframe); // 这一行必须，iframe挂在到dom树上才会发请求
        // 5分钟之后删除（onload方法对于下载链接不起作用，就先抠脚一下吧）
        setTimeout(()=>{
          iframe.remove();
        }, 5 * 60 * 1000);
        
        // 调用接口
        // downLoadfile(name).then(res => {
        //   debugger;
        //   var type = res.headers['content-type'];
        //   const blob = new Blob([res], { type: type });
        //   const url = window.URL.createObjectURL(
        //     blob
        //   );
        //   // const link = document.createElement('a');
        //   // link.style.display = 'none';
        //   // link.href = url || '###';
        //   // link.setAttribute('target', '_blank');
        //   // document.body.appendChild(link);
        //   // link.click();
        //   // const url = window.URL.createObjectURL(
        //   //   new Blob([res.data])
        //   // );
        //   const link = document.createElement('a');
        //   link.style.display = 'none';
        //   link.href = url || '###';
        //   link.setAttribute('download', name);
        //   document.body.appendChild(link);
        //   link.click();
        // })
      }
    },
    // 获取待我审批列表
    getToDoListDetail(){
      debugger;
      var _data = {
        Page:this.ToDocurrent,
        PageSize:this.ToDopageSize
      }
      queryToDoListDetail(_data).then(res => {
        debugger;
        if(res.IsSuccessful == true) {
          if(res.Result == null){
            this.ToDoList = [];
            this.ToDototal = 0;
            return false;
          }
          this.ToDototal = res.Result.ToDototal;
          var row = res.Result.ApplyDetailList;
          for(let i = 0; i < row.length; i++) {
            var jsonList = JSON.stringify(row[i].UploadFileList);
            row[i].jsonList = jsonList;
          }
          this.ToDoList = row;
        }else{
          this.$Message.error(res.Reason);
        }
      })
    },
    // 翻页
    handleChangeToDoPage(){
      debugger;
      this.ToDocurrent = val;
      this.getToDoListDetail();
    }
  }
}
</script>

<style scoped>
.craeteTop{
    width:100%;
    height:65px;
    font-size: 24px;
    font-weight: 600;
    background: #cdd0d4;
    line-height: 65px;
    padding-left: 60px;
    color: #323232;
  }
  .unltlistfyq{
    text-align: right;
    margin-right:15px;
    height:100px;
    line-height: 100px;
  }
  table{
    margin: 10px 30px;
    width: calc(100% - 60px);
  }
  table tr{
    height: 50px;
  }
  table td{
    border:1px rgba(228, 228, 228, 1) solid;
    text-align: center;
  }
</style>
