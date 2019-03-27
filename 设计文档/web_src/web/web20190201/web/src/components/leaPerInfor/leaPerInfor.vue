<template>
  <div>
    <div class="craeteTop">
      干部个人信息
    </div>
    <div class="leacen">
      <!--左边部分-->
      <div class="leaLef">
        <!--左上部分-->
        <div class="leaLefTop">
          <div class="leaName">
            <h3>{{name}}</h3>
            <Icon class="letIos1" type="md-person" />
          </div>
          <div class="leaIos" @click="iconBtn">
            <Tooltip content="编辑干部基本信息">
              <Icon type="md-create" />
            </Tooltip>  
          </div>
        </div>
        <!--左下部分-->
        <div class="letLefBott">
          <div class="letLi">
            <div class="letLiBox">
              <h3>姓  名</h3>
              <span>{{name}}</span>
            </div>
            <div class="letLiBox">
              <h3>性   别</h3>
              <span v-html="formatterSex(sex)"></span>
            </div>
         </div>
         <div class="letLi">
            <div class="letLiBox">
              <h3>出生年月</h3>
              <span>{{birth}}</span>
            </div>
            <div class="letLiBox">
              <h3>身份号码</h3>
              <span>{{idCard}}</span>
            </div>
         </div>
         <div class="letLi">
            <div class="letLiBox">
              <h3>单位全称</h3>
              <span>{{fullName}}</span>
            </div>
            <div class="letLiBox">
              <h3>单位简介</h3>
              <span>{{organSimple}}</span>
            </div>
         </div>
         <div class="letLi">
            <div class="letLiBox">
              <h3>单位类型</h3>
              <span>{{organType}}</span>
            </div>
            <div class="letLiBox">
              <h3>任职职位</h3>
              <span>{{rank}}</span>
            </div>
         </div>
         <div class="letLi">
            <div class="letLiBox">
              <h3>任职时间</h3>
              <span>{{officeDate}}</span>
            </div>
            <div class="letLiBox">
              <h3>职位级别</h3>
              <span>{{level}}</span>
            </div>
         </div>
         <div class="letLi">
            <div class="letLiBox">
              <h3>分管工作</h3>
              <span>{{duty}}</span>
            </div>
         </div>
        </div>
      </div>
      <!--右边部分-->
      <div class="learig">
        <div class="leaRigTop">
          <div class="leaName">
            <h3>{{currentScore}}</h3>
            <Icon class="jb" type="ios-trophy" />
          </div>
          <div class="leaIos" @click="iconBtnrig">
            <Tooltip content="编辑干部积分信息">
              <Icon type="md-create" class="letIos1" />
            </Tooltip>
          </div>
        </div>
        <div class="leaRigTot">
          <div class="letLi">
            <div class="letLiBox">
              <h3>基础分</h3>
              <span>{{InitialScore}}</span>
            </div>
          </div>
          <div class="letLi">
            <div class="letLiBox">
              <h3>调整分</h3>
              <span>{{adjustScore}}</span>
            </div>
          </div>
          <div class="letLiss">
            <template v-for="(item, index) in tempApprovedApplyItemList">
              <div :key="index" class="letLis">
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore>0"><span>+</span>{{item.ItemScore}}</h4>
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore==0">{{item.ItemScore}}</h4>
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore<0">{{item.ItemScore}}</h4>
                <p style="width:70%;overflow:hidden;display:inline-block;">{{item.ItemDescription}}</p>
              </div>
            </template>
            <div v-if="ApprovedApplyItemList.length>5&&tempApprovedApplyItemList.length!=ApprovedApplyItemList.length" style="text-align:right;">
              <i-button type="primary" @click="handleMoreInfo(1)">更多</i-button>
            </div>
          </div>
          <div class="letLi">
            <div class="letLiBox">
              <h3>审批中</h3>
            </div>
          </div>
          <div class="letLiss">
            <template v-for="(item, index) in tempApprovingApplyItemList">
              <div :key="index" class="letLis">
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore>0"><span>+</span>{{item.ItemScore}}</h4>
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore==0">{{item.ItemScore}}</h4>
                <h4 style="width:20%;overflow:hidden;" v-if="item.ItemScore<0">{{item.ItemScore}}</h4>
                <p style="width:50%;overflow:hidden;display:inline-block;">{{item.ItemDescription}}</p>
                <!-- <template slot-scope=""> -->
                  <span @click="handleUndo(item)" style="width:20%;overflow:hidden;display:inline-block;cursor:pointer;">
                    撤回<i class="ivu-icon ivu-icon-ios-undo-outline"></i>
                  </span>
                <!-- </template> -->
              </div>
            </template>
            <div v-if="ApprovingApplyItemList.length>5&&tempApprovingApplyItemList.length!=ApprovingApplyItemList.length" style="text-align:right;">
              <i-button type="primary" @click="handleMoreInfo(2)">更多</i-button>
            </div>
          </div>
        </div>
        <div class="but">
          <Button type="primary"  @click="ToIntCreate">积分申请</Button>
        </div>
      </div>
    </div>

    <!-- 撤销提示 -->
    <Modal v-model="isUndoDialog" width="360">
        <p slot="header" style="color:#f60;text-align:center">
            <Icon type="information-circled"></Icon>
            <span>确认撤回该条积分申请？</span>
        </p>
        <div style="text-align:center">
            <p>是否确认撤回？</p>
        </div>
        <div slot="footer">
            <i-button type="info" size="large" long :loading="modal_loading_undo" @click="doUndoInt">撤回</i-button>
        </div>
    </Modal>
    <!-- 撤销提示 end -->

  </div>
