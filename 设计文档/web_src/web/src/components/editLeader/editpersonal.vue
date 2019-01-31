<template>
  <div class="personal">
    <div class="craeteTop">
      编辑干部
    </div>

    <div class="createFlow" >
      <Row class="createFlows">
        <Col span="15" push="6">
          <Steps :current="current">
            <Step title="个人信息"></Step>
            <Step title="单位信息"></Step>
          </Steps>
        </Col>
      </Row>
    </div>

    <div class="createBt">
      <PerInfor v-show="index == 0" ref="PerInfor" :Per="PerData" @getPer="getPer"></PerInfor>
      <Unitinfor v-show="index == 1" ref="UnitInfor" :Unit="UnitData" @getUnit="getUnit"></Unitinfor>
    </div>
    <!--下一步-->
    <div class="createAn">
      <Button type="primary" @click="prev"  v-if="sh">上一步</Button>
      <Button type="primary" @click="show" v-if="str1">{{str}}</Button>

      <!--to="/Unitinfor"-->
    </div>
  </div>
</template>

<script>
  import PerInfor from "./editperInfor";
  import Unitinfor from "./editunitinfor";
  import Cookies from 'js-cookie';
  import {editLeaderInfo} from '@/api/leaderList';
  import {getLeaderInfoDetiel} from '@/api/leaderList';
    export default {
        name: "personal",
      components:{PerInfor,Unitinfor},
      data(){
          return{
            index:0,
            list:[1,2],
            str:'下一步',
            str1:true,
            sh:false,
            current: 0,
            // 个人信息
            personData:{},
            // 单位信息
            unitData:{},
            // 向子组件需要传递的值
            PerData:{},
            UnitData: {},
            // 记录传过来的id
            OfficerID: null,
            // 记录编辑的值
            AddUserID:null,
            Birthday:null,
            Duty:null,
            Gender:null,
            IdentifyNumber:null,
            LevelID:null,
            LevelName:null,
            Name:null,
            OfficerID:null,
            OnOfficeDate:null,
            OrganFullName:null,
            OrganShortName:null,
            OrganTypeID:null,
            OrganTypeName:null,
            OrganizationID:null,
            PositionID:null,
            PositionName:null
          }
      },
      created(){
          debugger;
          var data = this.$route.query;
            this.OfficerID = data.OfficerID;
          var _data = {
            OfficerID: this.OfficerID,
            CurrentUserID: Cookies.get('UserID')
          }
          getLeaderInfoDetiel(_data).then(res => {
              debugger;
              if(res.IsSuccessful == true){
                  var data = res.Result;
                  this.AddUserID = data.AddUserID;
                  var time = data.Birthday.split(' ')[0];
                  // this.Birthday = data.Birthday;
                  this.Birthday = time;
                  this.Duty = data.Duty;
                  this.Gender = data.Gender;
                  this.IdentifyNumber = data.IdentifyNumber;
                  this.LevelID = data.LevelID;
                  this.LevelName = data.LevelName;
                  this.Name = data.Name;
                  this.OfficerID = data.OfficerID;
                  this.OnOfficeDate = data.OnOfficeDate;
                  this.OrganFullName = data.OrganFullName;
                  this.OrganShortName = data.OrganShortName;
                  this.OrganTypeID = data.OrganTypeID;
                  this.OrganTypeName = data.OrganTypeName;
                  this.OrganizationID = data.OrganizationID;
                  this.PositionID = data.PositionID;
                  this.PositionName = data.PositionName;
                  // 子组件
                  this.PerData = {
                      Name: this.Name,
                      Gender:this.Gender,
                      IdentifyNumber:this.IdentifyNumber,
                      Birthday:this.Birthday
                  };
                  this.UnitData = {
                      OrganTypeID:this.OrganTypeID,
                      OrganTypeName:this.OrganTypeName,
                      OrganFullName:this.OrganFullName,
                      OrganizationID:this.OrganizationID,
                      PositionID:this.PositionID,
                      PositionName:this.PositionName,
                      LevelID:this.LevelID,
                      LevelName:this.LevelName,
                      OnOfficeDate:this.OnOfficeDate,
                      Duty:this.Duty
                  }
              }else{
                  this.$Message.error(res.Reason);
              }
          })
      },
      methods:{
        // 获取个人信息
        getPer(data) {
          debugger;
          this.personData = data;
        },
        // 获取单位信息
        getUnit(data) {
          debugger;
          this.unitData = data;
        },
        // 获取积分信息
        getInt(data) {
          debugger;
          this.intData = data;
        },
        show(){
          debugger;
          if (this.current == 2) {
            this.current = 2;
          } else {
            this.current += 1;
          }
          if(this.index<=2){
            this.index = this.index+1;
            if(this.index==1){
              this.sh = true;
            }
            if(this.index==2){
              // 组织数据
              var personData = this.personData;
              this.$refs.UnitInfor.showUnitData();
              var unitData = this.unitData;
              // 调用子页面的方法
            //   this.$refs.IntInfor.showIntData();
              // 组织数据
              var Name = personData.name;
              var Gender = personData.sex;
              if(Gender == '男') {
                Gender = 1;
              } else if(Gender == '女') {
                Gender = 2;
              }
              var IdentifyNumber = personData.idCard;
              var Birthday = personData.birthday;
              var OrganizationID = unitData.unit;
              var PositionID = unitData.duties;
              var LevelID = unitData.level;
              var OnOfficeDate = unitData.time;
              var Duty = unitData.chargeWork;
              var UpdateUserID = Cookies.get('UserID');
              var OfficerID = this.OfficerID;
              var _data = {
                OfficerID,
                Name,
                Gender,
                IdentifyNumber,
                Birthday,
                OrganizationID,
                PositionID,
                LevelID,
                OnOfficeDate,
                Duty,
                UpdateUserID,
              }
              this.index=1;
              debugger;
              editLeaderInfo(_data).then(res => {
                debugger;
                if(res.IsSuccessful == true) {
                  this.$Message.success('编辑干部成功！');
                  this.$router.push({name:'LeaderList'});
                }else{
                  // this.$Message.error('添加干部失败！');
                  this.$Message.error(res.Reason);
                }
              })
            }
          }
          if(this.index >0 ){
              this.str = '编辑';
          }
          // 获取每个页面的值
          if(this.index == 1) {// 第一个页面
            // 调用子页面的方法
            this.$refs.PerInfor.showPerData();
          } 
        },
        prev(){
          this.index = this.index-1;
          if(this.index<2){
            this.sh = true;

            if(this.index<1){
              this.sh = false;
            }

          }

          if(this.index <2 ){
            this.str = '下一步';

          }
          if (this.current <= 2) {
            this.current -= 1;
          } if(this.current == 0){
            return
          }
        }
      }
    }

</script>

<style scoped>
  .personal{
    height:100%;
    background: #fff;
    /*overflow-y: scroll;*/
  }
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
  .createBt{
    width: 100%;
    /*height: 100%;*/
  }
  .createAn{
    text-align: center;
    margin-bottom:30px;
  }

  .createFlow{
    height:60px;
    margin: 0px 30px 0 30px;
    border-bottom: 1px solid  #f7f7f9;
    /*line-height: 70px;*/
    padding-top: 20px;
  }


</style>
<style>
  .ivu-col-span-15{
    width: 46.5%;
  }
  .ivu-col-push-6 {
    left: 6%;
  }
</style>
