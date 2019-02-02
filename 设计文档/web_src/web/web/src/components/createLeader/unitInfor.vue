<template>
  <div class="personal">
    <div class="centers">
      <div class="personals">
        <span> 单位信息</span>
      </div>
      <div class="personalList">
        <!--镇办 区政府-->
        <div class="createZb">
          <Tabs class="createZbs" :animated="false" >
            <template v-for="(item,index) in TownToDo">
              <TabPane :key="index" :label="item.CategoryName" :name="item.CategoryName">
                <div>
                    <RadioGroup  v-model="part" on-change="handClickUnitPart">
                      <template v-for="(cell, cellIndex) in item.OrganTypeList">
                          <!-- <Radio :key="cellIndex" :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                            <span>{{cell.OrganTypeName}}</span>
                          </Radio> -->
                          <span :key="cellIndex" @click="handClickUnitPart(cell.OrganTypeID)">
                            <Radio v-if="cell.disabled==true" disabled :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                              <span>{{cell.OrganTypeName}}</span>
                            </Radio>
                            <Radio v-else :label="cell.OrganTypeID" :value="cell.OrganTypeID">
                              <span>{{cell.OrganTypeName}}</span>
                            </Radio>
                          </span>
                      </template>
                    </RadioGroup>
                  </div>
              </TabPane>
            </template>
          </Tabs>
        </div>
        <!--&lt;!&ndash;input框&ndash;&gt;-->
        <div class="personalName">
          <span class="personalspantwo" style="margin-right:60px;margin-top:0;">所在单位</span>
          <div style="text-align:left ">
            <!-- <Input  class="input" v-model="unit" />
            <p>单位简称，最长不超过10个字</p> -->
            <Select v-model="UnitClassification" style="width:300px;">
              <template v-for="item in UnitClassificationOption">
                <Option v-if="item.disabled==true" disabled :value="item.value" :key="item.value">{{ item.label }}</Option>
                <Option v-else :value="item.value" :key="item.value">{{ item.label }}</Option>
              </template>
            </Select>
          </div>
        </div>
        <div class="personalName">
          <span class="personalspantwo" style="margin-right:60px;margin-top:0;">现任职务</span>
          <!-- <Input  class="input" v-model="duties" /> -->
          <div style="text-align:left ">
            <Select v-model="PositionValue" style="width:300px;">
              <!--@on-change="selectBtn"-->
              <Option v-for="item in positionList" :value="item.value" :key="item.value">{{ item.label }}</Option>
            </Select>
          </div>
        </div>
        <div class="personalName">
          <span class="personalspanone" style="margin-top:0;margin-right:92px;">级别</span>
          <!-- <Input  class="input" v-model="level" /> -->
          <div style="text-align:left ">
          <Select v-model="levelValue" style="width:300px;">
            <!--@on-change="selectBtn"-->
            <Option v-for="item in levelList" :value="item.value" :key="item.value">{{ item.label }}</Option>
          </Select>
          </div>
        </div>

        <div class="personalName">
          <span class="personalspanone" style="margin-right:60px;">任职时间</span>
          <Row style="margin-top:15px;">
            <!-- <DatePicker class="yearsYear" type="year" placeholder="Select year" style="width: 200px"></DatePicker> -->
            <DatePicker type="date"  placeholder="请选择" style="width: 300px" v-model="time"></DatePicker>
          </Row>
        </div>
        <!--分管工作文本域-->
        <div class="textareaas">
          <span>分管工作</span>
          <Input v-model="chargeWork" class="textareas" type="textarea" :rows="4" placeholder="请输入..." />
        </div>
      </div>
    </div>
  </div>
  </template>