</template>

<script>
    import Cookies from 'js-cookie';
    import {getLeaderInfoDetiel, getLeaderInfoScore ,undoInitInfo} from '@/api/leaderList';
    export default {
        name: "leaPerInfor",
        data(){
          return{
            // 获取到的信息
            OfficerID:null,
            name: null,
            sex: null,
            birth: null,
            idCard: null,
            fullName: null,
            organSimple: null,
            organType: null,
            rank: null,
            officeDate: null,
            level: null,
            duty: null,
            // 积分信息

            InitialScore: 0,
            adjustScore: 0,
            ApprovedApplyItemList: [],// 审批过
            ApprovingApplyItemList:[],// 审批中
            tempApprovedApplyItemList:[],
            tempApprovingApplyItemList:[],
            backupApprovedApplyItemList:[],
            backupApprovingApplyItemList:[],
            currentScore: 0,

            // 撤回积分申请
            isUndoDialog:false,
            UndoID:null,
            modal_loading_undo:false
          }
        },
        created(){
          debugger;
          var data = this.$route.query;
          // this.name = data.name;// 姓名
          // this.sex = data.sex;// 性别
          // this.birth = data.birth;// 出生日期
          // this.idCard = data.IdentifyNumber;// 身份证
          // this.fullName = data.unit;// 单位全称
          // this.organSimple = data.
          this.OfficerID = data.OfficerID
          var _data = {
            OfficerID: this.OfficerID,
            CurrentUserID: Cookies.get('UserID')
          }
          getLeaderInfoDetiel(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true){
              var data = res.Result;
              this.name = data.Name;// 名称
              this.sex = data.Gender;// 性别
              var date1 = data.Birthday;
              date1 = date1.split(' ')[0];
              this.birth = date1;// 出生年月
              this.idCard = data.IdentifyNumber;// 身份证号
              this.fullName = data.OrganFullName;// 单位全称
              this.organSimple = data.OrganShortName;// 单位简称
              this.organType = data.OrganTypeName// 单位类型
              this.rank = data.PositionName;// 任职职位
              var date2 = data.OnOfficeDate;
              date2 = date2.split(' ')[0];
              this.officeDate = date2;// 任职时间
              this.level = data.LevelName;
              this.duty = data.Duty;
            }else{
              this.$Message.error(res.Reason);
            }
          });
          getLeaderInfoScore(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true){
              var data = res.Result;
              this.InitialScore = data.InitialScore;
              this.currentScore = data.CurrentScore;
              var adjust = data.CurrentScore - data.InitialScore;
              this.adjustScore = adjust;
              this.ApprovedApplyItemList = data.ApprovedApplyItemList;
              this.ApprovingApplyItemList = data.ApprovingApplyItemList;
              if(data.ApprovedApplyItemList.length > 5){
                var list1 = data.ApprovedApplyItemList.slice(0,5);
                this.tempApprovedApplyItemList = list1;
                this.backupApprovedApplyItemList = list1;
              }else{
                this.tempApprovedApplyItemList = data.ApprovedApplyItemList;
                this.backupApprovedApplyItemList = data.ApprovedApplyItemList;
              }
              if(data.ApprovingApplyItemList.length > 5) {
                var list2 = data.ApprovingApplyItemList.slice(0,5);
                this.tempApprovingApplyItemList = list2;
                this.backupApprovingApplyItemList = list2;
              }else{
                this.tempApprovingApplyItemList = data.ApprovingApplyItemList;
                this.backupApprovingApplyItemList = data.ApprovingApplyItemList;
              }

            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
      methods:{
        // 查询积分
        queryInfoScore(){
          debugger;
          var _data = {
            OfficerID: this.OfficerID,
            CurrentUserID: Cookies.get('UserID')
          }
          getLeaderInfoScore(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true){
              var data = res.Result;
              this.InitialScore = data.InitialScore;
              this.currentScore = data.CurrentScore;
              var adjust = data.CurrentScore - data.InitialScore;
              this.adjustScore = adjust;
              this.ApprovedApplyItemList = data.ApprovedApplyItemList;
              this.ApprovingApplyItemList = data.ApprovingApplyItemList;
              if(data.ApprovedApplyItemList.length > 5){
                var list1 = data.ApprovedApplyItemList.slice(0,5);
                this.tempApprovedApplyItemList = list1;
                this.backupApprovedApplyItemList = list1;
              }else{
                this.tempApprovedApplyItemList = data.ApprovedApplyItemList;
                this.backupApprovedApplyItemList = data.ApprovedApplyItemList;
              }
              if(data.ApprovingApplyItemList.length > 5) {
                var list2 = data.ApprovingApplyItemList.slice(0,5);
                this.tempApprovingApplyItemList = list2;
                this.backupApprovingApplyItemList = list2;
              }else{
                this.tempApprovingApplyItemList = data.ApprovingApplyItemList;
                this.backupApprovingApplyItemList = data.ApprovingApplyItemList;
              }

            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        // 撤回
        handleUndo(val){
          debugger;
          var ApplyID = val.ApplyID;
          this.isUndoDialog = true;
          this.UndoID = ApplyID;
          this.modal_loading_undo = false;
        },
        // 执行撤回操作
        doUndoInt(){
          debugger;
          this.modal_loading_undo = true;
          var ApplyID = this.UndoID;
          var CurrentUserID = Cookies.get('UserID');
          var _data = {
            ApplyID,
            CurrentUserID
          }
          undoInitInfo(_data).then(res => {
            debugger;
            this.modal_loading_undo = false;
            if(res.IsSuccessful == true) {
              this.$Message.success('撤回审批中积分申请成功！');
              this.isUndoDialog = false;
                // 查询数据
                this.queryInfoScore();
            }else{
              // this.$Message.error('重置为初始密码失败！');
              this.$Message.error(res.Reason);
            }
          })
        },
        // 加载更多
        handleMoreInfo(type){
          debugger;
          if(type == 1){
            this.tempApprovedApplyItemList = this.ApprovedApplyItemList;
          }else if(type == 2){
            this.tempApprovingApplyItemList = this.ApprovingApplyItemList;
          }
        },
        // 格式化性别
        formatterSex(data){
          if(data == 1){
            return '男';
          }else if(data == 2){
            return '女';
          }
        },
        // 跳转到提交项
        ToIntCreate(){
          debugger;
          var OfficerID = this.OfficerID;
          var data = {
            OfficerID:OfficerID
          }
          this.$router.push({path:"intCreate", query:data})
        },
        //   go(){
        //   this.$router.push("intCreate")
        // }
        iconBtn(){
          debugger;
          var OfficerID = this.OfficerID;
          var data = {
            OfficerID:OfficerID
          }
          // this.$router.push("CreateLeader")
          this.$router.push({path:'EditLeader',query:data});
        },
        iconBtnrig(){
          var OfficerID = this.OfficerID;
          var data = {
            OfficerID:OfficerID
          }
          this.$router.push({path:"intCreate", query:data})
          // this.$router.push("IntCreate")
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
  .leacen{
    width:100%;
    height:100%;

  }
  .leaLef{
       width: 50%;
       height:100%;
       float:left;
       padding: 60px 60px;
     }
  .learig{
    width: 50%;
    height:100%;
    float:left;
    padding: 60px 60px;
  }
  .leaLefTop{
    display: -webkit-flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 30px;
  }
  .leaName{
    height:50px;
    line-height: 50px;
  }
  .leaName h3{
    font-size: 22px;
    display: inline-block;
  }
  .leaName .letIos1{
    font-size:20px;
    color:#1296db;
    padding-bottom: 5px;
  }
  .leaName .jb{
    color:#f5cc07;
    font-size:22px;
    padding-bottom: 5px;
  }
  .leaRigTop{
    display: -webkit-flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 50px;
  }
  .leaIos{
    font-size: 22px;
    color: #bfbfbf;
    cursor:pointer;
  }
  .leaIos:hover{
    color:#1296db;
  }
  .letLi{
    display: -webkit-flex;
    align-items: center;
    padding-top: 15px;
   border-bottom: 1px dashed black;
  }
  .letLiBox{
    text-align: left;
    width: 50%;
    height:35px;
  }
  .letLiBox h3{
    display: inline-block;
    font-weight: 100;
    margin-right: -20px;
    font-size:16px;
    width: 50%;
  }
  .letLiBox span{
    font-size: 14px;
    width: 50%;
  }
  .letLis{
    display:-webkit-flex;
    margin-left: 50px;
  }
  .letLis h4{
   /* margin:0 50px 0 350px; */
   /* margin:0 50px 0 50px; */
  }
  .but{
    width: 100%;
    height: 80px;
    text-align: center;
    line-height: 80px;
    margin-top: 50px;

  }
  .letLiss {
    padding-bottom: 15px;
    border-bottom: 1px dashed #f8f8f9;
  }
  .but button{
    width: 150px;
    height: 60px;
    /* background: blue; */
    border-radius: 5%;
    outline:none;
    border: 0;
    color: white;
    cursor:pointer;
  }

</style>