<script>
  import {queryUnitName} from '@/api/usersManage';
  import {queryCarreLevel, queryLevelList} from '@/api/leaderList';
  import {queryUnitType,queryUnitData} from '@/api/unitList'
  import Cookies from 'js-cookie'
    export default {
        name: "unitInfor",
      data(){
          return{
            TownToDo: [],
            Department: [],
            // 单位名称
            UnitClassification:null,
            UnitClassificationOption:[],
            // 职位分类
            positionList:[],
            PositionValue: null,
            // 级别分类
            levelValue: null,
            levelList:[],
            chargeWork:'',// 分管工作
            time: '',// 任职时间
            level: '',// 级别
            duties:'',// 现任职务
            unit: '',// 所在单位
            part: '',// 单位信息
          }
      },
      created() {
        debugger;
        if(Cookies.get('RoleID') == 4){
          this.part = parseInt(Cookies.get('OrganTypeID'));
          this.UnitClassification = parseInt(Cookies.get('OrganID'));
        }
        // 获取单位分类
        queryUnitName().then(res => {
          debugger;
          if(res.IsSuccessful == true) {
            this.UnitClassificationOption = [];
            var Option = [];
            for(let i = 0; i < res.Result.OrganList.length; i++) {
              var tempObj = {};
              if(Cookies.get('RoleID') == 4){
                if(Cookies.get('OrganID') == res.Result.OrganList[i].OrganID){
                  tempObj.disabled = false;
                }else{
                  tempObj.disabled = true;
                }
              }else{
                tempObj.disabled = false;
              }
              tempObj.label = res.Result.OrganList[i].OrganFullName;
              tempObj.value = res.Result.OrganList[i].OrganID;
              Option.push(tempObj);
            }
            this.UnitClassificationOption = Option;
            this.UnitList = Option;
          } else {
            this.$Message.error(res.Reason);
          }
        });
        // 获取职位
        queryCarreLevel().then(res => {
          debugger;
          if(res.IsSuccessful == true) {
            var row = res.Result.PositionList;
            var list = [];
            for(let i = 0; i < row.length; i++) {
              var tempObj = {};
              tempObj.label = row[i].PositionName;
              tempObj.value = row[i].PositionID;
              list.push(tempObj);
            }
            this.positionList = list;
          }else{
            this.$Message.error(res.Reason);
          }
        });
        // 获取级别
        queryLevelList().then(res => {
          debugger;
          if(res.IsSuccessful == true) {
            var row = res.Result.LevelList;
            var list = [];
            for(let i = 0; i < row.length; i++) {
              var tempObj = {};
              tempObj.label = row[i].LevelName;
              tempObj.value = row[i].LevelID;
              list.push(tempObj);
            }
            this.levelList = list;
          }else{
            this.$Message.error(res.Reason);
          }
        });
        queryUnitType().then(res => {
            debugger;
            if(res.IsSuccessful == true) {
              var row = res.Result.CategoryList;
              if(Cookies.get('RoleID') == 4){// 二级管理员
                for(let i = 0;i<row.length;i++){
                  for(let j = 0; j < row[i].OrganTypeList.length;j++){
                    if(Cookies.get('OrganTypeID') == row[i].OrganTypeList[j].OrganTypeID){
                      row[i].OrganTypeList[j].disabled = false;
                    }else{
                      row[i].OrganTypeList[j].disabled = true;
                    }
                  }
                }
              }else{
                for(let i = 0;i<row.length;i++){
                  for(let j = 0;j< row[i].OrganTypeList.length;j++){
                    row[i].disabled = false;
                  }
                }
              }
              this.TownToDo = row;
              // for(let i = 0; i < row.length; i++) {
              //   if(row[i].CategoryName == '镇办'){
              //     this.TownToDo = row[i].OrganTypeList;
              //   }else if(row[i].CategoryName == '区级部门'){
              //     this.Department = row[i].OrganTypeList;
              //   }
              // }
            }else{
              this.$Message.error(res.Reason);
            }
          });
      },
      methods:{
        // 点击单位分类
        handClickUnitPart(id){
          debugger;
          var _data = {
            OrganTypeID:id,
            Keyword: null,
            Sort:'',
            Order:'asc',
            Page:1,
            PageSize:500,
          }
          // 请求接口
          queryUnitData(_data).then(res => {
            debugger;
            if(res.IsSuccessful == true){
              this.UnitClassificationOption = [];
              var option = [];
              for(let i = 0; i < res.Result.OrganInfoList.length; i++) {
                var tempObj = {};
                if(Cookies.get('RoleID') == 4){
                  if(Cookies.get('OrganID') == res.Result.OrganInfoList[i].OrganID){
                    tempObj.disabled = false;
                  }else{
                    tempObj.disabled = true;
                  }
                }else{
                  tempObj.disabled = false;
                }
                tempObj.label = res.Result.OrganInfoList[i].OrganFullName;
                tempObj.value = res.Result.OrganInfoList[i].OrganID;
                option.push(tempObj);
              }
              this.UnitClassificationOption = option;
            }else{
              this.$Message.error(res.Reason);
            }
          })
        },
        // 获取值
        showUnitData() {
          debugger;
          var part = this.part;
          var unit = this.UnitClassification;
          var duties = this.PositionValue;
          var level = this.levelValue;
          var time = this.time;
          var chargeWork = this.chargeWork;
          var form = {
            part,
            unit,
            duties,
            level,
            time,
            chargeWork
          }
          this.$emit('getUnit',form);
        }
      }

    }

</script>

<style scoped>
  .personal{
    margin:30px auto 0;
    text-align: center;
    display: -webkit-flex;
    justify-content: center;
    -webkit-justify-content: center;
  }
  .centers{
    margin: 0 auto;
  }
  .personals{
    width:100%;
  }
  .personals span{
    width: 90px;
    font-size: 14px;
    color: darkgray;
    display: block;
    padding-left: 2px;
    border-left:3px solid #4990e2;
  }
  .personalList{
    text-align: center;
    margin: 20px auto;
    /*height: 100%;*/
    width: 100%;
  }
  .personalList span{
    color: black;
    font-size: 16px;
    /*text-align: left;*/
  }
  .personalList p{
    font-size: 14px;
    color:darkgray;
  }

  .createZb{
    margin: 30px 0;
  }
/*input框样式*/
  .input{
    width:420px;
    line-height: 60px;
  }
  .personalName{
    display: -webkit-flex;
    -webkit-justify-content:flex-start;
    margin:12px 0;
  }
  .personalName .personalspanone{
    margin-right:88px;
    margin-top: 15px;
  }
  .personalName .personalspantwo{
    margin-right:52px;
    margin-top: 15px;
  }
/*选择时间*/
  .years{
    margin-top: 20px;
    width: 100%;
    display: -webkit-flex;

  }
  .years .yearsYear{
    margin-left: 60px;
    margin-right:13px;
  }
  .years span{
    display: block;
    /*margin:30px 0 20px 0;*/
  }
  /*文本域*/
  .textareaas{
    display: -webkit-flex;
    justify-content: flex-start;
    align-items: center;
    margin: 35px 0;
  }
  .textareaas span{
    display: block;
    margin-right:60px;
  }
  .textareas{
    width: 420px;
  }
</style>
<style>
  .ivu-tabs-bar{
    border-bottom: 0px solid #dcdee2;
  }
  .ivu-tabs-nav{
    float:none;
    font-size: 16px;
  }
  .ivu-radio-wrapper{
    font-size: 14px;
  }
  .ivu-tabs-nav-scroll{
    display: inline-block;
  }

/*时间修改*/

  .ivu-row{
    display: flex;
    justify-content: center;
  }
</style>
